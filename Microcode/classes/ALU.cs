using System;
using Architecture.classes.Bus;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes
{
    public class ALU
    {
        public static ALU Instance { get; } = new ALU();
        
        private static SBus SBus = SBus.Instance;
        private static DBus DBus = DBus.Instance;
        private static RBus RBus = RBus.Instance;

        private static void SetRBusValue(dynamic value)
        {
            RBus.Value = value;
        }

        public void DoOperation()
        {
            var mir = MIR.Instance;
            var aluOperation = (ushort) (mir.Value >> Constants.AluIndex);
            var mask = 2 ^ Constants.AluSize - 1;
            aluOperation = (ushort) (aluOperation & mask);
            switch ((AluOperations) aluOperation)
            {
                case AluOperations.NONE:
                    return;
                case AluOperations.SUM:
                    SUM();
                    break;
                case AluOperations.AND:
                    AND();
                    break;
                case AluOperations.OR:
                    OR();
                    break;
                case AluOperations.XOR:
                    break;
                case AluOperations.NEG:
                    break;
                case AluOperations.ASR:
                    break;
                case AluOperations.ASL:
                    break;
                case AluOperations.LSR:
                    break;
                case AluOperations.ROL:
                    break;
                case AluOperations.ROR:
                    break;
                case AluOperations.RLC:
                    break;
                case AluOperations.RRC:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
        private static void SUM()
        {
            SetRBusValue(SBus.Value + DBus.Value);
        }

        private static void AND()
        {
            SetRBusValue(SBus.Value & DBus.Value);
        }

        private static void OR()
        {
            SetRBusValue(SBus.Value | DBus.Value);
        }

        private static void XOR()
        {
            SetRBusValue(SBus.Value ^ DBus.Value);
        }

        private static void NEG()
        {
            SetRBusValue(~DBus.Value);
        }

        private static void ASR()
        {
            throw new NotImplementedException();
        }

        private static void ASL()
        {
            throw new NotImplementedException();
        }

        private static void LSR()
        {
            throw new NotImplementedException();
        }

        private static void ROL()
        {
            throw new NotImplementedException();
        }

        private static void ROR()
        {
            throw new NotImplementedException();
        }

        private static void RLC()
        {
            throw new NotImplementedException();
        }

        private static void RRC()
        {
            throw new NotImplementedException();
        }
    }
}