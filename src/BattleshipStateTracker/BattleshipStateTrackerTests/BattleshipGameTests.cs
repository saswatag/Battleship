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
    }
}
