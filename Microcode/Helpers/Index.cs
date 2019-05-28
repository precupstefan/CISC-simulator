using System;
using System.Reflection.Emit;
using System.Security.Cryptography;
using Architecture.classes.Registers;
using Architecture.enums;

namespace Architecture.Helpers
{
    public class Index
    {
        public static Index Instance { get; } = new Index();

        public byte GetIndexValue()
        {
            var ir = IRRegister.Instance;
            var mask = (ushort)Math.Pow(2 , Constants.JumpOffsetSize) - 1;
            var index = ir.Value & mask;

            switch ((IndexValues) index)
            {
                case IndexValues.INDEX0:
                    return 0;
                case IndexValues.INDEX1:
                    return GetClass();
                case IndexValues.INDEX2:
                    return (byte) (MAS()<<1);
                case IndexValues.INDEX3:
                    return (byte) (MAD()<<1);
                case IndexValues.INDEX4:
                    return OpCodeB1();
                case IndexValues.INDEX5:
                    return OpCodeB2();
                case IndexValues.INDEX6:
                    return OpCodeB3();
                case IndexValues.INDEX7:
                    return OpCodeB4();
                case IndexValues.INDEX8:
                    break;
                case IndexValues.INDEX9:
                    break;
                case IndexValues.INDEX10:
                    break;
                case IndexValues.INDEX11:
                    break;
                case IndexValues.INDEX12:
                    break;
                case IndexValues.INDEX13:
                    break;
                case IndexValues.INDEX14:
                    break;
                case IndexValues.INDEX15:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return 0;
        }

        private byte GetClass()
        {
            var instruction = IRRegister.Instance.Value;
            var HIGH = (byte) (instruction >> 8);
            var IR15 = (byte) (HIGH >> 7);
            var IR14 = (byte) ((HIGH >> 6) & 1);
            var IR13 = (byte) ((HIGH >> 5) & 1);
            var CL1 = (byte) (IR15 & IR14);
            var CL0 = (byte) (IR15 & (~IR13));

            var instructionClass = (byte) ((CL1 << 1) | CL0);
            return instructionClass;
        }

        private byte MAS()
        {
            var value = InstructionHelper.Instance.GetShiftedMicroInstructionByBits(10);
            return (byte) (value & 3);
        }

        private byte MAD()
        {
            var value = InstructionHelper.Instance.GetShiftedMicroInstructionByBits(4);
            return (byte) (value & 3);
        }

        private byte OpCodeB1()
        {
            var shiftedValue = InstructionHelper.Instance.GetShiftedMicroInstructionByBits(12);
            return (byte) (shiftedValue & 7);
        }


        private byte OpCodeB2()
        {
            var shiftedValue = InstructionHelper.Instance.GetShiftedMicroInstructionByBits(6);
            return (byte) (shiftedValue & 0xF);
        }

        private byte OpCodeB3()
        {
            var shiftedValue = InstructionHelper.Instance.GetShiftedMicroInstructionByBits(8);
            return (byte) (shiftedValue & 0xF);
        }

        private byte OpCodeB4()
        {
            var instruction = IRRegister.Instance.Value;
            return (byte) (instruction & 0XFFF);
        }

    }
}