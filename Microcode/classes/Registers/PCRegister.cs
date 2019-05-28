namespace Architecture.classes.Registers
{
    public class PCRegister : AbstractRegister<ushort>
    {
        public static PCRegister Instance { get; } = new PCRegister();

        public override ushort Value
        {
            get => Registers.PC;
            set => Registers.PC = value;
        }
    }
}