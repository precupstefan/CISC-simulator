using System;
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

       
    }
}