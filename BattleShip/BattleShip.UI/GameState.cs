using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    public class GameState 
    {
        public Player P1 { get; }

        public Player P2 { get; }

        public bool IsPlayerOneTurn { get; set; }

        public GameState(Player p1, Player p2, bool isPlayerOneTurn)
        {
            P1 = p1;
            P2 = p2;
            IsPlayerOneTurn = isPlayerOneTurn;

        }
    }
}
