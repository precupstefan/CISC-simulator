using Architecture.classes.Bus;

namespace Architecture.classes.Registers
{
    public abstract class AbstractRegister<T>
    {
        protected SBus Sbus = SBus.Instance;
        protected DBus DBus = DBus.Instance;
        protected Architecture.Registers Registers = Architecture.Registers.Instance;

        public abstract T Value { get; set; }

        public void SetSBusValue()
        {
            Sbus.Value = this.Value;
        }

        public void SetDBusValue()
        {
            DBus.Value = this.Value;
        }
    }
}