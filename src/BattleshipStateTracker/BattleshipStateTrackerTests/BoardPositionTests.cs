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
        [MemberData(nameof(FewBoardPositionValuesWithSameXAndYPositions))]
        public void BoardPositions_With_Same_XPosition_And_XPosition_Are_Equal(BoardPosition[] boardPositions)
        {
            boardPositions.Distinct().Count().Should().Be(1);
        }

        [Theory]
        [MemberData(nameof(FewBoardPositionValuesWithDifferentXAndYPositions))]
        public void BoardPositions_With_Different_XPosition_And_XPosition_Are_UnEqual(BoardPosition[] boardPositions)
        {
            Assert.True(boardPositions.Distinct().Count().Equals(boardPositions.Count()));
        }

        #region Helpers
        public static IEnumerable<object[]> FewBoardPositionValuesWithSameXAndYPositions()
        {
            return new List<object[]>
            {
                new object[] { new BoardPosition[] { new BoardPosition(3,3), new BoardPosition(3, 3) } },
                new object[] { new BoardPosition[] { new BoardPosition(0,0), new BoardPosition(0, 0) } }
            };
        }

        public static IEnumerable<object[]> FewBoardPositionValuesWithDifferentXAndYPositions()
        {
            return new List<object[]>
            {
                new object[] { new BoardPosition[] { new BoardPosition(3,2), new BoardPosition(2, 3) } },
                new object[] { new BoardPosition[] { new BoardPosition(0,9), new BoardPosition(9, 0) } },
                new object[] { new BoardPosition[] { new BoardPosition(1,6), new BoardPosition(5, 6) } }
            };
        }
        #endregion
    }
}
