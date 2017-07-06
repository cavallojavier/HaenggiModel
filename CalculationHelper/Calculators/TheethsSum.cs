using HaenggiModel.Model;
using System.Collections.Generic;
using System.Linq;

namespace HaenggiModel.CalculationHelper.Calculators
{
    public class TheethsSum
    {
        public decimal SumSuperiorTen { get; set; }

        public decimal SumInferiorTen { get; set; }

        public decimal SumSuperiorTwelve { get; set; }

        public decimal SumInferiorTwelve { get; set; }

        public decimal SumSuperiorSix { get; set; }

        public decimal SumInferiorSix { get; set; }

        public decimal SumInferiorFour { get; set; }

        public decimal SumSuperiorFour { get; set; }

        public static TheethsSum GetResults(RoothCalculationEntity theethMessure)
        {
            var result = new TheethsSum();

            var incrementalSuperiorListToSum = new List<decimal>()
                {theethMessure.Tooth11, theethMessure.Tooth12, theethMessure.Tooth21, theethMessure.Tooth22};

            var incrementalInferiorListToSum = new List<decimal>()
                {theethMessure.Tooth41, theethMessure.Tooth42, theethMessure.Tooth31, theethMessure.Tooth32};
            
            // Obtain Sum of minor table
            result.SumSuperiorFour = GetSum(incrementalSuperiorListToSum);
            result.SumInferiorFour = GetSum(incrementalInferiorListToSum);

            // Add fields of next table
            incrementalSuperiorListToSum.Add(theethMessure.Tooth13);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth23);

            incrementalInferiorListToSum.Add(theethMessure.Tooth33);
            incrementalInferiorListToSum.Add(theethMessure.Tooth43);

            result.SumSuperiorSix = GetSum(incrementalSuperiorListToSum);
            result.SumInferiorSix = GetSum(incrementalInferiorListToSum);

            // Add fields of next table
            incrementalSuperiorListToSum.Add(theethMessure.Tooth14);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth15);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth24);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth25);

            incrementalInferiorListToSum.Add(theethMessure.Tooth34);
            incrementalInferiorListToSum.Add(theethMessure.Tooth35);
            incrementalInferiorListToSum.Add(theethMessure.Tooth44);
            incrementalInferiorListToSum.Add(theethMessure.Tooth45);

            result.SumSuperiorTen = GetSum(incrementalSuperiorListToSum);
            result.SumInferiorTen = GetSum(incrementalInferiorListToSum);

            // Add fields of next table
            incrementalSuperiorListToSum.Add(theethMessure.Tooth16);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth26);

            incrementalInferiorListToSum.Add(theethMessure.Tooth36);
            incrementalInferiorListToSum.Add(theethMessure.Tooth46);

            result.SumSuperiorTwelve = GetSum(incrementalSuperiorListToSum);
            result.SumInferiorTwelve = GetSum(incrementalInferiorListToSum);

            return result;
        }

        /// <summary>
        /// Gets the sum.
        /// </summary>
        /// <param name="listToSum">The list to sum.</param>
        /// <returns></returns>
        private static decimal GetSum(IEnumerable<decimal> listToSum)
        {
            return listToSum.Sum();
        }
    }

    public class MouthSum
    {
        public decimal RightSuperiorAvailableSpace { get; set; }

        public decimal RightInferiorAvailableSpace { get; set; }

        public decimal LeftSuperiorAvailableSpace { get; set; }

        public decimal LeftInferiorAvailableSpace { get; set; }

        /// <summary>
        /// Gets the results.
        /// </summary>
        /// <param name="mouthCalculation">The mouth calculation.</param>
        /// <returns></returns>
        public static MouthSum GetResults(MouthCalculationEntity mouthCalculation)
        {
            var result = new MouthSum();

            result.RightSuperiorAvailableSpace = mouthCalculation.RightSuperiorCanine +
                                                 mouthCalculation.RightSuperiorPremolar;

            result.RightInferiorAvailableSpace = mouthCalculation.RightInferiorCanine +
                                                 mouthCalculation.RightInferiorPremolar;

            result.LeftSuperiorAvailableSpace = mouthCalculation.LeftSuperiorCanine +
                                                 mouthCalculation.LeftSuperiorPremolar;

            result.LeftInferiorAvailableSpace = mouthCalculation.LeftInferiorCanine +
                                                 mouthCalculation.LeftInferiorPremolar;

            return result;
        }
    }
}
