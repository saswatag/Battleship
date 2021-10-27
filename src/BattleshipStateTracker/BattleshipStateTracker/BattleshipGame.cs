using System;

namespace BattleshipStateTracker
{
    public static class AttackResponse
    {
        public static (bool Hit, bool SunkShip, bool GameWon) Hit = (Hit: true, SunkShip: false, GameWon: false);

        public static (bool Hit, bool SunkShip, bool GameWon) Miss = (Hit: false, SunkShip: false, GameWon: false);

        public static (bool Hit, bool SunkShip, bool GameWon) HitAndSunk = (Hit: true, SunkShip: true, GameWon: false);

        public static (bool Hit, bool SunkShip, bool GameWon) SunkAndLost = (Hit: true, SunkShip: true, GameWon: true);

        public static (bool Hit, bool SunkShip, bool GameWon) AlreadyLost = (Hit: false, SunkShip: false, GameWon: true);
    }

    public class BattleshipGame
    {
        public Player PlayerOne { get; init; }
        public Player PlayerTwo { get; init; }

        public BattleshipGame(Player player1, Player player2)
        {
            if (player1.Board.IsEmpty() || player2.Board.IsEmpty())
                throw new ArgumentException("Game cannot be started with empty boards");

            PlayerOne = player1;
            PlayerTwo = player2;
        }

        public (bool Hit, bool SunkShip, bool GameWon) AttackPlayerOneAt(BoardPosition attackPosition)
        {
            if (PlayerOne.LostGame)
                return AttackResponse.AlreadyLost;

            var attackResponse = PlayerOne.TakeAttack(attackPosition);
            switch (attackResponse)
            {
                case AttackResponseNew.Hit:
                    return AttackResponse.Hit;

                case AttackResponseNew.HitAndSunk:
                    {
                        if (PlayerOne.LostGame)
                            return AttackResponse.SunkAndLost;
                        else
                            return AttackResponse.HitAndSunk;
                    }

                default:
                    return AttackResponse.Miss;
                    
            }
        }
    }
}