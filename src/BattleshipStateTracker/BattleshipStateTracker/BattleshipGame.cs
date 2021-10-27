using System;

namespace BattleshipStateTracker
{
    public static class AttackResponse
    {
        public static (bool Hit, bool SunkShip, bool GameWon) Hit = (Hit: true, SunkShip: false, GameWon: false);

        public static (bool Hit, bool SunkShip, bool GameWon) Miss = (Hit: false, SunkShip: false, GameWon: false);

        public static (bool Hit, bool SunkShip, bool GameWon) HitAndSunk = (Hit: true, SunkShip: true, GameWon: false);

        public static (bool Hit, bool SunkShip, bool GameWon) SunkAndWon = (Hit: true, SunkShip: true, GameWon: true);
    }

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

        public (bool Hit, bool SunkShip, bool GameWon) AttackPlayerOneAt(BoardPosition attackPosition)
        {
            if(PlayerOneBoard.IsShipAt(attackPosition.XPosition, attackPosition.YPosition))
            {
                if (PlayerOneBoard.HasShipAtPositionSunk(attackPosition))
                    return AttackResponse.HitAndSunk;
                else
                    return AttackResponse.Hit;
            }

            return AttackResponse.Miss;
        }
    }
}