using CalculationHelper.Calculators;
using HaenggiModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HaenggiModelCalculatorTests.CalculationsTests.BoltonTests
{
    [TestClass]
    public class BoltonTests : BaseTest
    {
        private RoothCalculationEntity theethMessure;

        [TestInitialize]
        public void TestInit()
        {
            theethMessure = SetRoothTheetDefaultMessures();
        }

        [TestMethod]
        public void GetBoltonTotalResultShouldSuccess()
        {
            var boltonResult = BoltonCalculator.GetBoltonTotalResult(theethMessure);

            Assert.AreEqual(boltonResult.SuperiorExcess, (decimal)1, "Superior excess Invalid");
            Assert.AreEqual(boltonResult.InferiorExcess, (decimal)-0.6, "Inferior excess Invalid");
            Assert.AreEqual(boltonResult.Mandibular12Ideal, (decimal)88.6, "Ideal Mandibular Patient Invalid");
            Assert.AreEqual(boltonResult.Mandibular12Pac, (decimal)88, "Mandibular Patient Invalid");
            Assert.AreEqual(boltonResult.Maxilar12Ideal, (decimal)96, "Ideal Maxilar Patient Invalid");
            Assert.AreEqual(boltonResult.Maxilar12Pac, (decimal)97, "Maxilar Patient Invalid");
        }

        [TestMethod]
        public void GetBoltonPreviousResultShouldSuccess()
        {
            var boltonResult = BoltonCalculator.GetBoltonPreviousResult(theethMessure);

            Assert.AreEqual(boltonResult.SuperiorExcess, (decimal)-5, "Superior excess Invalid");
            Assert.AreEqual(boltonResult.InferiorExcess, (decimal)4, "Inferior excess Invalid");
            Assert.AreEqual(boltonResult.Mandibular6Ideal, (decimal)34, "Ideal Mandibular Patient Invalid");
            Assert.AreEqual(boltonResult.Mandibular6Pac, (decimal)38, "Mandibular Patient Invalid");
            Assert.AreEqual(boltonResult.Maxilar6Ideal, (decimal)49, "Ideal Maxilar Patient Invalid");
            Assert.AreEqual(boltonResult.Maxilar6Pac, (decimal)44, "Maxilar Patient Invalid");
        }
    }
}
