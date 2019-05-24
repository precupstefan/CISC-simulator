namespace Microcode.classes
{
    public class MDRRegister : AbstractRegister<short>
    {
        public static MDRRegister Instance { get; } = new MDRRegister();
        public override short Value
        {
            get => Registers.MDR;
            set => Registers.MDR = value;
        }
    }
}