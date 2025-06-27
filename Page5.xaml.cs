using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;

namespace SecuBot.GUI
{
    /// <summary>
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        private int score;
        private int total;

        public Page5(int score, int total)
        {
            InitializeComponent();
            MessageBox.Show(ResultText.Text); // Confirm it's not blank

            this.score = score;
            this.total = total;

            ResultText.Text = $"You scored {score} out of {total}!";

            double percent = ((double)score / total) * 100;
            if (percent >= 90)
                MessageText.Text = "Excellent! You're a cybersecurity pro! 🔐";
            else if (percent >= 70)
                MessageText.Text = "Nice job! You know your stuff! 👍";
            else if (percent >= 50)
                MessageText.Text = "Not bad! A little more practice and you'll be a pro.";
            else
                MessageText.Text = "Keep learning and stay safe online! 💡";
        }

        private void PlayAgain_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page4());
        }

        private void ExitGame_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.ChatPageInstance.ReceiveGameResultMessage(score, total);
            NavigationService.Navigate(MainWindow.ChatPageInstance);
        }
    }
}
