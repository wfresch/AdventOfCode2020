using System;
using System.Collections.Generic;

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
                if (IsValid(passport))
                {
                    validCount++;
                }
            }

            return validCount;
        }

        private static bool IsValid(Passport passport)
        {
            return (passport.BirthYear.HasValue
                && passport.IssueYear.HasValue
                && passport.ExpirationYear.HasValue
                && !string.IsNullOrEmpty(passport.Height)
                && !string.IsNullOrEmpty(passport.HairColor)
                && !string.IsNullOrEmpty(passport.EyeColor)
                && !string.IsNullOrEmpty(passport.PassportId)
            );
        }
    }

    class Passport
    {
        public int? BirthYear { get; private set; }
        public int? IssueYear { get; private set; }
        public int? ExpirationYear { get; private set; }
        public string Height { get; private set; }
        public string HairColor { get; private set; }
        public string EyeColor { get; private set; }
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

        public void LogProperties()
        {
            Console.WriteLine($"BirthYear: {BirthYear}");
            Console.WriteLine($"IssueYear: {IssueYear}");
            Console.WriteLine($"ExpirationYear: {ExpirationYear}");
            Console.WriteLine($"Height: {Height}");
            Console.WriteLine($"HairColor: {HairColor}");
            Console.WriteLine($"EyeColor: {EyeColor}");
            Console.WriteLine($"PassportId: {PassportId}");
            Console.WriteLine($"CountryId: {CountryId}");
        }
    }
}
