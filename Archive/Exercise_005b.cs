using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<Ticket> _tickets;
        private static Flight _flight;

        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadLines("../Input/5.txt");
            _tickets = new List<Ticket>();

            BuildTickets(lines);
            FillFlight();
            //ListSeats();
            Console.WriteLine(FindMissingSeat());
                        
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

        private static int DrillDown(string input, char lowerHalf, char upperHalf)
        {
            var length = input.Length;
            var lowerLimit = 1.0;
            var upperLimit = Math.Pow(2, length);
            var middle = Math.Floor((lowerLimit + upperLimit) / 2);
            
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
            }

            var lastDirection = input[length - 1];
            return lastDirection == lowerHalf ? (int) lowerLimit : (int) upperLimit - 1;
        }

        private static void FillFlight()
        {
            _flight = new Flight(_tickets);
        }

        // private static void ListSeats()
        // {
        //     foreach(var ticket in _flight.Tickets)
        //     {
        //         Console.WriteLine($"Row: {ticket.Row}, Column: {ticket.Column}");
        //     }
        // }

        private static int FindMissingSeat()
        {
            var numberOfRows = _flight.LastRow - _flight.FirstRow + 1;
            var numberOfColumns = _flight.LastColumn - _flight.FirstColumn + 1;
            
            for (int r = _flight.FirstRow; r <= _flight.LastRow; r ++)
            {
                for (int c = _flight.FirstColumn; c <= _flight.LastColumn; c ++)
                {
                    //Console.WriteLine($"Looking for row {r}, column {c}");

                    if (!_flight.Tickets.Any(t => t.Row == r && t.Column == c))
                    {
                        Ticket missingSeat = new Ticket(r, c);
                        return missingSeat.GetId();
                    }
                }
            }
            return -1;
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

    class Flight
    {
        public int FirstRow { get; private set; }
        public int LastRow { get; private set; }
        public int FirstColumn { get; private set; }
        public int LastColumn { get; private set; }

        public List<Ticket> Tickets { get; private set; }

        public Flight(List<Ticket> tickets)
        {
            FirstRow = int.MaxValue;
            LastRow = 0;
            FirstColumn = int.MaxValue;
            LastColumn = 0;
            Tickets = tickets;
            
            foreach(var ticket in Tickets)
            {
                if (ticket.Row < FirstRow)
                {
                    FirstRow = ticket.Row;
                }
                if (ticket.Row > LastRow)
                {
                    LastRow = ticket.Row;
                }
                if (ticket.Column < FirstColumn)
                {
                    FirstColumn = ticket.Column;
                }
                if (ticket.Column > LastColumn)
                {
                    LastColumn = ticket.Column;
                }
            }
            //Console.WriteLine($"FirstRow: {FirstRow}, LastRow: {LastRow}, FirstColumn: {FirstColumn}, LastColumn: {LastColumn}");
        }
    }
}
