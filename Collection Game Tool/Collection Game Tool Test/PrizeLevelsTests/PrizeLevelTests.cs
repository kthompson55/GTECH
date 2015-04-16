using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Collection_Game_Tool.PrizeLevels;

namespace Collection_Game_Tool_Test.PrizeLevelsTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Prize_Comparison()
        {
            PrizeLevel pl1 = new PrizeLevel();
            PrizeLevel pl2 = new PrizeLevel();

            pl1.prizeValue = 50;
            pl2.prizeValue = 100;

            Assert.IsTrue(pl1.CompareTo(pl2) < 0);
        }

        [TestMethod]
        public void Test_InstantWin_Toggle()
        {
            PrizeLevel pl1 = new PrizeLevel();

            pl1.isInstantWin = false;

            pl1.toggleIsInstantWin();

            Assert.IsTrue(pl1.isInstantWin);

            pl1.toggleIsInstantWin();

            Assert.IsFalse(pl1.isInstantWin);
        }
    }
}
