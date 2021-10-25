using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;
using BattleshipStateTracker;

namespace BattleshipStateTrackerTests
{
    public class BoardPositionTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 3)]
        [InlineData(9, 9)]
        public void BoardPositionCanBeInitializedWithValidValues(int validXPositionInBoard, int validYPositionInBoard)
        {
            // Arrange and Act
            BoardPosition position = new BoardPosition(validXPositionInBoard, validYPositionInBoard);

            // Assert
            position.XPosition.Should().BeInRange(0, 9, "Valid x position value are in the range 0 to 9");
            position.YPosition.Should().BeInRange(0, 9, "Valid x position value are in the range 0 to 9");
        }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(10, 10)]
        public void BoardPositionCannotBeInitializedWithInValidValues(int inValidXPositionInBoard, int inValidYPositionInBoard)
        {
            // Arrange and Act
            Action invalidBoardPositionAction = () => new BoardPosition(inValidXPositionInBoard, inValidYPositionInBoard);

            // Assert
            invalidBoardPositionAction.Should().Throw<ArgumentException>().
                WithMessage($"Both X and Y positions '({inValidXPositionInBoard}, {inValidYPositionInBoard})' should be in the range 0 to 9");
        }
    }
}
