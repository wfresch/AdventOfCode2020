using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<int> _adapters;
        private static int _maxAdapter;

        static void Main(string[] args)
        {
            _adapters = new List<int>();
            var lines = System.IO.File.ReadLines("../Input/10.txt");
            BuildAdapterList(lines);
            Console.WriteLine(FindVoltageJumps());
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static void BuildAdapterList(IEnumerable<string> lines)
        {
            var max = 0;

            foreach(var line in lines)
            {
                var currentValue = int.Parse(line);
                if (currentValue > max)
                {
                    max = currentValue;
                }

                _adapters.Add(currentValue);
            }

            _maxAdapter = max;
        }

        static int FindVoltageJumps()
        {
            var singleVoltageJumps = 0;
            var tripleVoltageJumps = 0;
            var currentVoltage = 0;

            while (currentVoltage < _maxAdapter)
            {
                var matchFound = false;

                for (int jump = 1; jump <= 3 && !matchFound; jump ++)
                {
                    var desiredVoltage = currentVoltage + jump;
                    //Console.WriteLine($"Looking for a voltage of {desiredVoltage}");
                    
                    if (_adapters.Contains(desiredVoltage))
                    {
                        singleVoltageJumps += (jump == 1) ? 1 : 0;
                        tripleVoltageJumps += (jump == 3) ? 1 : 0;
                        
                        currentVoltage = desiredVoltage;
                        //Console.WriteLine($"Match found.  Current voltage is now {currentVoltage}.");
                        matchFound = true;
                    }
                    else
                    {
                        //Console.WriteLine($"Voltage of {desiredVoltage} not found.");
                    }
                }
            }

            tripleVoltageJumps++;
            return singleVoltageJumps * tripleVoltageJumps;
        }
    }
}
