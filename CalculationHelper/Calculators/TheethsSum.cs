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
                {theethMessure.Tooth11.Value, theethMessure.Tooth12.Value, theethMessure.Tooth21.Value, theethMessure.Tooth22.Value};

            var incrementalInferiorListToSum = new List<decimal>()
                {theethMessure.Tooth41.Value, theethMessure.Tooth42.Value, theethMessure.Tooth31.Value, theethMessure.Tooth32.Value};
            
            // Obtain Sum of minor table
            result.SumSuperiorFour = GetSum(incrementalSuperiorListToSum);
            result.SumInferiorFour = GetSum(incrementalInferiorListToSum);

            // Add fields of next table
            incrementalSuperiorListToSum.Add(theethMessure.Tooth13.Value);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth23.Value);

            incrementalInferiorListToSum.Add(theethMessure.Tooth33.Value);
            incrementalInferiorListToSum.Add(theethMessure.Tooth43.Value);

            result.SumSuperiorSix = GetSum(incrementalSuperiorListToSum);
            result.SumInferiorSix = GetSum(incrementalInferiorListToSum);

            // Add fields of next table
            incrementalSuperiorListToSum.Add(theethMessure.Tooth14.Value);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth15.Value);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth24.Value);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth25.Value);

            incrementalInferiorListToSum.Add(theethMessure.Tooth34.Value);
            incrementalInferiorListToSum.Add(theethMessure.Tooth35.Value);
            incrementalInferiorListToSum.Add(theethMessure.Tooth44.Value);
            incrementalInferiorListToSum.Add(theethMessure.Tooth45.Value);

            result.SumSuperiorTen = GetSum(incrementalSuperiorListToSum);
            result.SumInferiorTen = GetSum(incrementalInferiorListToSum);

            // Add fields of next table
            incrementalSuperiorListToSum.Add(theethMessure.Tooth16.Value);
            incrementalSuperiorListToSum.Add(theethMessure.Tooth26.Value);

            incrementalInferiorListToSum.Add(theethMessure.Tooth36.Value);
            incrementalInferiorListToSum.Add(theethMessure.Tooth46.Value);

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

            result.RightSuperiorAvailableSpace = mouthCalculation.RightSuperiorCanine.Value +
                                                 mouthCalculation.RightSuperiorPremolar.Value;

            result.RightInferiorAvailableSpace = mouthCalculation.RightInferiorCanine.Value +
                                                 mouthCalculation.RightInferiorPremolar.Value;

            result.LeftSuperiorAvailableSpace = mouthCalculation.LeftSuperiorCanine.Value +
                                                 mouthCalculation.LeftSuperiorPremolar.Value;

            result.LeftInferiorAvailableSpace = mouthCalculation.LeftInferiorCanine.Value +
                                                 mouthCalculation.LeftInferiorPremolar.Value;

            return result;
        }
    }
}
