using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CalculationHelper.CustomExceptions;
using HaenggiModel.Presentation.Properties;

namespace HaenggiModel.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void ApplySkin(Uri skinDictionaryUri)
        {
            // Load the ResourceDictionary into memory.
            var skinDict = Application.LoadComponent(skinDictionaryUri) as ResourceDictionary;

            Collection<ResourceDictionary> mergedDicts = base.Resources.MergedDictionaries;

            // Remove the existing skin dictionary, if one exists.
            // NOTE: In a real application, this logic might need
            // to be more complex, because there might be dictionaries
            // which should not be removed.
            if (mergedDicts.Count > 0)
                mergedDicts.Clear();

            // Apply the selected skin so that all elements in the
            // application will honor the new look and feel.
            mergedDicts.Add(skinDict);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeCultures();

            // Global exception handling  
            Application.Current.DispatcherUnhandledException +=
                new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
        }

        /// <summary>
        /// Initializes the cultures.
        /// </summary>
        private void InitializeCultures()
        {
            if (Thread.CurrentThread.CurrentCulture.Name.Contains(Settings.Default.EnglishCulture + "-"))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.EnglishCulture);
            }
            else if (Thread.CurrentThread.CurrentCulture.Name.Contains(Settings.Default.SpanishCulture + "-"))
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Settings.Default.SpanishCulture);
            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Settings.Default.SpanishCulture);
            }
        }

        /// <summary>
        /// Applications the dispatcher unhandled exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var result = MessageBoxResult.Cancel;
            
            try
            {
                result = ShowUnhandeledException(e.Exception);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                try
                {
                    MessageBox.Show(HaenggiModel.Presentation.Properties.Resources.Error_GeneralError,
                                    HaenggiModel.Presentation.Properties.Resources.Error_GeneralError, MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                finally
                {
                    Application.Current.Shutdown();
                }
            }

            // Exits the program when the user clicks Abort. 
            if (result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        /// <summary>
        /// Shows the unhandeled exception.
        /// </summary>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        private MessageBoxResult ShowUnhandeledException(Exception e)
        {
            var errorMsg = string.Empty;
            var title = string.Empty;

            if (e is CalculationCustomException)
            {
                title = HaenggiModel.Presentation.Properties.Resources.Error_CalculationError;

                errorMsg = HaenggiModel.Presentation.Properties.Resources.Error_CalculationErrorOccurred;
                //errorMsg = errorMsg + e.Message;
            }
            else if (e is ElementNotFoundException)
            {
                title = HaenggiModel.Presentation.Properties.Resources.Error_TableIndexNotFound;

                errorMsg = HaenggiModel.Presentation.Properties.Resources.Error_IndexNotFound;
            }
            else if (e is IOException)
            {
                title = HaenggiModel.Presentation.Properties.Resources.Error_Conexion;

                errorMsg = HaenggiModel.Presentation.Properties.Resources.Error_DeviceNotFound;
            }
            else
            {
                errorMsg = HaenggiModel.Presentation.Properties.Resources.Error_GeneralError;
            }

            try
            {
                Trace.TraceError(errorMsg + e.Message.ToString());
                Trace.TraceInformation("Stack Trace: " + e.StackTrace);
            }
            catch (Exception)
            {
                // Do Nothing
            }
            
            return MessageBox.Show(errorMsg, title, MessageBoxButton.OK, MessageBoxImage.Stop);
        }
    }
}
