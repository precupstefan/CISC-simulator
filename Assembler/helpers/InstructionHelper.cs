using Assembly.enums;

namespace Assembly.helpers
{
    public static class InstructionHelper
    {

        public static void GetInstructionClass(ushort instruction)
        {
            byte HIGH = (byte) (instruction >> 8);
//            return HIGH;
        }
        
    }
}