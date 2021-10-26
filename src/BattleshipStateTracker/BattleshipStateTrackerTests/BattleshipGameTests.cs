using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;
using BattleshipStateTracker;
using System.Collections.Generic;

namespace BattleshipStateTrackerTests
{
    public class BattleshipGameTests
    {
        [Fact]
        public void CannotSetupGameWithEmptyBoards()
        {
            // Arrange and Act
            Action gameSetupAction = () => new BattleshipGame(new BattleshipBoard(), new BattleshipBoard());

            // Assert
            gameSetupAction.Should().Throw<ArgumentException>().WithMessage("Game cannot be started with empty boards");
        }

        [Fact]
        public void GameCanBeSetupIfBoardsArePopulatedWithSingleShip()
        {
            // Arrange and Act
            var game = new BattleshipGame(new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            game.PlayerOneBoard.ShipCount.Should().Be(1);
            game.PlayerTwoBoard.ShipCount.Should().Be(1);
        }

        [Fact]
        public void GameCanBeSetupWithMultiShipBoardsWhereShipsDoNotOverlap()
        {
            // Arrange and Act
            var shipsForBoard1 = new List<Ship>()
            {
                new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal),
                new Ship("Destroyer", new BoardPosition(3, 3), 3, ShipOrientation.Horizontal),
                new Ship("Destroyer", new BoardPosition(5, 5), 3, ShipOrientation.Vertical)
            }.AsReadOnly();

            var shipsForBoard2 = new List<Ship>()
            {
                new Ship("Destroyer", new BoardPosition(0, 0), 4, ShipOrientation.Horizontal),
                new Ship("Destroyer", new BoardPosition(5, 5), 3, ShipOrientation.Vertical)
            }.AsReadOnly();

            var game = new BattleshipGame(new BattleshipBoard(shipsForBoard1), new BattleshipBoard(shipsForBoard2));

            // Assert
            game.PlayerOneBoard.ShipCount.Should().Be(shipsForBoard1.Count);
            game.PlayerTwoBoard.ShipCount.Should().Be(shipsForBoard2.Count);
        }
    }
}
