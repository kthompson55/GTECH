using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Collection_Game_Tool.Services;
using System.Globalization;

namespace Collection_Game_Tool_Test.ServicesTests
{
    [TestClass]
    public class RangeRuleTest
    {
        [TestMethod]
        public void Test_Range_In_Limit()
        {
            RangeRule validator = new RangeRule();
            validator.Max = 20;
            validator.Min = 1;

            bool result = validator.Validate("3", new CultureInfo("en-US")).IsValid;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Test_Range_Out_Of_Limit()
        {
            RangeRule validator = new RangeRule();
            validator.Max = 20;
            validator.Min = 1;

            bool result = validator.Validate("40", new CultureInfo("en-US")).IsValid;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_Range_Incorrect_Input()
        {
            RangeRule validator = new RangeRule();
            validator.Max = 5;
            validator.Min = 0;

            bool result = validator.Validate(false, new CultureInfo("en-US")).IsValid;

            Assert.IsFalse(result);
        }
    }
}
