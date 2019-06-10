using System;
using Architecture.Helpers;

namespace Architecture.classes.Registers
{
    public class IRRegister : AbstractRegister<ushort>
    {
        public static IRRegister Instance { get; } = new IRRegister();

        public override ushort Value
        {
            get => Registers.IR;
            set => Registers.IR = value;
        }

        public byte GetJumpOffset()
        {
            var mask = (ushort) Math.Pow(2, Constants.JumpOffsetSize) - 1;
            return (byte) (Value & mask);
        }

        public bool IsDestinationDirectlyAddressed()
        {
            var value = InstructionHelper.Instance.GetShiftedInstructionByBits(4);
            return (value & 3) == 1;
        }
    }
}