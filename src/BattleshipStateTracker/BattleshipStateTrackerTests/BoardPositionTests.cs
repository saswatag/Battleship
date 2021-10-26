using System;
using Xunit;
using FluentAssertions;
using System.Linq;
using System.Collections.ObjectModel;
using BattleshipStateTracker;
using System.Collections.Generic;

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

        [Theory]
        [MemberData(nameof(BoardPositionValuesWithSameXAndYPositions))]
        public void BoardPositions_With_Same_XPosition_And_XPosition_Are_Equal(BoardPosition[] boardPositions)
        {
            boardPositions.Distinct().Count().Should().Be(1);
        }

        #region Helpers
        public static IEnumerable<object[]> BoardPositionValuesWithSameXAndYPositions()
        {
            return new List<object[]>
            {
                new object[] { new BoardPosition[] { new BoardPosition(3,3), new BoardPosition(3, 3) } },
                new object[] { new BoardPosition[] { new BoardPosition(0,0), new BoardPosition(0, 0) } }
            };
        }
        #endregion
    }
}
