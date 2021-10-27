using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;
using BattleshipStateTracker;
using System.Collections.Generic;

namespace BattleshipStateTrackerTests
{
    public class BoardSetupTests
    {
        [Fact]
        public void BattleshipsCanBePlacedOnTheBoard()
        {
            // Arrange and Act
            int startingXPosition = 0;
            int startingYPosition = 0;
            int shipLength = 2;
            BattleshipBoard board = new BattleshipBoard(new Ship("Destroyer", new BoardPosition(startingXPosition, startingYPosition), shipLength, ShipOrientation.Horizontal));

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
            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;
            BattleshipBoard board = new BattleshipBoard(new Ship("Destroyer", new BoardPosition(anyXPosition, anyYPosition), anyShipLength, ShipOrientation.Horizontal));

            // Assert
            var allExpectedOccupiedColumns = GetExpectedTargetShipPositions(anyXPosition, anyShipLength);
            allExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, anyYPosition).Should().BeTrue());
            board.IsEmpty().Should().BeFalse();
        }

        [Fact]
        public void MoreThanOneShipCannotOccupySamePosition()
        {
            // Arrange and Act
            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;

            int firstShipXPosition = anyXPosition;
            int firstShipYPosition = anyYPosition;
            int firstShipLength = anyShipLength;

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition;
            int secondShipLength = firstShipLength - 1;

            int thirdShipXPosition = firstShipXPosition + 1;
            int thirdShipYPosition = firstShipYPosition;
            int thirdShipLength = firstShipLength - 1;

            var shipsForBoard = new List<Ship>()
            {
                new Ship("Destroyer 1", new BoardPosition(firstShipXPosition, firstShipYPosition), firstShipLength, ShipOrientation.Horizontal),
                new Ship("Destroyer 2", new BoardPosition(secondShipXPosition, secondShipYPosition), secondShipLength, ShipOrientation.Horizontal),
                new Ship("Destroyer 3", new BoardPosition(thirdShipXPosition, thirdShipYPosition), thirdShipLength, ShipOrientation.Horizontal),
            }.AsReadOnly();
            Action boardSetupAction = () => new BattleshipBoard(shipsForBoard);

            // Assert
            boardSetupAction.Should().Throw<ArgumentException>().WithMessage("Board couldn't accomodate all ships. Some ship positions overlap.");
        }

        [Fact]
        public void PlaceMultipleShipsSuccessfullyInHorizontalOrientation()
        {
            // Arrange and Act
            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;

            int firstShipXPosition = anyXPosition;
            int firstShipYPosition = anyYPosition;
            int firstShipLength = anyShipLength;

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition + 1;
            int secondShipLength = anyShipLength;

            int thirdShipXPosition = firstShipXPosition;
            int thirdShipYPosition = firstShipYPosition + 3;
            int thirdShipLength = anyShipLength + 2;

            var shipsForBoard = new List<Ship>()
            {
                new Ship("Destroyer 1", new BoardPosition(firstShipXPosition, firstShipYPosition), firstShipLength, ShipOrientation.Horizontal),
                new Ship("Destroyer 2", new BoardPosition(secondShipXPosition, secondShipYPosition), secondShipLength, ShipOrientation.Horizontal),
                new Ship("Destroyer 3", new BoardPosition(thirdShipXPosition, thirdShipYPosition), thirdShipLength, ShipOrientation.Horizontal),
            }.AsReadOnly();
            var board = new BattleshipBoard(shipsForBoard);

            // Assert
            var ship1ExpectedOccupiedColumns = GetExpectedTargetShipPositions(firstShipXPosition, firstShipLength);
            var ship2ExpectedOccupiedColumns = GetExpectedTargetShipPositions(secondShipXPosition, secondShipLength);
            var ship3ExpectedOccupiedColumns = GetExpectedTargetShipPositions(thirdShipXPosition, thirdShipLength);
            
            ship1ExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, firstShipYPosition).Should().BeTrue());
            ship2ExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, secondShipYPosition).Should().BeTrue());
            ship3ExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, thirdShipYPosition).Should().BeTrue());
        }

        [Fact]
        public void PlacingBatleshipInVerticalOrientation()
        {
            // Arrange and Act
            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;
            var board = new BattleshipBoard(new Ship("Destroyer", new BoardPosition(anyXPosition, anyYPosition), anyShipLength, ShipOrientation.Vertical));

            // Assert
            var allExpectedOccupiedRows = GetExpectedTargetShipPositionsInVerticalOrientation(anyYPosition, anyShipLength);
            allExpectedOccupiedRows.ToList().ForEach(x => board.IsShipAt(anyXPosition, x).Should().BeTrue());
            board.IsEmpty().Should().BeFalse();
        }

        [Fact]
        public void PlacingMultipleShipInVerticalOrientation()
        {
            // Arrange and Act
            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;

            int firstShipXPosition = anyXPosition;
            int firstShipYPosition = anyYPosition;
            int firstShipLength = anyShipLength;

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition + 1;
            int secondShipLength = anyShipLength;
            
            int thirdShipXPosition = firstShipXPosition + 2;
            int thirdShipYPosition = firstShipYPosition + 2;
            int thirdShipLength = anyShipLength + 2;
            
            var shipsForBoard = new List<Ship>()
            {
                new Ship("Destroyer 1", new BoardPosition(firstShipXPosition, firstShipYPosition), firstShipLength, ShipOrientation.Vertical),
                new Ship("Destroyer 2", new BoardPosition(secondShipXPosition, secondShipYPosition), secondShipLength, ShipOrientation.Vertical),
                new Ship("Destroyer 3", new BoardPosition(thirdShipXPosition, thirdShipYPosition), thirdShipLength, ShipOrientation.Vertical),
            }.AsReadOnly();
            var board = new BattleshipBoard(shipsForBoard);

            // Assert
            var ship1ExpectedOccupiedRows = GetExpectedTargetShipPositionsInVerticalOrientation(firstShipYPosition, firstShipLength);
            var ship2ExpectedOccupiedRows = GetExpectedTargetShipPositionsInVerticalOrientation(secondShipYPosition, secondShipLength);
            var ship3ExpectedOccupiedRows = GetExpectedTargetShipPositionsInVerticalOrientation(thirdShipYPosition, thirdShipLength);

            ship1ExpectedOccupiedRows.ToList().ForEach(yPosition => board.IsShipAt(firstShipXPosition, yPosition).Should().BeTrue());
            ship2ExpectedOccupiedRows.ToList().ForEach(yPosition => board.IsShipAt(secondShipXPosition, yPosition).Should().BeTrue());
            ship3ExpectedOccupiedRows.ToList().ForEach(yPosition => board.IsShipAt(thirdShipXPosition, yPosition).Should().BeTrue());
        }

        [Fact]
        public void PlacingMultipleShipsInMixedOrientation()
        {
            // Arrange and Act
            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;

            int firstShipXPosition = anyXPosition;
            int firstShipYPosition = anyYPosition;
            int firstShipLength = anyShipLength;

            int secondShipXPosition = firstShipXPosition - 1;
            int secondShipYPosition = firstShipYPosition + 1;
            int secondShipLength = anyShipLength;

            int thirdShipXPosition = firstShipXPosition - 2;
            int thirdShipYPosition = firstShipYPosition - 2;
            int thirdShipLength = anyShipLength + 2;

            var shipsForBoard = new List<Ship>()
            {
                new Ship("Destroyer 1", new BoardPosition(firstShipXPosition, firstShipYPosition), firstShipLength, ShipOrientation.Vertical),
                new Ship("Destroyer 2", new BoardPosition(secondShipXPosition, secondShipYPosition), secondShipLength, ShipOrientation.Vertical),
                new Ship("Destroyer 3", new BoardPosition(thirdShipXPosition, thirdShipYPosition), thirdShipLength, ShipOrientation.Horizontal),
            }.AsReadOnly();
            var board = new BattleshipBoard(shipsForBoard);

            // Assert
            var ship1ExpectedOccupiedRows = GetExpectedTargetShipPositionsInVerticalOrientation(firstShipYPosition, firstShipLength);
            var ship2ExpectedOccupiedRows = GetExpectedTargetShipPositionsInVerticalOrientation(secondShipYPosition, secondShipLength);
            var ship3ExpectedOccupiedColumns = GetExpectedTargetShipPositions(thirdShipXPosition, thirdShipLength);
            
            ship1ExpectedOccupiedRows.ToList().ForEach(yPosition => board.IsShipAt(firstShipXPosition, yPosition).Should().BeTrue());
            ship2ExpectedOccupiedRows.ToList().ForEach(yPosition => board.IsShipAt(secondShipXPosition, yPosition).Should().BeTrue());
            ship3ExpectedOccupiedColumns.ToList().ForEach(xPosition => board.IsShipAt(xPosition, thirdShipYPosition).Should().BeTrue());
        }

        [Fact]
        public void PlacingBatleshipUpdatesShipCount()
        {
            // Arrange and Act
            int anyXPosition = 2;
            int anyYPosition = 2;
            int anyShipLength = 3;
            var board = new BattleshipBoard(new Ship("Destroyer", new BoardPosition(anyXPosition, anyYPosition), anyShipLength, ShipOrientation.Horizontal));

            // Assert
            var allExpectedOccupiedColumns = GetExpectedTargetShipPositions(anyXPosition, anyShipLength);
            allExpectedOccupiedColumns.ToList().ForEach(x => board.IsShipAt(x, anyYPosition).Should().BeTrue());
            board.IsEmpty().Should().BeFalse();
            board.ShipCount.Should().Be(1);
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
