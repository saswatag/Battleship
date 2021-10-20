using System;
using System.Linq;

namespace BattleshipStateTrackerTests
{
    internal class BattleshipBoard
    {
        const char FREE_SPOT_CHARACTER = '.';
        const int ROWS = 10;
        const int COLUMNS = 10;

        private char[][] Board { get; } = new char[ROWS][];
        public BattleshipBoard()
        {
            InitializeEmptyBoard();
        }

        internal void PlaceShipAt(int startingXPosition, int startingYPosition, int shipLength)
        {
            
        }

        internal bool IsShipAt(int startingXPosition, int startingYPosition)
        {
            return true;
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