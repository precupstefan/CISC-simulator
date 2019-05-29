namespace Architecture.enums
{
    public enum ShiftAndOtherOperations : ushort
    {
        None = 0,
        plus1PC = 1,
        plus1Sp = 2,
        minus1SP = 3,
        PdFLAGS = 4,
        CLC = 5,
        CLV = 6,
        CLZ = 7,
        CLS = 8,
        CCC = 9,
        SEC = 10,
        SEV = 11,
        SEZ = 12,
        SES = 13,
        SCC = 14,
    }
}