using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<Seat> _seats;
        private static Seat[] _sortedSeats;
        
        static void Main(string[] args)
        {
            var seatLines = System.IO.File.ReadLines("../Input/5.txt");
            _seats = new List<Seat>();
            BuildSeats(seatLines);
            SortSeats();

            Console.WriteLine(FindMissingSeat());
                                    
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildSeats(IEnumerable<string> seatLines)
        {
            foreach(var seatLine in seatLines)
            {
                var seat = new Seat(seatLine);
                _seats.Add(seat);
            }
        }

        private static void SortSeats()
        {
            _sortedSeats = new Seat[_seats.Count()];
            var sortedSeatList = _seats.OrderBy(x => x.SeatId);
            var cursor = 0;

            foreach(var sortedSeat in sortedSeatList)
            {
                _sortedSeats[cursor] = sortedSeat;
                cursor++;
            }
        }

        private static int FindMissingSeat()
        {
            for(int i = 0; i < _sortedSeats.Length - 1; i ++)
            {
                if(_sortedSeats[i].SeatId + 1 != _sortedSeats[i + 1].SeatId)
                {
                    //Console.WriteLine($"Current seat is id {_sortedSeats[i].SeatId}, and next seat is id {_sortedSeats[i + 1].SeatId}.");

                    return _sortedSeats[i].SeatId + 1;
                }
            }
            return -1;
        }
    }

    class Seat
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int SeatId { get; private set; }

        public Seat(string seatCode)
        {
            var rowCode = seatCode.Substring(0, 7);
            var rowBinary = ConvertCodeToBinary(rowCode, 'B', 'F');
            Row = Convert.ToInt32(rowBinary, 2);

            var columnCode = seatCode.Substring(7);
            var columnBinary = ConvertCodeToBinary(columnCode, 'R', 'L');
            Column = Convert.ToInt32(columnBinary, 2);

            SeatId = (Row * 8) + Column;
        }

        private string ConvertCodeToBinary(string input, char upper, char lower)
        {
            string output = "";

            foreach(char inputCharacter in input)
            {
                output += (inputCharacter == upper) ? "1" : "0";
            }

            return output;
        }
    }

}
