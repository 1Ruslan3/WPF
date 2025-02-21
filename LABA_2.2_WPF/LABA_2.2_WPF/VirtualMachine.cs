using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LABA_2._2_WPF
{
    public class VirtualMachine
    {
        public int[] Registers { get; } = new int[512];
        public List<List<int>> Values { get;  } = new List<List<int>>();

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
            int i = 0;
            StringBuilder log = new StringBuilder();
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
                            case 1: 
                                reg3  = ~reg1;
                                log.AppendLine($"[Инструкция {i}] Инверсия {reg1}  = {reg3}");
                                Registers[i] = reg3;                               
                                break;
                            case 2:
                                reg3 = reg1 | reg2;
                                log.AppendLine($"[Инструкция {i}] OR: R{reg1} | R{reg2} = {reg3}");                                
                                Registers[i] = reg3;
                                break;
                            case 3: 
                                reg3 = reg1 & reg2;
                                log.AppendLine($"[Инструкция {i}] AND: R{reg1} & R{reg2} = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 4:
                                reg3 = reg1 ^ reg2;
                                log.AppendLine($"[Инструкция {i}] XOR: R{reg1} ^ R{reg2} = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 5: 
                                reg3 = (~reg1) | reg2;
                                log.AppendLine($"[Инструкция {i}] Импликация: ~R{reg1} | R{reg2} = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 6:
                                reg3 = reg1 & (~reg2);
                                log.AppendLine($"[Инструкция {i}] Коимпликация: R{reg1} & ~R{reg2} = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 7:
                                reg3 = ~(reg1 ^ reg2);
                                log.AppendLine($"[Инструкция {i}] Эквиваленция: ~(R{reg1} ^ R{reg2}) = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 8:
                                reg3 = ~(reg1 | reg2);
                                log.AppendLine($"[Инструкция {i}] Стрелка Пирса: ~(R{reg1} | R{reg2}) = {reg3}");
                                break;                                
                            case 9:
                                reg3 = ~(reg1 & reg2);
                                log.AppendLine($"[Инструкция {i}] Штрих Шеффера: ~(R{reg1} & R{reg2}) = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 10:
                                reg3 = reg1 + reg2;
                                log.AppendLine($"[Инструкция {i}] Сложение: R{reg1} + R{reg2} = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 11:
                                reg3 = reg1 - reg2;
                                log.AppendLine($"[Инструкция {i}] Вычитание: R{reg1} - R{reg2}) =  {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 12:
                                reg3 = reg1 * reg2;
                                log.AppendLine($"[Инструкция {i}] Умножение: R{reg1} * R{reg2}) =  {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 13:
                                reg3 = reg1 / reg2;
                                log.AppendLine($"[Инструкция {i}] Деление: R{reg1} / R{reg2} = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 14:
                                reg3 = reg1 % reg2;
                                log.AppendLine($"[Инструкция {i}] Остаток: R{reg1} % R{reg2} = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 15:
                                (reg1, reg2) = (reg2, reg1);
                                Registers[i] = reg3;
                                break;
                            case 16:
                                //byte value1 = (byte)reg3;
                                //byte[] bytes = BitConverter.GetBytes(reg1);
                                //int index = reg2 % bytes.Length;
                                //bytes[index] = value1;
                                //reg1 = BitConverter.ToInt32(bytes, 0);
                                //log.AppendLine($"[Инструкция {i}] Запись байта: байт {value1} в R{reg1}:[{index}]");
                                //Registers[i] = reg3;
                                break;
                            case 17:
                                string result = Convert.ToString(reg1, reg2);
                                log.AppendLine($"{result}");
                                Registers[i] = reg3;
                                break;
                            case 18:
                                string input = Microsoft.VisualBasic.Interaction.InputBox($"Введите число в системе счисления {reg2}:", "Ввод данных", "");
                                if (int.TryParse(input, out int parsedValue))
                                {
                                    reg1 = parsedValue;
                                    log.AppendLine($"[Инструкция {i}] Ввод: {parsedValue} → R1");
                                }
                                else
                                {
                                    throw new Exception("Некорректный ввод!");
                                }
                                Registers[i] = reg3;
                                break;
                            case 19:
                                int p = 0;
                                while (reg1 % Math.Pow(2, p) == 0)
                                {
                                    p++;
                                }

                                reg3 = p - 1;                                
                                log.AppendLine($"[Инструкция {i}] Макс. степень 2: R1 = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 20:
                                reg3 = reg1 << reg2;
                                log.AppendLine($"[Инструкция {i}] Сдвиг влево: R1 << R2 = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 21:
                                reg3 = reg1 >> reg2;
                                log.AppendLine($"[Инструкция {i}] Сдвиг вправо: R1 >> R2 → = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 22:
                                reg3 = (reg1 << reg2) | (reg1 >> (32 - reg2));
                                log.AppendLine($"[Инструкция {i}] Циклический сдвиг влево: R1 = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 23:
                                reg3 = (reg1 >> reg2) | (reg1 << (32 - reg2));
                                log.AppendLine($"[Инструкция {i}] Циклический сдвиг вправо: R1 = {reg3}");
                                Registers[i] = reg3;
                                break;
                            case 24:
                                reg1 = reg2;
                                log.AppendLine($"[Инструкция {i}] Копирование: {reg1}");
                                Registers[i] = reg3;
                                break;
                            default:
                                throw new Exception("Unknown opcode");
                        }
                        ++i;
                    }
                }
            }
            MessageBox.Show(log.ToString(), "Результат выполнения инструкций");
        }
    }
}
