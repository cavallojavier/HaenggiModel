namespace CalculationHelper.Entities
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

            bonesSpaces.PerineoSuperiorArch = mouseMessure.LeftSuperiorCanine + mouseMessure.LeftSuperiorIncisive +
                                      mouseMessure.LeftSuperiorPremolar +
                                      mouseMessure.RightSuperiorCanine + mouseMessure.RightSuperiorIncisive +
                                      mouseMessure.RightSuperiorPremolar;

            bonesSpaces.PerineoInferiorArch = mouseMessure.LeftInferiorCanine + mouseMessure.LeftInferiorIncisive +
                                      mouseMessure.LeftInferiorPremolar +
                                      mouseMessure.RightInferiorCanine + mouseMessure.RightInferiorIncisive +
                                      mouseMessure.RightInferiorPremolar;

            bonesSpaces.SuperiorBonesIntercanine = mouseMessure.LeftSuperiorIncisive + mouseMessure.RightSuperiorIncisive;
            bonesSpaces.InferiorBonesIntercanine = mouseMessure.LeftInferiorIncisive + mouseMessure.RightInferiorIncisive;

            bonesSpaces.Bones13To23 = mouseMessure.LeftSuperiorIncisive + mouseMessure.LeftSuperiorCanine +
                              mouseMessure.RightSuperiorIncisive + mouseMessure.RightSuperiorCanine;

            bonesSpaces.Bones33To43 = mouseMessure.LeftInferiorIncisive + mouseMessure.LeftInferiorCanine +
                              mouseMessure.RightInferiorIncisive + mouseMessure.RightInferiorCanine;

            return bonesSpaces;
        }
    }
}
