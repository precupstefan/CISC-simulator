using System;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes.Registers
{
    public class MIR : AbstractRegister<ulong>
    {
        public static MIR Instance { get; } = new MIR();

        public override ulong Value { get; set; } = 0;
        
        public ushort GetJumpIndex()
        {
            var jumpIndex = Value >> Constants.IndexJumpIndex;
            var mask = (ushort) Math.Pow(2, Constants.IndexJumpSize) - 1;
            jumpIndex = jumpIndex & (ulong) mask;
            return (ushort) jumpIndex;
        }

        public bool isNegTOrFalse()
        {
            var value = Value >> Constants.IndexJumpSize + Constants.JumpOffsetSize;
            return (value & 1) != 1;
        }
       
        public void CheckForOtherOperationsAndExecuteIfPresent()
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
                    FLAGRegister.Instance.SetCarryFlag(false);
                    break;
                case ShiftAndOtherOperations.CLV:
                    FLAGRegister.Instance.SetOverflowFlag(false);
                    break;
                case ShiftAndOtherOperations.CLZ:
                    FLAGRegister.Instance.SetZeroFlag(false);
                    break;
                case ShiftAndOtherOperations.CLS:
                    FLAGRegister.Instance.SetSignFlag(false);
                    break;
                case ShiftAndOtherOperations.CCC:
                    break;
                case ShiftAndOtherOperations.SEC:
                    FLAGRegister.Instance.SetCarryFlag(true);
                    break;
                case ShiftAndOtherOperations.SEV:
                    FLAGRegister.Instance.SetOverflowFlag(true);
                    break;
                case ShiftAndOtherOperations.SEZ:
                    FLAGRegister.Instance.SetZeroFlag(true);
                    break;
                case ShiftAndOtherOperations.SES:
                    FLAGRegister.Instance.SetSignFlag(true);
                    break;
                case ShiftAndOtherOperations.SCC:
                    break;
                case ShiftAndOtherOperations.BP0:
                    State.Instance.Halt = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}