using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<Bag> _bags;
        
        static void Main(string[] args)
        {
            //var bagLines = System.IO.File.ReadLines("../Input/7_practice.txt");
            //var bagLines = System.IO.File.ReadLines("../Input/7b_practice.txt");
            var bagLines = System.IO.File.ReadLines("../Input/7.txt");
            
            _bags = new List<Bag>();
            
            BuildBags(bagLines);
            Console.WriteLine(CountBagsInside("shiny gold"));
                                    
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildBags(IEnumerable<string> bagLines)
        {
            foreach(var bagLine in bagLines)
            {
                var bag = new Bag(bagLine);
                _bags.Add(bag);
            }
        }

        private static int CountBagsInside(string color)
        {
            //Console.WriteLine($"Counting bags inside of {color}.");

            var bag = _bags.First(x => x.Color == color);
            if (bag.Bags.Count == 0)
            {
                return 0;
            }

            var count = 0;
            foreach(var bagInside in bag.Bags)
            {
                var number = bagInside.Number;
                count += number;
                count += (number * CountBagsInside(bagInside.Color));
            }

            return count;
        }

    }

    class Bag
    {
        public int Number { get; set; }
        public string Color { get; set; }
        public List<Bag> Bags { get; set; }

        public Bag(string input)
        {
            Number = 1;

            var inputParts = input.Split(" bags contain ");
            Color = inputParts[0];
            //Console.WriteLine(Color);

            var contents = inputParts[1];
            var contentParts = contents.Split(", ");
            Bags = new List<Bag>();

            foreach(var content in contentParts)
            {
                var bagParts = content.Split(' ');

                if ($"{bagParts[0]} {bagParts[1]}" == "no other")
                {
                    break;
                }
                else 
                {
                    var bagCount = int.Parse(bagParts[0]);
                    var bagColor = $"{bagParts[1]} {bagParts[2]}";
                    //Console.WriteLine($"Adding {bagCount} {bagColor} bag(s) to {Color}.");
                    var bag = new Bag(bagColor, bagCount);
                    Bags.Add(bag);
                }
            }
            
        }

        public Bag(string color, int count)
        {
            Number = count;
            Color = color;
            Bags = new List<Bag>();
        }
    }

}
