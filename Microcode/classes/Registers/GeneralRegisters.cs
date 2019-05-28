namespace Architecture.classes.Registers
{
    public class GeneralRegisters : AbstractRegister<short[]>
    {
        public static GeneralRegisters Instance { get; } = new GeneralRegisters();

        public override short[] Value
        {
            get => Registers.GeneralRegisters;
            set
            {
                Registers.GeneralRegisters = value;
                Registers.TriggerGeneralRegisterUpdate();
            }
        }
    }
}