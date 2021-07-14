using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static Ship _ship;
        
        static void Main(string[] args)
        {
            _ship = new Ship();
            var instructionRows = System.IO.File.ReadLines("../Input/12.txt");
            var instructions = new List<Instruction>();
            
            foreach(var row in instructionRows)
            {
                var instruction = new Instruction(row);
                instructions.Add(instruction);
            }

            _ship.ProcessInstructions(instructions);
                                    
            Console.WriteLine(_ship.Location.GetManhattanDistance());
                                                
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

    }

    class Instruction
    {
        public InstructionType InstructionType { get; private set; }
        public Direction? Direction { get; private set; }
        public Rotation? Rotation { get; private set; }

        public int Units { get; private set; }

        public Instruction(string instructionInput)
        {
            var instructionType = instructionInput.Substring(0, 1);
            switch(instructionType)
            {
                case "F":
                    InstructionType = InstructionType.MoveForward;
                    break;
                case "L":
                    InstructionType = InstructionType.Rotate;
                    Rotation = Code.Rotation.Left;
                    break;
                case "R":
                    InstructionType = InstructionType.Rotate;
                    Rotation = Code.Rotation.Right;
                    break;
                case "N":
                    InstructionType = InstructionType.MoveByCompass;
                    Direction = Code.Direction.North;
                    break;
                case "E":
                    InstructionType = InstructionType.MoveByCompass;
                    Direction = Code.Direction.East;
                    break;
                case "S":
                    InstructionType = InstructionType.MoveByCompass;
                    Direction = Code.Direction.South;
                    break;
                case "W":
                    InstructionType = InstructionType.MoveByCompass;
                    Direction = Code.Direction.West;
                    break;
            }
            
            
            var units = instructionInput.Substring(1);
            Units = Int32.Parse(units);
        }
    }

    class Ship
    {
        public Direction Orientation { get; set; }
        public Location Location { get; set; }

        public Ship()
        {
            Orientation = Direction.East;
            Location = new Location();
        }

        public void Turn(Rotation rotation, int degrees)
        {
            var quarterIncrements = degrees / 90;
            var quarterTurns = quarterIncrements % 4;
            
            //Console.Write($"Rotating {rotation.ToString()} {degrees} degrees. ");

            if (rotation == Rotation.Left)
            {
                quarterTurns *= -1;
            }

            var newOrientation = ((int) Orientation + quarterTurns);
            
            if (newOrientation > 3)
            {
                newOrientation -= 4;
            }
            else if (newOrientation < 0)
            {
                newOrientation += 4;
            }

            Orientation = (Direction) newOrientation;
            //Console.WriteLine($"Orientation is now {Orientation.ToString()}");
        }
        public void Move(int units)
        {
            var direction = Orientation;
            //Console.WriteLine($"Moving forward ({direction.ToString()}) {units} units.");

            Move(direction, units);
        }

        public void Move(Direction direction, int units, bool log = false)
        {
            if (direction == Direction.South || direction == Direction.West)
            {
                units *= -1;
            }

            if (log)
            {
                //Console.WriteLine($"Moving {direction.ToString()} {units} units.");
            }

            if (direction == Direction.West || direction == Direction.East)
            {
                Location.x += units;
            }
            else
            {
                Location.y += units;
            }
        }

        public void ProcessInstructions(List<Instruction> instructions)
        {
            foreach(var instruction in instructions)
            {
                ProcessInstruction(instruction);
            }
        }

        private void ProcessInstruction(Instruction instruction)
        {
            switch(instruction.InstructionType)
            {
                case InstructionType.MoveForward:
                    Move(instruction.Units);
                    break;
                case InstructionType.MoveByCompass:
                    Move((Direction) instruction.Direction, instruction.Units, true);
                    break;
                case InstructionType.Rotate:
                    Turn((Rotation) instruction.Rotation, instruction.Units);
                    break;
            }
        }
    }

    class Location
    {
        public int x { get; set; }
        public int y { get; set; }

        public Location()
        {
            x = 0;
            y = 0;
        }

        public int GetManhattanDistance() 
        {
            return Math.Abs(x) + Math.Abs(y);
        }
    }
    
    enum InstructionType
    {
        MoveForward = 0,
        Rotate = 1,
        MoveByCompass = 2
    }

    enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    enum Rotation
    {
        Left = 0,
        Right = 1
    }
        
}
