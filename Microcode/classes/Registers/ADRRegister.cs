namespace Architecture.classes.Registers
{
    public class ADRRegister : AbstractRegister<ushort>
    {
        public static ADRRegister Instance { get; } = new ADRRegister();

        public override ushort Value
        {
            get => Registers.ADR;
            set => Registers.ADR = value;
        }
    }
}