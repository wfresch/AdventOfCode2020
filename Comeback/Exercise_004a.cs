using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Code
{
    class Program
    {
        private static Stack<Passport> _passports;

        static void Main(string[] args)
        {
            var passportLines = System.IO.File.ReadLines("../Input/4.txt");
            _passports = new Stack<Passport>();
            AddBlankPassport();
            BuildPassports(passportLines);
            Console.WriteLine(CountValidPassports());
                                    
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void AddBlankPassport()
        {
            var passport = new Passport();
            _passports.Push(passport);
        }

        private static void BuildPassports(IEnumerable<string> passportLines)
        {
            foreach(var passportLine in passportLines)
            {
                if (String.IsNullOrEmpty(passportLine))
                {
                    AddBlankPassport();
                }
                else
                {
                    var currentPassport = _passports.Peek();
                    currentPassport.AddCredentials(passportLine);
                }
            }
        }

        private static int CountValidPassports()
        {
            var validCount = 0;

            foreach(var passport in _passports)
            {
                var context = new ValidationContext(passport, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(passport, context, results, true);

                validCount += (isValid ? 1 : 0);
            }

            return validCount;
        }
        
    }

    class Passport
    {
        [Range(1, int.MaxValue)]
        public int BirthYear { get; set; }
        [Range(1, int.MaxValue)]
        public int IssueYear { get; set; }
        [Range(1, int.MaxValue)]
        public int ExpirationYear { get; set; }
        [Required]
        public string Height { get; set; }
        [Required]
        public string HairColor { get; set; }
        [Required]
        public string EyeColor { get; set; }
        [Required]
        public string PassportId { get; set; }
        public int CountryId { get; set; }

        public void AddCredentials(string inputLine)
        {
            var inputSections = inputLine.Split(' ');
            
            foreach(var inputSection in inputSections)
            {
                var keyValuePair = inputSection.Split(':');
                var key = keyValuePair[0];
                var value = keyValuePair[1];

                switch(key) 
                {
                    case "byr":
                        BirthYear = int.Parse(value);
                        break;
                    case "iyr":
                        IssueYear = int.Parse(value);
                        break;
                    case "eyr":
                        ExpirationYear = int.Parse(value);
                        break;
                    case "hgt":
                        Height = value;
                        break;
                    case "hcl":
                        HairColor = value;
                        break;
                    case "ecl":
                        EyeColor = value;
                        break;
                    case "pid":
                        PassportId = (value);
                        break;
                    case "cid":
                        CountryId = int.Parse(value);
                        break;
                }
            }
        }

    }
}
