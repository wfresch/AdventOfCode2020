using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static int _sumOfCounts;
        private static List<GroupResult> _groupResults;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/6.txt");
            _sumOfCounts = 0;
            _groupResults = new List<GroupResult>();

            BuildGroupResults(lines);
            FindSumOfCounts();
            Console.WriteLine(_sumOfCounts);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildGroupResults(IEnumerable<string> lines)
        {
            BuildNewGroupResult();

            foreach(var line in lines)
            {
                var trimmedLine = line.Replace(" ", "");
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    _groupResults.Last().IndividualAnswers.Add(trimmedLine);
                }
                else
                {
                    BuildNewGroupResult();
                }
            }
        }

        private static void BuildNewGroupResult()
        {
            var groupResult = new GroupResult();
            _groupResults.Add(groupResult);
        }

        private static void FindSumOfCounts()
        {
            foreach(var groupResult in _groupResults)
            {
                var uniqueAnswers = groupResult.GetUniqueAnswers();
                _sumOfCounts += uniqueAnswers;
                //Console.WriteLine($"Adding {uniqueAnswers}, and sum is now {_sumOfCounts}");
            }
        }
    }

    class GroupResult
    {
        public List<string> IndividualAnswers { get; set; }

        public GroupResult()
        {
            IndividualAnswers = new List<string>();
        }

        public int GetUniqueAnswers()
        {
            var uniqueAnswers = new HashSet<char>();
                
            foreach(var individualAnswer in IndividualAnswers)
            {
                for(int i = 0; i < individualAnswer.Length; i ++)
                {
                    uniqueAnswers.Add(individualAnswer[i]);
                }
            }

            return uniqueAnswers.Count;
        }
    }
}
