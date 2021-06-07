using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<Bag> _bags;
        private static HashSet<string> _possibleBags;
        
        static void Main(string[] args)
        {
            //var bagLines = System.IO.File.ReadLines("../Input/7_practice.txt");
            var bagLines = System.IO.File.ReadLines("../Input/7.txt");
            _bags = new List<Bag>();
            _possibleBags = new HashSet<string>();
            
            BuildBags(bagLines);
            CountBagsContainingColor("shiny gold");
            Console.WriteLine(_possibleBags.Count);
                                    
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

        private static void CountBagsContainingColor(string color)
        {
            foreach(var bag in _bags)
            {
                //Console.WriteLine($"Checking for {color} bags inside of {bag.Color}.");
            
                if (bag.Bags.Any(x => x.Color == color))
                {
                    _possibleBags.Add(bag.Color);
                    //Console.WriteLine($"A {color} bag was found.");
                    CountBagsContainingColor(bag.Color);//, count);
                }
            }

        }

    }

    class Bag
    {
        public string Color { get; set; }

        public List<Bag> Bags { get; set; }

        public bool Inspected { get; set; }

        public Bag(string input)
        {
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
                    var bagColor = $"{bagParts[1]} {bagParts[2]}";
                    //Console.WriteLine(bagColor);
                    var bag = new Bag(bagColor, false);
                    Bags.Add(bag);
                }
            }
            
        }

        public Bag(string color, bool inspected)
        {
            Color = color;
            Bags = new List<Bag>();
            Inspected = inspected;
        }
    }

}
