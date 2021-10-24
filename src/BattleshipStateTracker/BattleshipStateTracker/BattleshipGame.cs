using System;

namespace BattleshipStateTracker
{
    public class BattleshipGame
    {
        private BattleshipBoard PlayerOneBoard { get; }
        private BattleshipBoard PlayerTwoBoard { get; }

        public BattleshipGame(BattleshipBoard battleshipBoard1, BattleshipBoard battleshipBoard2)
        {
            if (battleshipBoard1.IsEmpty() || battleshipBoard2.IsEmpty())
                throw new ArgumentException("Game cannot be started with empty boards");

            PlayerOneBoard = battleshipBoard1;
            PlayerTwoBoard = battleshipBoard2;
        }
    }
}