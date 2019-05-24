using System;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Windows;
using Microcode.Annotations;

namespace Microcode
{
    public class Registers : INotifyPropertyChanged
    {
        public static Registers Instance { get; } = new Registers();

        private bool displayHex = false;

        public bool DisplayHex
        {
            get => displayHex;
            set
            {
                displayHex = value;
                RaisePropertyChanged("Architecture");
            }
        }

        public string tada = "TATAADA";

        public string tadu
        {
            get => tada;
            set
            {
                tada = value;
                RaisePropertyChanged("tadu");
            }
        }


        public short[] _GeneralRegisters = new short[15];
        public ushort _Flags = 0;
        public ushort _SP = ushort.MaxValue;
        public short _T = 0;
        public ushort _PC = 0;
        public ushort _IVR = 0;
        public ushort _ADR = 0;
        public short _MDR = 0;
        public ushort _IR = 0;


        #region REGISTERS

        public short[] GeneralRegisters
        {
            get => _GeneralRegisters;
            set
            {
                _GeneralRegisters = value;
                RaisePropertyChanged("GetGeneralRegister");
            }
        }

        public ushort Flags
        {
            get => _Flags;
            set
            {
                _Flags = value;
                RaisePropertyChanged("GetFlags");
            }
        }

        public ushort SP
        {
            get => _SP;
            set
            {
                _SP = value;
                RaisePropertyChanged("GetSP");
            }
        }

        public short T
        {
            get => _T;
            set
            {
                _T = value;
                RaisePropertyChanged("GetT");
            }
        }

        public ushort PC
        {
            get => _PC;
            set
            {
                _PC = value;
                RaisePropertyChanged("GetPC");
            }
        }

        public ushort IVR
        {
            get => _IVR;
            set
            {
                _IVR = value;
                RaisePropertyChanged("GetIVR");
            }
        }

        public ushort ADR
        {
            get => _ADR;
            set
            {
                _ADR = value;
                RaisePropertyChanged("GetADR");
            }
        }

        public short MDR
        {
            get => _MDR;
            set
            {
                _MDR = value;
                RaisePropertyChanged("GetMDR");
            }
        }

        public ushort IR
        {
            get => _IR;
            set
            {
                _IR = value;
                RaisePropertyChanged("GetIR");
            }
        }

        #endregion

        #region REGISTERS_UI

        public string[] GetGeneralRegister
        {
            get
            {
                string[] something = new string[15];
                for (int i = 0; i < _GeneralRegisters.Length; i++)
                {
                    something[i] = ConvertToStringAsBinOrHex(_GeneralRegisters[i]);
                }

                return something;
            }
        }

        public string GetFlags => ConvertToStringAsBinOrHex(Flags);

        public string GetSP => ConvertToStringAsBinOrHex(SP);

        public string GetT => ConvertToStringAsBinOrHex(T);

        public string GetPC => ConvertToStringAsBinOrHex(PC);

        public string GetIVR => ConvertToStringAsBinOrHex(IVR);

        public string GetADR => ConvertToStringAsBinOrHex(ADR);

        public string GetMDR => ConvertToStringAsBinOrHex(MDR);

        public string GetIR => ConvertToStringAsBinOrHex(IR);

        #endregion


        private string ConvertToStringAsBinOrHex<T>(T variable)
        {
            return displayHex ? ConvertToStringAsHex(variable) : ConvertToStringAsBin(variable);
        }

        private string ConvertToStringAsBin<T>(T variable)
        {
            switch (variable)
            {
                case short _:
                    return Convert.ToString((short) (object) variable, 2);
                case ushort _:
                    return Convert.ToString((ushort) (object) variable, 2);
                default:
                    throw new FormatException($"{variable.GetType()} is not implemented");
            }
        }

        private string ConvertToStringAsHex<T>(T variable)
        {
            switch (variable)
            {
                case short _:
                    return "0x" + Convert.ToString((short) (object) variable, 16);
                case ushort _:
                    return "0x" + Convert.ToString((ushort) (object) variable, 16);
                default:
                    throw new FormatException($"{variable.GetType()} is not implemented");
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}