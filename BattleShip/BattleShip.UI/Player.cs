using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;

namespace BattleShip.UI
{
    public class Player
    {
        public string Name { get; }
        public Board Board { get; }

        public Player(string name, Board board)
        {
            Name = name;
            Board = board;
        }
    }
}
