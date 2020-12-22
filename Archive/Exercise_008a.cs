using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static Code[] _linesOfCode;
        private static int _accumulator;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/8.txt");
            _linesOfCode = new Code[lines.Count()];
            _accumulator = 0;

            BuildLinesOfCode(lines);
            RunCode();
            Console.WriteLine(_accumulator);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildLinesOfCode(IEnumerable<string> lines)
        {
            var cursor = 0;

            foreach(var line in lines)
            {
                var code = new Code(line);
                _linesOfCode[cursor] = code;
                cursor++;
            }
        }

        private static void RunCode()
        {
            var valid = true;
            var cursor = 0;

            while (valid)
            {
                var currentLine = _linesOfCode[cursor];
                if (currentLine.Visited)
                {
                    return;
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
            }
        }
    }

    class Code
    {
        public string Command { get; private set; }
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
