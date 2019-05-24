namespace Microcode.classes.Bus
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