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
            var flightRows = System.IO.File.ReadLines("../Input/11.txt");
                        
            _flight = new Flight(flightRows);
                                    
            RunSimulations();
            Console.WriteLine(_flight.CountOccupiedSeats());
                                                
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        static void RunSimulations()
        {
            var changes = -1;
            var guardrail = 0;

            while (changes != 0 && guardrail <= 1000)
            {
                changes = _flight.RunSimulationRound();

                if (changes != 0)
                {
                    _flight.UpdateFlight();
                }

                guardrail++;
            }
        }

    }

    class Flight
    {
        public Seat[,] Seats { get; set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public Flight(IEnumerable<string> inputs) {
            Rows = inputs.Count();

            if (Rows == 0)
            {
                return;
            }

            Columns = inputs.ElementAt(0).Length;

            Seats = new Seat[Rows, Columns];
            var currentRow = 0;
            var currentColumn = 0;

            foreach(var input in inputs)
            {
                currentColumn = 0;

                foreach(var seat in input)
                {
                    Seats[currentRow, currentColumn] = new Seat(seat);
                    currentColumn++;
                }

                currentRow++;
            }
        }

        public int RunSimulationRound()
        {
            var changes = 0;

            for (int i = 0; i < Rows; i ++) 
            {
                for (int j = 0; j < Columns; j ++)
                {
                    var currentStatus = Seats[i, j].Status;
                    var simulationStatus = SeatOutcome(i, j);

                    if (currentStatus != simulationStatus)
                    {
                        changes++;
                    }
                }
            }

            return changes;
        }

        public void UpdateFlight()
        {
            for (int i = 0; i < Rows; i ++) 
            {
                for (int j = 0; j < Columns; j ++)
                {
                    Seats[i, j].Status = Seats[i, j].FutureStatus;
                }
            }
        }

        public SeatStatus SeatOutcome(int row, int column)
        {
            var seat = Seats[row, column];
            var status = seat.Status;

            if (seat.Status == SeatStatus.Empty && VisibleSeatsOccupied(row, column) == 0)
            {
                seat.FutureStatus = SeatStatus.Occupied;
                return SeatStatus.Occupied;
            }

            if (seat.Status == SeatStatus.Occupied && VisibleSeatsOccupied(row, column) >= 5)
            {
                seat.FutureStatus = SeatStatus.Empty;
                return SeatStatus.Empty;
            }

            return status;
        }

        public int AdjacentSeatsOccupied(int row, int column)
        {
            var adjacentSeatsOccupied = 0;

            for (int i = Math.Max(row - 1, 0); i <= Math.Min(row + 1, Rows - 1); i ++)
            {
                for (int j = Math.Max(column - 1, 0); j <= Math.Min(column + 1, Columns - 1); j ++)
                {
                    if (row == i && column == j)
                    {
                        continue;
                    }

                    var seat = Seats[i, j];

                    adjacentSeatsOccupied += (seat.Status == SeatStatus.Occupied) ? 1 : 0;
                }
            }

            return adjacentSeatsOccupied;
        }

        public int VisibleSeatsOccupied(int row, int column)
        {
            return LookSideways(row, column) + LookVertically(row, column) + LookDiagonally(row, column);
        }

        public int LookSideways(int row, int column)
        {
            var horizontalOccupiedSeats = 0;

            for (int j = Math.Max(column - 1, 0); j >= 0; j --)
            {
                if (j == column)
                {
                    continue;
                }
                
                var seat = Seats[row, j];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    horizontalOccupiedSeats++;
                    break;
                }
            }

            for (int j = Math.Min(column + 1, Columns - 1); j < Columns; j ++)
            {
                if (j == column)
                {
                    continue;
                }
                
                var seat = Seats[row, j];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    horizontalOccupiedSeats++;
                    break;
                }
            }

            return horizontalOccupiedSeats;
        }

        public int LookVertically(int row, int column)
        {
            var verticalOccupiedSeats = 0;

            for (int i = Math.Max(row - 1, 0); i >= 0; i --)
            {
                if (i == row)
                {
                    continue;
                }
                
                var seat = Seats[i, column];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    verticalOccupiedSeats++;
                    break;
                }
            }

            for (int i = Math.Min(row + 1, Rows - 1); i < Rows; i ++)
            {
                if (i == row)
                {
                    continue;
                }
                
                var seat = Seats[i, column];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    verticalOccupiedSeats++;
                    break;
                }
            }

            return verticalOccupiedSeats;
        }

        public int LookDiagonally(int row, int column)
        {
            var diagonalOccupiedSeats = 0;

            for (int i = Math.Max(row - 1, 0); i >= 0; i --)
            {
                if (i == row)
                {
                    continue;
                }
                
                var rowDelta = Math.Abs(row - i);
                var leftColumn = column - rowDelta;

                if (leftColumn < 0)
                {
                    continue;
                }

                var seat = Seats[i, leftColumn];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    diagonalOccupiedSeats++;
                    break;
                }
            }

            for (int i = Math.Max(row - 1, 0); i >= 0; i --)
            {
                if (i == row)
                {
                    continue;
                }
                
                var rowDelta = Math.Abs(row - i);
                var rightColumn = column + rowDelta;

                if (rightColumn > (Columns - 1))
                {
                    continue;
                }

                var seat = Seats[i, rightColumn];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    diagonalOccupiedSeats++;
                    break;
                }
            }

            for (int i = Math.Min(row + 1, Rows - 1); i < Rows; i ++)
            {
                if (i == row)
                {
                    continue;
                }
                
                var rowDelta = Math.Abs(row - i);
                var leftColumn = column - rowDelta;

                if (leftColumn < 0)
                {
                    continue;
                }

                var seat = Seats[i, leftColumn];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    diagonalOccupiedSeats++;
                    break;
                }
            }

            for (int i = Math.Min(row + 1, Rows - 1); i < Rows; i ++)
            {
                if (i == row)
                {
                    continue;
                }
                
                var rowDelta = Math.Abs(row - i);
                var rightColumn = column + rowDelta;

                if (rightColumn > (Columns - 1))
                {
                    continue;
                }

                var seat = Seats[i, rightColumn];

                if (seat.Status == SeatStatus.Empty)
                {
                    break;
                }

                if (seat.Status == SeatStatus.Occupied)
                {
                    diagonalOccupiedSeats++;
                    break;
                }
            }

            return diagonalOccupiedSeats;
        }
        
        public int CountOccupiedSeats()
        {
            var occupiedSeats = 0;

            for (int i = 0; i < Rows; i ++)
            {
                for (int j = 0; j < Columns; j ++)
                {
                    occupiedSeats += (Seats[i, j].Status == SeatStatus.Occupied) ? 1 : 0;
                }
            }

            return occupiedSeats;
        }

        public void PrintSeats()
        {
            for (int i = 0; i < Rows; i ++)
            {
                var seatLine = "";

                for (int j = 0; j < Columns; j ++)
                {
                    seatLine += Seats[i, j].Print();
                }
                Console.WriteLine(seatLine);
            }
        }
    }

    class Seat
    {
        public SeatStatus Status { get; set; }
        public SeatStatus FutureStatus { get; set; }

        public Seat(char status)
        {
            switch(status)
            {
                case 'L':
                    Status = SeatStatus.Empty;
                    FutureStatus = SeatStatus.Empty;
                    break;
                case '#':
                    Status = SeatStatus.Occupied;
                    FutureStatus = SeatStatus.Occupied;
                    break;
                case '.':
                default:
                    Status = SeatStatus.Floor;
                    FutureStatus = SeatStatus.Floor;
                    break;
            }
        }

        public char Print()
        {
            switch(Status)
            {
                case SeatStatus.Empty:
                    return 'L';
                    break;
                case SeatStatus.Occupied:
                    return '#';
                    break;
                case SeatStatus.Floor:
                default:
                    return '.';
                    break;
            }
        }
    }

    enum SeatStatus
    {
        Empty = 0,
        Occupied = 1,
        Floor = 2
    }
        
}
