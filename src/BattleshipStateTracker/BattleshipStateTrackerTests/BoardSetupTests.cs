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
            board.PlaceShipAt(startingXPosition, startingYPosition, shipLength, ShipOrientation.Horizontal);

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
            board.PlaceShipAt(anyXPosition, anyYPosition, anyShipLength, ShipOrientation.Horizontal);

            // Assert
            var allExpectedOccupiedColumns = GetExpectedTargetShipPositions(anyXPosition, anyShipLength);
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
            bool firstShipPlaced = board.PlaceShipAt(firstShipXPosition, firstShipYPosition, firstShipLength, ShipOrientation.Horizontal);

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition;
            int secondShipLength = firstShipLength - 1;
            bool secondShipPlaced = board.PlaceShipAt(secondShipXPosition, secondShipYPosition, secondShipLength, ShipOrientation.Horizontal);

            int thirdShipXPosition = firstShipXPosition + 1;
            int thirdShipYPosition = firstShipYPosition;
            int thirdShipLength = firstShipLength - 1;
            bool thirdShipPlaced = board.PlaceShipAt(thirdShipXPosition, thirdShipYPosition, thirdShipLength, ShipOrientation.Horizontal);

            // Assert
            var allExpectedOccupiedColumns = GetExpectedTargetShipPositions(firstShipXPosition, firstShipLength);
            allExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, anyYPosition).Should().BeTrue());

            firstShipPlaced.Should().BeTrue();
            secondShipPlaced.Should().BeFalse();
            thirdShipPlaced.Should().BeFalse();
        }

        [Fact]
        public void PlaceMultipleShipsSuccessfullyInHorizontalOrientation()
        {
            // Arrange and Act
            BattleshipBoard board = new BattleshipBoard();

            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;

            int firstShipXPosition = anyXPosition;
            int firstShipYPosition = anyYPosition;
            int firstShipLength = anyShipLength;
            bool firstShipPlaced = board.PlaceShipAt(firstShipXPosition, firstShipYPosition, firstShipLength, ShipOrientation.Horizontal);

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition + 1;
            int secondShipLength = anyShipLength;
            bool secondShipPlaced = board.PlaceShipAt(secondShipXPosition, secondShipYPosition, secondShipLength, ShipOrientation.Horizontal);

            int thirdShipXPosition = firstShipXPosition;
            int thirdShipYPosition = firstShipYPosition + 3;
            int thirdShipLength = anyShipLength + 2;
            bool thirdShipPlaced = board.PlaceShipAt(thirdShipXPosition, thirdShipYPosition, thirdShipLength, ShipOrientation.Horizontal);

            // Assert
            var ship1ExpectedOccupiedColumns = GetExpectedTargetShipPositions(firstShipXPosition, firstShipLength);
            var ship2ExpectedOccupiedColumns = GetExpectedTargetShipPositions(secondShipXPosition, secondShipLength);
            var ship3ExpectedOccupiedColumns = GetExpectedTargetShipPositions(thirdShipXPosition, thirdShipLength);

            firstShipPlaced.Should().BeTrue();
            ship1ExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, firstShipYPosition).Should().BeTrue());

            secondShipPlaced.Should().BeTrue();
            ship2ExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, secondShipYPosition).Should().BeTrue());

            thirdShipPlaced.Should().BeTrue();
            ship3ExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, thirdShipYPosition).Should().BeTrue());
        }

        [Fact]
        public void PlacingBatleshipInVerticalOrientation()
        {
            // Arrange and Act
            BattleshipBoard board = new BattleshipBoard();

            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;
            bool shipPlaced = board.PlaceShipAt(anyXPosition, anyYPosition, anyShipLength, ShipOrientation.Vertical);

            // Assert
            var allExpectedOccupiedRows = GetExpectedTargetShipPositionsInVerticalOrientation(anyYPosition, anyShipLength);
            allExpectedOccupiedRows.ToList().ForEach(x => board.IsShipAt(anyXPosition, x).Should().BeTrue());
            shipPlaced.Should().BeTrue();
            board.IsEmpty().Should().BeFalse();
        }

        [Fact]
        public void PlacingMultipleShipInVerticalOrientation()
        {
            // Arrange and Act
            BattleshipBoard board = new BattleshipBoard();

            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;

            int firstShipXPosition = anyXPosition;
            int firstShipYPosition = anyYPosition;
            int firstShipLength = anyShipLength;
            bool firstShipPlaced = board.PlaceShipAt(firstShipXPosition, firstShipYPosition, firstShipLength, ShipOrientation.Vertical);

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition + 1;
            int secondShipLength = anyShipLength;
            bool secondShipPlaced = board.PlaceShipAt(secondShipXPosition, secondShipYPosition, secondShipLength, ShipOrientation.Vertical);

            int thirdShipXPosition = firstShipXPosition + 2;
            int thirdShipYPosition = firstShipYPosition + 2;
            int thirdShipLength = anyShipLength + 2;
            bool thirdShipPlaced = board.PlaceShipAt(thirdShipXPosition, thirdShipYPosition, thirdShipLength, ShipOrientation.Vertical);

            // Assert
            var ship1ExpectedOccupiedColumns = GetExpectedTargetShipPositionsInVerticalOrientation(firstShipYPosition, firstShipLength);
            var ship2ExpectedOccupiedColumns = GetExpectedTargetShipPositionsInVerticalOrientation(secondShipYPosition, secondShipLength);
            var ship3ExpectedOccupiedColumns = GetExpectedTargetShipPositionsInVerticalOrientation(thirdShipYPosition, thirdShipLength);

            firstShipPlaced.Should().BeTrue();
            ship1ExpectedOccupiedColumns.ToList().ForEach(yPosition => board.IsShipAt(firstShipXPosition, yPosition).Should().BeTrue());

            secondShipPlaced.Should().BeTrue();
            ship2ExpectedOccupiedColumns.ToList().ForEach(yPosition => board.IsShipAt(secondShipXPosition, yPosition).Should().BeTrue());

            thirdShipPlaced.Should().BeTrue();
            ship3ExpectedOccupiedColumns.ToList().ForEach(yPosition => board.IsShipAt(thirdShipXPosition, yPosition).Should().BeTrue());
        }

        #region Helpers

        private ReadOnlyCollection<int> GetExpectedTargetShipPositions(int startingXPosition, int shipLength)
        {
            return Enumerable.Range(0, shipLength).Select(position => position + startingXPosition).ToList().AsReadOnly();
        }

        private ReadOnlyCollection<int> GetExpectedTargetShipPositionsInVerticalOrientation(int startingYPosition, int shipLength)
        {
            return Enumerable.Range(0, shipLength).Select(position => position + startingYPosition).ToList().AsReadOnly();
        }

        #endregion
    }
}
