using Microcode.classes.Bus;

namespace Microcode.classes
{
    public abstract class AbstractRegister<T>
    {
        protected SBus Sbus = SBus.Instance;
        protected DBus DBus = DBus.Instance;
        protected Registers Registers = Registers.Instance;

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