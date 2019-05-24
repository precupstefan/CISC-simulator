using System;
using Microcode;

namespace Sequencer
{
    public class Sequencer
    {

        private MPM mpm = new MPM();
        private ushort[] memory;
        private Action<string> LOG;

        public Sequencer(ushort[] memory)
        {
            this.memory = memory;
        }

        public Sequencer SetLogAction(Action<string> action)
        {
            this.LOG = action;
            return this;
        }
    }
}