using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;

namespace BattleshipStateTrackerTests
{
    public class BoardSetupTests
    {
        [Fact]
        public void BattleshipsCanBePlacedOnTheBoard()
        {
            // Arrange and Act
            BattleshipBoard board = new BattleshipBoard();

            int startingXPosition = 0;
            int startingYPosition = 0;
            int shipLength = 2;
            board.PlaceShipAt(startingXPosition, startingYPosition, shipLength);

            // Assert
            board.IsShipAt(startingXPosition, startingYPosition).Should().BeTrue();
        }

        [Fact]
        public void SetupAnEmptyBoard()
        {
            // Arrange and Act
            BattleshipBoard board = new BattleshipBoard();

            // Assert
            board.IsEmpty().Should().BeTrue();
        }

        [Fact]
        public void PlacingBatleshipUpdatesBoardOccupancy()
        {
            // Arrange and Act
            BattleshipBoard board = new BattleshipBoard();

            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;
            board.PlaceShipAt(anyXPosition, anyYPosition, anyShipLength);

            // Assert
            var allExpectedOccupiedColumns = Enumerable.Range(anyXPosition, anyShipLength).ToList().AsReadOnly();
            allExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, anyYPosition).Should().BeTrue());
        }
    }
}
