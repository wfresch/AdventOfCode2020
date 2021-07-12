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
            //Console.WriteLine("RunSimulations()");
            var changes = -1;
            var guardrail = 0;

            while (changes != 0 && guardrail <= 1000)
            {
                //_flight.PrintSeats();
                
                changes = _flight.RunSimulationRound();
                // Console.WriteLine($"changes is now: {changes}.");
                // Console.WriteLine($"guardrail is currently: {guardrail}.");

                if (changes != 0)
                {
                    _flight.UpdateFlight();
                }

                guardrail++;
                
                // if (guardrail == 1)
                // {
                //     _flight.PrintSeats();
                // }
                //Console.WriteLine($"guardrail is now: {guardrail}.");
            }
        }

    }

    class Flight
    {
        public Seat[,] Seats { get; set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public Flight(IEnumerable<string> inputs) {
            //Console.WriteLine($"Creating a new Flight.");
            Rows = inputs.Count();

            if (Rows == 0)
            {
                return;
            }

            Columns = inputs.ElementAt(0).Length;

            //Console.WriteLine($"Creating new Seats array w/ {Rows} rows and {Columns} columns.");
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
            //Console.WriteLine("RunSimulationRound()");
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
                    //Console.WriteLine($"Status for row {i}, column {j} was {Seats[i,j].Status.ToString()}, and will now be {Seats[i,j].FutureStatus}.");
                    Seats[i, j].Status = Seats[i, j].FutureStatus;
                }
            }
        }

        public SeatStatus SeatOutcome(int row, int column)
        {
            //Console.WriteLine($"Calculating SeatOutcome for row {row}, column {column}.");

            var seat = Seats[row, column];
            var status = seat.Status;

            if (seat.Status == SeatStatus.Empty && AdjacentSeatsOccupied(row, column) == 0)
            {
                //Console.WriteLine("SeatOutcome scenario 1");
                seat.FutureStatus = SeatStatus.Occupied;
                return SeatStatus.Occupied;
            }

            if (seat.Status == SeatStatus.Occupied && AdjacentSeatsOccupied(row, column) >= 4)
            {
                //Console.WriteLine("SeatOutcome scenario 2");
                seat.FutureStatus = SeatStatus.Empty;
                return SeatStatus.Empty;
            }

            //Console.WriteLine($"SeatOutcome scenario 3 for row {row} and column {column}; status is {status.ToString()}.");
            return status;
        }

        public int AdjacentSeatsOccupied(int row, int column)
        {
            //Console.WriteLine($"Checking AdjacentSeatsOccupied for row {row}, column {column}.");

            var adjacentSeatsOccupied = 0;

            var test1 = Math.Max(row - 1, 0);
            var test2 = Math.Min(row + 1, Rows - 1);
            var test3 = Math.Max(column - 1, 0);
            var test4 = Math.Min(column + 1, Columns - 1);

            //Console.WriteLine($"{test1}, {test2}, {test3}, {test4}");
            
            for (int i = Math.Max(row - 1, 0); i <= Math.Min(row + 1, Rows - 1); i ++)
            {
                //Console.WriteLine($"AdjacentSeatsOccupied for row {row}.");

                for (int j = Math.Max(column - 1, 0); j <= Math.Min(column + 1, Columns - 1); j ++)
                {
                    if (row == i && column == j)
                    {
                        //Console.WriteLine($"AdjacentSeatsOccupied check for row {row}, column {column}, but i is {i} and j is {j}.");
                        continue;
                    }

                    //var seat = Seats[row, column];
                    var seat = Seats[i, j];

                    adjacentSeatsOccupied += (seat.Status == SeatStatus.Occupied) ? 1 : 0;
                }
            }

            //Console.WriteLine($"adjacentSeatsOccupied for row {row}, column {column} is: {adjacentSeatsOccupied}.");
            return adjacentSeatsOccupied;
        }

        public int CountOccupiedSeats()
        {
            var occupiedSeats = 0;

            for (int i = 0; i < Rows; i ++)
            {
                for (int j = 0; j < Columns; j ++)
                {
                    // if (Seats[i,j].Status == SeatStatus.Occupied)
                    // {
                    //     Console.WriteLine($"Row {i}, Column {j} is Occupied");
                    // }

                    occupiedSeats += (Seats[i, j].Status == SeatStatus.Occupied) ? 1 : 0;
                    //Console.WriteLine($"occupiedSeats is currently: {occupiedSeats}.");
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
            //Console.WriteLine($"Creating a new Seat w/ status of {status}");

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
            //Console.WriteLine($"Printing {Status.ToString()}.");

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
