using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static int[] _joltages;
        
        static void Main(string[] args)
        {
            var joltageLines = System.IO.File.ReadLines("../Input/10_practice_001.txt");
            //var joltageLines = System.IO.File.ReadLines("../Input/10.txt");
                        
            _joltages = new int[joltageLines.Count()];
            
            BuildJoltages(joltageLines);
            Console.WriteLine(FindTotalCombinations());
                                    
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
            _joltages = unsortedJoltages.ToArray();
        }

        private static int FindJoltageDistribution()
        {
            var oneJoltageDiffs = 0;
            var threeJoltageDiffs = 0;

            for (int i = 0; i < _joltages.Length - 1; i ++)
            {
                var joltage = _joltages[i];
                var next = _joltages[i + 1];
                var diff = next - joltage;

                if (diff == 1)
                {
                    //Console.WriteLine($"{next} - {joltage} = one");
                    oneJoltageDiffs++;
                }
                else if (diff == 3)
                {
                    //Console.WriteLine($"{next} - {joltage} = three");
                    threeJoltageDiffs++;
                }
            }

            //Console.WriteLine($"oneJoltageDiffs: {oneJoltageDiffs}, threeJoltageDiffs: {threeJoltageDiffs}");
            return oneJoltageDiffs * (threeJoltageDiffs + 1);
        }

        private static int FindJoltageCombinations()
        {
            var totalCombinations = 1;

            for (int i = 0; i < _joltages.Length; i ++)
            {
                var currentJoltage = _joltages[i];
                var currentJumps = -1;

                for (int j = 1; j <= 3; j ++)
                {
                    var index = i + j;
                    
                    if (index > (_joltages.Length - 1))
                    {
                        break;
                    }

                    if (_joltages[index] - currentJoltage <= 3)
                    {
                        currentJumps++;
                    }
                }

                totalCombinations += currentJumps;
            }

            return totalCombinations;
        }
    }
}
