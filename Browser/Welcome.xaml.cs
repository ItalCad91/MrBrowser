using System.Windows;
using System.Windows.Controls;

namespace Browser
{
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
        }

        public void RefreshWindow()
        {
            // You can reset the content of the window here
            // For example, clear the search bar and reset the welcome message
            Search_bar.Text = string.Empty;
            welcomePanel.Visibility = Visibility.Visible;
        }

        private void WelcomeReload(object sender, RoutedEventArgs e)
        {
            RefreshWindow();
        }

        private void Welcome_Search_bar(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Welcome_Search_Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the search query from the TextBox
            string searchQuery = Search_bar.Text.Trim();

            // Check if the search query is valid (e.g., not empty)
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                // Create and show the main window
                MainWindow mainWindow = new MainWindow(searchQuery); // Pass the search query as a parameter
                mainWindow.Show();

                // Close the welcome window
                Close();
            }
            else
            {
                // Display an error message to the user (optional)
                MessageBox.Show("Please enter a valid search query.");
            }
        }



    }
}
