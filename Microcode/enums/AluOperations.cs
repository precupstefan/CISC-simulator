namespace Microcode.enums
{
    public enum AluOperations: ushort
    {
        NONE=0000,
        SUM=0001,
        AND=0010,
        OR=0011,
        XOR=0100,
        NEG=0101,
        ASR=0110,
        ASL=0111,
        LSR=1000,
        ROL=1001,
        ROR=1010,
        RLC=1011,
        RRC=1100,

    }
}