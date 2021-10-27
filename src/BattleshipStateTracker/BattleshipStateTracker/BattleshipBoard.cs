using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BattleshipStateTracker
{
    public enum ShipOrientation
    {
        Horizontal,
        Vertical
    }

    public class BattleshipBoard
    {
        private Dictionary<BoardPosition, Ship> ShipPlacements { get; init; }
        private ReadOnlyCollection<Ship> Ships { get; init; }
        public int ShipCount
        {
            get
            {
                return Ships is not null ? Ships.Count : 0;
            }
        }

        public BattleshipBoard()
        {
            ShipPlacements = new Dictionary<BoardPosition, Ship>();
            Ships = new ReadOnlyCollection<Ship>(new List<Ship>());
        }

        public BattleshipBoard(Ship ship) : this(new List<Ship>() { ship }.AsReadOnly())
        {
        }

        public BattleshipBoard(ReadOnlyCollection<Ship> shipsForBoard)
        {
            ShipPlacements = new Dictionary<BoardPosition, Ship>();
            if (shipsForBoard.Any(ship => !PlaceShip(ship)))
                throw new ArgumentException("Board couldn't accomodate all ships. Some ship positions overlap.");

            this.Ships = shipsForBoard;
        }

        public bool IsShipAt(int startingXPosition, int startingYPosition)
        {
            return ShipPlacements.ContainsKey(new BoardPosition(startingXPosition, startingYPosition));
        }

        public bool IsEmpty()
        {
            return ShipCount.Equals(0) ? true : false;
        }

        private bool PlaceShip(Ship ship)
        {
            if (CanBoardAccomodateShip(ship))
            {
                foreach(var boardPosition in ship.OccupiedBoardPositions)
                    ShipPlacements.Add(boardPosition, ship);

                return true;
            }
            else
                return false;
        }

        private bool CanBoardAccomodateShip(Ship ship)
        {
            if (ship.OccupiedBoardPositions.Any(position => IsShipAt(position.XPosition, position.YPosition)))
                return false;

            return true;
        }
    }
}