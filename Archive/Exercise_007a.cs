using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<Bag> _bags;
        private static HashSet<string> _uniqueContainers;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/7.txt");
            _bags = new List<Bag>();
            _uniqueContainers = new HashSet<string>();

            BuildBagRules(lines);
            CountContainingBags(new HashSet<string>{ "shiny gold" });
            Console.WriteLine(_uniqueContainers.Count);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildBagRules(IEnumerable<string> lines)
        {
            foreach(var line in lines)
            {
                var bag = new Bag(line);
                _bags.Add(bag);
            }
        }

        private static void CountContainingBags(HashSet<string> colors)
        {
            HashSet<string> containersForTheseColors = new HashSet<string>();
            
            foreach(var color in colors)
            {
                foreach(var bag in _bags)
                {
                    if (bag.Contents.Contains(color))
                    {
                        _uniqueContainers.Add(bag.Color);
                        containersForTheseColors.Add(bag.Color);
                    }
                }
            }

            if (containersForTheseColors.Count > 0)
            {
                CountContainingBags(containersForTheseColors);
            }
            else
            {
                return;
            }
        }
    }

    class Bag
    {
        public string Color { get; private set; }
        public List<string> Contents { get; set; }
        
        public Bag(string rules)
        {
            var ruleParts = rules.Split(" bags contain ");
            Color = ruleParts[0];
            Contents = new List<string>();

            var contents = ruleParts[1].TrimEnd('.');
            if (contents.StartsWith("no"))
            {
                return;
            }

            var individualContents = contents.Split(", ");
            for (int i = 0; i < individualContents.Length; i ++)
            {
                var description = individualContents[i].Split(' ');
                Contents.Add($"{description[1]} {description[2]}");
            }
        }
    }
}
