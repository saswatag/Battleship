using System;
using System.Collections.Generic;

namespace BattleshipStateTracker
{
    public class Ship
    {
        private readonly List<BoardPosition> _occupiedBoardPositions;

        public string Name { get; init; }
        public BoardPosition PositionedAt { get; init; }
        public int Length { get; init; }
        public ShipOrientation Orientation { get; init; }
        public IReadOnlyCollection<BoardPosition> OccupiedBoardPositions 
        {
            get
            {
                return _occupiedBoardPositions.AsReadOnly();
            }
        }

        public Ship(string name, BoardPosition positionedAt, int shipLength, ShipOrientation orientation)
        {
            if (shipLength <= 0 || shipLength > 10)
                throw new ArgumentException($"Ship length shuold be in the range 1 to 10.");

            Name = name;
            Length = shipLength;
            PositionedAt = positionedAt;
            Orientation = orientation;

            _occupiedBoardPositions = new List<BoardPosition>();
            if (orientation == ShipOrientation.Horizontal)
            {
                for (int count = 0; count < shipLength; count++)
                    _occupiedBoardPositions.Add(new BoardPosition(positionedAt.XPosition + count, positionedAt.YPosition));
            }
            else if (orientation == ShipOrientation.Vertical)
            {
                for (int count = 0; count < shipLength; count++)
                    _occupiedBoardPositions.Add(new BoardPosition(positionedAt.XPosition, positionedAt.YPosition + count));
            }
        }
    }
}