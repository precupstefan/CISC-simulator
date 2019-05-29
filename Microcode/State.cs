namespace Architecture
{
    public class State
    {
        public static State Instance { get; } = new State();

        public uint InstructionExecutionStep = 0;
        public bool Interrupt = false;
        public bool Halt = false;

    }
} 