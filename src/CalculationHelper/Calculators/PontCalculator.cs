using System;
using HaenggiModel.Model;

namespace HaenggiModel.CalculationHelper.Calculators
{
    public class PontCalculator
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static Pont GetResult(RoothCalculationEntity theethMessure)
        {
            var pontResult = new Pont();

            var sumResult = TheethsSum.GetResults(theethMessure);
            pontResult.Pont14To24 = CalculationBase.RoundUpResult(sumResult.SumSuperiorFour * 100 / 84);
            pontResult.Pont16To26 = CalculationBase.RoundUpResult(sumResult.SumSuperiorFour * 100 / 65);
            
            pontResult.ArchLong = CalculationTables.PontTable.FindPontValue(sumResult.SumSuperiorFour);

            return pontResult;
        }
    }
}
