using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly.enums
{
    enum InstructionsValue : ushort
    {
        // B1
        MOV = 0x0000,
        ADD = 0x1000,
        SUB = 0x2000,
        CMP = 0x3000,
        AND = 0x4000,
        OR  = 0x5000,
        XOR = 0x6000,

        // B2
        CLR  = 0x8000,
        NEG  = 0x8040,
        INC  = 0x8080,
        DEC  = 0x80C0,
        ASL  = 0x8100,
        ASR  = 0x8140,
        LSR  = 0x8180,
        ROL  = 0x81C0,
        ROR  = 0x8200,
        RLC  = 0x8240,
        RRC  = 0x8280,
        JMP  = 0x82C0,
        CALL = 0x8300,
        PUSH = 0x8340,
        POP  = 0x8380,

        // B3
        BR  = 0xC000,
        BNE = 0xC100,
        BEQ = 0xC200,
        BPL = 0xC300,
        BMI = 0xC400,
        BCS = 0xC500,
        BCC = 0xC600,
        BVS = 0xC700,
        BVC = 0xC800,

        // B4
        CLC       = 0xE000,
        CLV       = 0xE001,
        CLZ       = 0xE002,
        CLS       = 0xE003,
        CCC       = 0xE004,
        SEC       = 0xE005,
        SEV       = 0xE006,
        SEZ       = 0xE007,
        SES       = 0xE008,
        SCC       = 0xE009,
        NOP       = 0xE00A,
        RET       = 0xE00B,
        RETI      = 0xE00C,
        HALT      = 0xE00D,
        WAIT       = 0xE00E,
        PUSH_PC   = 0xE00F,
        POP_PC    = 0xE010,
        PUSH_FLAG = 0xE011,
        POP_FLAG  = 0xE012
    }
}

