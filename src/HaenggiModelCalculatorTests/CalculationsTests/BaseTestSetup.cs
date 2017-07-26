using System.Globalization;
using System.Threading;
using HaenggiModel.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HaenggiModelCalculatorTests.CalculationsTests
{
    public abstract class BaseTest
    {
        [TestInitialize]
        public void Setup()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            this.OnBaseTestSetup();
        }

        protected virtual void OnBaseTestSetup()
        {
        }

        /// <summary>
        /// Sets the rooth theet default messures.
        /// </summary>
        /// <returns></returns>
        public RoothCalculationEntity SetRoothTheetDefaultMessures()
        {
            var roothMessures = new RoothCalculationEntity();

            roothMessures.Tooth11 = 8;
            roothMessures.Tooth12 = 6;
            roothMessures.Tooth13 = 8;
            roothMessures.Tooth14 = 8;
            roothMessures.Tooth15 = 7;
            roothMessures.Tooth16 = 11;
            roothMessures.Tooth17 = 0;

            roothMessures.Tooth21 = 8;
            roothMessures.Tooth22 = 6;
            roothMessures.Tooth23 = 8;
            roothMessures.Tooth24 = 8;
            roothMessures.Tooth25 = 8;
            roothMessures.Tooth26 = 11;
            roothMessures.Tooth27 = 0;

            roothMessures.Tooth31 = 6;
            roothMessures.Tooth32 = 6;
            roothMessures.Tooth33 = 7;
            roothMessures.Tooth34 = 7;
            roothMessures.Tooth35 = 7;
            roothMessures.Tooth36 = 11;
            roothMessures.Tooth37 = 11;

            roothMessures.Tooth41 = 6;
            roothMessures.Tooth42 = 6;
            roothMessures.Tooth43 = 7;
            roothMessures.Tooth44 = 7;
            roothMessures.Tooth45 = 7;
            roothMessures.Tooth46 = 11;
            roothMessures.Tooth47 = 11;

            return roothMessures;
        }

        /// <summary>
        /// Sets the mouth default messures.
        /// </summary>
        public MouthCalculationEntity SetMouthDefaultMessures()
        {
            var mouthMessure = new MouthCalculationEntity();

            mouthMessure.RightSuperiorIncisive = 4;
            mouthMessure.RightSuperiorCanine = 8;
            mouthMessure.RightSuperiorPremolar = 16;

            mouthMessure.LeftSuperiorIncisive = 4;
            mouthMessure.LeftSuperiorCanine = 8;
            mouthMessure.LeftSuperiorPremolar = 13;

            mouthMessure.RightInferiorIncisive = 4;
            mouthMessure.RightInferiorCanine = 8;
            mouthMessure.RightInferiorPremolar = 11;

            mouthMessure.LeftInferiorIncisive = 4;
            mouthMessure.LeftInferiorCanine = 8;
            mouthMessure.LeftInferiorPremolar = 19;

            return mouthMessure;
        }
    }
}
