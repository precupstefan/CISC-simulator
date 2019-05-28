namespace Architecture.classes.Registers
{
    public class TRegister : AbstractRegister<short>
    {
        public static TRegister Instance { get; } = new TRegister();

        public override short Value
        {
            get => Registers.T;
            set => Registers.T = value;
        }
    }
}