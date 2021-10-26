using System;

namespace BattleshipStateTracker
{
    public class BoardPosition
    {
        private const int ROWS = 10;
        private const int COLUMNS = 10;

        public int XPosition { get; init; }
        public int YPosition { get; init; }

        public BoardPosition(int xPosition, int yPosition)
        {
            if(xPosition < 0 || xPosition >= ROWS || yPosition < 0 || yPosition >= ROWS)
                throw new ArgumentException($"Both X and Y positions '({xPosition}, {yPosition})' should be in the range 0 to 9");

            XPosition = xPosition;
            YPosition = yPosition;
        }

        public override bool Equals(object obj)
        {
            BoardPosition otherPosition = obj as BoardPosition;

            if (obj == null)
                return false;

            return this.XPosition == otherPosition.XPosition && this.YPosition == otherPosition.YPosition;
        }

        public override int GetHashCode()
        {
            return XPosition * 19 + YPosition;
        }
    }
}