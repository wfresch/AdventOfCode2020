using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static Flight _flight;

        static void Main(string[] args)
        {
            //_adapters = new List<int>();
            var lines = System.IO.File.ReadAllLines("../Input/1.txt");
            _flight = new Flight(lines.Count());
            FillFlight(lines);

            Console.WriteLine(ApplyRulesMultipleTimes());
                        
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static void FillFlight(string[] lines)
        {
            for (int i = 0; i < lines.Length; i ++)
            {
                for (int j = 0; j < lines[i].Length; j ++)
                {
                    _flight.Seats[i,j] = new Seat(lines[i][j]);
                }
            }
        }

        static int ApplyRulesMultipleTimes()
        {
            var numberOfSwaps = -1;
            // var previousSwaps = 0;
            // var newSwaps = 0;
            var counter = 1;

            do
            {
                //previousSwaps = newSwaps;
                // newSwaps = ApplyRulesOnce();
                numberOfSwaps = ApplyRulesOnce();
                Console.WriteLine($"Number of swaps during iteration {counter}: {numberOfSwaps}.");
                counter++;
            }
            //while (previousSwaps != newSwaps);
            while (numberOfSwaps != 0);
            
            var occupiedSeats = _flight.GetOccupiedSeats();
            return occupiedSeats;
        }

        static int ApplyRulesOnce()
        {
            var boundary = _flight.RowCount;
            var seatsChanged = 0;

            for (int i = 0; i < boundary; i ++)
            {
                for (int j = 0; j < boundary; j ++)
                {
                    var seat = _flight.GetSeat(i, j);
                    if (seat.State == SeatState.Floor)
                    {
                        continue;
                    }

                    var occupiedAdjacentSeats = _flight.OccupiedAdjacentSeats(i, j);

                    if (seat.State == SeatState.Empty && occupiedAdjacentSeats == 0)
                    {
                        Console.WriteLine($"Seat at row {i}, column {j} has no occupied adjacent seats.  Time to sit.");
                        _flight.SetSeat(i, j, SeatState.Occupied);
                        seatsChanged++;
                    }
                    else if (seat.State == SeatState.Occupied && occupiedAdjacentSeats >= 4)
                    {
                        Console.WriteLine($"Seat at row {i}, column {j} has at least 4 occupied adjacent seats.  Time to vacate.");
                        _flight.SetSeat(i, j, SeatState.Empty);
                        seatsChanged++;
                    }
                    else
                    {
                        Console.WriteLine($"Seat at row {i}, column {j} is an else-case with {occupiedAdjacentSeats} occupied adjacent seats.");
                    }
                }
            }

            return seatsChanged;
        }

    }

    class Seat
    {
        public SeatState State { get; set; }
        public bool ToBeSwapped { get; set; }

        public Seat(char stateIdentifier)
        {
            switch (stateIdentifier)
            {
                case '.':
                    State = SeatState.Floor;
                    break;
                case 'L':
                    State = SeatState.Empty;
                    break;
                case '#':
                    State = SeatState.Occupied;
                    break;
            }
        }

        // public void Fill()
        // {
        //     State = SeatState.Occupied;
        // }

        // public void Vacate()
        // {
        //     State = SeatState.Empty;
        // }

    }

    enum SeatState
    {
        Floor = 0,
        Empty = 1,
        Occupied = 2
    }

    class Flight
    {
        public Seat[,] Seats { get; set; }

        public int RowCount { get; private set; }
        public int ColumnsCount { get; private set; }
        
        public Flight(int rows)//, int columns)
        {
            RowCount = rows;
            ColumnsCount = rows;
            Seats = new Seat[rows,rows];
        }

        public int OccupiedAdjacentSeats(int row, int column)
        {
            var occupiedCount = 0;
            var rowAboveExists = row > 0;
            var rowBelowExists = row < (RowCount - 1);
            var columnLeftExists = column > 0;
            var columnRightExists = column < (ColumnsCount - 1);

            occupiedCount += (rowAboveExists && columnLeftExists && (Seats[row - 1, column - 1].State == SeatState.Occupied)) ? 1 : 0;
            occupiedCount += (rowAboveExists && (Seats[row - 1, column].State == SeatState.Occupied)) ? 1 : 0;
            occupiedCount += (rowAboveExists && columnRightExists && (Seats[row - 1, column + 1].State == SeatState.Occupied)) ? 1 : 0;

            occupiedCount += (columnLeftExists && (Seats[row, column - 1].State == SeatState.Occupied)) ? 1 : 0;
            occupiedCount += (columnRightExists && (Seats[row, column + 1].State == SeatState.Occupied)) ? 1 : 0;

            occupiedCount += (rowBelowExists && columnLeftExists && (Seats[row + 1, column - 1].State == SeatState.Occupied)) ? 1 : 0;
            occupiedCount += (rowBelowExists && (Seats[row + 1, column].State == SeatState.Occupied)) ? 1 : 0;
            occupiedCount += (rowBelowExists && columnRightExists && (Seats[row + 1, column + 1].State == SeatState.Occupied)) ? 1 : 0;

            return occupiedCount;
        }

        public Seat GetSeat(int row, int column)
        {
            return Seats[row, column];
        }

        public void SetSeat(int row, int column, SeatState state)
        {
            Seats[row,column].State = state;
        }

        public int GetOccupiedSeats()
        {
            var occupiedSeats = 0;

            for (int i = 0; i < RowCount; i ++)
            {
                for (int j = 0; j < ColumnsCount; j ++)
                {
                    if (Seats[i,j].State == SeatState.Occupied)
                    {
                        occupiedSeats++;
                    }
                }
            }

            return occupiedSeats;
        }
    }
}
