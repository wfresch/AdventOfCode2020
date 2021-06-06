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
        public List<UniqueAnswer> UniqueAnswers { get; set; }

        public GroupAnswers()
        {
            UniqueAnswers = new List<UniqueAnswer> 
            {
                new UniqueAnswer('a'), new UniqueAnswer('b'), new UniqueAnswer('c'), new UniqueAnswer('d'), new UniqueAnswer('e'), new UniqueAnswer('f'),
                new UniqueAnswer('g'), new UniqueAnswer('h'), new UniqueAnswer('i'), new UniqueAnswer('j'), new UniqueAnswer('k'), new UniqueAnswer('l'),
                new UniqueAnswer('m'), new UniqueAnswer('n'), new UniqueAnswer('o'), new UniqueAnswer('p'), new UniqueAnswer('q'), new UniqueAnswer('r'),
                new UniqueAnswer('s'), new UniqueAnswer('t'), new UniqueAnswer('u'), new UniqueAnswer('v'), new UniqueAnswer('w'), new UniqueAnswer('x'),
                new UniqueAnswer('y'), new UniqueAnswer('z')
            };
        }
        
        public void AddSetOfAnswers(string setOfAnswers)
        {
            foreach(var answer in UniqueAnswers)
            {
                if (!setOfAnswers.Contains(answer.Answer))
                {
                    answer.MarkInvalid();
                }
            }
        }

        public int GetUniqueAnswerCount()
        {
            return UniqueAnswers.Count(x => x.Valid);
        } 
        
    }

    class UniqueAnswer
    {
        public char Answer { get; private set; }
        public bool Valid { get; private set; }

        public UniqueAnswer(char answer)
        {
            Answer = answer;
            Valid = true;
        }

        public void MarkInvalid()
        {
            Valid = false;
        }
    }

}
