using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Ships;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    public class Setup
    {
        internal GameState Start()
        {
            string Playername = ConsoleInput.Playername(1);
            string Playername2 = ConsoleInput.Playername(2);

            Board player1board = GameBoard(Playername);
            Board player2board = GameBoard(Playername2);

            Player p1 = new Player(Playername, player1board);
            Player p2 = new Player(Playername2, player2board);

            bool IsPlayernameTurn = PlayernameTurn();
            return new GameState(p1, p2, IsPlayernameTurn);
        }

        private bool PlayernameTurn()
        {
            return RNG.Coinflip();
        }

        private Board GameBoard(string Playername)
        {
            Board board = new Board();

            for (ShipType s= ShipType.Destroyer; s<=ShipType.Carrier; s++)
            {
                bool IsPlacementValid = false;
                do
                {
                    PlaceShipRequest request = new PlaceShipRequest();
                    request.Coordinate = ConsoleInput.GetCoord(Playername);
                    request.Direction = ConsoleInput.GetDirect(Playername);
                    request.ShipType = s;

                    var result = board.PlaceShip(request);

                    if (result == ShipPlacement.NotEnoughSpace)
                    {
                        ConsoleOutput.Out();
                    }
                    else if (result == ShipPlacement.Overlap)
                    {
                        ConsoleOutput.Overlap();
                    }
                    else if (result == ShipPlacement.Ok)
                    {
                        ConsoleOutput.Ok();

                        IsPlacementValid = true;
                    }
                }
                while (!IsPlacementValid);
            }
            return board;
        }
    }
}
