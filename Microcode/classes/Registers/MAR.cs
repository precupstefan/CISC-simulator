using System;
using Architecture.enums;
using Architecture.Helpers;

namespace Architecture.classes.Registers
{
    public class MAR : AbstractRegister<ushort>
    {
        public static MAR Instance { get; } = new MAR();
        public override ushort Value { get; set; } = 0;

        public void PrepareForNextMicroInstruction()
        {
            var mir = MIR.Instance;
            var instruction = mir.Value;
            var jumpyStuff = (ushort) (instruction >> Constants.JumpyStuffIndex);
            var mask = (ushort) Math.Pow(2,Constants.JumpyStuffSize) - 1;
            jumpyStuff = (ushort) (jumpyStuff & mask);
            switch((JumpyStuff) jumpyStuff)
            {
                case JumpyStuff.NONE:
                    return;
                case JumpyStuff.STEP:
                    Value++;
                    break;
                case JumpyStuff.JuMP:
                    Value = mir.GetJumpIndex();
                    break;
                case JumpyStuff.JUMPI:
                    Value = mir.GetJumpIndex();
                    Value += Index.Instance.GetIndexValue();
                    break;
                case JumpyStuff.B1:
                    break;
                case JumpyStuff.AD:
                    break;
                case JumpyStuff.Z:
                    break;
                case JumpyStuff.C:
                    break;
                case JumpyStuff.V:
                    break;
                case JumpyStuff.S:
                    break;
                case JumpyStuff.INTR:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}