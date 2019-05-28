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
    }
}