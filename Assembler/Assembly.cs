using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembly.enums;
using Assembly.helpers;

namespace Assembly
{

    public class Assembler
    {
        private List<string> instructions = new List<string>();

        public void ReadFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                if (line.Contains('#'))
                {
                    continue;
                }
                instructions.Add(line.ToUpper());
            }
        }

        public List<string> GetInstructions()
        {
            return instructions;
        }

        public void Assemble()
        {
            foreach (string line in instructions)
            {
                string[] keywords = line.Split(' ');
                var instructionValue = Enum.Parse(typeof(InstructionsValue), keywords[0]);
                ushort instruction = (ushort)instructionValue;
                InstructionHelper.GetInstructionClass(instruction);
            }
        }

    }


}
