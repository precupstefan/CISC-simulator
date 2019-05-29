using System;
using System.Diagnostics;
using Architecture;
using Architecture.classes;
using Architecture.classes.Bus;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Sequencer
{
    public class Sequencer
    {
        public static Sequencer Instance { get; } = new Sequencer();

        private MPM mpm = new MPM();
        private Memory Memory = Memory.Instance;
        private ALU ALU = ALU.Instance;
        private Logger.Logger Logger = global::Logger.Logger.Instance;
        private State state = State.Instance;

        private void Step()
        {
            if (state.Halt) return;

            switch (state.InstructionExecutionStep)
            {
                case 0:
                {
                    var mir = MIR.Instance;
                    var mar = MAR.Instance;
                    var mpm = new MPM();
                    mir.Value = mpm[mar.Value];
                    state.InstructionExecutionStep = 1;
                    break;
                }

                case 1:
                {
                    ALU.DoOperation();
                    Memory.CheckForOperationAndExecuteIfPresent();
                    CheckForOtherOperationsAndExecuteIfPresent();
                    MAR.Instance.PrepareForNextMicroInstruction();
                    state.InstructionExecutionStep = 0;
                    break;
                }

                default:
                {
                    throw new Exception("STATE OUT OF RANGE");
                }
            }
        }

        private void CheckForOtherOperationsAndExecuteIfPresent()
        {
            var instr = MIR.Instance.Value;
            var mask = (ushort) Math.Pow(2, Constants.OtherOperationsSize) - 1;
            var operation = (ushort) (instr >> Constants.OtherOperationsIndex);
            operation = (ushort) (operation & mask);
            switch ((ShiftAndOtherOperations) operation)
            {
                case ShiftAndOtherOperations.None:
                    break;
                case ShiftAndOtherOperations.plus1PC:
                    PCRegister.Instance.Value++;
                    break;
                case ShiftAndOtherOperations.plus1Sp:
                    SPRegister.Instance.Value++;
                    break;
                case ShiftAndOtherOperations.minus1SP:
                    SPRegister.Instance.Value--;
                    break;
                case ShiftAndOtherOperations.PdFLAGS:
                    // TODO: IMPLEMENT PDFLAGS
                    FLAGRegister.Instance.Value = 0;
                    break;
                case ShiftAndOtherOperations.CLC:
                    break;
                case ShiftAndOtherOperations.CLV:
                    break;
                case ShiftAndOtherOperations.CLZ:
                    break;
                case ShiftAndOtherOperations.CLS:
                    break;
                case ShiftAndOtherOperations.CCC:
                    break;
                case ShiftAndOtherOperations.SEC:
                    break;
                case ShiftAndOtherOperations.SEV:
                    break;
                case ShiftAndOtherOperations.SEZ:
                    break;
                case ShiftAndOtherOperations.SES:
                    break;
                case ShiftAndOtherOperations.SCC:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ExecuteCycle()
        {
            Step();
            Step();
            Logger.Warning($"MICROINSTRUCTION {Convert.ToString((long) MIR.Instance.Value, 16).PadLeft(10, '0')}");
        }

        public void Execute()
        {
        }
    }
}