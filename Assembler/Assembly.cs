using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assembly.enums;
using Assembly.exceptions;
using Assembly.helpers;
using Microcode.classes;
using GeneralRegisters = Assembly.enums.GeneralRegisters;

namespace Assembly
{
    public class Assembler
    {
        private List<string> instructions = new List<string>();
        private Memory memory = Memory.Instance;
        private ushort pc = 0;
        private ushort pcIncrement = 1;
        private Dictionary<string, int> label = new Dictionary<string, int>();
        private Dictionary<int, string> flaggedInstructionsForLabel = new Dictionary<int, string>();
        private Logger.Logger Logger = global::Logger.Logger.Instance;
        
        public Assembler()
        {
            Logger.Log("Initializing Assembly process");
        }

        public void ReadFromFile(string path)
        {
            Logger.Log("Reading from file...");
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (line.Contains('#'))
                {
                    continue;
                }

                instructions.Add(line.ToUpper());
            }
            Logger.Log("Successfully read contents of file");
        }

        public List<string> GetInstructions()
        {
            return instructions;
        }

        public void Assemble()
        {
            Logger.Log("Starting assembly process...");
            foreach (var line in instructions)
            {
                if (line.Contains(':'))
                {
                    var labelName = line.Remove(line.Length - 1);
                    label.Add(labelName, pc);
                    continue;
                }

                var instruction = AssembleInstruction(line);
                memory[pc] = instruction;
                pc += pcIncrement;
                pcIncrement = 1;
            }

            if (flaggedInstructionsForLabel.Count != 0)
                AddMissingLabelsToInstructions();
            
            Logger.Log("Assembly complete!");
        }

        private ushort GetInstruction(string operation)
        {
            if (operation == null) throw new ArgumentNullException(nameof(operation));
            var instructionValue = Enum.Parse(typeof(InstructionsValue), operation);
            var instruction = (ushort) instructionValue;
            return instruction;
        }

        private ushort AssembleInstruction(string line)
        {
            var keywords = line.Split(' ');
            var instruction = GetInstruction(keywords[0]);
            switch (InstructionHelper.GetInstructionClass(instruction))
            {
                case ClassCodification.B1:
                    instruction = AssembleInstructionB1(instruction, keywords);
                    break;
                case ClassCodification.B2:
                    instruction = AssembleInstructionB2(instruction, keywords[1]);
                    break;
                case ClassCodification.B3:
                    instruction = AssembleInstructionB3(instruction, keywords[1]);
                    break;
                case ClassCodification.B4:
                    return instruction;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return instruction;
        }

        private ushort AssembleInstructionB1(ushort instruction, string[] keywords)
        {
            instruction = ManageSourceRegister(instruction, keywords[2]);
            instruction = ManageDestinationRegister(instruction, keywords[1]);
            return instruction;
        }

        private ushort AssembleInstructionB2(ushort instruction, string destinationRegister)
        {
            instruction = ManageDestinationRegister(instruction, destinationRegister);
            return instruction;
        }

        private ushort AssembleInstructionB3(ushort instruction, string labelName)
        {
            instruction = (ushort) (instruction | CalculateOffset(labelName));
            return instruction;
        }

        private byte CalculateOffset(string labelName)
        {
            if (label.ContainsKey(labelName))
            {
                var offset = label[labelName] - pc;
                return (byte) (offset >= -128
                    ? offset
                    : throw new ArgumentOutOfRangeException($"Distance to {labelName} greater than 128"));
            }

            flaggedInstructionsForLabel.Add(pc, labelName);
            return 0;
        }

        private ushort ManageSourceRegister(ushort instruction, string sourceRegister)
        {
            instruction = AddMASToInstruction(instruction, sourceRegister);
            instruction = ManageRegister(instruction, sourceRegister, 6);

            return instruction;
        }

        private ushort ManageDestinationRegister(ushort instruction, string destinationRegister)
        {
            instruction = AddMADToInstruction(instruction, destinationRegister);
            instruction = ManageRegister(instruction, destinationRegister, 0);

            return instruction;
        }


        private ushort AddMASToInstruction(ushort instruction, string operand)
        {
            switch (GetAccessMode(operand))
            {
                case AccessMode.IMMEDIATE:
                    return instruction;
                case AccessMode.DIRECT:
                    instruction = (ushort) (instruction | 0x0400);
                    return instruction;
                case AccessMode.INDIRECT:
                    instruction = (ushort) (instruction | 0x0800);
                    return instruction;
                case AccessMode.INDEXED:
                    instruction = (ushort) (instruction | 0x0C00);
                    return instruction;
                default:
                    throw new AccessModeException("Unknown or illegal access mode for destination register");
            }
        }

        private ushort AddMADToInstruction(ushort instruction, string operand)
        {
            switch (GetAccessMode(operand))
            {
                case AccessMode.DIRECT:
                    instruction = (ushort) (instruction | 0x0010);
                    return instruction;
                case AccessMode.INDIRECT:
                    instruction = (ushort) (instruction | 0x0020);
                    return instruction;
                case AccessMode.INDEXED:
                    instruction = (ushort) (instruction | 0x0030);
                    return instruction;
                case AccessMode.IMMEDIATE:
                    return instruction;
                default:
                    throw new AccessModeException("Unknown or illegal access mode for destination register");
            }
        }

        private AccessMode GetAccessMode(string register)
        {
            if (!register.StartsWith("(") && !register.StartsWith("R"))
            {
                if (register.Contains("R"))
                {
                    return AccessMode.INDEXED;
                }
            }

            if (register.StartsWith("("))
            {
                return AccessMode.INDIRECT;
            }

            return register.StartsWith("R") ? AccessMode.DIRECT : AccessMode.IMMEDIATE;
        }

        private ushort ManageRegister(ushort instruction, string register, int bitsToShift)
        {
            switch (InstructionHelper.GetAddressingMode(instruction, bitsToShift))
            {
                case AccessMode.IMMEDIATE:
                    AddValueToMemory(register);
                    break;
                case AccessMode.DIRECT:
                    instruction = AddRegisterToInstruction(instruction, register, bitsToShift);
                    break;
                case AccessMode.INDIRECT:
                    instruction = AddIndirectAccessToInstruction(instruction, register, bitsToShift);
                    break;
                case AccessMode.INDEXED:
                    instruction = AddIndexedAccessToInstruction(instruction, register, bitsToShift);
                    break;
                default:
                    throw new AccessModeException($"{register} has no known access mode");
            }

            return instruction;
        }

        private ushort AddIndexedAccessToInstruction(ushort instruction, string register, int bitsToShift)
        {
            var keywords = register.Split('(');

            AddValueToMemory(keywords[0]);

            register = StringHelper.RemoveAtFirstChar(keywords[1], ')');

            var shiftedRegisterNumber = GetShiftedRegisterNumber(register, bitsToShift);

            instruction = (ushort) (instruction | shiftedRegisterNumber);

            return instruction;
        }

        private ushort AddIndirectAccessToInstruction(ushort instruction, string register, int bitsToShift)
        {
            register = register.Replace("(", "");
            register = StringHelper.RemoveAtFirstChar(register, ')');
            var shiftedRegisterNumber = GetShiftedRegisterNumber(register, bitsToShift);
            instruction = (ushort) (instruction | shiftedRegisterNumber);
            return instruction;
        }

        private ushort AddRegisterToInstruction(ushort instruction, string register, int bitsToShift)
        {
            var shiftedRegisterNumber = GetShiftedRegisterNumber(register, bitsToShift);
            instruction = (ushort) (instruction | shiftedRegisterNumber);
            return instruction;
        }

        private void AddValueToMemory(string value)
        {
            var _value = Convert.ToUInt16(value);
            memory[pc + pcIncrement] = _value;
            pcIncrement++;
        }

        private ushort GetRegisterNumber(string register)
        {
            var registerType = Enum.Parse(typeof(GeneralRegisters), register);
            var registerNumber = (ushort) registerType;
            return registerNumber;
        }

        private ushort GetShiftedRegisterNumber(string register, int bitsToShift)
        {
            var registerNumber = GetRegisterNumber(register);

            var shiftedRegisterNumber = (ushort) (registerNumber << bitsToShift);
            return shiftedRegisterNumber;
        }

        private void AddMissingLabelsToInstructions()
        {
            foreach (var flaggedInstruction in flaggedInstructionsForLabel)
            {
                if (!label.ContainsKey(flaggedInstruction.Value))
                {
                    throw new IndexOutOfRangeException($"no defined label start for label {flaggedInstruction.Value}");
                }

                var instructionPosition = flaggedInstruction.Key;
                var desiredPosition = label[flaggedInstruction.Value];
                var offset = desiredPosition > instructionPosition
                    ? desiredPosition - instructionPosition
                    : instructionPosition - desiredPosition;
                if (offset > 127 || offset < -128)
                {
                    throw new OffsetOutOfRangeException($"Label {flaggedInstruction.Value} is too far away");
                }

                memory[instructionPosition] = (ushort) (memory[instructionPosition] | (sbyte) offset);
            }
        }
    }
}