using System;

namespace Code
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("../Input/1.txt");

            Console.WriteLine(GetProduct(lines));
            
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static string GetProduct(string[] lines)
        {
            var length = lines.Length;

            for (int i = 0; i < length; i ++)
            {
                var digit1 = int.Parse(lines[i]);

                for (int j = i + 1; j < length; j++)
                {
                    var digit2 = int.Parse(lines[j]);

                    if ((digit1 + digit2) == 2020)
                    {
                        var product = digit1 * digit2;
                        return $"{digit1} plus {digit2} total 2020, and their product is {product}.";
                    }
                }
            }

            return "No matching pair found.";
        }
    }
}
