namespace Architecture.enums
{
    public enum ShiftAndOtherOperations : ushort
    {
        None = 0000,
        plus1PC = 0001,
        plus1Sp = 0010,
        minus1SP = 0011,
        PdFLAGS = 0100,
        CLC = 0101,
        CLV = 0110,
        CLZ = 0111,
        CLS = 1000,
        CCC = 1001,
        SEC = 1010,
        SEV = 1011,
        SEZ = 1100,
        SES = 1101,
        SCC = 1110,
    }
}