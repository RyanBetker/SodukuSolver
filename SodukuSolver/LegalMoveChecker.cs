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
                return true;

                //is column legal?
                int startingIndexOfColumn = trialCell.PositionInArray % 9;
                
                if (IsColumnLegal(trialCell, trialGame))
                {
                    //is box legal?
                    if(IsBoxLegal(trialGame))
                    {
                        isLegalMove = true;
                    }                    
                }                
            }            

            return isLegalMove;
        }

        public static bool IsColumnLegal(Cell trialCell, Cell[] trialGame)
        {
            return true;
            //TODO: get all indexes and distinct them (like above)
            throw new NotImplementedException();
            //can call into CellValuesAreUnique
        }

        public static bool IsRowLegal(Cell trialCell, Cell[] trialGame)
        {
            bool isLegalMove;

            int startingIndexOfRow = trialCell.PositionInArray - trialCell.XCoordinates;
            //how about when it's 2? or 8? Should be 0
            //when 9-17, should be 9.

            var cellsInRow = trialGame.Skip(startingIndexOfRow).Take(9).ToList();

            if (CellValuesAreUnique(cellsInRow)) //(cellsInRow are not unique)
            {
                isLegalMove = false;
            }
            else
            {
                isLegalMove = true;
            }

            return isLegalMove;
        }

        private static bool CellValuesAreUnique(List<Cell> cellsInRow)
        {
            var filledCells = cellsInRow.Where(c => c.Value > 0);
            var duplicatedGroups = filledCells.GroupBy(c => c.Value).Where(g => g.Count() > 1);

            return duplicatedGroups.Any();
        }


        public static bool IsBoxLegal(Cell[] trialGame)
        {
            bool isLegalMove;
            int startingIndexOfBox = 0;
            //get all cell indexes in the box

            //save off all cells. Are they unique?
            List<Cell> cellsInBox = GetCellsInBox(trialGame, startingIndexOfBox);
            if (CellValuesAreUnique(cellsInBox))
            {
                isLegalMove = false;
            }
            isLegalMove = true;
            return isLegalMove;
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
