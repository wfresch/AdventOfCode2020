using System;
using System.Collections.Generic;

namespace Code
{
    class Program
    {
        private static List<Ticket> _tickets;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/5.txt");
            _tickets = new List<Ticket>();

            BuildTickets(lines);
            Console.WriteLine(GetMaxId());
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildTickets(IEnumerable<string> lines)
        {
            foreach(var line in lines)
            {
                var rowDirections = line.Substring(0,7);
                var columnDirections = line.Substring(7);
                var row = DrillDown(rowDirections, 'F', 'B');
                var column = DrillDown(columnDirections, 'L', 'R');
                _tickets.Add(new Ticket(row, column));
            }
        }

        private static int GetMaxId()
        {
            var maxId = -1;

            foreach(var ticket in _tickets)
            {
                var id = ticket.GetId();

                if (id > maxId)
                {
                    maxId = id;
                }
            }

            return maxId;
        }

        private static int DrillDown(string input, char lowerHalf, char upperHalf)
        {
            //Console.WriteLine($"DrillDown({input}, {lowerHalf}, {upperHalf})");
            var length = input.Length;
            var lowerLimit = 1.0;
            var upperLimit = Math.Pow(2, length);
            var middle = Math.Floor((lowerLimit + upperLimit) / 2);
            //Console.WriteLine($"lowerLimit: {lowerLimit}, upperLimit: {upperLimit}, middle: {middle}");

            for (int i = 0; i < length - 1; i ++)
            {
                var direction = input[i];

                if (direction == lowerHalf)
                {
                    upperLimit = middle;
                }
                else if (direction == upperHalf)
                {
                    lowerLimit = middle;
                }
                middle = Math.Floor((lowerLimit + upperLimit) / 2);
                //Console.WriteLine($"lowerLimit: {lowerLimit}, upperLimit: {upperLimit}, middle: {middle}");
            }

            var lastDirection = input[length - 1];
            return lastDirection == lowerHalf ? (int) lowerLimit : (int) upperLimit - 1;
        }

    }

    class Ticket
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public Ticket(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int GetId()
        {
            //Console.WriteLine($"GetId for row {Row} and column {Column}");
            return (Row * 8) + Column;
        }
    }
}
