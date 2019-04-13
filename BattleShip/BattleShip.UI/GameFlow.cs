using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.Requests;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    public class GameFlow 
    {
        public static void Start(GameState state)
        {
            Player attackingPlayer = null;
            Player defendingPlayer = null;
            bool isGameOver=false;
            while (!isGameOver)
            {
                if (state.IsPlayerOneTurn)
                {
                    attackingPlayer = state.P1;
                    defendingPlayer = state.P2;
                }
                else
                {
                    attackingPlayer = state.P2;
                    defendingPlayer = state.P1;
                }

                ConsoleOutput.DrawBoard(defendingPlayer.Board);

                Coordinate attack = ConsoleInput.GetCoord(attackingPlayer.Name);

                FireShotResponse ShotFireStatus = defendingPlayer.Board.FireShot(attack);

                switch (ShotFireStatus.ShotStatus)
                {
                    case ShotStatus.Hit:
                        Console.WriteLine("Hit confirmed! Next players turn.");
                        Console.ReadLine();
                        Console.Clear();
                        state.IsPlayerOneTurn = !state.IsPlayerOneTurn;
                        break;

                    case ShotStatus.Miss:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Welp! You tried but that shot was a miss. Next players turn.");
                        Console.ReadLine();
                        Console.Clear();
                        state.IsPlayerOneTurn = !state.IsPlayerOneTurn;
                        break;

                    case ShotStatus.Invalid:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("...can't shoot there, coordinates must be inside of the grid, try again.");
                        Console.ReadLine();
                        break;

                    case ShotStatus.Duplicate:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Whoops! Didn't look at your board? Already shot there, try again.");
                        Console.ReadLine();
                        break;

                    case ShotStatus.HitAndSunk:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Yahtzee! You sunk a ship.");
                        Console.ReadLine();
                        Console.Clear();
                        state.IsPlayerOneTurn = !state.IsPlayerOneTurn;
                        break;

                    case ShotStatus.Victory:
                        isGameOver = true;
                        ConsoleOutput.GameOver();
                        Console.Clear();
                        break;
                }
            }
            ConsoleOutput.GameOver();
        }
    }
}
