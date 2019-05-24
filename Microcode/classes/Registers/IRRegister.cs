namespace Microcode.classes
{
    public class IRRegister : AbstractRegister<ushort>
    {
        public static IRRegister Instance { get; } = new IRRegister();

        public override ushort Value
        {
            get => Registers.IR;
            set => Registers.IR = value;
        }
    }
}