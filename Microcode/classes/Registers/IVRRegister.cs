namespace Architecture.classes.Registers
{
    class IVRRegister : AbstractRegister<ushort>
    {
        public static IVRRegister Instance { get; } = new IVRRegister();

        public override ushort Value
        {
            get => Registers.IVR;
            set => Registers.IVR = value;
        }
    }
}