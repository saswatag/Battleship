using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace BattleshipStateTrackerTests
{
    internal enum ShipOrientation
    {
        Horizontal,
        Vertical
    }

    internal class BattleshipBoard
    {
        const char FREE_SPOT_CHARACTER = '.';
        const char OCCUPIED_SPOT_CHARACTER = '*';
        const int ROWS = 10;
        const int COLUMNS = 10;
                
        private char[,] Board { get; } = new char[ROWS, COLUMNS];
        public BattleshipBoard()
        {
            InitializeEmptyBoard();
        }

        internal bool PlaceShipAt(int startingXPosition, int startingYPosition, int shipLength, ShipOrientation orientation)
        {
            if(CanShipBePlacedAt(startingXPosition, startingYPosition, shipLength, orientation))
            {
                if (orientation == ShipOrientation.Horizontal)
                {
                    for (int count = 0; count < shipLength; count++)
                        Board[startingXPosition + count, startingYPosition] = OCCUPIED_SPOT_CHARACTER;
                }
                else if(orientation == ShipOrientation.Vertical)
                {
                    for (int count = 0; count < shipLength; count++)
                        Board[startingXPosition, startingYPosition + count] = OCCUPIED_SPOT_CHARACTER;
                }

                return true;
            }
            else
                return false;
        }

        internal bool IsShipAt(int startingXPosition, int startingYPosition)
        {
            return Board[startingXPosition, startingYPosition].Equals(OCCUPIED_SPOT_CHARACTER);
        }

        internal bool IsEmpty()
        {
            foreach(var boardItem in Board)
                if (!boardItem.Equals(FREE_SPOT_CHARACTER))
                    return false;

            return true;
        }

        private void InitializeEmptyBoard()
        {
            for(int rowIndex = 0; rowIndex < Board.GetLength(0); rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Board.GetLength(1); columnIndex++)
                    Board[rowIndex, columnIndex] = FREE_SPOT_CHARACTER;
            }
        }

        private bool CanShipBePlacedAt(int startingXPosition, int startingYPosition, int shipLength, ShipOrientation orientation)
        {
            if (startingXPosition < 0 || startingXPosition >= ROWS || startingYPosition < 0 || startingYPosition >= COLUMNS)
                return false;

            if(orientation == ShipOrientation.Horizontal)
            {
                if (startingXPosition + shipLength >= COLUMNS)
                    return false;

                var targetShipPositions = GetTargetShipPositions(startingXPosition, shipLength);
                if (targetShipPositions.Any(position => IsShipAt(position, startingYPosition)))
                    return false;
            }                
            else if (orientation == ShipOrientation.Vertical)
            {
                if (startingYPosition + shipLength >= ROWS)
                    return false;

                var targetShipPositions = GetTargetShipPositionsInVerticalOrientation(startingYPosition, shipLength);
                if (targetShipPositions.Any(position => IsShipAt(startingXPosition, position)))
                    return false;
            }

            return true;
        }

        private ReadOnlyCollection<int> GetTargetShipPositions(int startingXPosition, int shipLength)
        {
            return Enumerable.Range(0, shipLength).Select(x => x + startingXPosition).ToList().AsReadOnly();
        }

        private ReadOnlyCollection<int> GetTargetShipPositionsInVerticalOrientation(int startingYPosition, int shipLength)
        {
            return Enumerable.Range(0, shipLength).Select(position => position + startingYPosition).ToList().AsReadOnly();
        }
    }
}