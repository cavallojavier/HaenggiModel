using System.Windows;
using System.Windows.Controls;
using HaenggiModel.CalculationHelper;
using HaenggiModel.Model;
using HaenggiModel.ExportService;

namespace HaenggiModel.Presentation.UserControls
{
    /// <summary>
    /// Interaction logic for ResultUserControl.xaml
    /// </summary>
    public partial class Result : UserControl
    {
        private readonly RoothCalculationEntity theetMessure;
        private readonly MouthCalculationEntity mouseMessures;
        private readonly MessureInformation patientInformation;
        private readonly ResultsMessures result;

        public Result(RoothCalculationEntity theetMessure, MouthCalculationEntity mouseMessures, MessureInformation information)
        {
            this.mouseMessures = mouseMessures;
            this.theetMessure = theetMessure;
            this.patientInformation = information;

            result = new MessuresResultsProvider(mouseMessures, theetMessure).GetResult();
            
            InitializeComponent();

            SetValues(result);
        }

        /// <summary>
        /// Displays the help.
        /// </summary>
        /// <param name="showHelp">if set to <c>true</c> [show help].</param>
        public void DisplayHelp(bool showHelp)
        {
            // invoke content display help.
        }

        /// <summary>
        /// Handles the form unload.
        /// </summary>
        public void HandleFormUnload()
        {
            
        }

        /// <summary>
        /// Sets the values.
        /// </summary>
        /// <param name="results">The results.</param>
        private void SetValues(ResultsMessures results)
        {
            // Bolton
            this.Bolton.txtPreviousBolton.Text = results.BoltonPreviousRelation.IsSuperiorExcess
                                              ? ConvertResultToString(results.BoltonPreviousRelation.SuperiorExcess)
                                              : ConvertResultToString(results.BoltonPreviousRelation.InferiorExcess);

            this.Bolton.txtBoltonTotal.Text = results.BoltonPreviousRelation.IsSuperiorExcess
                                           ? ConvertResultToString(results.BoltonTotal.SuperiorExcess)
                                           : ConvertResultToString(results.BoltonTotal.InferiorExcess);

            this.Bolton.lblBoltonAnterior.Content = GetBoltonExcessLabel(results.BoltonPreviousRelation.IsSuperiorExcess);
            this.Bolton.lblBoltonTotal.Content = GetBoltonExcessLabel(results.BoltonTotal.IsSuperiorExcess);

            // Tanaka
            this.Tanaka.superior.Text = ConvertResultToString(results.Tanaka.Superior);
            this.Tanaka.inferior.Text = ConvertResultToString(results.Tanaka.Inferior);

            //Pont
            this.Pont.txtPont14To24.Text = ConvertResultToString(results.Pont.Pont14To24);
            this.Pont.txtPont16To26.Text = ConvertResultToString(results.Pont.Pont16To26);
            this.Pont.txtPontArchLong.Text = ConvertResultToString(results.Pont.ArchLong);

            // Disc
            this.Discrepancy.txtDiscSuperior.Text = ConvertResultToString(results.DentalBoneDiscrepancy.Superior);
            this.Discrepancy.txtDiscInferior.Text = ConvertResultToString(results.DentalBoneDiscrepancy.Inferior);
            this.Discrepancy.txtDiscAnteroSup.Text = ConvertResultToString(results.DentalBoneDiscrepancy.SuperiorAntero);
            this.Discrepancy.txtDiscAnteroInf.Text = ConvertResultToString(results.DentalBoneDiscrepancy.InferiorAntero);
            this.Discrepancy.txtDiscIncisivesInf.Text = ConvertResultToString(results.DentalBoneDiscrepancy.InferiorIncisives);

            // Moyers
            this.Moyers.txtMoyersRightSuperior.Text = ConvertResultToString(results.Moyers.RightSuperior);
            this.Moyers.txtMoyersRightInferior.Text = ConvertResultToString(results.Moyers.RightInferior);
            this.Moyers.txtMoyersLeftSuperior.Text = ConvertResultToString(results.Moyers.LeftSuperior);
            this.Moyers.txtMoyersLeftInferior.Text = ConvertResultToString(results.Moyers.LeftInferior);
        }

        /// <summary>
        /// Gets the bolton excess label.
        /// </summary>
        /// <param name="isSuperior">if set to <c>true</c> [is superior].</param>
        /// <returns></returns>
        private string GetBoltonExcessLabel(bool isSuperior)
        {
            return isSuperior ? Properties.Resources.SuperiorExcess : Properties.Resources.InferiorExcess;
        }

        /// <summary>
        /// Converts the result to string.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private string ConvertResultToString(decimal? result)
        {
            return result.HasValue ? result.Value.ToString() : "-";
        }

        /// <summary>
        /// Handles the Click event of the BtnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            dynamic parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.contentControl.Content = new Messures(this.theetMessure, this.mouseMessures, this.patientInformation);
        }

        /// <summary>
        /// Handles the Click event of the BtnExportExcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnExportExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "Excel Files|*.xls, xlsx",
                FilterIndex = 0,
                RestoreDirectory = true,
                FileName = "Haenggi Results.xlsx",
            };

            var fileName = string.Empty;

            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                fileName = saveDialog.FileName;
            }

            ExportToExcel.Export(theetMessure, mouseMessures, patientInformation, result, fileName);
        }

        /// <summary>
        /// Handles the Click event of the BtnExportPdf control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnExportPdf_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "PDF Files|*.pdf",
                FilterIndex = 0,
                RestoreDirectory = true,
                FileName = "Haenggi Results.pdf",
            };

            var fileName = string.Empty;

            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                fileName = saveDialog.FileName;
            }

            ExportToPdf.Export(result, patientInformation, fileName);
        }
    }
}
