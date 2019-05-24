namespace Microcode.classes
{
    public class FLAGRegister : AbstractRegister<ushort>
    {
        public static FLAGRegister Instance { get; } = new FLAGRegister();

        public override ushort Value
        {
            get => Registers.Flags;
            set => Registers.Flags = value;
        }
    }
}