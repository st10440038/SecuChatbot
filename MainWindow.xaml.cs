using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SecuBot.GUI
{

    public partial class MainWindow : Window
    {
        public static Page1 ChatPageInstance;
        public static List<string> GlobalActivityLog = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            ChatPageInstance = new Page1(); // create once
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            WelcomePanel.Visibility = Visibility.Collapsed;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(ChatPageInstance);
        }

        // Task Manager button
        private void OpenTaskManager_Click(object sender, RoutedEventArgs e)
        {
            WelcomeCard.Visibility = Visibility.Collapsed;
            MainFrame.Visibility = Visibility.Visible;
            MainFrame.Navigate(new Page2()); // navigate to your Task Manager
        }

        // Home button
        private void GoHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Visibility = Visibility.Collapsed;
            WelcomeCard.Visibility = Visibility.Visible;
        }

    }

}