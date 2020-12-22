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
                var unanimousAnswers = groupResult.GetUnanimousAnswers();
                _sumOfCounts += unanimousAnswers;
                //Console.WriteLine($"Adding {unanimousAnswers}, and sum is now {_sumOfCounts}");
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

        public int GetUnanimousAnswers()
        {
            var unanimousAnswers = new Dictionary<char, int>();
                
            foreach(var individualAnswer in IndividualAnswers)
            {
                for(int i = 0; i < individualAnswer.Length; i ++)
                {
                    var answer = individualAnswer[i];

                    if (unanimousAnswers.ContainsKey(answer))
                    {
                        unanimousAnswers[answer] += 1;
                    }
                    else 
                    {
                        unanimousAnswers.Add(answer, 1);
                    }
                }
            }

            return unanimousAnswers.Count(x => x.Value == IndividualAnswers.Count);
        }
    }
}
