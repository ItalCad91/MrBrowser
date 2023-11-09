using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Browser
{
    public partial class MainWindow : Window
    {
        private bool previousPageWasWelcome = false;

        public MainWindow(string searchQuery)
        {
            InitializeComponent();

            // Subscribe to the Navigating and Navigated events
            webBrowser1.Navigating += webBrowser1_Navigating;
            webBrowser1.Navigated += webBrowser1_Navigated;

            // Set the search query in the TextBox
            Search_bar.Text = searchQuery;

            // Perform the search operation
            PerformSearch();
        }

        private async void PerformSearch()
        {
            string userInput = Search_bar.Text.Trim();
            if (!Uri.IsWellFormedUriString(userInput, UriKind.Absolute))
            {
                userInput = "http://" + userInput;
            }

            // Use the Dispatcher to navigate on the UI thread
            await Dispatcher.InvokeAsync(() => webBrowser1.Navigate(new Uri(userInput)));
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            bool Previous = webBrowser1.CanGoBack;
            if (!Previous)
            {
                // Go back to the Welcome.xaml page
                WelcomeWindow welcomeWindow = new WelcomeWindow();
                welcomeWindow.Show();

                // Close the current MainWindow
                Close();
            }
            else
            {
                webBrowser1.GoBack();
            }
        }

        private void Reload(object sender, RoutedEventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            bool canGoForward = CanWebBrowserGoForward();

            if (canGoForward)
            {
                // If the WebBrowser can go forward, enable and show the button
                webBrowser1.GoForward();
            }
        }

        private void Search_Button(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }

        private void webBrowser1_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // Disable script error dialogs
            dynamic activeX = this.webBrowser1.GetType().InvokeMember("ActiveXInstance",
                System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, this.webBrowser1, new object[] { });

            activeX.Silent = true;
        }

        private void webBrowser1_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Uri != null && e.Uri.AbsoluteUri.Equals("pack://siteoforigin:,,,/Welcome.xaml"))
            {
                previousPageWasWelcome = true;
            }
            else
            {
                previousPageWasWelcome = false;
            }

            if (webBrowser1.Source != null && webBrowser1.Source.Scheme == "res")
            {
                // Handle resource loading errors
                MessageBox.Show("Resource loading error: " + e.Uri);
            }
        }

        private void Search_bar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private bool CanWebBrowserGoForward()
        {
            // Check if the WebBrowser can go forward
            // You can modify this condition based on your requirements
            return webBrowser1.Source != null && webBrowser1.CanGoForward;
        }
    }


}
