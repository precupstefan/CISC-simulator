using System;
using System.Diagnostics;
using Architecture;
using Architecture.classes;
using Architecture.classes.Bus;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Sequencer
{
    public class Sequencer
    {
        public static Sequencer Instance { get; } = new Sequencer();

        private MPM mpm = new MPM();
        private Memory Memory = Memory.Instance;
        private ALU ALU = ALU.Instance;
        private Logger.Logger Logger = global::Logger.Logger.Instance;
        private State state = State.Instance;

        private void Step()
        {
            if (state.Halt)
            {
                Logger.Error("Program reached HALT State. Please reassemble the program");
                return;
            }

            switch (state.InstructionExecutionStep)
            {
                case 0:
                {
                    var mir = MIR.Instance;
                    var mar = MAR.Instance;
                    var mpm = new MPM();
                    mir.Value = mpm[mar.Value];
                    state.InstructionExecutionStep = 1;
                    break;
                }

                case 1:
                {
                    ALU.DoOperation();
                    MIR.Instance.CheckForOtherOperationsAndExecuteIfPresent();
                    Memory.CheckForOperationAndExecuteIfPresent();
                    MAR.Instance.PrepareForNextMicroInstruction();
                    state.InstructionExecutionStep = 0;
                    break;
                }

                default:
                {
                    throw new Exception("STATE OUT OF RANGE");
                }
            }
        }

        public void ExecuteCycle()
        {
            Step();
            Step();
            Logger.Warning($"MICROINSTRUCTION {Convert.ToString((long) MIR.Instance.Value, 16).PadLeft(10, '0')}");
        }

        public void ExecuteFullCycle()
        {
            while (!State.Instance.Halt)
            {
                Step();
            }
            Logger.Warning("Finished execution of program");
        }
    }
}