using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcode.classes
{
    class IVRRegister : AbstractRegister<ushort>
    {
        public static IVRRegister Instance { get; } = new IVRRegister();
        public override ushort Value { get=>Registers.IVR; set => Registers.IVR=value; }
    }
}
