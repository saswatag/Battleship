using System;
using Xunit;
using FluentAssertions;

namespace BattleshipStateTrackerTests
{
    public class BoardSetupTests
    {
        [Fact]
        public void BattleshipsCanBePlacedOnTheBoard()
        {
            BattleshipBoard board = new BattleshipBoard();

            int startingXPosition = 0;
            int startingYPosition = 0;
            int shipLength = 2;
            board.PlaceShipAt(startingXPosition, startingYPosition, shipLength);

            board.IsShipAt(startingXPosition, startingYPosition).Should().BeTrue();
        }

        [Fact]
        public void SetupAnEmptyBoard()
        {
            BattleshipBoard board = new BattleshipBoard();

            board.IsEmpty().Should().BeTrue();
        }
    }
}
