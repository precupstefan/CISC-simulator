namespace Architecture.classes.Registers
{
    public class FLAGRegister : AbstractRegister<ushort>
    {
        public static FLAGRegister Instance { get; } = new FLAGRegister();

        public override ushort Value
        {
            get => Registers.Flags;
            set => Registers.Flags = value;
        }

        public void SetCarryFlag(bool value)
        {
            Value = (ushort) (value ? Value | 1 : Value & 0xFFFE);
        }

        public void SetSignFlag(bool value)
        {
            Value = (ushort) (value ? Value | 0x0080 : Value & 0xFF7F);
        }

        public void SetZeroFlag(bool value)
        {
            Value = (ushort) (value ? Value | 0x0040 : Value & 0xFFBF);
        }

        public void SetOverflowFlag(bool value)
        {
            Value = (ushort) (value ? Value | 0x0800 : Value & 0xF7FF);
        }


        public bool GetCarryFlag()
        {
            return (Value & 1) == 1;
        }
        
        public bool GetSignFlag()
        {
            return (Value & 0x080) == 1;
        }
        
        public bool GetZeroFlag()
        {
            return (Value & 0x0040) == 1;
        }
        
        public bool GetOverflowFlag()
        {
            return (Value & 0x0800) == 1;
        }
    }
}