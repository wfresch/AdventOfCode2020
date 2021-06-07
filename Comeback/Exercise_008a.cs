using System;
using System.Collections.Generic;
using System.Linq;

namespace Code
{
    class Program
    {
        private static ProgramInstruction[] _programInstructions;
        
        static void Main(string[] args)
        {
            var programInstructionLines = System.IO.File.ReadLines("../Input/8.txt");
                        
            _programInstructions = new ProgramInstruction[programInstructionLines.Count()];
            
            BuildProgramInstructions(programInstructionLines);
            Console.WriteLine(ExecuteProgram());
                                    
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static void BuildProgramInstructions(IEnumerable<string> programInstructionLines)
        {
            var counter = 0;

            foreach(var programInstructionLine in programInstructionLines)
            {
                var programInstruction = new ProgramInstruction(programInstructionLine);
                _programInstructions[counter] = programInstruction;
                counter++;
            }
        }

        private static int ExecuteProgram()
        {
            var cursor = 0;
            var accumulatorValue = 0;

            while(true)
            {
                var programInstruction = _programInstructions[cursor];
                programInstruction.MarkVisited();

                switch(programInstruction.OperationType)
                {
                    case (Operation.Accumulate):
                        accumulatorValue += programInstruction.Argument;
                        cursor++;
                        break;
                    case (Operation.Jump):
                        cursor += programInstruction.Argument;
                        break;
                    case (Operation.NoOperation):
                        cursor++;
                        break;
                    default:
                        break;                    
                }

                if (cursor > (_programInstructions.Count() - 1))
                {
                    break;
                }
                
                if (_programInstructions[cursor].Visited)
                {
                    break;
                }
            }

            return accumulatorValue;
        }

    }

    class ProgramInstruction
    {
        public Operation OperationType { get; private set; }
        public int Argument { get; private set; }
        public bool Visited { get; private set; }

        public ProgramInstruction(string input)
        {
            Visited = false;

            var inputParts = input.Split(' ');

            var operationName = inputParts[0];
            switch(operationName)
            {
                case "acc":
                    OperationType = Operation.Accumulate;
                    break;
                case "jmp":
                    OperationType = Operation.Jump;
                    break;
                case "nop":
                    OperationType = Operation.NoOperation;
                    break;
                default:
                    break;
            }

            var argumentString = inputParts[1];
            Argument = int.Parse(argumentString);
        }

        public void MarkVisited()
        {
            Visited = true;
        }
    }

    enum Operation
    {
        Accumulate,
        Jump,
        NoOperation
    }

}
