namespace Microcode.classes
{
    public class Memory
    {
        public static Memory Instance { get; } = new Memory();

        public ushort this[int a]
        {
            get => memory[a];
            set => memory[a] = value;
        }

        private ushort[] memory = new ushort[65536];
    }
}