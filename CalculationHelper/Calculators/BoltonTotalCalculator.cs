using CalculationHelper.CalculationTables;
using HaenggiModel.Model;

namespace CalculationHelper.Calculators
{
    public class BoltonCalculator
    {
        private const decimal BoltonTotalBreakPoint = (decimal)91.3;
        private const decimal BoltonPreviousBreakPoint = (decimal)77.2;

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static BoltonTotal GetBoltonTotalResult(RoothCalculationEntity theethMessure)
        {
            var result = new BoltonTotal();

            var theeths = TheethsSum.GetResults(theethMessure);

            result.Maxilar12Pac = theeths.SumSuperiorTwelve;
            result.Mandibular12Pac = theeths.SumInferiorTwelve;

            result.Total = CalculationBase.RoundUpResult(result.Mandibular12Pac / result.Maxilar12Pac * 100);

            result.IsSuperiorExcess = result.Total < BoltonTotalBreakPoint;

            result.Maxilar12Ideal = BoltonTable.FindBoltonTotalByMandibularValue(result.Mandibular12Pac);
            result.SuperiorExcess = result.Maxilar12Pac - result.Maxilar12Ideal;

            result.Mandibular12Ideal = BoltonTable.FindBoltonTotalByMaxilarValue(result.Maxilar12Pac);
            result.InferiorExcess = result.Mandibular12Pac - result.Mandibular12Ideal;

            return result;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="theethMessure">The theeth messure.</param>
        /// <returns></returns>
        public static BoltonPreviousRelation GetBoltonPreviousResult(RoothCalculationEntity theethMessure)
        {
            var result = new BoltonPreviousRelation();

            var theeths = TheethsSum.GetResults(theethMessure);

            result.Maxilar6Pac = theeths.SumSuperiorSix;
            result.Mandibular6Pac = theeths.SumInferiorSix;

            result.Total = CalculationBase.RoundUpResult(result.Mandibular6Pac / result.Maxilar6Pac * 100);
            
            result.IsSuperiorExcess = result.Total < BoltonPreviousBreakPoint;

            result.Mandibular6Ideal = BoltonTable.FindPreviousRelationBoltonByMaxilarValue(result.Maxilar6Pac);
            result.InferiorExcess = result.Mandibular6Pac - result.Mandibular6Ideal;

            result.Maxilar6Ideal = BoltonTable.FindPreviousRelationBoltonByMandibularValue(result.Mandibular6Pac);
            result.SuperiorExcess = result.Maxilar6Pac - result.Maxilar6Ideal;

            return result;
        }
    }
}