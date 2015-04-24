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
        public void testBuildGameDataTwoDivisionsFivePicks()
        {
            GameSetupModel gs = new GameSetupModel();
            gs.maxPermutations = 100;
            gs.picks = 5;

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
            fgs.buildGameData(dms, pls, gs, "simpleTestTwoDivisionsFivePicks");
        }


        [TestMethod]
        public void testBuildGameDataTwoDivisionsTenPicks()
        {
            GameSetupModel gs = new GameSetupModel();
            gs.maxPermutations = 100;
            gs.picks = 6;

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
            fgs.buildGameData(dms, pls, gs, "simpleTestTwoDivisionsTenPicks");
        }

        [TestMethod]
        public void testBuildGameDataThreeDivisionsTenPicks()
        {
            GameSetupModel gs = new GameSetupModel();
            gs.maxPermutations = 100;
            gs.picks = 10;

            PrizeLevel pl1 = new PrizeLevel();
            pl1.isInstantWin = false;
            pl1.numCollections = 5;
            pl1.prizeValue = 100;

            PrizeLevel pl2 = new PrizeLevel();
            pl2.isInstantWin = false;
            pl2.numCollections = 4;
            pl2.prizeValue = 50;

            PrizeLevel pl3 = new PrizeLevel();
            pl2.isInstantWin = false;
            pl2.numCollections = 2;
            pl2.prizeValue = 10;

            PrizeLevels pls = new PrizeLevels();
            pls.addPrizeLevel(pl1);
            pls.addPrizeLevel(pl2);


            DivisionModel dm1 = new DivisionModel();
            dm1.addPrizeLevel(pl1);

            DivisionModel dm2 = new DivisionModel();
            dm2.addPrizeLevel(pl2);

            //7picks;
            DivisionModel dm3 = new DivisionModel();
            dm3.addPrizeLevel(pl1);
            dm3.addPrizeLevel(pl3);

            DivisionsModel dms = new DivisionsModel();
            dms.addDivision(dm1);
            dms.addDivision(dm2);
            dms.addDivision(dm3);

            FileGenerationService fgs = new FileGenerationService();
            fgs.buildGameData(dms, pls, gs, "simpleTestThreeDivisionsTenPicks");
        }


        [TestMethod]
        public void testBuildGameDataThreeDivisionsFifteenPicks()
        {
            GameSetupModel gs = new GameSetupModel();
            gs.maxPermutations = 100;
            gs.picks = 15;

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

            DivisionModel dm3 = new DivisionModel();
            dm3.addPrizeLevel(pl1);
            dm3.addPrizeLevel(pl2);

            DivisionsModel dms = new DivisionsModel();
            dms.addDivision(dm1);
            dms.addDivision(dm2);
            dms.addDivision(dm3);

            FileGenerationService fgs = new FileGenerationService();
            fgs.buildGameData(dms, pls, gs, "simpleTestThreeDivisionsFifteenPicks");
        }

        [TestMethod]
        public void testBuildGameDataMaxDivison()
        {
            GameSetupModel gs = new GameSetupModel();
            gs.maxPermutations = 100000;
            gs.picks = 20;

            int numPrizeLevels = 12;
            PrizeLevel[] prizes = new PrizeLevel[numPrizeLevels];
            PrizeLevels pls = new PrizeLevels();
            for (int i = 0; i < numPrizeLevels; i++)
            {
                prizes[i] = new PrizeLevel();
                prizes[i].isInstantWin = false;
                prizes[i].numCollections = i + 1;
                prizes[i].prizeValue = 100 * i ;
                pls.addPrizeLevel(prizes[i]);
            }

            int numberOfDivions = 30;
            DivisionModel[] divisions = new DivisionModel[numberOfDivions];
            DivisionsModel dms = new DivisionsModel();
            Random rand = new Random();
            for (int i = 0; i < numberOfDivions; i++)
            {
                divisions[i] = new DivisionModel();
                divisions[i].addPrizeLevel(prizes[rand.Next(0,12)]);
                dms.addDivision(divisions[i]);
            }

            FileGenerationService fgs = new FileGenerationService();
            fgs.buildGameData(dms, pls, gs, "simpleTestMaxDivisionsMaxPicks");
        }
    }
}
