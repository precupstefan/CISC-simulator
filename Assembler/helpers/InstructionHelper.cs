using Assembly.enums;

namespace Assembly.helpers
{
    public static class InstructionHelper
    {
        public static ClassCodification GetInstructionClass(ushort instruction)
        {
            var HIGH = (byte) (instruction >> 8);
            var IR15 = (byte) (HIGH >> 7);
            var IR14 = (byte) ((HIGH >> 6) & 1);
            var IR13 = (byte) ((HIGH >> 5) & 1);
            var CL1 = (byte) (IR15 & IR14);
            var CL0 = (byte) (IR15 & (~IR13));

            var instructionClass = (byte) ((CL1 << 1) | CL0);
            return (ClassCodification) instructionClass;
        }

        public static AccessMode GetAddressingMode(ushort instruction, int bitsToShift)
        {
            uint value;
            if (bitsToShift == 6)
            {
                value = (uint) (instruction >> 10);
            }
            else
            {
                value = (uint) (instruction >> 4);
            }

            var accessMode = (byte) (value & 3);
            return (AccessMode) accessMode;
        }
    }
}