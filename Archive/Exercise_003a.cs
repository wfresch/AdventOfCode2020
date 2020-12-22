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

            Console.WriteLine(GetTreeCount(lines));
            
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static int GetTreeCount(string[] lines)
        {
            var trees = 0;
            var xCoordinate = 0;
            
            for (int i = 0; i < lines.Length - 1; i ++)
            {
                xCoordinate = GetNextXCoordinate(xCoordinate);
                if (lines[i + 1][xCoordinate].Equals('#'))
                {
                    trees++;
                }
            }

            return trees;
        }

        private static int GetNextXCoordinate(int currentXCoordinate)
        {
            var nextXCoordinate = currentXCoordinate + 3;

            if (nextXCoordinate > (_lineLength - 1))
            {
                nextXCoordinate = (nextXCoordinate - _lineLength);
            }
            return nextXCoordinate;
        }
    }

}
