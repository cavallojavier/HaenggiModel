using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HaenggiModel.Model;
using DeviceCommunication;

namespace HaenggiModel.Presentation.UserControls
{
    /// <summary>
    /// Interaction logic for Messures.xaml
    /// </summary>
    public partial class Messures : UserControl
    {
        /// <summary>
        /// The serial port
        /// </summary>
        private readonly SerialPort serialPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="Messures"/> class.
        /// </summary>
        public Messures()
        {
            serialPort = DeviceHelper.Device;
            CleanUpTextboxs();
            this.InitForm(new RoothCalculationEntity(), new MouthCalculationEntity(), new MessureInformation() { DateMessure = DateTime.Now });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Messures"/> class.
        /// </summary>
        /// <param name="theetMessure">The theet messure.</param>
        /// <param name="mouseMessures">The mouse messures.</param>
        /// <param name="patientInformation">The patient information.</param>
        public Messures(RoothCalculationEntity theetMessure, MouthCalculationEntity mouseMessures, MessureInformation patientInformation)
        {
            serialPort = DeviceHelper.Device;

            this.InitForm(theetMessure, mouseMessures, patientInformation);
        }

        /// <summary>
        /// Displays the help.
        /// </summary>
        /// <param name="showHelp">if set to <c>true</c> [show help].</param>
        public void DisplayHelp(bool showHelp)
        {
            this.helperBackground.Visibility = showHelp ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Renders the result.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void RenderResult(object sender, RoutedEventArgs e)
        {
            var theeth = MapTheethMessures();
            var mouth = MapMouseMessures();
            var information = MapInformation();

            PortCommunication.ClosePort(serialPort);
            
            dynamic parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.contentControl.Content = new Result(theeth, mouth, information);
        }

        /// <summary>
        /// Removes the received value.
        /// </summary>
        public void HandleFormUnload()
        {
            PortCommunication.ClosePort(serialPort);
        }

        /// <summary>
        /// Initializes the controls.
        /// </summary>
        private void InitForm(RoothCalculationEntity theetMessure, MouthCalculationEntity mouseMessures, MessureInformation patientInformation)
        {
            InitializeComponent();

            this.Dispatcher.ShutdownStarted += CloseForm;

            if (serialPort == null)
            {
                MessageBox.Show(HaenggiModel.Presentation.Properties.Resources.DeviceNotFoundError, HaenggiModel.Presentation.Properties.Resources.Error);
                return;
            }

            DeviceHelper.SetDataEventhandler(ProcessDataReceived);

            SetMouseValues(mouseMessures);

            SetPatientValues(patientInformation);

            SetTheethValues(theetMessure);

            DisplayHelp(false);
        }

        /// <summary>
        /// Handles the Click event of the btnTakeSample control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void TakeSamples(object sender, RoutedEventArgs e)
        {
            if (serialPort == null)
                throw new NullReferenceException(HaenggiModel.Presentation.Properties.Resources.DeviceNotFoundError);

            if (serialPort.IsOpen)
            {
                PortCommunication.ClosePort(serialPort);
                ToogleButtons(true);
            }
            else
            {
                PortCommunication.OpenPort(serialPort);
                txt17.Focus();
                ToogleButtons(false);
            }
        }

        /// <summary>
        /// Called when [focus].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnFocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Foreground = new SolidColorBrush(Colors.DarkBlue);
            }
        }

        /// <summary>
        /// Called when [defocus].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDefocus(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
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

            this.Dispatcher.Invoke(delegate {
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
        /// Maps the theeth messures.
        /// </summary>
        /// <returns></returns>
        private RoothCalculationEntity MapTheethMessures()
        {
            var theethEntity = new RoothCalculationEntity();

            theethEntity.Tooth11 = ParseValue(txt11.Text);
            theethEntity.Tooth12 = ParseValue(txt12.Text);
            theethEntity.Tooth13 = ParseValue(txt13.Text);
            theethEntity.Tooth14 = ParseValue(txt14.Text);
            theethEntity.Tooth15 = ParseValue(txt15.Text);
            theethEntity.Tooth16 = ParseValue(txt16.Text);
            theethEntity.Tooth17 = ParseValue(txt17.Text);

            theethEntity.Tooth21 = ParseValue(txt21.Text);
            theethEntity.Tooth22 = ParseValue(txt22.Text);
            theethEntity.Tooth23 = ParseValue(txt23.Text);
            theethEntity.Tooth24 = ParseValue(txt24.Text);
            theethEntity.Tooth25 = ParseValue(txt25.Text);
            theethEntity.Tooth26 = ParseValue(txt26.Text);
            theethEntity.Tooth27 = ParseValue(txt27.Text);

            theethEntity.Tooth31 = ParseValue(txt31.Text);
            theethEntity.Tooth32 = ParseValue(txt32.Text);
            theethEntity.Tooth33 = ParseValue(txt33.Text);
            theethEntity.Tooth34 = ParseValue(txt34.Text);
            theethEntity.Tooth35 = ParseValue(txt35.Text);
            theethEntity.Tooth36 = ParseValue(txt36.Text);
            theethEntity.Tooth37 = ParseValue(txt37.Text);

            theethEntity.Tooth41 = ParseValue(txt41.Text);
            theethEntity.Tooth42 = ParseValue(txt42.Text);
            theethEntity.Tooth43 = ParseValue(txt43.Text);
            theethEntity.Tooth44 = ParseValue(txt44.Text);
            theethEntity.Tooth45 = ParseValue(txt45.Text);
            theethEntity.Tooth46 = ParseValue(txt46.Text);
            theethEntity.Tooth47 = ParseValue(txt47.Text);

            return theethEntity;
        }

        /// <summary>
        /// Maps the mouse messures.
        /// </summary>
        /// <returns></returns>
        private MouthCalculationEntity MapMouseMessures()
        {
            var mouseEntity = new MouthCalculationEntity();

            mouseEntity.LeftSuperiorCanine = ParseValue(this.txtLeftSuperiorCanine.Text);
            mouseEntity.LeftSuperiorPremolar = ParseValue(this.txtLeftSuperiorPremolar.Text);
            mouseEntity.LeftSuperiorIncisive = ParseValue(this.txtLeftSuperiorIncisive.Text);
            mouseEntity.RightSuperiorCanine = ParseValue(this.txtRightSuperiorCanine.Text);
            mouseEntity.RightSuperiorPremolar = ParseValue(this.txtRightSuperiorPremolar.Text);
            mouseEntity.RightSuperiorIncisive = ParseValue(this.txtRightSuperiorIncisive.Text);

            mouseEntity.LeftInferiorCanine = ParseValue(this.txtLeftInferiorCanine.Text);
            mouseEntity.LeftInferiorPremolar = ParseValue(this.txtLeftInferiorPremolar.Text);
            mouseEntity.LeftInferiorIncisive = ParseValue(this.txtLeftInferiorIncisive.Text);
            mouseEntity.RightInferiorCanine = ParseValue(this.txtRightInferiorCanine.Text);
            mouseEntity.RightInferiorPremolar = ParseValue(this.txtRightInferiorPremolar.Text);
            mouseEntity.RightInferiorIncisive = ParseValue(this.txtRightInferiorIncisive.Text);

            return mouseEntity;
        }

        /// <summary>
        /// Sets the theeth values.
        /// </summary>
        /// <param name="theetMessure">The theet messure.</param>
        private void SetTheethValues(RoothCalculationEntity theetMessure)
        {
            txt11.Text = theetMessure.Tooth11.ToString();
            txt12.Text = theetMessure.Tooth12.ToString();
            txt13.Text = theetMessure.Tooth13.ToString();
            txt14.Text = theetMessure.Tooth14.ToString();
            txt15.Text = theetMessure.Tooth15.ToString();
            txt16.Text = theetMessure.Tooth16.ToString();
            txt17.Text = theetMessure.Tooth17.ToString();

            txt21.Text = theetMessure.Tooth21.ToString();
            txt22.Text = theetMessure.Tooth22.ToString();
            txt23.Text = theetMessure.Tooth23.ToString();
            txt24.Text = theetMessure.Tooth24.ToString();
            txt25.Text = theetMessure.Tooth25.ToString();
            txt26.Text = theetMessure.Tooth26.ToString();
            txt27.Text = theetMessure.Tooth27.ToString();

            txt31.Text = theetMessure.Tooth31.ToString();
            txt32.Text = theetMessure.Tooth32.ToString();
            txt33.Text = theetMessure.Tooth33.ToString();
            txt34.Text = theetMessure.Tooth34.ToString();
            txt35.Text = theetMessure.Tooth35.ToString();
            txt36.Text = theetMessure.Tooth36.ToString();
            txt37.Text = theetMessure.Tooth37.ToString();

            txt41.Text = theetMessure.Tooth41.ToString();
            txt42.Text = theetMessure.Tooth42.ToString();
            txt43.Text = theetMessure.Tooth43.ToString();
            txt44.Text = theetMessure.Tooth44.ToString();
            txt45.Text = theetMessure.Tooth45.ToString(); 
            txt46.Text = theetMessure.Tooth46.ToString();
            txt47.Text = theetMessure.Tooth47.ToString();
        }

        /// <summary>
        /// Sets the mouse values.
        /// </summary>
        /// <param name="mouseMessures">The mouse messures.</param>
        private void SetMouseValues(MouthCalculationEntity mouseMessures)
        {
            this.txtLeftSuperiorCanine.Text = mouseMessures.LeftSuperiorCanine.ToString();
            this.txtLeftSuperiorPremolar.Text = mouseMessures.LeftSuperiorPremolar.ToString();
            this.txtLeftSuperiorIncisive.Text= mouseMessures.LeftSuperiorIncisive.ToString();
            this.txtRightSuperiorCanine.Text = mouseMessures.RightSuperiorCanine.ToString();
            this.txtRightSuperiorPremolar.Text = mouseMessures.RightSuperiorPremolar.ToString();
            this.txtRightSuperiorIncisive.Text = mouseMessures.RightSuperiorIncisive.ToString();

            this.txtLeftInferiorCanine.Text = mouseMessures.LeftInferiorCanine.ToString();
            this.txtLeftInferiorPremolar.Text= mouseMessures.LeftInferiorPremolar.ToString();
            this.txtLeftInferiorIncisive.Text = mouseMessures.LeftInferiorIncisive.ToString();
            this.txtRightInferiorCanine.Text = mouseMessures.RightInferiorCanine.ToString();
            this.txtRightInferiorPremolar.Text = mouseMessures.RightInferiorPremolar.ToString();
            this.txtRightInferiorIncisive.Text = mouseMessures.RightInferiorIncisive.ToString();
        }

        /// <summary>
        /// Sets the patient values.
        /// </summary>
        /// <param name="patientInformation">The patient information.</param>
        private void SetPatientValues(MessureInformation patientInformation)
        {
            txtPatient.Text = patientInformation.PatientName;
            txtProfesional.Text = patientInformation.UserName;
            txtHCNumber.Text = patientInformation.HcNumber;
            dateMessure.Text = patientInformation.DateMessure.ToShortDateString();
        }

        /// <summary>
        /// Parses the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private decimal ParseValue(string value)
        {
            decimal outPut;
            decimal.TryParse(value, out outPut);

            return outPut;
        }

        /// <summary>
        /// Maps the information.
        /// </summary>
        /// <returns></returns>
        private MessureInformation MapInformation()
        {
            var info = new MessureInformation();

            info.DateMessure = Convert.ToDateTime(this.dateMessure.Text);
            info.HcNumber = this.txtHCNumber.Text;
            info.PatientName = this.txtPatient.Text;
            info.UserName = this.txtProfesional.Text;

            return info;
        }

        /// <summary>
        /// Toogles the buttons.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        private void ToogleButtons(bool enabled)
        {
            btnTakeSampleText.Text = enabled ? HaenggiModel.Presentation.Properties.Resources.TakeSample
                    : HaenggiModel.Presentation.Properties.Resources.Stop;

            btnResults.IsEnabled = enabled;
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

        /// <summary>
        /// Cleans up textboxs.
        /// </summary>
        private void CleanUpTextboxs()
        {
            foreach (TextBox item in FindVisualChildren<TextBox>(this))
            {
                item.Text = string.Empty;
            }
        }

        /// <summary>
        /// Finds the visual children.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj">The dep object.</param>
        /// <returns></returns>
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
