namespace Microcode.classes.Bus
{
    public class RBus: Bus
    {
        public static DBus Instance { get; } = new DBus();
    }
}