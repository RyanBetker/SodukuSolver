using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodukuSolver;

namespace SodukuSolverTest
{
    [TestClass]
    public class LegalMoveCheckerTests
    {
        [TestMethod]
        public void IsLegalMove_EmptyBoard_ReturnsTrue()
        {
            //Arrange
            var board = Program.CreateBoard();

            Assert.IsTrue(LegalMoveChecker.IsLegalMove(board[0], board, 4));
        }

        [TestMethod]
        public void IsLegalMove_CellTaken_ReturnsFalse()
        {
            //Arrange
            var board = Program.CreateBoard();
            board[0].Value = 3;

            //Act/Assert
            Assert.IsFalse(LegalMoveChecker.IsLegalMove(board[0], board, 4));
        }

        [TestMethod]
        public void IsRowLegal_NumberExistsTwice_ReturnsFalse()
        {
            //Arrange
            var board = Program.CreateBoard();
            board[1].Value = 4;
            board[2].Value = 4;

            Assert.IsFalse(LegalMoveChecker.IsRowLegal(board[1], board));
        }

        [TestMethod]
        public void IsRowLegal_NoDupes_ReturnsTrue()
        {
            //Arrange
            var board = Program.CreateBoard();
            board[1].Value = 4;
            board[2].Value = 3;
            board[8].Value = 1;

            Assert.IsTrue(LegalMoveChecker.IsRowLegal(board[1], board));
        }

        [TestMethod]
        public void IsColumnLegal_NumberExistsTwice_ReturnsFalse()
        {
            //Arrange
            var board = Program.CreateBoard();
            board[1].Value = 4;
            board[10].Value = 4;

            Assert.IsFalse(LegalMoveChecker.IsColumnLegal(board[1], board));
        }
        
        [TestMethod]
        public void IsBoxLegal_BoxSingleValue_ReturnsTrue()
        {
            //Arrange
            var board = Program.CreateBoard();
            board[1].Value = 4;

            Assert.IsTrue(LegalMoveChecker.IsBoxLegal(board[1], board));
        }

        [TestMethod]
        public void IsBoxLegal_BoxDupeValue_ReturnsFalse()
        {
            //Arrange
            var board = Program.CreateBoard();
            board[11].Value = 4;
            board[1].Value = 4;

            Assert.IsFalse(LegalMoveChecker.IsBoxLegal(board[1], board));
        }
    }
}
