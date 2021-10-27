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
        public void Game_CanBe_Setup_With_MultiShipBoards_Where_ShipsDoNotOverlap()
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

        [Fact]
        public void Game_CannotBe_Setup_With_MultiShipBoards_Where_ShipsOverlap()
        {
            // Arrange and Act
            var shipsForBoard1 = new List<Ship>()
            {
                new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal),
                new Ship("Destroyer", new BoardPosition(4, 2), 3, ShipOrientation.Horizontal),
                new Ship("Destroyer", new BoardPosition(5, 5), 3, ShipOrientation.Vertical)
            }.AsReadOnly();

            var shipsForBoard2 = new List<Ship>()
            {
                new Ship("Destroyer", new BoardPosition(0, 0), 4, ShipOrientation.Horizontal),
                new Ship("Destroyer", new BoardPosition(3, 0), 3, ShipOrientation.Vertical)
            }.AsReadOnly();
            
            Action newGameAction = () => new BattleshipGame(new BattleshipBoard(shipsForBoard1), new BattleshipBoard(shipsForBoard2));

            // Assert
            newGameAction.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Any_Valid_BoardPosition_CanBe_Attacked()
        {
            var game = new BattleshipGame(new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            Action attackAction = () => game.AttackPlayerOneAt(new BoardPosition(2, 2));

            // Assert
            attackAction.Should().NotThrow<ArgumentException>();
        }

        [Fact]
        public void Attack_OnShipPosition_IsA_Hit()
        {
            var game = new BattleshipGame(new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            var attackHit = game.AttackPlayerOneAt(new BoardPosition(2, 2));

            // Assert
            attackHit.Should().Be(AttackResponse.Hit);
        }

        [Fact]
        public void Attack_Not_OnShipPosition_IsA_Miss()
        {
            var game = new BattleshipGame(new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            var attackHit = game.AttackPlayerOneAt(new BoardPosition(0, 1));

            // Assert
            attackHit.Should().Be(AttackResponse.Miss);
        }

        [Fact]
        public void Attack_On_AllShipPositions_SinksTheShip()
        {
            var game = new BattleshipGame(new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            game.AttackPlayerOneAt(new BoardPosition(2, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(3, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(4, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(5, 2)).Should().Be(AttackResponse.SunkAndWon);
        }

        [Fact]
        public void CombinationOf_HitAndMiss_To_AllShipPositions_SinksTheShip()
        {
            var game = new BattleshipGame(new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            game.AttackPlayerOneAt(new BoardPosition(2, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(2, 1)).Should().Be(AttackResponse.Miss);
            game.AttackPlayerOneAt(new BoardPosition(2, 3)).Should().Be(AttackResponse.Miss);
            game.AttackPlayerOneAt(new BoardPosition(3, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(4, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(5, 2)).Should().Be(AttackResponse.SunkAndWon);
        }

        [Fact]
        public void Sinking_TheOnlyShipOnTheBoard_Is_A_Win()
        {
            var game = new BattleshipGame(new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            game.AttackPlayerOneAt(new BoardPosition(2, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(2, 1)).Should().Be(AttackResponse.Miss);
            game.AttackPlayerOneAt(new BoardPosition(2, 3)).Should().Be(AttackResponse.Miss);
            game.AttackPlayerOneAt(new BoardPosition(3, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(4, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(5, 2)).Should().Be(AttackResponse.SunkAndWon);
        }

        [Fact]
        public void Sinking_AllShips_Is_A_Win()
        {
            var player1Ships = new List<Ship>()
            {
                new Ship("Destroyer 1", new BoardPosition(2, 2), 3, ShipOrientation.Horizontal),
                new Ship("Destroyer 2", new BoardPosition(2, 4), 2, ShipOrientation.Vertical),
                new Ship("Destroyer 3", new BoardPosition(4, 4), 4, ShipOrientation.Horizontal),
            }.AsReadOnly();
            var game = new BattleshipGame(new BattleshipBoard(player1Ships),
                new BattleshipBoard(new Ship("Destroyer", new BoardPosition(2, 2), 4, ShipOrientation.Horizontal)));

            // Assert
            game.AttackPlayerOneAt(new BoardPosition(2, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(3, 2)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(4, 2)).Should().Be(AttackResponse.HitAndSunk);

            game.AttackPlayerOneAt(new BoardPosition(2, 4)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(2, 5)).Should().Be(AttackResponse.HitAndSunk);

            game.AttackPlayerOneAt(new BoardPosition(4, 4)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(5, 4)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(6, 4)).Should().Be(AttackResponse.Hit);
            game.AttackPlayerOneAt(new BoardPosition(7, 4)).Should().Be(AttackResponse.SunkAndWon);
        }
    }
}
