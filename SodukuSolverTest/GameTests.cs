using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodukuSolver;

namespace SodukuSolverTest
{
    [TestClass]
    public class GameTests
    {

        [TestMethod]
        public void CreateBoard_Default_Returns81FilledItems()
        {
            var board = Program.CreateBoard();
            Assert.AreEqual(81, board.Length);

            Assert.AreEqual(0, board[0].YCoordinates);
            Assert.AreEqual(0, board[0].XCoordinates);
                        
            Assert.AreEqual(1, board[9].YCoordinates);
            Assert.AreEqual(0, board[9].XCoordinates);

            Assert.AreEqual(8, board[80].YCoordinates);
            Assert.AreEqual(8, board[80].XCoordinates);            
        }


        [TestMethod]
        public void GetEmptyCell_EmptyBoard_ReturnsFirstCell()
        {
            var board = Program.CreateBoard();
            var emptyCell = Program.GetEmptyCell(board);

            Assert.AreEqual(0, emptyCell.PositionInArray);
        }

        [TestMethod]
        public void GetEmptyCell_OneInBoard_ReturnsSecondCell()
        {
            var board = Program.CreateBoard();

            board[0].Value = 2;

            var emptyCell = Program.GetEmptyCell(board);

            Assert.AreEqual(1, emptyCell.PositionInArray);
        }


        [TestMethod]
        public void IsFull_EmptyBoard_ReturnsFalse()
        {
            var board = new Cell[Program.GRID_SIZE];

            Assert.IsFalse(Program.IsFull(board));
        }

        [TestMethod]
        public void IsFull_HalfFilledBoard_ReturnsFalse()
        {
            var board = new Cell[Program.GRID_SIZE];
            for (int i = 0; i < 10; i++)
            {
                board[i] = new Cell();
                board[i].Value = 2;
            }
            Assert.IsFalse(Program.IsFull(board));
        }

        [TestMethod]
        public void IsFull_FilledBoard_ReturnsTrue()
        {
            var board = new Cell[Program.GRID_SIZE];
            for (int i = 0; i < Program.GRID_SIZE; i++)
            {
                board[i] = new Cell();
                board[i].Value = 2;
            }
            Assert.IsTrue(Program.IsFull(board));
        }
    }
}
