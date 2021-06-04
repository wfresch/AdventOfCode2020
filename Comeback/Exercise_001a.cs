using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static int[] _values;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("../Input/1.txt");
            _values = new int[lines.Length];
            ConvertLines(lines);
            
            Console.WriteLine(FindSumForTwo(2020));
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static void ConvertLines(string[] lines)
        {
            for (int i = 0; i < lines.Length; i ++)
            {
                _values[i] = Int32.Parse(lines[i]);
            }
        }

        static int FindSumForTwo(int sum)
        {
            for (int i = 0; i < _values.Length - 1; i ++)
            {
                var value1 = _values[i];

                for (int j = i + 1; j < _values.Length; j ++)
                {
                    var value2 = _values[j];

                    if (value1 + value2 == sum)
                    {
                        return (value1 * value2);
                    }
                }
            }
            return -1;
        }
        

    }

    
}
