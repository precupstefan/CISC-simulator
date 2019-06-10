using System;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes
{
    public class Memory
    {
        public static Memory Instance { get; } = new Memory();

        public ushort this[int a]
        {
            get => memory[a];
            set => memory[a] = value;
        }

        private ushort[] memory = new ushort[65536];

        private void ReadFromMemory()
        {
            var mdr = MDRRegister.Instance;
            var adr = ADRRegister.Instance;
            mdr.Value = (short) memory[adr.Value];
        }

        private void IFCH()
        {
            var ir = IRRegister.Instance;
            var adr = ADRRegister.Instance;
            ir.Value = memory[adr.Value];
            CheckFOrIllegalInstruction();
        }

        private void CheckFOrIllegalInstruction()
        {
            var instruction = IRRegister.Instance.Value;
            switch (InstructionHelper.Instance.GetInstructionClass())
            {
                case 0:
                    if ((instruction & 0x7000) == 0x7000)
                    {
                        State.Instance.Halt = true;
                        throw new Exception("ILLEGAL B1 Instruction");
                    }

                    break;
                case 1:
                    instruction = (ushort) (instruction & 0x9FC0);
                    if (instruction >> 12 != 0 || instruction >> 11 != 0 || instruction >> 10 != 0)
                    {
                        State.Instance.Halt = true;
                        throw new Exception("ILLEGAL B2 Instruction");
                    }

                    var shiftedInstruction = (instruction >> 6) & 0xF;
                    if (shiftedInstruction == 0xF)
                    {
                        State.Instance.Halt = true;
                        throw new Exception("ILLEGAL B2 Instruction");
                    }

                    break;
                case 3:
                    instruction = (ushort) ((instruction & 0xDF00) >> 8);
                    if (instruction >= 0xC9)
                    {
                        State.Instance.Halt = true;
                        throw new Exception("ILLEGAL B3 Instruction");
                    }

                    break;
                case 2:
                    if (instruction > 0xE012)
                    {
                        State.Instance.Halt = true;
                        throw new Exception("ILLEGAL B3 Instruction");
                    }

                    break;
                default:
                    throw new NotSupportedException("INVALID CLASS");
            }

        }

        private void WriteToMemory()
        {
            var mdr = MDRRegister.Instance;
            var adr = ADRRegister.Instance;
            memory[adr.Value] = (ushort) mdr.Value;
        }

        public void CheckForOperationAndExecuteIfPresent()
        {
            var instr = MIR.Instance.Value;
            var mask = 2 ^ Constants.MemoryOperationSize - 1;
            var operation = (ushort) (instr >> Constants.MemoryOperationIndex);
            operation = (ushort) (operation & mask);
            switch ((MemoryOperations) operation)
            {
                case MemoryOperations.NONE:
                    break;
                case MemoryOperations.IFCH:
                    IFCH();
                    return;
                case MemoryOperations.READ:
                    ReadFromMemory();
                    break;
                case MemoryOperations.WRITE:
                    WriteToMemory();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}