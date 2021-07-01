using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static int[] _joltages;
        private static Dictionary<int, long> _combinations;
        
        static void Main(string[] args)
        {
            var joltageLines = System.IO.File.ReadLines("../Input/10.txt");
                        
            _joltages = new int[joltageLines.Count()];
            _combinations = new Dictionary<int, long>();
                        
            BuildJoltages(joltageLines);
            Console.WriteLine(FindJoltageCombinations(0));
                                                
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildJoltages(IEnumerable<string> joltageLines)
        {
            var unsortedJoltages = new List<int>();
            unsortedJoltages.Add(0);

            foreach(var joltageLine in joltageLines)
            {
                var joltage = int.Parse(joltageLine);
                unsortedJoltages.Add(joltage);
            }

            unsortedJoltages.Sort();
            var max = unsortedJoltages.Last();
            unsortedJoltages.Add(max + 3);

            _joltages = unsortedJoltages.ToArray();
        }

        private static long FindJoltageCombinations(int cursor)
        {
            if (_combinations.ContainsKey(cursor) && _combinations.TryGetValue(cursor, out var existingCombinationValue))
            {
                return existingCombinationValue;
            }

            if (cursor >= (_joltages.Length - 2))
            {
                return StoreAndReturnCombination(cursor, 1);
            }

            long combinations = 0;

            for (int i = cursor + 1; i < Math.Min(cursor + 4, _joltages.Length); i++)
            {
                var difference = _joltages[i] - _joltages[cursor];

                if (difference <= 3)
                {
                    combinations += FindJoltageCombinations(i);
                }
                else
                {
                    break;
                }
            }

            //Console.WriteLine($"Setting {combinations} combinations for cursor {cursor} (ie - {_joltages[cursor]}).");
            return StoreAndReturnCombination(cursor, combinations);
        }

        private static long StoreAndReturnCombination(int cursor, long combinationValue)
        {
            _combinations.Add(cursor, combinationValue);
            return combinationValue;
        }

    }
        
}
