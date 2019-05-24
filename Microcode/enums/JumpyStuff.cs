namespace Microcode.enums
{
    public enum JumpyStuff : ushort
    {
        NONE=0000,
        STEP=0001,
        JuMP=0010,
        JUMPI=0011,
//        IF( N) INTR eticheta else STEP=0100,
        B1=0101,
        AD=0110,
        Z=0111,
        C=1000,
        V=1001,
        S=1010,
        INTR=1011,
    }
}