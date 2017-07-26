using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HaenggiModel.Model;
using HaenggiModel.DeviceCommunication;
using HaenggiModel.Presentation.ViewModels;

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

            this.InitForm(new MessuresViewModel(new RoothCalculationEntity(), new MouthCalculationEntity(), new PatientInformation()));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Messures"/> class.
        /// </summary>
        /// <param name="theetMessure">The theet messure.</param>
        /// <param name="mouseMessures">The mouse messures.</param>
        /// <param name="patientInformation">The patient information.</param>
        public Messures(RoothCalculationEntity theetMessure, MouthCalculationEntity mouseMessures, PatientInformation patientInformation)
        {
            serialPort = DeviceHelper.Device;

            this.InitForm(new MessuresViewModel(theetMessure, mouseMessures, patientInformation));
        }

        public Messures(MessuresViewModel viewModel)
        {
            serialPort = DeviceHelper.Device;
            CleanUpTextboxs();
            this.InitForm(viewModel);
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
            PortCommunication.ClosePort(serialPort);

            var dataContext = this.DataContext as MessuresViewModel;
    
            dynamic parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.contentControl.Content = new Result(dataContext);
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
        private void InitForm(MessuresViewModel viewModel)
        {
            InitializeComponent();

            this.Dispatcher.ShutdownStarted += CloseForm;

            if (serialPort == null)
            {
                MessageBox.Show(HaenggiModel.Presentation.Properties.Resources.DeviceNotFoundError, HaenggiModel.Presentation.Properties.Resources.Error);
                return;
            }

            DeviceHelper.SetDataEventhandler(ProcessDataReceived);

            this.DataContext = viewModel;

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
