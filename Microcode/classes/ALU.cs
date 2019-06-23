using System;
using System.Runtime.CompilerServices;
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
            var mask = Math.Pow(2, Constants.AluSize) - 1;
            aluOperation = (ushort) (aluOperation & (int) mask);
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
                    XOR();
                    break;
                case AluOperations.NEG:
                    NEG();
                    break;
                case AluOperations.ASR:
                    ASR();
                    break;
                case AluOperations.ASL:
                    ASL();
                    break;
                case AluOperations.LSR:
                    LSR();
                    break;
                case AluOperations.ROL:
                    ROL();
                    break;
                case AluOperations.ROR:
                    ROR();
                    break;
                case AluOperations.RLC:
                    RLC();
                    break;
                case AluOperations.RRC:
                    RRC();
                    break;
                case AluOperations.SUB:
                    SUB();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SUB()
        {
            SetRBusValue(DBus.Value - SBus.Value);
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
            SetRBusValue(DBus.Value / 2);
        }

        private static void ASL()
        {
            SetRBusValue(DBus.Value * 2);
        }

        private static void LSR()
        {
            SetRBusValue(DBus.Value >> 1);
        }

        private static void ROL()
        {
            SetRBusValue(DBus.Value << 1 | (DBus.Value >> 15 & 1));
        }

        private static void ROR()
        {
            SetRBusValue(DBus.Value >> 1 | ((DBus.Value & 1) << 15));
        }

        private static void RLC()
        {
            SetRBusValue(DBus.Value << 1 | Convert.ToByte(FLAGRegister.Instance.GetCarryFlag()));
            FLAGRegister.Instance.SetCarryFlag((DBus.Value & 0x8000) > 1);
        }

        private static void RRC()
        {
            var carry = FLAGRegister.Instance.GetCarryFlag() ? 0x1000 : 0;
            SetRBusValue(DBus.Value >> 1 | carry);
            FLAGRegister.Instance.SetCarryFlag(DBus.Value & 1);
            throw new NotImplementedException();
        }
    }
}