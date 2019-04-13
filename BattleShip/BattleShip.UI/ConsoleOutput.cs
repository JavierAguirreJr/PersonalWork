using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public static class ConsoleOutput
    {
        internal static void Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Battleship!!\n");
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Playername()
        {
            Console.WriteLine("What is your name?");
            Console.WriteLine();
        }

        internal static void CoordinateProcess(string name)
        {
            Console.WriteLine("Enter your coordinates");
            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
        }

        internal static void Out()
        {
            Console.WriteLine("Your coordinate is not in the grid, please input a valid coordinate.");
            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
        }

        internal static void Overlap()
        {
            Console.WriteLine("Can not place ships on top of each other, please place ship in another coordinate.");
            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
        }

        internal static void Ok()
        {
            Console.WriteLine("Ship placed properly.");
            Console.WriteLine();
            Console.ReadLine();
            Console.Clear();
        }

        internal static void DrawBoard(Board GameBoard)
        {
            for (int y = 1; y <= 10; y++)
            {
                for (int x = 1; x <= 10; x++)
                {
                    ShotHistory currentState = GameBoard.CheckCoordinate(new Coordinate(y, x));
                    switch (currentState)
                    {
                        case ShotHistory.Unknown:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("?");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case ShotHistory.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("M");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case ShotHistory.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("H");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write("  | ");
                }
                Console.WriteLine();
                Console.WriteLine("-------------------------------------------------");
            }
        }

        internal static void GameOver ()
        {
            Console.WriteLine("Winner!!! You sunk your opponents ships.");
        }
    }
}
