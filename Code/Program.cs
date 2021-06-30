using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static int[] _joltages;
        private static Stack<SubGroup> _subGroups;
        
        static void Main(string[] args)
        {
            //var joltageLines = System.IO.File.ReadLines("../Input/10.txt");
            var joltageLines = System.IO.File.ReadLines("../Input/10_practice_001c.txt");
            //var joltageLines = System.IO.File.ReadLines("../Input/10_practice_alternate.txt");
                        
            _joltages = new int[joltageLines.Count()];
            _subGroups = new Stack<SubGroup>();
                        
            BuildJoltages(joltageLines);
            Console.WriteLine(FindJoltageCombinations());
                                                
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
                    oneJoltageDiffs++;
                }
                else if (diff == 3)
                {
                    threeJoltageDiffs++;
                }
            }

            return oneJoltageDiffs * (threeJoltageDiffs + 1);
        }
        private static int FindJoltageCombinations()
        {
            //return AddJoltageCombinations(0, 0);
            //return _combinations;

            // BUILD STACK OF LISTS (or possibly arrays)
            // need a stack of lists
            // standard for-loop
            // add current item to list at top of stack
            // if (i+1) minus i is >= 3
            //     create new list and push to top of stack
            BuildSubgroups();

            // HELPER METHOD FOR EACH LIST
            // Call recursive method to get combinations for that list
            // Store number in a List of ints
            AssignSubgroupCombinations();

            // Loop through List of ints and multiply
            return CalculateTotalCombinations();


            //return -1;
        }

        private static void BuildSubgroups()
        {
            AddNewSubgroup();

            for (int i = 0; i < _joltages.Length - 1; i ++)
            {
                InsertNumberIntoCurrentSubgroup(_joltages[i]);

                if (_joltages[i+1] - _joltages[i] >= 3)
                {
                    AddNewSubgroup();
                }
            }

            InsertNumberIntoCurrentSubgroup(_joltages[_joltages.Length - 1]);

            Console.WriteLine($"Number of subgroups: {_subGroups.Count()}");
        }

        private static void AddNewSubgroup()
        {
            var subgroup = new SubGroup();
            _subGroups.Push(subgroup);
        }

        private static void InsertNumberIntoCurrentSubgroup(int number)
        {
            var currentSubgroup = _subGroups.Peek();
            currentSubgroup.AddNumber(number);
        }

        private static void AssignSubgroupCombinations()
        {
            var cursor = 0;

            foreach(var subgroup in _subGroups)
            {
                subgroup.GetCombinations();

                var subGroupContents = "";
                foreach(var number in subgroup.Numbers)
                {
                    subGroupContents += $"{number},";
                }
                subGroupContents.Trim(',');

                Console.WriteLine($"Combinations for subgroup[{cursor}] {{{subGroupContents}}}: {subgroup.Combinations}.");
                cursor++;
            }
        }

        private static int CalculateTotalCombinations()
        {
            var combinations = 1;

            foreach(var subgroup in _subGroups)
            {
                // var currentCombinations = subgroup.Combinations + 1;
                // combinations *= currentCombinations;
                combinations *= Math.Max(1, subgroup.Combinations);
            }

            return combinations;
        }
        
    }

    class SubGroup
    {
        public List<int> Numbers { get; set; }
        private int[] Joltages { get; set; }
        public int Combinations { get; private set; }

        public Dictionary<int, int> CombinationLookup { get; set; }

        public SubGroup()
        {
            Numbers = new List<int>();
            CombinationLookup = new Dictionary<int, int>();
        }

        public void AddNumber(int number)
        {
            Numbers.Add(number);
        }

        public void GetCombinations()
        {
            Joltages = Numbers.ToArray();
            //Combinations = AddCombinations(0);
            Combinations = PathsRemaining(0);
        }

        private int PathsRemaining(int cursor)
        {
            if (cursor == (Joltages.Length - 1))
            {
                return 1;
            }

            var sum = 0;

            var test = Math.Min(cursor + 4, Joltages.Length);
            Console.WriteLine($"test: {test}");

            for (int i = cursor + 1; i <= Math.Min(cursor + 3, Joltages.Length); i ++)
            {
                if (Joltages[i] - Joltages[cursor] <= 3)
                {
                    sum += PathsRemaining(i);
                }
            }

            return sum;
        }

        // private int AddCombinations(int cursor)
        // {
        //     var combinations = 0;
            
        //     if (Joltages.Length <= 2)
        //     {
        //         //Console.WriteLine($"For {Joltages[cursor]}, possible combinations is automatically 1.");
        //         return 1;
        //     }

        //     if (cursor == (Joltages.Length - 1))
        //     {
        //         Console.WriteLine($"Automatically returning zero for {Joltages[cursor]}.");
        //         return 0;
        //     }

        //     for (int i = 1; i <= 3; i ++)
        //     {
        //         //Console.WriteLine($"AddCombinations loop for {Joltages[cursor]}, i={i}");

        //         if ((cursor + i) < (Joltages.Length))
        //         {
        //             var difference = Joltages[cursor + i] - Joltages[cursor];
        //             //Console.WriteLine($"At cursor {cursor}, diff of {difference} for {Joltages[cursor+i]} - {Joltages[cursor]}.");

        //             if (difference < 3)
        //             {
        //                 //Console.WriteLine($"The difference of {Joltages[cursor+i]} - {Joltages[cursor]} = {difference}");
        //                 combinations++;
        //                 //Console.WriteLine($"combinations for {Joltages[cursor]} incremented by one to {combinations}.");

        //                 combinations += AddCombinations(cursor + i);
        //                 //Console.WriteLine($"combinations for {Joltages[cursor]} incremented after AddCombinations to {combinations}.");
        //             }
        //             else
        //             {
        //                 //Console.WriteLine($"The difference of {Joltages[cursor+i]} - {Joltages[i]} = {difference}");
        //             }
        //         }
        //         else
        //         {
        //             //Console.WriteLine($"Skipping because {cursor+i} is >= {Joltages.Length}.");
        //             break;
        //         }
        //     }

        //     //combinations = Math.Max(1, combinations);
        //     Console.WriteLine($"Combinations for {Joltages[cursor]}: {combinations}.");
        //     return combinations;
        //     //return parent ? combinations : (combinations - 1);
        // }
        
    }
}
