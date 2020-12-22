using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static IEnumerable<string> _lines;
        private static int _accumulator;

        static void Main(string[] args)
        {
            _lines = System.IO.File.ReadLines("../Input/8.txt");
            RunIterations();
            Console.WriteLine(_accumulator);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void RunIterations()
        {
            for (int i = 0; i < _lines.Count(); i ++)
            {
                _accumulator = 0;
                var linesOfCode = BuildLinesOfCode(_lines);
                
                //Console.WriteLine($"For iteration {i}, the first line of code is {linesOfCode[0].Command} {linesOfCode[0].Operand}{linesOfCode[0].Value}, and Visited is {linesOfCode[0].Visited.ToString()}.");

                if (linesOfCode[i].Command == "nop")
                {
                    //Console.WriteLine($"For line {i}, replace nop w/ jmp.");
                    linesOfCode[i].Command = "jmp";
                }
                else if (linesOfCode[i].Command == "jmp")
                {
                    //Console.WriteLine($"For line {i}, replace jmp w/ nop.");
                    linesOfCode[i].Command = "nop";
                }
                else
                {
                    //Console.WriteLine($"For line {i}, {linesOfCode[i].Command} isn't nop or jmp, skipping.");
                    continue;
                }

                //Console.WriteLine($"Running iteration {i}");
                if (RunCode(linesOfCode))
                {
                    return;
                }
                else
                {
                    continue;
                }
            }
        }

        private static Code[] BuildLinesOfCode(IEnumerable<string> lines)
        {
            var cursor = 0;
            var linesOfCode = new Code[lines.Count()];

            foreach(var line in lines)
            {
                var code = new Code(line);
                linesOfCode[cursor] = code;
                cursor++;
            }

            return linesOfCode;
        }

        private static bool RunCode(Code[] linesOfCode)
        {
            var valid = true;
            var cursor = 0;
            var length = linesOfCode.Count();

            while (valid && cursor < length)
            {
                var currentLine = linesOfCode[cursor];
                if (currentLine.Visited)
                {
                    Console.WriteLine($"Infinite Loop detected on cursor {cursor}.");
                    return false;
                }

                currentLine.Visited = true;

                switch(currentLine.Command)
                {
                    case "nop":
                        cursor++;
                        break;
                    case "acc":
                        if (currentLine.Operand == '+')
                        {
                            _accumulator += currentLine.Value;
                        }
                        else 
                        {
                            _accumulator -= currentLine.Value;
                        }
                        cursor++;
                        break;
                    case "jmp":
                        if (currentLine.Operand == '+')
                        {
                            cursor += currentLine.Value;
                        }
                        else 
                        {
                            cursor -= currentLine.Value;
                        }
                        break;
                }
                //Console.WriteLine($"_accumulator is now {_accumulator}.");
            }
            return true;
        }
    }

    class Code
    {
        public string Command { get; set; }
        public char Operand { get; private set; }
        public int Value { get; private set; }
        public bool Visited { get; set; }

        public Code(string input)
        {
            Command = input.Substring(0, 3);
            Operand = input[4];
            Value = int.Parse(input.Substring(5));
        }
    }
}
