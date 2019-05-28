namespace Architecture.enums
{
    public enum DBusOperations: ushort
    {
        NONE = 0000,
        PD0 = 0001,
        PdRG = 0010,
        PdIR = 0011,
        PdMdr = 0100,
        PdSP = 0101,
        PdAdr = 0110,
        PdT = 0111,
        PdPc = 1000,
        PdIVR = 1001,
        PdFlag = 1010,
        PD1 = 1011,
        PD_1 = 1100,
        PdIR_OFFSET = 1101,
    }
}