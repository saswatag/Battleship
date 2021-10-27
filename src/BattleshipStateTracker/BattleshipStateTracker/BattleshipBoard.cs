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
        private List<BoardPosition> Attacks { get; init; } 

        public int ShipCount
        {
            get
            {
                return Ships is not null ? Ships.Count : 0;
            }
        }

        private int SunkCount { get; set; }

        public BattleshipBoard()
        {
            ShipPlacements = new Dictionary<BoardPosition, Ship>();
            Ships = new ReadOnlyCollection<Ship>(new List<Ship>());
            Attacks = new List<BoardPosition>();
            SunkCount = 0;
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

            Attacks = new List<BoardPosition>();

            SunkCount = 0;
        }

        public bool IsShipAt(int startingXPosition, int startingYPosition)
        {
            return ShipPlacements.ContainsKey(new BoardPosition(startingXPosition, startingYPosition));
        }

        public bool HaveAllShipsSunk()
        {
            return SunkCount.Equals(Ships.Count);
        }

        public bool IsEmpty()
        {
            return ShipCount.Equals(0) ? true : false;
        }

        public bool HasShipAtPositionSunk(BoardPosition attackPosition)
        {
            Attacks.Add(attackPosition);
            var shipUnderAttack = ShipPlacements[attackPosition];

            if (shipUnderAttack.OccupiedBoardPositions.Intersect(Attacks).ToList().Count.Equals(shipUnderAttack.OccupiedBoardPositions.Count))
            {
                SunkCount++;
                return true;
            }

            return false;
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