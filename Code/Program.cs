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

    class Passport : IValidatableObject
    {
        [Range(1920, 2002)]
        public int BirthYear { get; set; }
        [Range(2010, 2020)]
        public int IssueYear { get; set; }
        [Range(2020, 2030)]
        public int ExpirationYear { get; set; }
        [Required, RegularExpression(@"^[0-9]{2,3}(cm|in)$")]
        public string Height { get; set; }
        [Required, RegularExpression(@"^\#[0-9,a-f]{6}$")]
        public string HairColor { get; set; }
        [Required, RegularExpression(@"^(amb|blu|brn|gry|grn|hzl|oth){1}$")]
        public string EyeColor { get; set; }
        [Required, RegularExpression(@"^[0-9]{9}$")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            var heightSplit = 0;
            for (int i = 0; i < Height.Length; i ++)
            {
                if (!int.TryParse(Height[i].ToString(), out var number))
                {
                    heightSplit = i;
                    break;
                }
            }

            var heightValueString = Height.Substring(0, heightSplit);
            //Console.WriteLine($"Grabbed {heightValueString} out of {Height}.");
            var heightValue = int.Parse(heightValueString);
            var heightUnits = Height.Substring(heightSplit);

            if (heightUnits != "cm" && heightUnits != "in")
            {
                var result = new ValidationResult("Height must be in cm or in.");
                results.Add(result);
            }
            else if (heightUnits == "cm" && (heightValue < 150 || heightValue > 193))
            {
                var result = new ValidationResult("Height must be between 150 and 193 cm.");
                results.Add(result);
            }
            else if (heightUnits == "in" && (heightValue < 59 || heightValue > 76))
            {
                var result = new ValidationResult("Height must be between 59 and 76 in.");
                results.Add(result);
            }

            return results;
        }

    }
}
