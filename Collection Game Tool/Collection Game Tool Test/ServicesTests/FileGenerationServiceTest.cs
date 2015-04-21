using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Collection_Game_Tool.Services;
using Collection_Game_Tool.PrizeLevels;
using Collection_Game_Tool.Divisions;
using Collection_Game_Tool.GameSetup;

namespace Collection_Game_Tool_Test.ServicesTests
{
    /// <summary>
    /// Summary description for FileGenerationServiceTest
    /// </summary>
    [TestClass]
    public class FileGenerationServiceTest
    {
        public FileGenerationServiceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void testBuildGameData()
        {
            GameSetupModel gs = new GameSetupModel();
            gs.maxPermutations = 100;
            gs.picks = 8;

            PrizeLevel pl1 = new PrizeLevel();
            pl1.isInstantWin = false;
            pl1.numCollections = 5;
            pl1.prizeValue = 100;

            PrizeLevel pl2 = new PrizeLevel();
            pl2.isInstantWin = false;
            pl2.numCollections = 4;
            pl2.prizeValue = 50;

            PrizeLevels pls = new PrizeLevels();
            pls.addPrizeLevel(pl1);
            pls.addPrizeLevel(pl2);


            DivisionModel dm1 = new DivisionModel();
            dm1.addPrizeLevel(pl1);
            
            DivisionModel dm2 = new DivisionModel();
            dm2.addPrizeLevel(pl2);

            DivisionsModel dms = new DivisionsModel();
            dms.addDivision(dm1);
            dms.addDivision(dm2);
          
            FileGenerationService fgs = new FileGenerationService();
            fgs.buildGameData(dms, pls, gs);
        }
    }
}
