using Architecture.classes.Registers;

namespace Architecture.Helpers
{
    public class InstructionHelper
    {
        public static InstructionHelper Instance { get; } = new InstructionHelper();
        
        public byte GetShiftedMicroInstructionByBits(ushort bits)
        {
            var instruction = MIR.Instance.Value;
            return (byte) (instruction >> bits);
        }
        
        public byte GetShiftedInstructionByBits(ushort bits)
        {
            var instruction = IRRegister.Instance.Value;
            return (byte) (instruction >> bits);
        }

        public byte GetInstructionClass()
        {
            var instruction = IRRegister.Instance.Value;
            var HIGH = (byte) (instruction >> 8);
            var IR15 = (byte) (HIGH >> 7);
            var IR14 = (byte) ((HIGH >> 6) & 1);
            var IR13 = (byte) ((HIGH >> 5) & 1);
            var CL1 = (byte) (IR15 & IR14);
            var CL0 = (byte) (IR15 & (~IR13));

            var instructionClass = (byte) ((CL1 << 1) | CL0);
            return instructionClass;
        }
    }
}