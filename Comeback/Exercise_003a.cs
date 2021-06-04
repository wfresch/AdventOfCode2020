using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        static void Main(string[] args)
        {
            var course = System.IO.File.ReadAllLines("../Input/3.txt");

            Console.WriteLine(TraverseCourse(course));
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static int TraverseCourse(string[] course)
        {
            var courseWidth = course[0].Length;
            var horizontalPosition = 0;
            var treeCount = 0;

            for (int i = 0; i < course.Length; i ++)
            {
                var currentSpot = course[i][horizontalPosition];
                //Console.WriteLine($"Current spot is [{i}][{horizontalPosition}]: {currentSpot}");
                if (currentSpot == '#')
                {
                    treeCount ++;
                }

                horizontalPosition += 3;

                if (horizontalPosition > courseWidth - 1)
                {
                    horizontalPosition -= courseWidth;
                }
            }

            return treeCount;
        }
    }
}
