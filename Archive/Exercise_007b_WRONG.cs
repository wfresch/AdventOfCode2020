using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    //16803 is too high
    //11604 is incorrect

    class Program
    {
        private static List<Bag> _bags;
        private static int _bagCount;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/7b_practice.txt");
            _bags = new List<Bag>();
            _bagCount = 0;

            BuildBagRules(lines);
            //var bagCount = CountBagsInside("shiny gold");
            //Console.WriteLine(bagCount);
            var bagCount = CountBagsInside("shiny gold");
            //CountBagsInside(new List<string>{ "shiny gold" });
            Console.WriteLine(bagCount);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildBagRules(IEnumerable<string> lines)
        {
            foreach(var line in lines)
            {
                var bag = new Bag(line);
                //Console.WriteLine($"Adding a bag with {bag.Color} color.");
                Console.Write($"Adding a bag with {bag.Color} color.  ");
                foreach(var content in bag.Contents)
                {
                    if (content.Number == 0)
                    {
                        Console.Write("(no bags inside)");
                    }
                    else
                    {
                        Console.Write($"{content.Number} {content.Color} bag(s) inside, ");
                    }
                }
                Console.WriteLine();

                _bags.Add(bag);
            }
        }

        private static int CountBagsInside(string bagColor)
        {
            Console.WriteLine($"Looking for bag with {bagColor} color.");
            
            var bag = _bags.FirstOrDefault(x => x.Color == bagColor);
            
            if (bag.Contents.Count == 1 && bag.Contents.First().Number == 0)
            {
                return 1;
            }

            var totalBagsInside = 0;
            foreach(var content in bag.Contents)
            {
                Console.WriteLine($"There's {content.Number} {content.Color} bag(s) inside.");
                //return content.Number * CountBagsInside(content.Color);
                var bagsInside = content.Number * CountBagsInside(content.Color);
                totalBagsInside += bagsInside;
                //return bagsInside;
            }
            //_bagCount += totalBagsInside;
            Console.WriteLine($"The bags inside {bagColor} are {totalBagsInside}, and total bag count is now {_bagCount}.");
            return totalBagsInside;
        }
        
    }

    class Bag
    {
        public string Color { get; private set; }
        public List<DetailedContent> Contents { get; set; }
        
        public Bag(string rules)
        {
            var ruleParts = rules.Split(" bags contain ");
            Color = ruleParts[0];
            Contents = new List<DetailedContent>();

            var contents = ruleParts[1].TrimEnd('.');
            if (contents.StartsWith("no"))
            {
                Contents.Add(new DetailedContent(0));
                return;
            }

            var individualContents = contents.Split(", ");
            for (int i = 0; i < individualContents.Length; i ++)
            {
                var detailedContent = new DetailedContent(individualContents[i]);
                Contents.Add(detailedContent);
            }
        }
    }

    class DetailedContent
    {
        public int Number { get; private set; }
        
        public string Color { get; private set; }

        public DetailedContent(string description)
        {
            var descriptionParts = description.Split(' ');

            Number = int.Parse(descriptionParts[0]);
            Color = $"{descriptionParts[1]} {descriptionParts[2]}";
        }

        public DetailedContent(int number)
        {
            Number = number;
        }
    }
}
