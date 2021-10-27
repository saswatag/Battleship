using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;
using BattleshipStateTracker;
using System.Collections.Generic;

namespace BattleshipStateTrackerTests
{
    public class ShipTests
    {
        [Fact]
        public void AShipCanOccupyOnlyValidBoardPositions()
        {
            // Arrange and Act
            var ship = new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal);

            // Assert
            var expectedOccupiedBoardPositions = new List<BoardPosition>()
            {
                new BoardPosition(2, 2),
                new BoardPosition(3, 2),
                new BoardPosition(4, 2),
                new BoardPosition(5, 2),
            }.AsReadOnly();
            ship.OccupiedBoardPositions.Should().BeEquivalentTo(expectedOccupiedBoardPositions);
        }

        [Fact]
        public void AShipCannotOccupyInvalidPositions()
        {
            // Arrange and Act
            Action newShipWithInvalidOccupiedPositionsAction = () => new Ship("Destroyer", new BoardPosition(2, 2), 10, ShipOrientation.Horizontal);

            // Assert
            newShipWithInvalidOccupiedPositionsAction.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(11)]
        public void ShipLength_CannotBe_Invalid(int inValidShipLength)
        {
            // Arrange and Act
            Action newShipWithInvalidShipLengthAction = () => new Ship("Destroyer", new BoardPosition(2, 2), inValidShipLength, ShipOrientation.Horizontal);

            // Assert
            newShipWithInvalidShipLengthAction.Should().Throw<ArgumentException>().
                WithMessage($"Ship length shuold be in the range 1 to 10.");
        }

        [Fact]
        public void AShipWith_MinimumLength_CanBeCreated()
        {
            // Arrange and Act
            int minimumShipLength = 1;
            var ship = new Ship("Destroyer", new BoardPosition(9, 9), minimumShipLength, ShipOrientation.Horizontal);

            // Assert
            var expectedOccupiedBoardPositions = new List<BoardPosition>()
            {
                new BoardPosition(9, 9)
            }.AsReadOnly();
            ship.OccupiedBoardPositions.Should().BeEquivalentTo(expectedOccupiedBoardPositions);
        }

        [Fact]
        public void AShipWith_MaximumLength_CanBeCreated()
        {
            // Arrange and Act
            int minimumShipLength = 10;
            var ship = new Ship("Destroyer", new BoardPosition(0, 0), minimumShipLength, ShipOrientation.Vertical);

            // Assert
            var expectedOccupiedBoardPositions = new List<BoardPosition>()
            {
                new BoardPosition(0, 0),
                new BoardPosition(0, 1),
                new BoardPosition(0, 2),
                new BoardPosition(0, 3),
                new BoardPosition(0, 4),
                new BoardPosition(0, 5),
                new BoardPosition(0, 6),
                new BoardPosition(0, 7),
                new BoardPosition(0, 8),
                new BoardPosition(0, 9),
            }.AsReadOnly();
            ship.OccupiedBoardPositions.Should().BeEquivalentTo(expectedOccupiedBoardPositions);
        }
    }
}
