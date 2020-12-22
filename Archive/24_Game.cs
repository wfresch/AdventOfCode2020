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
            var lines = System.IO.File.ReadLines("../Input/8_practice.txt");
            _linesOfCode = new Code[lines.Count()];
            //_accumulator = 0;

            BuildLinesOfCode(lines);
            RunIterations();
            Console.WriteLine(_accumulator);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

    }

    class MathProblem
    {
        public string
    }

    class MathNumber
    {
        public int Number { get; private set; }
        public bool LeftParethesis { get; set; }
        public bool RightParenthesis { get; set; }

        public MathNumber(int number)
        {
            Number = number;
        }

        public MathNumber(int number, bool leftParenthesis, bool rightParenthesis)
        {
            Number = number;
            LeftParethesis = leftParenthesis;
            RightParenthesis = rightParenthesis;
        }

        public override ToString()
        {
            var output = LeftParethesis ? "(" : "";
            output += Number;
            output += (RightParenthesis ? ")" : "");
            return output;
        }
    }
}
