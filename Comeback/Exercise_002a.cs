using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<PasswordRule> _rules;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/2.txt");
            _rules = new List<PasswordRule>();

            ConvertLines(lines);
            Console.WriteLine(CountValidPasswords());
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static void ConvertLines(IEnumerable<string> lines)
        {
            foreach(var line in lines)
            {
                var rule = new PasswordRule(line);
                _rules.Add(rule);
            }
        }

        static int CountValidPasswords()
        {
            var count = 0;

            foreach(var rule in _rules)
            {
                if (rule.IsValid())
                {
                    count ++;
                }
            }

            return count;
        }

    }

    class PasswordRule 
    {
        int MinOccurrences { get; set; }
        int MaxOccurrences { get; set; }
        char Letter { get; set; }
        string Password { get; set; }

        public PasswordRule(string input)
        {
            var stringParts = input.Split(' ');
            
            var occurrenceSection = stringParts[0];
            var occurrenceParts = occurrenceSection.Split('-');
            MinOccurrences = int.Parse(occurrenceParts[0]);
            MaxOccurrences = int.Parse(occurrenceParts[1]);
            
            var letterSection = stringParts[1];
            Letter = char.Parse(letterSection.Substring(0, 1));
            
            Password = stringParts[2];
        }

        public bool IsValid()
        {
            var count = 0;
            foreach(var passwordCharacter in Password)
            {
                if (passwordCharacter == Letter)
                {
                    count++;
                }
            }

            return (count >= MinOccurrences && count <= MaxOccurrences);
        }
    }
}
