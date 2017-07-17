namespace HaenggiModel.Model
{
    public class BonesSpaces
    {
        public decimal PerineoSuperiorArch { get; set; }
        
        public decimal PerineoInferiorArch { get; set; }

        public decimal SuperiorBonesIntercanine { get; set; }
        
        public decimal InferiorBonesIntercanine { get; set; }

        public decimal Bones13To23 { get; set; }
        
        public decimal Bones33To43 { get; set; }

        public static BonesSpaces GetBonesCalculation(MouthCalculationEntity mouseMessure)
        {
            var bonesSpaces = new BonesSpaces();

            bonesSpaces.PerineoSuperiorArch = mouseMessure.LeftSuperiorCanine.Value + mouseMessure.LeftSuperiorIncisive.Value +
                                      mouseMessure.LeftSuperiorPremolar.Value +
                                      mouseMessure.RightSuperiorCanine.Value + mouseMessure.RightSuperiorIncisive.Value +
                                      mouseMessure.RightSuperiorPremolar.Value;

            bonesSpaces.PerineoInferiorArch = mouseMessure.LeftInferiorCanine.Value + mouseMessure.LeftInferiorIncisive.Value +
                                      mouseMessure.LeftInferiorPremolar.Value +
                                      mouseMessure.RightInferiorCanine.Value + mouseMessure.RightInferiorIncisive.Value +
                                      mouseMessure.RightInferiorPremolar.Value;

            bonesSpaces.SuperiorBonesIntercanine = mouseMessure.LeftSuperiorIncisive.Value + mouseMessure.RightSuperiorIncisive.Value;
            bonesSpaces.InferiorBonesIntercanine = mouseMessure.LeftInferiorIncisive.Value + mouseMessure.RightInferiorIncisive.Value;

            bonesSpaces.Bones13To23 = mouseMessure.LeftSuperiorIncisive.Value + mouseMessure.LeftSuperiorCanine.Value +
                              mouseMessure.RightSuperiorIncisive.Value + mouseMessure.RightSuperiorCanine.Value;

            bonesSpaces.Bones33To43 = mouseMessure.LeftInferiorIncisive.Value + mouseMessure.LeftInferiorCanine.Value +
                              mouseMessure.RightInferiorIncisive.Value + mouseMessure.RightInferiorCanine.Value;

            return bonesSpaces;
        }
    }
}
