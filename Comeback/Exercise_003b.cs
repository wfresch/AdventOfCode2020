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
            long result = 1;

            result *= TraverseCourse(course, 1, 1);
            result *= TraverseCourse(course, 3, 1);
            result *= TraverseCourse(course, 5, 1);
            result *= TraverseCourse(course, 7, 1);
            result *= TraverseCourse(course, 1, 2);
            Console.WriteLine(result);
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static int TraverseCourse(string[] course, int right, int down)
        {
            var courseWidth = course[0].Length;
            var horizontalPosition = 0;
            var treeCount = 0;

            for (int i = 0; i < course.Length; i += down)
            {
                var currentSpot = course[i][horizontalPosition];
                //Console.WriteLine($"Current spot is [{i}][{horizontalPosition}]: {currentSpot}");
                if (currentSpot == '#')
                {
                    treeCount ++;
                }

                horizontalPosition += right;

                if (horizontalPosition > courseWidth - 1)
                {
                    horizontalPosition -= courseWidth;
                }
            }

            return treeCount;
        }
    }
}
