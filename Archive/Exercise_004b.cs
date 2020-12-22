using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Code
{
    class Program
    {
        private static List<Passport> _passports;
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/4.txt");
            _passports = new List<Passport>();

            BuildPassports(lines);
            Console.WriteLine(GetValidCount());
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildPassports(IEnumerable<string> lines)
        {
            var propertyInputs = "";

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    propertyInputs += $"{line} ";
                }
                else
                {
                    AddPassport(propertyInputs);
                    propertyInputs = "";                    
                }
            }

            if (!string.IsNullOrEmpty(propertyInputs))
            {
                AddPassport(propertyInputs);
            }
        }

        private static void AddPassport(string propertyInputs)
        {
            propertyInputs = propertyInputs.Trim();
            var passport = new Passport(propertyInputs);
            _passports.Add(passport);
        }

        private static int GetValidCount()
        {
            var validCount = 0;

            foreach(var passport in _passports)
            {
                var validationContext = new ValidationContext(passport, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                                
                if (Validator.TryValidateObject(passport, validationContext, validationResults, true)) 
                {
                    validCount++;
                }
            }

            return validCount;
        }
    }

    class Passport : IValidatableObject
    {
        [Required, Range(1920, 2002)]
        public int? BirthYear { get; private set; }
        [Required, Range(2010, 2020)]
        public int? IssueYear { get; private set; }
        [Required, Range(2020, 2030)]
        public int? ExpirationYear { get; private set; }
        [Required, RegularExpression(@"(^\d{2}in$)|(^\d{3}cm$)")]
        public string Height { get; private set; }
        [Required, RegularExpression(@"(^#[a-z0-9]{6}$)")]
        public string HairColor { get; private set; }
        [Required]
        public string EyeColor { get; private set; }
        [Required, RegularExpression(@"(^\d{9}$)")]
        public string PassportId { get; private set; }
        public int? CountryId { get; private set; }
        
        public Passport(string propertyInputs)
        {
            var individualProperties = propertyInputs.Split(' ');
            foreach (var individualProperty in individualProperties)
            {
                AssignProperty(individualProperty);
            }
        }

        public void AssignProperty(string keyValuePair)
        {
            var propertyParts = keyValuePair.Split(':');
            var propertyName = propertyParts[0];
            var propertyValue = propertyParts[1];

            switch (propertyName)
            {
                case "byr":
                    this.BirthYear = int.Parse(propertyValue);
                    break;
                case "iyr":
                    this.IssueYear = int.Parse(propertyValue);
                    break;
                case "eyr":
                    this.ExpirationYear = int.Parse(propertyValue);
                    break;
                case "hgt":
                    this.Height = propertyValue;
                    break;
                case "hcl":
                    this.HairColor = propertyValue;
                    break;
                case "ecl":
                    this.EyeColor = propertyValue;
                    break;
                case "pid":
                    this.PassportId = propertyValue;
                    break;
                case "cid":
                    this.CountryId = int.Parse(propertyValue);
                    break;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            //HEIGHT
            var units = this.Height.Substring(this.Height.Length - 2);
            var numericPortionOfHeight = int.Parse(this.Height.Substring(0, this.Height.Length - 2));
            if (units != "cm" && units != "in")
            {
                results.Add(new ValidationResult("Height must be in inches(in) or centimeters(cm)"));
            }
            else if (units == "cm" && (numericPortionOfHeight < 150 || numericPortionOfHeight > 193))
            {
                results.Add(new ValidationResult("Height must be between 150 and 193cm"));
            }
            else if (units == "in" && (numericPortionOfHeight < 59 || numericPortionOfHeight > 76))
            {
                results.Add(new ValidationResult("Height must be between 59 and 76cm"));
            }

            //EYE COLOR
            if (this.EyeColor != "amb" 
                && this.EyeColor != "blu"
                && this.EyeColor != "brn"
                && this.EyeColor != "gry"
                && this.EyeColor != "grn"
                && this.EyeColor != "hzl"
                && this.EyeColor != "oth")
            {
                results.Add(new ValidationResult("Eye Color must be: amb, blu, brn, gry, grn, hzl, or oth"));
            }

            return results;
        }
    }
}
