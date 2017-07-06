using System;
using HaenggiModel.Model;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace ExportService
{
    public class ExportToExcel
    {
        private static string templatePath = "\\Resources\\HaenggiCalculationResult.xlsx";
        private const string extension = "xlsx";
        private const string fileName = "{0}_{1}.{2}"; //{patientName}_{date}_{extentions}
        private const string roothSuperiorIndex = "11";
        private const string roothInferiorIndex = "13";

        private static string destinationPath;
        private static Application wapp;
        private static Workbook wbook;

        public static void Export(RoothCalculationEntity roothCalculation, 
                                        MouthCalculationEntity mouthCalculation, 
                                            MessureInformation patientInformation, 
                                                ResultsMessures result,
                                                string filePath)
        {
            // then go and load this into excel
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            
            wapp = new Application();
            try
            {
                wbook = wapp.Workbooks.Open(directory + templatePath, true, true,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                wbook.Title = filePath.Substring(filePath.LastIndexOf("/"), filePath.LastIndexOf("."));

                SetDataIntoSheetMessure(wbook.Worksheets.get_Item(1), roothCalculation, mouthCalculation, patientInformation);

                SetDataIntoSheetResult(wbook.Worksheets.get_Item(2), result);

                wbook.SaveCopyAs(filePath);
                wbook.Close(false);
                //SaveFile(patientInformation.PatientName);
            }
            catch (Exception)
            {
                wbook.Close(false);
                throw new Exception("Error al intentar exportar el resultado.");
            }
            finally
            {
                wapp.Workbooks.Close();
                wapp.Quit();
            }
        }

        /// <summary>
        /// Sets the data into sheet messure.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="roothCalculation">The rooth calculation.</param>
        /// <param name="mouthCalculation">The mouth calculation.</param>
        private static void SetDataIntoSheetMessure(Worksheet sheet, RoothCalculationEntity roothCalculation, MouthCalculationEntity mouthCalculation, MessureInformation patientInformation)
        {
            sheet.Activate();

            SetRoothValues(sheet, roothCalculation);

            SetMouthValues(sheet, mouthCalculation);

            SetPatientInfo(sheet, patientInformation);
        }

        /// <summary>
        /// Sets the data into sheet result.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="result">The result.</param>
        private static void SetDataIntoSheetResult(Worksheet sheet, ResultsMessures result)
        {
            // DentalBoneDiscrepancy
            sheet.get_Range("G4", "G4").Value2 = result.DentalBoneDiscrepancy.Superior;
            sheet.get_Range("G5", "G5").Value2 = result.DentalBoneDiscrepancy.Inferior;
            sheet.get_Range("G6", "G6").Value2 = result.DentalBoneDiscrepancy.SuperiorAntero;
            sheet.get_Range("G7", "G7").Value2 = result.DentalBoneDiscrepancy.InferiorAntero;
            sheet.get_Range("G8", "G8").Value2 = result.DentalBoneDiscrepancy.InferiorIncisives;

            // Moyers
            sheet.get_Range("K4", "K4").Value2 = result.Moyers.RightSuperior;
            sheet.get_Range("K5", "K5").Value2 = result.Moyers.RightInferior;
            sheet.get_Range("L4", "L4").Value2 = result.Moyers.LeftSuperior;
            sheet.get_Range("L5", "L5").Value2 = result.Moyers.LeftInferior;

            // Pont
            sheet.get_Range("C13", "C13").Value2 = result.Pont.Pont14To24;
            sheet.get_Range("D13", "D13").Value2 = result.Pont.Pont16To26;
            sheet.get_Range("E13", "E13").Value2 = result.Pont.ArchLong;

            // Tanaka
            sheet.get_Range("C8", "C8").Value2 = result.Tanaka.Superior;
            sheet.get_Range("D8", "D8").Value2 = result.Tanaka.Inferior;

            // Bolton
            // Labels
            sheet.get_Range("C4", "C4").Value2 = GetBoltonExcessLabel(result.BoltonTotal.IsSuperiorExcess);
            sheet.get_Range("C5", "C5").Value2 = GetBoltonExcessLabel(result.BoltonPreviousRelation.IsSuperiorExcess);

            // Values
            sheet.get_Range("D4", "D4").Value2 = result.BoltonPreviousRelation.IsSuperiorExcess
                                           ? ConvertResultToString(result.BoltonTotal.SuperiorExcess)
                                           : ConvertResultToString(result.BoltonTotal.InferiorExcess);

            sheet.get_Range("D5", "D5").Value2 = result.BoltonPreviousRelation.IsSuperiorExcess
                                              ? ConvertResultToString(result.BoltonPreviousRelation.SuperiorExcess)
                                              : ConvertResultToString(result.BoltonPreviousRelation.InferiorExcess);
        }

        /// <summary>
        /// Sets the rooth values.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="roothCalculation">The rooth calculation.</param>
        private static void SetRoothValues(Worksheet sheet, RoothCalculationEntity roothCalculation)
        {
            sheet.get_Range("C" + roothSuperiorIndex, "C" + roothSuperiorIndex).Value2 = roothCalculation.Tooth17;
            sheet.get_Range("D" + roothSuperiorIndex, "D" + roothSuperiorIndex).Value2 = roothCalculation.Tooth16;
            sheet.get_Range("E" + roothSuperiorIndex, "E" + roothSuperiorIndex).Value2 = roothCalculation.Tooth15;
            sheet.get_Range("F" + roothSuperiorIndex, "F" + roothSuperiorIndex).Value2 = roothCalculation.Tooth14;
            sheet.get_Range("G" + roothSuperiorIndex, "G" + roothSuperiorIndex).Value2 = roothCalculation.Tooth13;
            sheet.get_Range("H" + roothSuperiorIndex, "H" + roothSuperiorIndex).Value2 = roothCalculation.Tooth12;
            sheet.get_Range("I" + roothSuperiorIndex, "I" + roothSuperiorIndex).Value2 = roothCalculation.Tooth11;
            sheet.get_Range("J" + roothSuperiorIndex, "J" + roothSuperiorIndex).Value2 = roothCalculation.Tooth21;
            sheet.get_Range("K" + roothSuperiorIndex, "K" + roothSuperiorIndex).Value2 = roothCalculation.Tooth22;
            sheet.get_Range("L" + roothSuperiorIndex, "L" + roothSuperiorIndex).Value2 = roothCalculation.Tooth23;
            sheet.get_Range("M" + roothSuperiorIndex, "M" + roothSuperiorIndex).Value2 = roothCalculation.Tooth24;
            sheet.get_Range("N" + roothSuperiorIndex, "N" + roothSuperiorIndex).Value2 = roothCalculation.Tooth25;
            sheet.get_Range("O" + roothSuperiorIndex, "O" + roothSuperiorIndex).Value2 = roothCalculation.Tooth26;
            sheet.get_Range("P" + roothSuperiorIndex, "P" + roothSuperiorIndex).Value2 = roothCalculation.Tooth27;

            sheet.get_Range("C" + roothInferiorIndex, "C" + roothInferiorIndex).Value2 = roothCalculation.Tooth47;
            sheet.get_Range("D" + roothInferiorIndex, "D" + roothInferiorIndex).Value2 = roothCalculation.Tooth46;
            sheet.get_Range("E" + roothInferiorIndex, "E" + roothInferiorIndex).Value2 = roothCalculation.Tooth45;
            sheet.get_Range("F" + roothInferiorIndex, "F" + roothInferiorIndex).Value2 = roothCalculation.Tooth44;
            sheet.get_Range("G" + roothInferiorIndex, "G" + roothInferiorIndex).Value2 = roothCalculation.Tooth43;
            sheet.get_Range("H" + roothInferiorIndex, "H" + roothInferiorIndex).Value2 = roothCalculation.Tooth42;
            sheet.get_Range("I" + roothInferiorIndex, "I" + roothInferiorIndex).Value2 = roothCalculation.Tooth41;
            sheet.get_Range("J" + roothInferiorIndex, "J" + roothInferiorIndex).Value2 = roothCalculation.Tooth31;
            sheet.get_Range("K" + roothInferiorIndex, "K" + roothInferiorIndex).Value2 = roothCalculation.Tooth32;
            sheet.get_Range("L" + roothInferiorIndex, "L" + roothInferiorIndex).Value2 = roothCalculation.Tooth33;
            sheet.get_Range("M" + roothInferiorIndex, "M" + roothInferiorIndex).Value2 = roothCalculation.Tooth34;
            sheet.get_Range("N" + roothInferiorIndex, "N" + roothInferiorIndex).Value2 = roothCalculation.Tooth35;
            sheet.get_Range("O" + roothInferiorIndex, "O" + roothInferiorIndex).Value2 = roothCalculation.Tooth36;
            sheet.get_Range("P" + roothInferiorIndex, "P" + roothInferiorIndex).Value2 = roothCalculation.Tooth37;
        }

        /// <summary>
        /// Sets the mouth values.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="mouthCalculation">The mouth calculation.</param>
        private static void SetMouthValues(Worksheet sheet, MouthCalculationEntity mouthCalculation)
        {
            sheet.get_Range("S17", "S17").Value2 = mouthCalculation.RightSuperiorPremolar;
            sheet.get_Range("S15", "S15").Value2 = mouthCalculation.RightSuperiorCanine;
            sheet.get_Range("T13", "T13").Value2 = mouthCalculation.RightSuperiorIncisive;
            sheet.get_Range("V13", "V13").Value2 = mouthCalculation.LeftSuperiorIncisive;
            sheet.get_Range("X15", "X15").Value2 = mouthCalculation.LeftSuperiorCanine;
            sheet.get_Range("X17", "X17").Value2 = mouthCalculation.LeftSuperiorPremolar;
            sheet.get_Range("S29", "S29").Value2 = mouthCalculation.RightInferiorPremolar;
            sheet.get_Range("S31", "S31").Value2 = mouthCalculation.RightInferiorCanine;
            sheet.get_Range("T33", "T33").Value2 = mouthCalculation.RightInferiorIncisive;
            sheet.get_Range("V33", "V33").Value2 = mouthCalculation.LeftInferiorIncisive;
            sheet.get_Range("X31", "X31").Value2 = mouthCalculation.LeftInferiorCanine;
            sheet.get_Range("X29", "X29").Value2 = mouthCalculation.LeftInferiorPremolar;
        }

        /// <summary>
        /// Sets the patient information.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="patientInformation">The patient information.</param>
        private static void SetPatientInfo(Worksheet sheet, MessureInformation patientInformation)
        {
            sheet.get_Range("E26", "E26").Value2 = patientInformation.PatientName;
            sheet.get_Range("E27", "E27").Value2 = patientInformation.HcNumber.ToString();
            sheet.get_Range("E28", "E28").Value2 = patientInformation.DateMessure;
            sheet.get_Range("E29", "E29").Value2 = patientInformation.UserName;
        }

        /// <summary>
        /// Gets the bolton excess label.
        /// </summary>
        /// <param name="isSuperior">if set to <c>true</c> [is superior].</param>
        /// <returns></returns>
        private static string GetBoltonExcessLabel(bool isSuperior)
        {
            return isSuperior ? "Exceso Superior" : "Exceso Inferior";
        }

        /// <summary>
        /// Converts the result to string.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private static string ConvertResultToString(decimal? result)
        {
            return result.HasValue ? result.Value.ToString() : "-";
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="patientName">Name of the patient.</param>
        private static void SaveFile(string filePath)
        {
            wbook.SaveCopyAs(filePath);
            wbook.Close(false);
        }
    }
}
