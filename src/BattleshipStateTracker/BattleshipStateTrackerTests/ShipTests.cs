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
    }
}
