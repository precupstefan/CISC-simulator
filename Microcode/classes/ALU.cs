using System;
using System.Diagnostics.CodeAnalysis;
using Microcode.classes.Bus;

namespace Microcode.classes
{
    public static class ALU
    {
        private static SBus SBus = SBus.Instance;
        private static DBus DBus = DBus.Instance;
        private static RBus RBus = RBus.Instance;

        private static void SetRBusValue(dynamic value)
        {
            RBus.Value = value;
        }

        public static void SUM()
        {
            SetRBusValue(SBus.Value + DBus.Value);
        }

        public static void AND()
        {
            SetRBusValue(SBus.Value & DBus.Value);
        }

        public static void OR()
        {
            SetRBusValue(SBus.Value | DBus.Value);
        }

        public static void XOR()
        {
            SetRBusValue(SBus.Value ^ DBus.Value);
        }

        public static void NEG()
        {
            SetRBusValue(~DBus.Value);
        }

        public static void ASR()
        {
            throw new NotImplementedException();
        }

        public static void ASL()
        {
            throw new NotImplementedException();
        }

        public static void LSR()
        {
            throw new NotImplementedException();
        }

        public static void ROL()
        {
            throw new NotImplementedException();
        }

        public static void ROR()
        {
            throw new NotImplementedException();
        }

        public static void RLC()
        {
            throw new NotImplementedException();
        }

        public static void RRC()
        {
            throw new NotImplementedException();
        }
    }
}