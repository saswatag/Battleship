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
            var allExpectedOccupiedColumns = GetTargetShipPositions(anyXPosition, anyShipLength);
            allExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, anyYPosition).Should().BeTrue());
            board.IsEmpty().Should().BeFalse();
        }

        [Fact]
        public void MoreThanOneShipCannotOccupySamePosition()
        {
            // Arrange and Act
            BattleshipBoard board = new BattleshipBoard();

            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;

            int firstShipXPosition = anyXPosition;
            int firstShipYPosition = anyYPosition;
            int firstShipLength = anyShipLength;
            bool firstShipPlaced = board.PlaceShipAt(firstShipXPosition, firstShipYPosition, firstShipLength);

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition;
            int secondShipLength = firstShipLength - 1;
            bool secondShipPlaced = board.PlaceShipAt(secondShipXPosition, secondShipYPosition, secondShipLength);

            int thirdShipXPosition = firstShipXPosition + 1;
            int thirdShipYPosition = firstShipYPosition;
            int thirdShipLength = firstShipLength - 1;
            bool thirdShipPlaced = board.PlaceShipAt(thirdShipXPosition, thirdShipYPosition, thirdShipLength);

            // Assert
            var allExpectedOccupiedColumns = GetTargetShipPositions(firstShipXPosition, firstShipLength);
            allExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, anyYPosition).Should().BeTrue());

            firstShipPlaced.Should().BeTrue();
            secondShipPlaced.Should().BeFalse();
            thirdShipPlaced.Should().BeFalse();
        }

        #region Helpers

        private ReadOnlyCollection<int> GetTargetShipPositions(int startingXPosition, int shipLength)
        {
            return Enumerable.Range(1, shipLength).Select(x => x + startingXPosition).ToList().AsReadOnly();
        }

        #endregion
    }
}
