using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Collection_Game_Tool.Services;
using Collection_Game_Tool.PrizeLevels;
using Collection_Game_Tool.Divisions;
using Collection_Game_Tool.GameSetup;
using System.IO;

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
        public void testFileGenOne()
        {
            //Game Information
            GameSetupModel gsm = new GameSetupModel();
            gsm.isNearWin = true;
            gsm.nearWins = 1;
            gsm.maxPermutations = 1000;
            gsm.totalPicks = 10;

            PrizeLevels ps = new PrizeLevels();
           
            PrizeLevel p1 = new PrizeLevel();
            p1.numCollections = 5;
            p1.prizeValue = 1000;
            ps.addPrizeLevel(p1);
            PrizeLevel p2 = new PrizeLevel();
            p2.numCollections = 4;
            p2.prizeValue = 100;
            ps.addPrizeLevel(p2);
            PrizeLevel p3 = new PrizeLevel();
            p3.numCollections = 4;
            p3.prizeValue = 10;
            ps.addPrizeLevel(p3);
            PrizeLevel p4 = new PrizeLevel();
            p4.numCollections = 3;
            p4.prizeValue = 5;
            ps.addPrizeLevel(p4);
            PrizeLevel p5 = new PrizeLevel();
            p5.numCollections = 3;
            p5.prizeValue = 2;
            ps.addPrizeLevel(p5);
            PrizeLevel p6 = new PrizeLevel();
            p6.numCollections = 2;
            p6.prizeValue = 1;
            ps.addPrizeLevel(p6);

            DivisionsModel divs = new DivisionsModel();

            DivisionModel d1 = new DivisionModel();
            d1.addPrizeLevel(p1);
            divs.addDivision(d1);
            DivisionModel d2 = new DivisionModel();
            d2.addPrizeLevel(p2);
            divs.addDivision(d2);
            DivisionModel d3 = new DivisionModel();
            d3.addPrizeLevel(p3);
            divs.addDivision(d3);
            DivisionModel d4 = new DivisionModel();
            d4.addPrizeLevel(p4);
            divs.addDivision(d4);
            DivisionModel d5 = new DivisionModel();
            d5.addPrizeLevel(p5);
            divs.addDivision(d5);
            DivisionModel d6 = new DivisionModel();
            d6.addPrizeLevel(p6);
            divs.addDivision(d6);

            //file creation

            //file test
            for (int i = 0; i < 1000; i++)
            {
                String fileName = "FileGenTest" + i;
                FileGenerationService fgs = new FileGenerationService();
                fgs.buildGameData(divs, ps, gsm, "C:\\" + fileName + ".txt");

                try
                {
                    using (StreamReader sr = new StreamReader("C:\\" + fileName + ".txt"))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Assert.IsTrue(checkLine(divs, ps, line));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

        }

        [TestMethod]
        public void testFileGenTwo()
        {
            //Game Information
            GameSetupModel gsm = new GameSetupModel();
            gsm.isNearWin = true;
            gsm.nearWins = 1;
            gsm.maxPermutations = 1000;
            gsm.totalPicks = 10;

            PrizeLevels ps = new PrizeLevels();

            PrizeLevel p1 = new PrizeLevel();
            p1.numCollections = 3;
            p1.prizeValue = 1000;
            ps.addPrizeLevel(p1);
            PrizeLevel p2 = new PrizeLevel();
            p2.numCollections = 3;
            p2.prizeValue = 100;
            ps.addPrizeLevel(p2);
            PrizeLevel p3 = new PrizeLevel();
            p3.numCollections = 3;
            p3.prizeValue = 10;
            ps.addPrizeLevel(p3);
            PrizeLevel p4 = new PrizeLevel();
            p4.numCollections = 3;
            p4.prizeValue = 5;
            ps.addPrizeLevel(p4);
            PrizeLevel p5 = new PrizeLevel();
            p5.numCollections = 3;
            p5.prizeValue = 2;
            ps.addPrizeLevel(p5);
            PrizeLevel p6 = new PrizeLevel();
            p6.numCollections = 3;
            p6.prizeValue = 1;
            ps.addPrizeLevel(p6);

            DivisionsModel divs = new DivisionsModel();

            DivisionModel d1 = new DivisionModel();
            d1.addPrizeLevel(p1);
            divs.addDivision(d1);
            DivisionModel d2 = new DivisionModel();
            d2.addPrizeLevel(p2);
            divs.addDivision(d2);
            DivisionModel d3 = new DivisionModel();
            d3.addPrizeLevel(p3);
            divs.addDivision(d3);
            DivisionModel d4 = new DivisionModel();
            d4.addPrizeLevel(p4);
            divs.addDivision(d4);
            DivisionModel d5 = new DivisionModel();
            d5.addPrizeLevel(p5);
            divs.addDivision(d5);
            DivisionModel d6 = new DivisionModel();
            d6.addPrizeLevel(p6);
            divs.addDivision(d6);

            //file creation

            //file test
            for (int i = 0; i < 1000; i++)
            {
                String fileName = "FileGenTest" + i;
                FileGenerationService fgs = new FileGenerationService();
                fgs.buildGameData(divs, ps, gsm, "C:\\" + fileName + ".txt");

                try
                {
                    using (StreamReader sr = new StreamReader("C:\\" + fileName + ".txt"))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Assert.IsTrue(checkLine(divs, ps, line));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

        }

        [TestMethod]
        public void testFileGenThree()
        {
            //Game Information
            GameSetupModel gsm = new GameSetupModel();
            gsm.isNearWin = true;
            gsm.nearWins = 2;
            gsm.maxPermutations = 1000;
            gsm.totalPicks = 10;

            PrizeLevels ps = new PrizeLevels();

            PrizeLevel p1 = new PrizeLevel();
            p1.numCollections = 3;
            p1.prizeValue = 1000;
            ps.addPrizeLevel(p1);
            PrizeLevel p2 = new PrizeLevel();
            p2.numCollections = 3;
            p2.prizeValue = 100;
            ps.addPrizeLevel(p2);
            PrizeLevel p3 = new PrizeLevel();
            p3.numCollections = 3;
            p3.prizeValue = 10;
            ps.addPrizeLevel(p3);
            PrizeLevel p4 = new PrizeLevel();
            p4.numCollections = 3;
            p4.prizeValue = 5;
            ps.addPrizeLevel(p4);
            PrizeLevel p5 = new PrizeLevel();
            p5.numCollections = 3;
            p5.prizeValue = 2;
            ps.addPrizeLevel(p5);
            PrizeLevel p6 = new PrizeLevel();
            p6.numCollections = 3;
            p6.prizeValue = 1;
            ps.addPrizeLevel(p6);

            DivisionsModel divs = new DivisionsModel();

            DivisionModel d1 = new DivisionModel();
            d1.addPrizeLevel(p1);
            divs.addDivision(d1);
            DivisionModel d2 = new DivisionModel();
            d2.addPrizeLevel(p2);
            divs.addDivision(d2);
            DivisionModel d3 = new DivisionModel();
            d3.addPrizeLevel(p3);
            divs.addDivision(d3);
            DivisionModel d4 = new DivisionModel();
            d4.addPrizeLevel(p4);
            divs.addDivision(d4);
            DivisionModel d5 = new DivisionModel();
            d5.addPrizeLevel(p5);
            divs.addDivision(d5);
            DivisionModel d6 = new DivisionModel();
            d6.addPrizeLevel(p4);
            d6.addPrizeLevel(p5);
            divs.addDivision(d6);

            //file creation

            //file test
            for (int i = 0; i < 1000; i++)
            {
                String fileName = "FileGenTest" + i;
                FileGenerationService fgs = new FileGenerationService();
                fgs.buildGameData(divs, ps, gsm, "C:\\" + fileName + ".txt");

                try
                {
                    using (StreamReader sr = new StreamReader("C:\\" + fileName + ".txt"))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Assert.IsTrue(checkLine(divs, ps, line));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        [TestMethod]
        public void testFileGenFour()
        {
            //Game Information
            GameSetupModel gsm = new GameSetupModel();
            gsm.isNearWin = true;
            gsm.nearWins = 2;
            gsm.maxPermutations = 1000;
            gsm.totalPicks = 6;

            PrizeLevels ps = new PrizeLevels();

            PrizeLevel p1 = new PrizeLevel();
            p1.numCollections = 5;
            p1.prizeValue = 1000;
            ps.addPrizeLevel(p1);
            PrizeLevel p2 = new PrizeLevel();
            p2.numCollections = 4;
            p2.prizeValue = 100;
            ps.addPrizeLevel(p2);
            PrizeLevel p3 = new PrizeLevel();
            p3.numCollections = 3;
            p3.prizeValue = 10;
            ps.addPrizeLevel(p3);
            PrizeLevel p4 = new PrizeLevel();
            p4.numCollections = 3;
            p4.prizeValue = 5;
            ps.addPrizeLevel(p4);
            PrizeLevel p5 = new PrizeLevel();
            p5.numCollections = 3;
            p5.prizeValue = 2;
            ps.addPrizeLevel(p5);
            PrizeLevel p6 = new PrizeLevel();
            p6.numCollections = 2;
            p6.prizeValue = 1;
            ps.addPrizeLevel(p6);

            DivisionsModel divs = new DivisionsModel();

            DivisionModel d1 = new DivisionModel();
            d1.addPrizeLevel(p1);
            divs.addDivision(d1);
            DivisionModel d2 = new DivisionModel();
            d2.addPrizeLevel(p2);
            divs.addDivision(d2);
            DivisionModel d3 = new DivisionModel();
            d3.addPrizeLevel(p3);
            divs.addDivision(d3);
            DivisionModel d4 = new DivisionModel();
            d4.addPrizeLevel(p4);
            divs.addDivision(d4);
            DivisionModel d5 = new DivisionModel();
            d5.addPrizeLevel(p5);
            divs.addDivision(d5);
            DivisionModel d6 = new DivisionModel();
            d6.addPrizeLevel(p3);
            d6.addPrizeLevel(p4);
            divs.addDivision(d6);
            DivisionModel d7 = new DivisionModel();
            d7.addPrizeLevel(p5);
            d7.addPrizeLevel(p6);
            divs.addDivision(d7);

            //file creation

            //file test
            for (int i = 0; i < 1000; i++)
            {
                String fileName = "FileGenTest" + i;
                FileGenerationService fgs = new FileGenerationService();
                fgs.buildGameData(divs, ps, gsm, "C:\\" + fileName + ".txt");

                try
                {
                    using (StreamReader sr = new StreamReader("C:\\" + fileName + ".txt"))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Assert.IsTrue(checkLine(divs, ps, line));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

        }

        [TestMethod]
        public void testFileGenFive()
        {
            //Game Information
            GameSetupModel gsm = new GameSetupModel();
            gsm.isNearWin = true;
            gsm.nearWins = 1;
            gsm.maxPermutations = 1000;
            gsm.totalPicks = 10;

            PrizeLevels ps = new PrizeLevels();

            PrizeLevel p1 = new PrizeLevel();
            p1.numCollections = 5;
            p1.prizeValue = 1000;
            ps.addPrizeLevel(p1);
            PrizeLevel p2 = new PrizeLevel();
            p2.numCollections = 4;
            p2.prizeValue = 100;
            ps.addPrizeLevel(p2);
            PrizeLevel p3 = new PrizeLevel();
            p3.numCollections = 4;
            p3.prizeValue = 10;
            ps.addPrizeLevel(p3);
            PrizeLevel p4 = new PrizeLevel();
            p4.numCollections = 3;
            p4.prizeValue = 5;
            ps.addPrizeLevel(p4);
            PrizeLevel p5 = new PrizeLevel();
            p5.numCollections = 3;
            p5.prizeValue = 2;
            ps.addPrizeLevel(p5);
            PrizeLevel p6 = new PrizeLevel();
            p6.numCollections = 2;
            p6.prizeValue = 1;
            ps.addPrizeLevel(p6);

            DivisionsModel divs = new DivisionsModel();

            DivisionModel d1 = new DivisionModel();
            d1.addPrizeLevel(p3);
            divs.addDivision(d1);
            DivisionModel d2 = new DivisionModel();
            d2.addPrizeLevel(p1);
            divs.addDivision(d2);
            DivisionModel d3 = new DivisionModel();
            d3.addPrizeLevel(p6);
            divs.addDivision(d3);
            DivisionModel d4 = new DivisionModel();
            d4.addPrizeLevel(p4);
            divs.addDivision(d4);
            DivisionModel d5 = new DivisionModel();
            d5.addPrizeLevel(p5);
            divs.addDivision(d5);
            DivisionModel d6 = new DivisionModel();
            d6.addPrizeLevel(p2);
            divs.addDivision(d6);

            //file creation

            //file test
            for (int i = 0; i < 1000; i++)
            {
                String fileName = "FileGenTest" + i;
                FileGenerationService fgs = new FileGenerationService();
                fgs.buildGameData(divs, ps, gsm, "C:\\" + fileName + ".txt");

                try
                {
                    using (StreamReader sr = new StreamReader("C:\\" + fileName + ".txt"))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Assert.IsTrue(checkLine(divs, ps, line));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

        }

        [TestMethod]
        public void testFileGenSix()
        {
            //Game Information
            GameSetupModel gsm = new GameSetupModel();
            gsm.isNearWin = true;
            gsm.nearWins = 2;
            gsm.maxPermutations = 1000;
            gsm.totalPicks = 6;

            PrizeLevels ps = new PrizeLevels();

            PrizeLevel p1 = new PrizeLevel();
            p1.numCollections = 5;
            p1.prizeValue = 1000;
            ps.addPrizeLevel(p1);
            PrizeLevel p2 = new PrizeLevel();
            p2.numCollections = 4;
            p2.prizeValue = 100;
            ps.addPrizeLevel(p2);
            PrizeLevel p3 = new PrizeLevel();
            p3.numCollections = 3;
            p3.prizeValue = 10;
            ps.addPrizeLevel(p3);
            PrizeLevel p4 = new PrizeLevel();
            p4.numCollections = 3;
            p4.prizeValue = 5;
            ps.addPrizeLevel(p4);
            PrizeLevel p5 = new PrizeLevel();
            p5.numCollections = 3;
            p5.prizeValue = 2;
            ps.addPrizeLevel(p5);
            PrizeLevel p6 = new PrizeLevel();
            p6.numCollections = 2;
            p6.prizeValue = 1;
            ps.addPrizeLevel(p6);

            DivisionsModel divs = new DivisionsModel();

            DivisionModel d1 = new DivisionModel();
            d1.addPrizeLevel(p5);
            d1.addPrizeLevel(p6);
            divs.addDivision(d1);
            DivisionModel d2 = new DivisionModel();
            d2.addPrizeLevel(p2);
            divs.addDivision(d2);
            DivisionModel d3 = new DivisionModel();
            d3.addPrizeLevel(p3);
            divs.addDivision(d3);
            DivisionModel d4 = new DivisionModel();
            d4.addPrizeLevel(p4);
            divs.addDivision(d4);
            DivisionModel d5 = new DivisionModel();
            d5.addPrizeLevel(p5);
            divs.addDivision(d5);
            DivisionModel d6 = new DivisionModel();
            d6.addPrizeLevel(p3);
            d6.addPrizeLevel(p4);
            divs.addDivision(d6);
            DivisionModel d7 = new DivisionModel();
            d7.addPrizeLevel(p5);
            d7.addPrizeLevel(p6);
            divs.addDivision(d7);

            //file creation

            //file test
            for (int i = 0; i < 1000; i++)
            {
                String fileName = "FileGenTest" + i;
                FileGenerationService fgs = new FileGenerationService();
                fgs.buildGameData(divs, ps, gsm, "C:\\" + fileName + ".txt");

                try
                {
                    using (StreamReader sr = new StreamReader("C:\\" + fileName + ".txt"))
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            Assert.IsTrue(checkLine(divs, ps, line));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }

        }

        [TestMethod]
        public void testIntToChar()
        {
            int a = intFromChar('A');
            Assert.IsTrue(a == 0);
            Assert.IsTrue(intFromChar('B') == 1);
            Assert.IsTrue(intFromChar('C') == 2);

        }

        [TestMethod]
        public void testCheckLinePass()
        {
            PrizeLevels ps = new PrizeLevels();
            PrizeLevel p1 = new PrizeLevel();
            p1.prizeValue = 1000;
            p1.numCollections = 3;
            ps.addPrizeLevel(p1);
            PrizeLevel p2 = new PrizeLevel();
            p2.prizeValue = 100;
            p2.numCollections = 3;
            ps.addPrizeLevel(p2);
            PrizeLevel p3 = new PrizeLevel();
            p3.prizeValue = 10;
            p3.numCollections = 3;
            ps.addPrizeLevel(p3);
            PrizeLevel p4 = new PrizeLevel();
            p4.prizeValue = 1;
            p4.numCollections = 3;
            ps.addPrizeLevel(p4);
            DivisionsModel divs = new DivisionsModel();
            DivisionModel d1 = new DivisionModel();
            d1.addPrizeLevel(p1);
            divs.addDivision(d1);
            DivisionModel d2 = new DivisionModel();
            d2.addPrizeLevel(p1);
            divs.addDivision(d2);
            DivisionModel d3 = new DivisionModel();
            d3.addPrizeLevel(p1);
            divs.addDivision(d3);
            DivisionModel d4 = new DivisionModel();
            d4.addPrizeLevel(p1);
            divs.addDivision(d4);
            String passLine = "1 A,A,A,B,C,D";
            String failLine = "1 A,A,B,B,C,D";
            Assert.IsTrue(checkLine(divs, ps, passLine));
            Assert.IsFalse(checkLine(divs, ps, failLine));

        }

        private bool checkLine(DivisionsModel divs,PrizeLevels ps, String line){
            bool lineIsCorrect = true;
            char[] lineChars = line.ToCharArray();
            int divisionIndex = int.Parse(lineChars[0].ToString());
            DivisionModel div = divs.getDivision(divisionIndex);
            int[] prizeLevelsNeeded = new int[ps.prizeLevels.Count];
            int[] prizeLevelsfound = new int[ps.prizeLevels.Count];
            for (int i = 0; i < ps.prizeLevels.Count; i++)
            {
                prizeLevelsNeeded[i] = ps.prizeLevels[i].numCollections;
            }
            for (int i = 2; i < lineChars.Length; i++)
            {
                if (lineChars[i] != ',')
                {
                    int partValue = intFromChar(lineChars[i]);
                    prizeLevelsfound[partValue]++;
                }
            }
            //checks if all values are less than or equal to the needed levels
            List<PrizeLevel> psad = div.getPrizeLevelsAtDivision();
            for (int i = 0; i < ps.prizeLevels.Count; i++)
            {
                if (!(prizeLevelsfound[i] <= prizeLevelsNeeded[i]))
                {
                    lineIsCorrect = false;
                }
            }
            //checks if all needed levels are the right value
            int[] neededPrizeLevelIndexs = new int[psad.Count];
            for (int i = 0; i < neededPrizeLevelIndexs.Length; i++)
            {
                int level = ps.getLevelOfPrize(psad[i]);
                if (prizeLevelsNeeded[level] != prizeLevelsfound[level])
                {
                    lineIsCorrect = false;
                }
            }
            return lineIsCorrect;
        }

        private int intFromChar(char value)
        {
            int character = (int)(value - 65);
            return character;
        } 
    }
}
