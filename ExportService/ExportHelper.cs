using CalculationHelper.Entities;
using System.IO;

namespace ExportService
{
    public class ExportHelper
    {
        /// <summary>
        /// Exports to PDF.
        /// </summary>
        /// <param name="roothCalculation">The rooth calculation.</param>
        /// <param name="mouthCalculation">The mouth calculation.</param>
        /// <param name="patientInformation">The patient information.</param>
        /// <param name="result">The result.</param>
        public static void ExportToPdf(RoothCalculationEntity roothCalculation,
                                        MouthCalculationEntity mouthCalculation,
                                            MessureInformation patientInformation,
                                                ResultsMessures result)
        {
            var exporter = new ExportToPdf();
            exporter.Export(roothCalculation, mouthCalculation, patientInformation, result);
        }

        public static Stream ExportToPdf(ResultsMessures result, MessureInformation patientInformation, string fileName)
        {
            var exporter = new ExportToPdf();
            return exporter.Export(result, patientInformation, fileName);
        }

        /// <summary>
        /// Exports to exccel.
        /// </summary>
        /// <param name="roothCalculation">The rooth calculation.</param>
        /// <param name="mouthCalculation">The mouth calculation.</param>
        /// <param name="patientInformation">The patient information.</param>
        /// <param name="result">The result.</param>
        public static void ExportToExcel(RoothCalculationEntity roothCalculation,
                                        MouthCalculationEntity mouthCalculation,
                                            MessureInformation patientInformation,
                                                ResultsMessures result)
        {
            var exporter = new ExportToExcel();
            exporter.Export(roothCalculation, mouthCalculation, patientInformation, result);
        }
    }
}
