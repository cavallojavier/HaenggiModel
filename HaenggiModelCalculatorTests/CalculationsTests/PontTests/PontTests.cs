using CalculationHelper.Calculators;
using HaenggiModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HaenggiModelCalculatorTests.CalculationsTests.PontTests
{
    /// <summary>
    /// Summary description for PontTests
    /// </summary>
    [TestClass]
    public class PontTests : BaseTest
    {
        private RoothCalculationEntity theethMessure;

        [TestInitialize]
        public void TestInit()
        {
            theethMessure = SetRoothTheetDefaultMessures();
        }

        [TestMethod]
        public void GetPontResultShouldSuccess()
        {
            var pontResult = PontCalculator.GetResult(theethMessure);

            Assert.AreEqual(pontResult.Pont14To24, (decimal)33.5, "14 to 24 Invalid");
            Assert.AreEqual(pontResult.Pont16To26, (decimal)43, "16 to 26 Invalid");
            Assert.AreEqual(pontResult.ArchLong, (decimal)16.5, "Arch Long Invalid");
        }

        [TestMethod]
        public void GetPontWithNoValidIndexTableShouldThrowError()
        {
            theethMessure.Tooth11 = 1;
            theethMessure.Tooth12 = 1;
            theethMessure.Tooth21 = 1;

            var pontResult = PontCalculator.GetResult(theethMessure);
            Assert.AreEqual(pontResult.ArchLong, null, "Arch Long Invalid");
        }
    }
}
