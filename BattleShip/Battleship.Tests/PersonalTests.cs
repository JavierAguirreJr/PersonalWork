using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BattleShip.BLL.Requests;
using BattleShip.UI;

namespace Battleship.Tests
{
    [TestFixture]
    public class PersonalTests
    {
        [TestCase("b10", 2, 10,true)]
        [TestCase("a10", 1, 10, true)]
        [TestCase("d12", 4, 12, false)]
        [TestCase("x5", 24, 5, false)]
        [TestCase(" ", 0, 0, false)]

        public static void CanUseCoordinate(string userInput, int x, int y, bool expected)
        {
            string coordInput = userInput;
            Coordinate coordinate = new Coordinate(x,y);

            bool isValid = ConsoleInput.CoordinateTryParse(userInput, out coordinate);

            Assert.AreEqual(expected, isValid);
            if (isValid)
            {
                Assert.AreEqual(coordinate.XCoordinate, x);
                Assert.AreEqual(coordinate.YCoordinate, y);
            }
        }
    }
}
