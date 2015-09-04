using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukuSolver
{
    public class Program
    {
        public const int MIN_VALUE = 1;
        public const int MAX_VALUE = 9;
        public const int GRID_SIZE = MAX_VALUE * MAX_VALUE;

        static void Main(string[] args)
        {
            Cell[] sodukuGameData = new Cell[80];
            sodukuGameData = InitializeGameDataRandomly();
            PrintGame(sodukuGameData);

            if (SolveSolution(sodukuGameData))
            {
                PrintGame(sodukuGameData);
            }

            return;
        }

        private static Cell[] InitializeGameDataRandomly()
        {
            Cell[] sodukuGameData = Program.CreateBoard();
            Random rnd = new Random();

            for (int i = 0; i < sodukuGameData.Length; i = i + rnd.Next(1, 9))//only fill some cells
            {
                bool isLegalMove = false;
                int counter = 0;

                Cell currentCell = sodukuGameData[i];

                do
                {
                    int cellValue = rnd.Next(1, 9);

                    if (LegalMoveChecker.IsLegalMove(currentCell, sodukuGameData, cellValue))
                    {
                        PrintGame(sodukuGameData);
                        Console.WriteLine("\n\n");

                        currentCell.Value = cellValue;
                        isLegalMove = true;
                    }
                    counter++;

                } while (isLegalMove == false && counter < 5);//prevent infinite loop
            }

            return sodukuGameData;
        }


        public static void PrintGame(Cell[] sodukuGameData)
        {
            Console.WriteLine("Printing game");

            for (int i = 0; i < sodukuGameData.Length; i++)
            {
                string valueToDisplay = sodukuGameData[i].Value > 0 ?
                    sodukuGameData[i].Value.ToString() : " ";

                Console.Write("{0} ", valueToDisplay);

                if (IsNewRow(i))
                {
                    Console.WriteLine("");
                    Console.WriteLine("--------------------------"); 
                }
                else if (IsNewColumn(i))
                {
                    Console.Write(" | " );
                }
            }

            Console.WriteLine("");

        }

        private static bool IsNewRow(int i)
        {
            return (i + 1) % 9 == 0;
        }

        private static bool IsNewColumn(int i)
        {
            return (i + 1) % 3 == 0;
        }

        public static bool SolveSolution(Cell[] sodukuGameData)
        {
            bool solved = false;
            if (IsFull(sodukuGameData))
            {
                solved = true;
            }

            int trialValue = MIN_VALUE;

            while (!solved && trialValue <= MAX_VALUE)
            {
                //Get first empty cell. 
                Cell candidateCell = GetEmptyCell(sodukuGameData);

                // Before we try a value in it, Is Legal Move?
                if (LegalMoveChecker.IsLegalMove(candidateCell, sodukuGameData, trialValue))
                {
                    SetCell(candidateCell, sodukuGameData, trialValue);
                    if (SolveSolution(sodukuGameData))
                    {
                        solved = true;
                    }
                    else
                    {
                        ClearCell(sodukuGameData, candidateCell);
                    }
                }
                else
                {
                    trialValue++;
                }
            }
            return false;
        }

        public static Cell[] CreateBoard()
        {
            var board = new Cell[Program.GRID_SIZE];
            for (int i = 0; i < Program.GRID_SIZE; i++)
            {
                board[i] = new Cell() { XCoordinates = i % 9, YCoordinates = i / 9 };
            }
            return board;
        }

        public static void ClearCell(Cell[] sodukuGameData, Cell candidateCell)
        {
            sodukuGameData[candidateCell.PositionInArray].Value = 0;
        }

        public static void SetCell(Cell emptyCell, Cell[] sodukuGameData, int value)
        {
            emptyCell.Value = value;
        }

        public static Cell GetEmptyCell(Cell[] sodukuGameData)
        {
            return sodukuGameData.First(c => c.Value == 0);
        }

        public static bool IsFull(Cell[] sodukuGameData)
        {
            //if any don't exist
            if (sodukuGameData.Any(c => c == null))
            {
                return false;
            }

            if (sodukuGameData.All(c => c != null && c.Value > 0))
            {
                return true;
            }

            return false;
        }
    }
}
