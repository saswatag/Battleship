using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;
using BattleshipStateTracker;

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
    }
}
