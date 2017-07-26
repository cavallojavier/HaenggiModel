using System.Windows;
using System.Windows.Controls;
using HaenggiModel.CalculationHelper;
using HaenggiModel.Model;
using HaenggiModel.ExportService;
using HaenggiModel.Presentation.ViewModels;

namespace HaenggiModel.Presentation.UserControls
{
    /// <summary>
    /// Interaction logic for ResultUserControl.xaml
    /// </summary>
    public partial class Result : UserControl
    {
        private readonly RoothCalculationEntity theetMessure;
        private readonly MouthCalculationEntity mouseMessures;
        private readonly PatientInformation patientInformation;
        private readonly ResultsMessures result;

        public Result(MessuresViewModel messureViewModule)
        {
            this.mouseMessures = messureViewModule.mouseModel;
            this.theetMessure = messureViewModule.roothModel;
            this.patientInformation = messureViewModule.patientModel;

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
            this.Bolton.DataContext = new BoltonViewModel(results.BoltonTotal, results.BoltonPreviousRelation);

            // Tanaka
            this.Tanaka.DataContext = new TanakaViewModel(results.Tanaka);

            //Pont
            this.Pont.DataContext = new PontViewModel(results.Pont);

            // Disc
            this.Discrepancy.DataContext = new DentalBoneDiscrepancyViewModel(results.DentalBoneDiscrepancy);

            // Moyers
            this.Moyers.DataContext = new MoyersViewModel(results.Moyers);
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
            var patientFileName = "Haenggi Results - " + patientInformation.PatientName + ".xlsx";
            var saveDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "Excel Files|*.xls, xlsx",
                FilterIndex = 0,
                RestoreDirectory = true,
                FileName = patientFileName,
            };

            var fileName = string.Empty;

            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                fileName = saveDialog.FileName;
                ExportToExcel.Export(theetMessure, mouseMessures, patientInformation, result, fileName);
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnExportPdf control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnExportPdf_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var patientFileName = "Haenggi Results - " + patientInformation.PatientName + ".pdf";
            var saveDialog = new Microsoft.Win32.SaveFileDialog()
            {
                Filter = "PDF Files|*.pdf",
                FilterIndex = 0,
                RestoreDirectory = true,
                FileName = patientFileName,
            };

            var fileName = string.Empty;

            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                fileName = saveDialog.FileName;
                ExportToPdf.Export(result, patientInformation, fileName);
            }
        }
    }
}
