using System;
using System.Windows;
using System.Windows.Controls;
using HaenggiModel.Presentation.UserControls;

namespace HaenggiModel.Presentation
{
    /// <summary>
    /// Interaction logic for Index.xaml
    /// </summary>
    public partial class Index : Page
    {
        public Index()
        {
            InitializeComponent();

            // Load the default skin.
            var mainGrid = this.Content as Grid;
            var item = mainGrid.ContextMenu.Items[0] as MenuItem;
            this.ApplySkinFromMenuItem(item);
        }

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

        public void SetResult()
        {
            this.contentControl.Content = new Result();
        }

        private void ApplySkinFromMenuItem(MenuItem item)
        {
            // Get a relative path to the ResourceDictionary which
            // contains the selected skin.
            string skinDictPath = item.Tag as string;
            Uri skinDictUri = new Uri(skinDictPath, UriKind.Relative);

            // Tell the Application to load the skin resources.
            var app = Application.Current as App;
            app.ApplySkin(skinDictUri);
        }

        private void LoadIndex(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Messures();
        }

        private void LoadCalibrator(object sender, RoutedEventArgs e)
        {
            this.contentControl.Content = new Calibrator();
        }
    }
}
