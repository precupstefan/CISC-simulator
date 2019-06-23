using System;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes.Bus
{
    public class RBus : Bus
    {
        public static RBus Instance { get; } = new RBus();

        private dynamic value = 0;

        public override dynamic Value
        {
            get => value;
            set
            {
                this.value = value;
                var mir = MIR.Instance;
                var rbusMicroInstruction = (ushort) (mir.Value >> Constants.RBusIndex);
                var mask = (ushort) Math.Pow(2, Constants.RBusSize) - 1;
                rbusMicroInstruction = (ushort) (rbusMicroInstruction & mask);
                switch ((RBusOperations) rbusMicroInstruction)
                {
                    case RBusOperations.NONE: return;
                    case RBusOperations.PmRG:
                        GeneralRegisters.Instance.Value[GetRBusRegister()] = (short)this.value;
                        Architecture.Registers.Instance.TriggerGeneralRegisterUpdate();
                        break;
                    case RBusOperations.PmIR:
                        IRRegister.Instance.Value = this.value;
                        break;
                    case RBusOperations.PmMdr:
                        MDRRegister.Instance.Value = (short) this.value;
                        break;
                    case RBusOperations.PmSP:
                        SPRegister.Instance.Value = this.value;
                        break;
                    case RBusOperations.PmAdr:
                        ADRRegister.Instance.Value = (ushort)this.value;
                        break;
                    case RBusOperations.PmT:
                        TRegister.Instance.Value = (short)this.value;
                        break;
                    case RBusOperations.PmPc:
                        PCRegister.Instance.Value = (ushort)this.value;
                        break;
                    case RBusOperations.PmIVR:
                        IVRRegister.Instance.Value = this.value;
                        break;
                    case RBusOperations.PmFlag:
                        FLAGRegister.Instance.Value = this.value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
            }
        }
        
        private int GetRBusRegister()
        {
            var value = IRRegister.Instance.Value;
            var mask = 0xF;
            return mask & value;
        }
    }
}