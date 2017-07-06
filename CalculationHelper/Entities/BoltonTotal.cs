namespace CalculationHelper.Entities
{
    public class BoltonTotal
    {
        public decimal Maxilar12Pac { get; set; }
        
        public decimal Mandibular12Pac { get; set; }

        public decimal? Mandibular12Ideal { get; set; }

        public decimal? Maxilar12Ideal { get; set; }

        public decimal? InferiorExcess { get; set; }

        public decimal? SuperiorExcess { get; set; }

        public decimal? Total { get; set; }

        public bool IsSuperiorExcess { get; set; }
    }
}
