using System;
using System.Collections.Generic;

namespace Code
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/2.txt");

            Console.WriteLine(GetMatchCount(lines));
            
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static int GetMatchCount(IEnumerable<string> lines)
        {
            var matches = 0;

            foreach (var line in lines)
            {
                var scenario = new Scenario(line);

                if (IsMatch(scenario))
                {
                    matches++;
                }
            }

            return matches;
        }

        private static bool IsMatch(Scenario scenario)
        {
            var matches = 0;
            var charArray = scenario.Password.ToCharArray();

            if (charArray[scenario.FirstIndex - 1].Equals(scenario.Letter))
            {
                matches++;
            }

            if (charArray[scenario.SecondIndex - 1].Equals(scenario.Letter))
            {
                matches++;
            }

            return (matches == 1);
        }
    }

    class Scenario 
    {
        public int FirstIndex { get; private set; }
        public int SecondIndex { get; private set; }
        public char Letter { get; private set; }
        public string Password { get; private set; }

        public Scenario(string input)
        {
            var inputParts = input.Split(' ');
            var indexes = inputParts[0];
            var indexParts = indexes.Split('-');

            FirstIndex = int.Parse(indexParts[0]);
            SecondIndex = int.Parse(indexParts[1]);
            Letter = inputParts[1][0];
            Password = inputParts[2];
        }
    }
}
