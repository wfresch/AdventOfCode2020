using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static List<Seat> _seats;

        static void Main(string[] args)
        {
            var seatLines = System.IO.File.ReadLines("../Input/5.txt");
            _seats = new List<Seat>();
            BuildSeats(seatLines);
            Console.WriteLine(GetMax());
                                    
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

        private static int GetMax()
        {
            var maxId = -1;
            
            foreach(var seat in _seats)
            {
                if (seat.SeatId > maxId)
                {
                    maxId = seat.SeatId;
                }
            }

            return maxId;
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
