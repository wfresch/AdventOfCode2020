using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static long[] _numbers;
        
        static void Main(string[] args)
        {
            //var numberLines = System.IO.File.ReadLines("../Input/9_practice.txt");
            var numberLines = System.IO.File.ReadLines("../Input/9.txt");
                        
            _numbers = new long[numberLines.Count()];
            
            BuildNumbers(numberLines);
            var firstWeakness = FindFirstWeakness(25);
            Console.WriteLine(FindEncryptionWeakness(firstWeakness));
                                    
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildNumbers(IEnumerable<string> numberLines)
        {
            var counter = 0;

            foreach(var numberLine in numberLines)
            {
                var number = long.Parse(numberLine);
                _numbers[counter] = number;
                counter++;
            }
        }

        private static long FindFirstWeakness(int groupSize)
        {
            for(int i = groupSize; i < _numbers.Length; i ++)
            {
                var target = _numbers[i];
                var found = false;

                for(int j = (i - groupSize); j < (i - 1); j ++)
                {
                    var number1 = _numbers[j];

                    for(int k = j + 1; k < i; k ++)
                    {
                        if (_numbers[j] + _numbers[k] == target)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

                if (!found)
                {
                    return target;
                }
            }

            return -1;
        }

        public static long FindEncryptionWeakness(long firstWeakness)
        {
            for (int i = 0; i < _numbers.Length - 1; i ++)
            {
                var sum = _numbers[i];
                if (sum >= firstWeakness)
                {
                    continue;
                }

                var min = sum;
                var max = sum;

                for (int j = i + 1; j < _numbers.Length; j ++)
                {
                    var currentNumber = _numbers[j];
                    min = (currentNumber < min) ? currentNumber : min;
                    max = (currentNumber > max) ? currentNumber : max;
                                        
                    sum += _numbers[j];

                    if (sum == firstWeakness)
                    {
                        return min + max;
                    }

                    if (sum > firstWeakness)
                    {
                        break;
                    }
                }
            }

            return -1;
        }
    }
}
