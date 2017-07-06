using CalculationHelper.Entities;

namespace ExportService
{
    public interface IExporter
    {
        /// <summary>
        /// Exports the specified rooth calculation.
        /// </summary>
        /// <param name="roothCalculation">The rooth calculation.</param>
        /// <param name="mouthCalculation">The mouth calculation.</param>
        /// <param name="patientInformation">The patient information.</param>
        /// <param name="result">The result.</param>
        void Export(RoothCalculationEntity roothCalculation,
                                        MouthCalculationEntity mouthCalculation,
                                            MessureInformation patientInformation,
                                                ResultsMessures result);
    }
}
