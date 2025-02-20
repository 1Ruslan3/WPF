using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LABA_2._2_WPF
{
    class Compiler
    {
        private Dictionary<string, int> opCodes = new Dictionary<string, int>
        {
            { "ADD", 0b00001 },
            { "SUB", 0b00010 },
            { "MUL", 0b00011 },
            { "DIV", 0b00100 },
            { "MOV", 0b00101 }
        };

        public List<uint> Compile(string code)
        {
            var instructions = new List<uint>();
            var lines = code.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2 || !opCodes.ContainsKey(parts[0]))
                    throw new Exception("Invalid instruction");

                var opCode = opCodes[parts[0]];
                var reg1 = int.Parse(parts[1]);
                var reg2 = parts.Length > 2 ? int.Parse(parts[2]) : 0;
                var reg3 = parts.Length > 3 ? int.Parse(parts[3]) : 0;

                var instruction = (uint)((opCode & 0x1F) | (reg1 & 0x1FF) << 5 | (reg2 & 0x1FF) << 14 | (reg3 & 0x1FF) << 23);
                instructions.Add(instruction);
            }

            return instructions;
        }

        public MemoryStream LoadInstructionsIntoMemoryStream(List<uint> instructions)
        {         
            MemoryStream memoryStream = new MemoryStream();
           
            foreach (var instruction in instructions)
            {
                byte[] bytes = BitConverter.GetBytes(instruction);
                memoryStream.Write(bytes, 0, bytes.Length);
            }
          
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        public void SaveToStream(List<uint> instructions, Stream stream)
        {
            using (var writer = new BinaryWriter(stream))
            {
                foreach (var instruction in instructions)
                {
                    writer.Write(instruction);
                }
            }
        }
    }
}
