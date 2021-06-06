using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static Stack<GroupAnswers> _stackOfGroups;
        
        static void Main(string[] args)
        {
            var groupLines = System.IO.File.ReadLines("../Input/6.txt");
            _stackOfGroups = new Stack<GroupAnswers>();
            BuildGroups(groupLines);
            
            Console.WriteLine(GetTotalUniqueAnswers());
                                    
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildGroups(IEnumerable<string> groupLines)
        {
            AddNewGroupOfAnswers();

            foreach(var groupLine in groupLines)
            {
                if (String.IsNullOrEmpty(groupLine))
                {
                    AddNewGroupOfAnswers();
                }
                else 
                {
                    var currentGroup = _stackOfGroups.Peek();
                    currentGroup.AddSetOfAnswers(groupLine);
                }
            }
        }

        private static void AddNewGroupOfAnswers()
        {
            var newGroup = new GroupAnswers();
            _stackOfGroups.Push(newGroup);
        }

        private static int GetTotalUniqueAnswers()
        {
            var sum = 0;

            foreach(var groupOfAnswers in _stackOfGroups)
            {
                sum += groupOfAnswers.GetUniqueAnswerCount();
            }

            return sum;
        }
        
    }

    class GroupAnswers
    {
        public HashSet<char> UniqueAnswers { get; set; }

        public GroupAnswers()
        {
            UniqueAnswers = new HashSet<char>();
        }
        // public GroupAnswers(string firstSetOfAnswers)
        // {
        //     UniqueAnswers = new HashSet<char>();

        //     AddSetOfAnswers(firstSetOfAnswers);
        // }

        public void AddSetOfAnswers(string setOfAnswers)
        {
            foreach(var answer in setOfAnswers)
            {
                UniqueAnswers.Add(answer);
            }
        }

        public int GetUniqueAnswerCount()
        {
            return UniqueAnswers.Count;
        } 
        
    }

}
