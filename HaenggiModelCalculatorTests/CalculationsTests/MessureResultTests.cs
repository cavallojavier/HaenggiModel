using CalculationHelper;
using HaenggiModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HaenggiModelCalculatorTests.CalculationsTests
{
    [TestClass]
    public class MessureResultTests : BaseTest
    {
        private MouthCalculationEntity mouthMessure;
        private RoothCalculationEntity theethMessure;

        [TestInitialize]
        public void TestInit()
        {
            SetDefaultMessuresInputsValue();
        }

        [TestMethod]
        public void GetResultShouldSuccess()
        {
            var provider = new MessuresResultsProvider(mouthMessure, theethMessure);

            var results = provider.GetResult();

            var moyers = results.Moyers;

            Assert.AreEqual(moyers.RightSuperior, (decimal)0.9, "Superior Right Invalid");
            Assert.AreEqual(moyers.LeftSuperior, (decimal)-2.1, "Superior Left Invalid");
            Assert.AreEqual(moyers.RightInferior, (decimal)-3.8, "Inferior Right Invalid");
            Assert.AreEqual(moyers.LeftInferior, (decimal)4.2, "Inferior Left Invalid");

            var pontResult = results.Pont;

            Assert.AreEqual(pontResult.Pont14To24, (decimal)33.5, "14 to 24 Invalid");
            Assert.AreEqual(pontResult.Pont16To26, (decimal)43, "16 to 26 Invalid");
            Assert.AreEqual(pontResult.ArchLong, (decimal)16.5, "Arch Long Invalid");

            var tanakaResult = results.Tanaka;

            Assert.AreEqual(tanakaResult.Inferior, (decimal)22.5, "Inferior Invalid");
            Assert.AreEqual(tanakaResult.Superior, (decimal)23, "Superior Invalid");

            var boltonResult = results.BoltonTotal;

            Assert.AreEqual(boltonResult.SuperiorExcess, (decimal)1, "Superior excess Invalid");
            Assert.AreEqual(boltonResult.InferiorExcess, (decimal)-0.6, "Inferior excess Invalid");
            Assert.AreEqual(boltonResult.Mandibular12Ideal, (decimal)88.6, "Ideal Mandibular Patient Invalid");
            Assert.AreEqual(boltonResult.Mandibular12Pac, (decimal)88, "Mandibular Patient Invalid");
            Assert.AreEqual(boltonResult.Maxilar12Ideal, (decimal)96, "Ideal Maxilar Patient Invalid");
            Assert.AreEqual(boltonResult.Maxilar12Pac, (decimal)97, "Maxilar Patient Invalid");

            var previousBoltonResult = results.BoltonPreviousRelation;

            Assert.AreEqual(previousBoltonResult.SuperiorExcess, (decimal)-5, "Superior excess Invalid");
            Assert.AreEqual(previousBoltonResult.InferiorExcess, (decimal)4, "Inferior excess Invalid");
            Assert.AreEqual(previousBoltonResult.Mandibular6Ideal, (decimal)34, "Ideal Mandibular Patient Invalid");
            Assert.AreEqual(previousBoltonResult.Mandibular6Pac, (decimal)38, "Mandibular Patient Invalid");
            Assert.AreEqual(previousBoltonResult.Maxilar6Ideal, (decimal)49, "Ideal Maxilar Patient Invalid");
            Assert.AreEqual(previousBoltonResult.Maxilar6Pac, (decimal)44, "Maxilar Patient Invalid");
        }

        private void SetDefaultMessuresInputsValue()
        {
            mouthMessure = SetMouthDefaultMessures();
            theethMessure = SetRoothTheetDefaultMessures();
        }
    }
}
