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
            
            Console.WriteLine(FindSumForThree(2020));
                        
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

        static int FindSumForThree(long sum)
        {
            for (int i = 0; i < _values.Length - 2; i ++)
            {
                var value1 = _values[i];

                for (int j = i + 1; j < _values.Length - 1; j ++)
                {
                    var value2 = _values[j];

                    for (int k = j + 1; k < _values.Length; k ++)
                    {
                        var value3 = _values[k];

                        if (value1 + value2 + value3 == sum)
                        {
                            return (value1 * value2 * value3);
                        }
                    }
                }
            }
            return -1;
        }
        

    }

    
}
