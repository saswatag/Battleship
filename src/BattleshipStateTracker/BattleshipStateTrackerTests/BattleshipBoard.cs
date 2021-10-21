using System;
using System.Linq;

namespace BattleshipStateTrackerTests
{
    internal class BattleshipBoard
    {
        const char FREE_SPOT_CHARACTER = '.';
        const char OCCUPIED_SPOT_CHARACTER = '*';
        const int ROWS = 10;
        const int COLUMNS = 10;

        private char[][] Board { get; } = new char[ROWS][];
        public BattleshipBoard()
        {
            InitializeEmptyBoard();
        }

        internal void PlaceShipAt(int startingXPosition, int startingYPosition, int shipLength)
        {
            if(CanShipBePlacedAt(startingXPosition, startingYPosition, shipLength))
            {
                for(int count = 0; count < shipLength; count++)
                    Board[startingXPosition + count][startingYPosition] = OCCUPIED_SPOT_CHARACTER;
            }    
        }

        private bool CanShipBePlacedAt(int startingXPosition, int startingYPosition, int shipLength)
        {
            if (startingXPosition < 0 || startingXPosition >= ROWS || startingYPosition < 0 || startingYPosition >= COLUMNS)
                return false;

            if (startingXPosition + shipLength >= COLUMNS)
                return false;

            return true;
        }

        internal bool IsShipAt(int startingXPosition, int startingYPosition)
        {
            return Board[startingXPosition][startingYPosition].Equals(OCCUPIED_SPOT_CHARACTER);
        }

        internal bool IsEmpty()
        {
            return Board.All(x => x.All(y => y.Equals(FREE_SPOT_CHARACTER)));
        }

        private void InitializeEmptyBoard()
        {
            var mat = Enumerable.Repeat(FREE_SPOT_CHARACTER, ROWS).ToArray();
            Array.Fill(Board, mat, 0, COLUMNS);
        }
    }
}