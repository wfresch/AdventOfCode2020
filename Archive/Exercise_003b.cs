using System;
using System.Collections.Generic;

namespace Code
{
    class Program
    {
        private static int _lineLength;
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("../Input/3.txt");
            _lineLength = lines[0].Length;

            long treeProduct = 1;
            treeProduct *= GetTreeCount(lines, 1, 1);
            treeProduct *= GetTreeCount(lines, 1, 3);
            treeProduct *= GetTreeCount(lines, 1, 5);
            treeProduct *= GetTreeCount(lines, 1, 7);
            treeProduct *= GetTreeCount(lines, 2, 1);
            Console.WriteLine(treeProduct);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static int GetTreeCount(string[] lines, int drop, int run)
        {
            var trees = 0;
            var xCoordinate = 0;
            var yCoordinate = 0;
            
            while (yCoordinate < lines.Length)
            {
                xCoordinate = GetNextXCoordinate(xCoordinate, run);
                yCoordinate += drop;

                if (yCoordinate >= lines.Length)
                {
                    break;
                }

                if (lines[yCoordinate][xCoordinate].Equals('#'))
                {
                    trees++;
                }
            }
            
            return trees;
        }

        private static int GetNextXCoordinate(int currentXCoordinate, int run)
        {
            var nextXCoordinate = currentXCoordinate + run;

            if (nextXCoordinate > (_lineLength - 1))
            {
                nextXCoordinate = (nextXCoordinate - _lineLength);
            }
            return nextXCoordinate;
        }
    }
}
