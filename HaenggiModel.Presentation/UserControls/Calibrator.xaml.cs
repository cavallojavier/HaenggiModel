using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CalculationHelper;
using HaenggiModel.Model;
using DeviceCommunication;

namespace HaenggiModel.Presentation.UserControls
{
    /// <summary>
    /// Interaction logic for Calibrator.xaml
    /// </summary>
    public partial class Calibrator : UserControl
    {
        private readonly SerialPort serialPort;
        private SerialPortEvent serialPortEvent;

        public Calibrator()
        {
            this.serialPort = DeviceHelper.Device;

            this.InitForm();
        }
        
        private void InitForm()
        {
            InitializeComponent();
            
            this.Dispatcher.ShutdownStarted += CloseForm;

            if (serialPort == null)
            {
                MessageBox.Show(HaenggiModel.Presentation.Properties.Resources.DeviceNotFoundError, HaenggiModel.Presentation.Properties.Resources.Error);
                return;
            }

            DeviceHelper.SetDataEventhandler(ProcessDataReceived);
            
            DisplayHelp(false);

            OpenPort();

            Keyboard.Focus(txtSample1);
            txtSample1.Focus();
        }

        /// <summary>
        /// Displays the help.
        /// </summary>
        /// <param name="showHelp">if set to <c>true</c> [show help].</param>
        public void DisplayHelp(bool showHelp)
        {
            // do Nothing
        }

        /// <summary>
        /// Handles the form unload.
        /// </summary>
        public void HandleFormUnload()
        {
            PortCommunication.ClosePort(serialPort);
        }

        /// <summary>
        /// Handles the Click event of the BtnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
        private void BtnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PortCommunication.ClosePort(serialPort);

            dynamic parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.LoadIndex(sender, e);
        }

        /// <summary>
        /// Processes the data received.
        /// </summary>
        /// <param name="data">The data.</param>
        [STAThread]
        private void ProcessDataReceived(byte[] data)
        {
            var result = DeviceHelper.GetReceivedData(data);

            if (result == null)
                return;

            this.Dispatcher.Invoke((Action)delegate {

                SetReceivedValue(string.Format("{0:0.0}", result));
            });
        }

        /// <summary>
        /// Sets the received value.
        /// </summary>
        /// <param name="value">The value.</param>
        private void SetReceivedValue(string value)
        {
            var control = Keyboard.FocusedElement;
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);

            var textBox = (control ?? focusedControl) as TextBox;
            if (textBox != null)
            {
                textBox.Text = value;
                textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CloseForm(object sender, EventArgs e)
        {
            PortCommunication.ClosePort(serialPort);
        }

        private void OpenPort()
        {
            if (serialPort == null)
                throw new NullReferenceException(Properties.Resources.DeviceNotFoundError);

            PortCommunication.OpenPort(serialPort);
        }
    }
}
