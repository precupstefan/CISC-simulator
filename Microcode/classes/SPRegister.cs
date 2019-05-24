namespace Microcode.classes
{
    public class SPRegister : AbstractRegister<ushort>
    {
        public static SPRegister Instance { get; } = new SPRegister();
        public override ushort Value
        {
            get => Registers.SP;
            set => Registers.SP = value;
        }
    }
}