using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public class ConsoleInput
    {
        public static string Playername(int Playernumber)
        {
            Console.WriteLine($"Player {Playernumber} What is your name?\n");
            string Playername = Console.ReadLine();
            Console.Clear();
            return Playername;
        }

        internal static Coordinate GetCoord(string Playername)
        {

            bool IsValidCoordinate = false;

            Coordinate validCoordinate = null;

            while (!IsValidCoordinate)
            {
                Console.WriteLine($"{Playername}, Enter your coordinates.");

                String userInput = Console.ReadLine();
                IsValidCoordinate = CoordinateTryParse(userInput, out validCoordinate);
            }   
            return validCoordinate;

        }

        public static bool CoordinateTryParse(string userInput, out Coordinate validCoordinate)
        {
            validCoordinate = null;
            if(userInput.Length>1)
            {
                char yPart = userInput[0];

            String xPart = userInput.Substring(1);
            int x;
            int y;

                if (yPart >= 'a' && yPart <= 'j')
                {
                    if (int.TryParse(xPart, out x))
                    {
                        if (x >= 1 && x <= 10)
                        {
                            y = (yPart - 'a' + 1);
                            validCoordinate = new Coordinate(y, x);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        internal static ShipDirection GetDirect(string Playername)
        {
            Console.WriteLine("Enter direction it will face.");
            Console.WriteLine("Press U for up, D for down, L for left or R for right");

            string input = Console.ReadLine();

            ShipDirection direction = new ShipDirection();

            switch (input)
            {
                case "U":
                case "u":
                    direction = ShipDirection.Up;
                    break;

                case "D":
                case "d":
                    direction = ShipDirection.Down;
                    break;

                case "L":
                case "l":
                    direction = ShipDirection.Left;
                    break;

                case "R":
                case "r":
                    direction = ShipDirection.Right;
                    break;

                default:
                    Console.WriteLine("Invalid input. Please make a selection from the prompt.");
                    break;
            }
            return direction;
        }
    }
}
