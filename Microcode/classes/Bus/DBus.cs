using System;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes.Bus
{
    public class DBus : Bus
    {
        public static DBus Instance { get; } = new DBus();

        public override dynamic Value
        {
            get
            {
                var mir = MIR.Instance;
                var dbusMicroInstruction = (ushort) (mir.Value >> Constants.DBusIndex);
                var mask = 2 ^ Constants.DBusSize - 1;
                dbusMicroInstruction = (ushort) (dbusMicroInstruction & mask);
                switch ((DBusOperations) dbusMicroInstruction)
                {
                    case DBusOperations.PD0: return 0;
                    case DBusOperations.PD1: return 1;
                    case DBusOperations.PdT: return TRegister.Instance.Value;
                    case DBusOperations.NONE: return null;
                    case DBusOperations.PdRG: return GeneralRegisters.Instance.Value[GetDBusRegister()];
                    case DBusOperations.PdIR: return IRRegister.Instance.Value;
                    case DBusOperations.PdMdr: return MDRRegister.Instance.Value;
                    case DBusOperations.PdSP: return SPRegister.Instance.Value;
                    case DBusOperations.PdAdr: return ADRRegister.Instance.Value;
                    case DBusOperations.PdPc: return PCRegister.Instance.Value;
                    case DBusOperations.PdIVR: return IVRRegister.Instance.Value;
                    case DBusOperations.PdFlag: return FLAGRegister.Instance.Value;
                    case DBusOperations.PD_1: return -1;
                    case DBusOperations.PdIR_OFFSET: return (IRRegister.Instance.Value & 255);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set => throw new System.NotImplementedException();
        }
        
        private int GetDBusRegister()
        {
            var value = IRRegister.Instance.Value;
            var mask = 0xF;
            return mask & value;
        }
    }
}