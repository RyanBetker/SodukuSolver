using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukuSolver
{
    public class LegalMoveChecker
    {
        public static bool IsLegalMove(Cell trialCell, IReadOnlyCollection<Cell> sodukuGameData, int trialValue)
        {
            bool isLegalMove = false;
            Cell[] trialGame = Program.CreateBoard();

            //copy, so we don't change the original data
            trialGame = sodukuGameData.ToArray();

            if (trialValue < 1 || trialValue > 9)
            {
                throw new ArgumentOutOfRangeException("Value must be between 1 and 9");
            }

            if (trialCell.Value > 0)
            {
                return false;//value already given
            }
            
            trialCell.Value = trialValue; 
            
            trialGame[trialCell.PositionInArray] = trialCell;

            //is row legal?            
            if (IsRowLegal(trialCell, trialGame))
            {                
                if (IsColumnLegal(trialCell, trialGame))
                {
                    //is box legal?
                    if(IsBoxLegal(trialCell, trialGame))
                    {
                        isLegalMove = true;
                    }                    
                }                
            }            

            return isLegalMove;
        }

        public static bool IsColumnLegal(Cell trialCell, Cell[] trialGame)
        {
            int startingIndexOfColumn = trialCell.PositionInArray % 9;

            List<Cell> cellsInColumn = new List<Cell>(9);            
            //get all cells in the column
            for (int i = startingIndexOfColumn; i < Program.GRID_SIZE; i = i + 9)
            {
                cellsInColumn.Add(trialGame[i]);
            }

            if (CellValuesAreUnique(cellsInColumn))
            {
                return true;
            }
            return false;
        }

        public static bool IsRowLegal(Cell trialCell, Cell[] trialGame)
        {
            int startingIndexOfRow = trialCell.PositionInArray - trialCell.XCoordinates;

            var cellsInRow = trialGame.Skip(startingIndexOfRow).Take(9).ToList();

            if (CellValuesAreUnique(cellsInRow))
            {
                return true;
            }
            return false;
        }

        private static bool CellValuesAreUnique(List<Cell> cellsInRow)
        {
            var filledCells = cellsInRow.Where(c => c.Value > 0);
            var duplicatedGroups = filledCells.GroupBy(c => c.Value).Where(g => g.Count() > 1);

            return duplicatedGroups.Any() == false;
        }
        
        public static bool IsBoxLegal(Cell trialCell, Cell[] trialGame)
        {
            int startingIndexOfBox = GetStartingIndexOfBox(trialCell);
            
            //get all cell indexes in the box
            List<Cell> cellsInBox = GetCellsInBox(trialGame, startingIndexOfBox);

            //save off all cells. Are they unique?
            if (CellValuesAreUnique(cellsInBox))
            {
                return true;
            }
            return false;
        }

        public static int GetStartingIndexOfBox(Cell trialCell)
        {
            int startingIndexOfBox = 0;
            const int GRID_LENGTH = Program.MAX_VALUE;

            //get starting column of box
            startingIndexOfBox = trialCell.PositionInArray - (trialCell.PositionInArray % 3);

            //get starting row of box
            int startingRow = trialCell.PositionInArray / GRID_LENGTH;
            startingIndexOfBox -= 9 * (startingRow % 3);//make it be the top row of the grid (either 0, 3, or 6)

            //Remember to subtract one back from actual positions :)

            return startingIndexOfBox;
        }

        private static List<Cell> GetCellsInBox(Cell[] trialGame, int startingIndexOfBox)
        {
            List<Cell> cellsInBox = new List<Cell>(9);
            AddBoxRowToCells(trialGame, startingIndexOfBox, cellsInBox);
            AddBoxRowToCells(trialGame, startingIndexOfBox + 9, cellsInBox);
            AddBoxRowToCells(trialGame, startingIndexOfBox + 18, cellsInBox);
            return cellsInBox;
        }

        private static void AddBoxRowToCells(Cell[] trialGame, int startingIndex, List<Cell> cellsInBox)
        {
            cellsInBox.Add(trialGame[startingIndex]);
            cellsInBox.Add(trialGame[startingIndex + 1]);
            cellsInBox.Add(trialGame[startingIndex + 2]);
        }
    }
}
