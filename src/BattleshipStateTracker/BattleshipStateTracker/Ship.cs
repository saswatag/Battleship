using System.Collections.Generic;

namespace BattleshipStateTracker
{
    public class Ship
    {
        public string Name { get; init; }
        public BoardPosition PositionedAt { get; init; }
        public int Length { get; init; }
        public ShipOrientation Orientation { get; init; }
        public IReadOnlyCollection<BoardPosition> OccupiedBoardPositions { get; init; }

        Ship(string name, BoardPosition positionedAt, int shipLength, ShipOrientation orientation)
        {
            Name = name;
            Length = shipLength;
            PositionedAt = positionedAt;
            Orientation = orientation;
        }
    }
}