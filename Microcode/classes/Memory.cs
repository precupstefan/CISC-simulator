using System;
using Architecture.classes.Registers;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes
{
    public class Memory
    {
        public static Memory Instance { get; } = new Memory();

        public ushort this[int a]
        {
            get => memory[a];
            set => memory[a] = value;
        }

        private ushort[] memory = new ushort[65536];

        private void ReadFromMemory()
        {
            var mdr = MDRRegister.Instance;
            var adr = ADRRegister.Instance;
            mdr.Value = (short) memory[adr.Value];
        }

        private void IFCH()
        {
            var ir = IRRegister.Instance;
            var pc = PCRegister.Instance;
            ir.Value = memory[pc.Value];
        }

        private void WriteToMemory()
        {
            var mdr = MDRRegister.Instance;
            var adr = ADRRegister.Instance;
            memory[adr.Value] = (ushort) mdr.Value;
        }

        public void CheckForOperationAndExecuteIfPresent()
        {
            var instr = MIR.Instance.Value;
            var mask = 2 ^ Constants.MemoryOperationSize - 1;
            var operation = (ushort) (instr >> Constants.MemoryOperationIndex);
            operation = (ushort) (operation & mask);
            switch ((MemoryOperations) operation)
            {
                case MemoryOperations.NONE:
                    break;
                case MemoryOperations.IFCH:
                    IFCH();
                    return;
                case MemoryOperations.READ:
                    ReadFromMemory();
                    break;
                case MemoryOperations.WRITE:
                    WriteToMemory();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}