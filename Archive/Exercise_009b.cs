using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static long[] _input;
        private static int _preamble;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/9.txt");
            _preamble = 25;
            BuildNumberList(lines);
            var invalidNumber = FindInvalidNumber();
            Console.WriteLine(FindEncryptionWeakness(invalidNumber));
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static void BuildNumberList(IEnumerable<string> lines)
        {
            _input = new long[lines.Count()];
            var cursor = 0;
            
            foreach(var line in lines)
            {
                long number = long.Parse(line);
                _input[cursor] = number;
                cursor++;
            }
        }

        static long FindEncryptionWeakness(long invalidNumber)
        {
            var invalidIndex = Array.IndexOf(_input, invalidNumber);
            for (int i = 0; i < invalidIndex - 1; i ++)
            {
                var firstNumber = _input[i];
                var min = firstNumber;
                var max = firstNumber;
                var sum = firstNumber;

                for (int j = i + 1; (j < invalidIndex) && sum < invalidNumber; j ++)
                {
                    var nextNumber = _input[j];

                    if (nextNumber < min)
                    {
                        min = nextNumber;
                    }

                    if (nextNumber > max)
                    {
                        max = nextNumber;
                    }

                    sum += _input[j];
                    if (sum == invalidNumber)
                    {
                        return min + max;
                    }
                }
            }
            return -1;
        }

        static long FindInvalidNumber()
        {
            var count = _input.Count();
            
            for(int i = _preamble; i < _input.Count(); i ++)
            {
                var desiredSum = _input[i];
                var found = false;
                var range = new long[_preamble];
                Array.Copy(_input, i - _preamble, range, 0, _preamble);
                 
                for (int j = 0; (j < range.Length - 1) && !found; j ++)
                {
                    var firstNumber = range[j];

                    for (int k = j + 1; k < range.Length && !found; k ++)
                    {
                        var secondNumber = range[k];
                        if (firstNumber + secondNumber == desiredSum)
                        {
                            found = true;
                        }
                        else
                        {
                            //Console.WriteLine($"{firstNumber} + {secondNumber} != {desiredSum}.");
                        }
                    }
                }

                if (!found)
                {
                    return _input[i];
                }
            }
            return -1;
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
