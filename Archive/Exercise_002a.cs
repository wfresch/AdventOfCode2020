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
            var occurrences = 0;
            var charArray = scenario.Password.ToCharArray();

            for (int i = 0; i < charArray.Length; i ++)
            {
                if (charArray[i].Equals(scenario.Letter))
                {
                    occurrences++;
                    if (occurrences > scenario.MaxOccurrences)
                    {
                        return false;
                    }
                }
            }

            return (occurrences >= scenario.MinOccurrences); // && occurrences <= scenario.MaxOccurrences);
        }
    }

    class Scenario 
    {
        public int MinOccurrences { get; private set; }
        public int MaxOccurrences { get; private set; }
        public char Letter { get; private set; }
        public string Password { get; private set; }

        public Scenario(string input)
        {
            var inputParts = input.Split(' ');
            var boundaries = inputParts[0];
            var boundaryParts = boundaries.Split('-');

            MinOccurrences = int.Parse(boundaryParts[0]);
            MaxOccurrences = int.Parse(boundaryParts[1]);
            Letter = inputParts[1][0];
            Password = inputParts[2];
        }
    }
}
