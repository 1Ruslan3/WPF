using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA_2._2_WPF
{
    public class VirtualMachine
    {
        public uint[] Registers { get; } = new uint[512];

        //public void Execute(List<uint> instructions)      // MemoryStream 
        //{
        //    foreach (var instruction in instructions)
        //    {
        //        var opCode = instruction & 0x1F;
        //        var reg1 = (instruction >> 5) & 0x1FF;
        //        var reg2 = (instruction >> 14) & 0x1FF;
        //        var reg3 = (instruction >> 23) & 0x1FF;

        //        switch (opCode)
        //        {
        //            case 0b00001: // ADD
        //                Registers[reg3] = reg1 + reg2;
        //                break;
        //            case 0b00010: // SUB
        //                Registers[reg3] = Registers[reg1] - Registers[reg2];
        //                break;
        //            case 0b00011: // MUL
        //                Registers[reg3] = Registers[reg1] * Registers[reg2];
        //                break;
        //            case 0b00100: // DIV
        //                Registers[reg3] = Registers[reg1] / Registers[reg2];
        //                break;
        //            case 0b00101: // MOV
        //                Registers[reg3] = Registers[reg1];
        //                break;
        //            default:
        //                throw new Exception("Unknown opcode");
        //        }
        //    }
        //}

        public void Execute(MemoryStream memorys)      // MemoryStream 
        {
            using (MemoryStream memoryStream = memorys)
            {
                byte[] buffer = new byte[4];
                while (memoryStream.Position < memoryStream.Length)
                {
                    int bytesRead = memoryStream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 4)
                    {
                        var value = BitConverter.ToInt32(buffer, 0);
                        var opCode = value & 0x1F;
                        var reg1 = (value >> 5) & 0x1FF;
                        var reg2 = (value >> 14) & 0x1FF;
                        var reg3 = (value >> 23) & 0x1FF;
                        switch (opCode)
                        {
                            case 0b00001: // ADD
                                Registers[reg3] = (uint)reg1 + (uint)reg2;
                                break;
                            case 0b00010: // SUB
                                Registers[reg3] = Registers[reg1] - Registers[reg2];
                                break;
                            case 0b00011: // MUL
                                Registers[reg3] = Registers[reg1] * Registers[reg2];
                                break;
                            case 0b00100: // DIV
                                Registers[reg3] = Registers[reg1] / Registers[reg2];
                                break;
                            case 0b00101: // MOV
                                Registers[reg3] = Registers[reg1];
                                break;
                            default:
                                throw new Exception("Unknown opcode");
                        }
                    }
                }
            }
        }
    }
}
