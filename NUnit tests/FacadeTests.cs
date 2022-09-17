using NUnit.Framework;

namespace NUnit_tests
{
    using Monopoly;
    using System.Collections.Generic;

    public class FacadeTests
    {
        [SetUp]
        public void Setup() 
        {
        }

        [Test]
        public void MakeMonopolyBoard_BoardIsBiggerThanForty_ReturnsFalse() 
        {
            Facade facade = new();

            Board board = facade.MakeMonopolyBoard();

            Assert.IsFalse(board.TheBoard.Length > 40);
        }

        [Test]
        public void MakeMonopolyBoard_BoardIsLessThanForty_ReturnsFalse() 
        {
            Facade facade = new();

            Board board = facade.MakeMonopolyBoard();

            Assert.IsFalse(board.TheBoard.Length < 40);
        }

        [Test]
        public void MakeMonopolyBoard_BoardIsEqualForty_ReturnsTrue() 
        {
            Facade facade = new();

            Board board = facade.MakeMonopolyBoard();

            Assert.IsTrue(board.TheBoard.Length == 40);
        }

        [Test]
        public void MakeMonopolyBoard_GeneretesBoardCorrect_ReturnsTrue() 
        {
            Facade facade = new();

            Board board = facade.MakeMonopolyBoard();

            // 0 element should always be start
            Assert.IsTrue(board.TheBoard[0].GetType() == typeof(Start));
        }
    }
}