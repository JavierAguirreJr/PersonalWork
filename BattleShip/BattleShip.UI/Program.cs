using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;

namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = false;
            do
            {
                ConsoleOutput.Start();

                Setup setup = new Setup();
                GameState state = setup.Start();

                GameFlow game = new GameFlow();
                GameFlow.Start(state);
                break;

            }
            while (playAgain);
            {
                Console.WriteLine("Would you like to play again? :)");
                Console.WriteLine("Press Y for yes, press Q to quit.");
                String userInput = Console.ReadLine();
                if (userInput == "Y" || userInput=="y")
                {
                    Setup restart = new Setup();
                    restart.Start();
                }
                else if (userInput == "Q" || userInput=="q")
                    return;
            }
        }
    }
}
