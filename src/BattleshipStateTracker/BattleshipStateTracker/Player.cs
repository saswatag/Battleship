using System;
using System.Collections.Generic;

namespace BattleshipStateTracker
{
    public class Player
    {
        public string Name { get; init; }
        public BattleshipBoard Board { get; init; }
        public bool LostGame { get; private set; }
        public int ShipCount
        {
            get
            {
                return Board.ShipCount;
            }
        }

        public Player(string name, BattleshipBoard board)
        {
            Name = name;
            Board = board;
            LostGame = false;
        }

        public AttackResponse TakeAttack(BoardPosition attackPosition)
        {
            if (Board.IsShipAt(attackPosition.XPosition, attackPosition.YPosition))
            {
                if (Board.HasShipAtPositionSunk(attackPosition))
                {
                    if (Board.HaveAllShipsSunk())
                    {
                        LostGame = true;
                    }

                    return AttackResponse.HitAndSunk;
                }
                else
                    return AttackResponse.Hit;
            }

            return AttackResponse.Miss;
        }
    }
}