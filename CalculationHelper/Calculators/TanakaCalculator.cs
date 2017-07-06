using HaenggiModel.Model;

namespace CalculationHelper.Calculators
{
    public class TanakaCalculator
    {
        public static TanakaJohnston GetResult(RoothCalculationEntity theethMessure)
        {
            var tanakaResult = new TanakaJohnston();

            var inferiorSum = TheethsSum.GetResults(theethMessure).SumInferiorFour;

            tanakaResult.Superior = (inferiorSum / 2) + 11;
            tanakaResult.Inferior = tanakaResult.Superior - (decimal)0.5;

            return tanakaResult;
        }
    }
}
