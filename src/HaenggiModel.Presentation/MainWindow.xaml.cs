using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using HaenggiModel.DeviceCommunication;
using HaenggiModel.Presentation.UserControls;

namespace HaenggiModel.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            // Load the default skin.
            var mainGrid = this.Content as Grid;
            var item = mainGrid.ContextMenu.Items[0] as MenuItem;
            this.ApplySkinFromMenuItem(item);

            this.contentControl.Content = new Messures();
        }

        /// <summary>
        /// Called when [menu item click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as MenuItem;

            // Update the checked state of the menu items.
            var mainGrid = this.Content as Grid;

            foreach (MenuItem mi in mainGrid.ContextMenu.Items)
            {
                mi.IsChecked = mi.Equals(item);
            }

            // Load the selected skin.
            this.ApplySkinFromMenuItem(item);
        }

        /// <summary>
        /// Applies the skin from menu item.
        /// </summary>
        /// <param name="item">The item.</param>
        private void ApplySkinFromMenuItem(MenuItem item)
        {
            // Get a relative path to the ResourceDictionary which
            // contains the selected skin.
            var skinDictPath = item.Tag as string;
            var skinDictUri = new Uri(skinDictPath, UriKind.Relative);

            // Tell the Application to load the skin resources.
            var app = Application.Current as App;
            app.ApplySkin(skinDictUri);
        }

        /// <summary>
        /// Loads the index.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void LoadIndex(object sender, RoutedEventArgs e)
        {
            dynamic currentContent = this.contentControl.Content;
            currentContent.HandleFormUnload();

            this.contentControl.Content = new Messures();
            ShowHideDisplay(false);
        }

        /// <summary>
        /// Loads the calibrator.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void LoadCalibrator(object sender, RoutedEventArgs e)
        {
            dynamic currentContent = this.contentControl.Content;
            currentContent.HandleFormUnload();

            this.contentControl.Content = new Calibrator();
            ShowHideDisplay(false);
        }

        /// <summary>
        /// Displays the help.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        public void DisplayHelp(object sender, RoutedEventArgs e)
        {
            var showHelp = this.helperBackground.Visibility.Equals(Visibility.Collapsed);
            this.ShowHideDisplay(showHelp);
        }

        /// <summary>
        /// Shows the hide display.
        /// </summary>
        /// <param name="showHelp">if set to <c>true</c> [show help].</param>
        private void ShowHideDisplay(bool showHelp)
        {
            this.helperBackground.Visibility = showHelp ? Visibility.Visible : Visibility.Collapsed;

            // invoke content display help.
            dynamic messureHelp = this.contentControl.Content;
            messureHelp.DisplayHelp(showHelp);

            this.contentControl.IsEnabled = !showHelp;

            btnHelp.Style = (showHelp ? FindResource("activeImageButton_base") : FindResource("imageButton")) as Style;
        }
    }
}
