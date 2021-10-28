using System;

namespace BattleshipStateTracker
{
    public enum AttackResponse
    {
        Hit,
        Miss,
        HitAndSunk,
        GameOver
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

        public AttackResponse AttackPlayerOneAt(BoardPosition attackPosition)
        {
            if (PlayerOne.LostGame)
                return AttackResponse.GameOver;

            return PlayerOne.TakeAttack(attackPosition);
        }
    }
}