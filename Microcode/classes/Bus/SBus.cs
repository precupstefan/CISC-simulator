using System;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes.Bus
{
    public class SBus : Bus
    {
        public static SBus Instance { get; } = new SBus();


        public override dynamic Value
        {
            get
            {
                var mir = MIR.Instance;
                var sbusMicroInstruction = (ushort) (mir.Value >> Constants.SBusIndex);
                switch ((SBusOperations) sbusMicroInstruction)
                {
                    case SBusOperations.PD0: return 0;
                    case SBusOperations.PD1: return 1;
                    case SBusOperations.PdT: return TRegister.Instance.Value;
                    case SBusOperations.NONE: return null;
                    case SBusOperations.PdRG: return GeneralRegisters.Instance.Value[GetSBusRegister()];
                    case SBusOperations.PdIR: return IRRegister.Instance.Value;
                    case SBusOperations.PdMdr: return MDRRegister.Instance.Value;
                    case SBusOperations.PdSP: return SPRegister.Instance.Value;
                    case SBusOperations.PdAdr: return ADRRegister.Instance.Value;
                    case SBusOperations.PdPc: return PCRegister.Instance.Value;
                    case SBusOperations.PdIVR: return IVRRegister.Instance.Value;
                    case SBusOperations.PdFlag: return FLAGRegister.Instance.Value;
                    case SBusOperations.PD_1: return -1;
                    case SBusOperations.PdIR_OFFSET: return (IRRegister.Instance.Value & 255);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set => throw new System.NotImplementedException();
        }


        private int GetSBusRegister()
        {
            var value = InstructionHelper.Instance.GetShiftedInstructionByBits(6);
            var mask = 0xF;
            return mask & value;
        }
    }
}