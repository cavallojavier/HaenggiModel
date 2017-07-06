namespace CalculationHelper.Entities
{
    public class BoltonPreviousRelation
    {
        public decimal Maxilar6Pac { get; set; }

        public decimal Mandibular6Pac { get; set; }

        public decimal? Mandibular6Ideal { get; set; }

        public decimal? Maxilar6Ideal { get; set; }

        public decimal? InferiorExcess { get; set; }

        public decimal? SuperiorExcess { get; set; }

        public decimal? Total { get; set; }

        public bool IsSuperiorExcess { get; set; }
    }
}
