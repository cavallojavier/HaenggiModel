using CalculationHelper.CalculationTables;
using HaenggiModel.Model;

namespace CalculationHelper.Calculators
{
    public class MoyersCalculator
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="mouthMessure">The mouth messure.</param>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static Moyers GetResult(MouthCalculationEntity mouthMessure, RoothCalculationEntity theethMessure)
        {
            var result = new Moyers();

            var theeths = TheethsSum.GetResults(theethMessure);
            var mouthCalculations = MouthSum.GetResults(mouthMessure);

            var incisivesSum = theeths.SumInferiorFour;

            result.RightSuperior = GetMoyerResult(incisivesSum, mouthCalculations.RightSuperiorAvailableSpace, true);

            result.RightInferior = GetMoyerResult(incisivesSum, mouthCalculations.RightInferiorAvailableSpace, false);

            result.LeftSuperior = GetMoyerResult(incisivesSum, mouthCalculations.LeftSuperiorAvailableSpace, true);

            result.LeftInferior = GetMoyerResult(incisivesSum, mouthCalculations.LeftInferiorAvailableSpace, false);

            return result;
        }

        /// <summary>
        /// Gets the moyer result.
        /// </summary>
        /// <param name="inferiorIncisivesSum">The inferior incisives sum.</param>
        /// <param name="availableSpace">The available space.</param>
        /// <param name="isSuperior">if set to <c>true</c> [is superior].</param>
        /// <returns></returns>
        private static decimal? GetMoyerResult(decimal inferiorIncisivesSum, decimal availableSpace, bool isSuperior)
        {
            // Find reference in Moyers table
            var referenceValue = isSuperior ? MoyersTable.FindMoyerSuperiorValue(inferiorIncisivesSum)
                                                : MoyersTable.FindMoyerInferiorValue(inferiorIncisivesSum);

            // Moyers = step 4 - step 1
            return availableSpace - referenceValue;
        }
    }
}
