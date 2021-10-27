using System;

namespace BattleshipStateTracker
{
    public class BattleshipGame
    {
        public BattleshipBoard PlayerOneBoard { get; init; }
        public BattleshipBoard PlayerTwoBoard { get; init; }

        public BattleshipGame(BattleshipBoard battleshipBoard1, BattleshipBoard battleshipBoard2)
        {
            if (battleshipBoard1.IsEmpty() || battleshipBoard2.IsEmpty())
                throw new ArgumentException("Game cannot be started with empty boards");

            PlayerOneBoard = battleshipBoard1;
            PlayerTwoBoard = battleshipBoard2;
        }

        public bool AttackPlayerOneAt(BoardPosition attackPosition)
        {
            if(PlayerOneBoard.IsShipAt(attackPosition.XPosition, attackPosition.YPosition))
                return true;

            return false;
        }
    }
}