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

            if (SolveSolution(sodukuGameData))
            {
                PrintGame(sodukuGameData);
            }

            return;
        }

        public static void PrintGame(Cell[] sodukuGameData)
        {
            throw new NotImplementedException();
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
                if (IsLegalMove(candidateCell, sodukuGameData, trialValue))
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

            //Do 
            //Try next possible solution

            //Possible Solution:
            //GetEmptyCell
            //If Legal to put next value in
            //PutNumberInCell
            //Test if solved
            //If not, put next number in


            //While not having solution


            //my solution was:
            //Test columns for solution
            //Test rows
            //Test boxes

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
            throw new NotImplementedException();
        }

        public static void SetCell(Cell emptyCell, Cell[] sodukuGameData, int trialValue)
        {
            throw new NotImplementedException();
        }

        public static bool IsLegalMove(Cell emptyCell, Cell[] sodukuGameData, int trialValue)
        {
            throw new NotImplementedException();
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
