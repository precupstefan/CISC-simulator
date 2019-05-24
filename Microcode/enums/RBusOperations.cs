namespace Microcode.enums
{
    public enum RBusOperations: ushort
    {
        NONE = 0000,
        PmRG = 0001,
        PmIR = 0010,
        PmMdr = 0011,
        PmSP = 0100,
        PmAdr = 0101,
        PmT = 0110,
        PmPc = 0111,
        PmIVR = 1000,
        PmFlag = 1001,
    }
}