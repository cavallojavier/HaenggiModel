using HaenggiModel.Model;

namespace CalculationHelper.Calculators
{
    public static class DentalBoneDiscrepancyCalculator
    {
        public static DentalBoneDiscrepancy GetResult(MouthCalculationEntity mouseMessure, RoothCalculationEntity theethMessure)
        {
            var bonesSpaces = BonesSpaces.GetBonesCalculation(mouseMessure);
            var theeths = TheethsSum.GetResults(theethMessure);

            var dentalDiscrepancy = new DentalBoneDiscrepancy();

            dentalDiscrepancy.Superior = bonesSpaces.PerineoSuperiorArch - theeths.SumSuperiorTen;
            dentalDiscrepancy.Inferior = bonesSpaces.PerineoInferiorArch - theeths.SumInferiorTen;

            dentalDiscrepancy.SuperiorAntero = bonesSpaces.Bones13To23 - theeths.SumSuperiorSix;
            dentalDiscrepancy.InferiorAntero = bonesSpaces.Bones33To43 - theeths.SumInferiorSix;

            dentalDiscrepancy.InferiorIncisives = bonesSpaces.InferiorBonesIntercanine - theeths.SumInferiorFour;

            return dentalDiscrepancy;
        }
    }
}
