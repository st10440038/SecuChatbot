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

namespace SecuBot.GUI
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {

        private class Question
        {
            public string Text { get; set; }
            public List<string> Choices { get; set; } // null means True/False type
            public string CorrectAnswer { get; set; }
            public string Explanation { get; set; }  // Explanation for feedback
            public bool IsTrueFalse => Choices == null;
        }


        private List<Question> questions;
        private int currentIndex = 0;
        private int score = 0;


        public Page3()
        {
            InitializeComponent();
            LoadQuestions();
            ShowQuestion();
        }


        private void LoadQuestions()
        {
            questions = new List<Question>()
            {
                new Question
                {
                    Text = "What does VPN stand for?",
                    Choices = new List<string>{ "Virtual Private Network", "Very Personal Network", "Virtual Public Network", "Verified Private Node" },
                    CorrectAnswer = "Virtual Private Network",
                    Explanation = "A VPN creates a secure, encrypted connection to protect your data online."
                },
                new Question
                {
                    Text = "True or False: Phishing attacks try to steal personal information by pretending to be a trustworthy entity.",
                    Choices = new List<string>{ "True", "False" },
                    CorrectAnswer = "True",
                    Explanation = "Phishing uses fake messages or websites to trick you into giving personal info."
                },
                new Question
                {
                    Text = "Which one is a strong password example?",
                    Choices = new List<string>{ "password123", "123456", "P@ssw0rd!", "qwerty" },
                    CorrectAnswer = "P@ssw0rd!",
                    Explanation = "Strong passwords mix letters, numbers, and symbols to stay secure."
                },
                new Question
                {
                    Text = "True or False: It's safe to use the same password for multiple accounts.",
                    Choices = new List<string>{ "True", "False" },                    
                    CorrectAnswer = "False",
                    Explanation = "Reusing passwords risks multiple accounts if one is hacked."
                },
                new Question
                {
                    Text = "What is two-factor authentication (2FA)?",
                    Choices = new List<string>{ "Using two passwords", "Using a password and a second form of verification", "Logging in twice", "Using two usernames" },
                    CorrectAnswer = "Using a password and a second form of verification",
                    Explanation = "2FA adds a second step (like a code) to improve login security."
                },
                new Question
                {
                    Text = "True or False: Public Wi-Fi networks are always secure.",
                    Choices = new List<string>{ "True", "False" },
                    CorrectAnswer = "False",
                    Explanation = "Public Wi-Fi can be unsafe; avoid sensitive activities on them."
                },
                new Question
                {
                    Text = "Malware is:",
                    Choices = new List<string>{ "A type of software", "A hardware component", "An antivirus program", "A strong password" },
                    CorrectAnswer = "A type of software",
                    Explanation = "Malware is harmful software designed to damage or gain unauthorized access."
                },
                new Question
                {
                    Text = "True or False: Regular software updates can help improve security.",
                    Choices = new List<string>{ "True", "False" },
                    CorrectAnswer = "True",
                    Explanation = "Updates patch security holes and protect against new threats."
                },
                new Question
                {
                    Text = "What is a firewall used for?",
                    Choices = new List<string>{ "Prevent unauthorized access", "Speed up internet", "Store files", "Increase battery life" },
                    CorrectAnswer = "Prevent unauthorized access",
                    Explanation = "Firewalls monitor and block suspicious network traffic to protect your system."
                },
                new Question
                {
                    Text = "True or False: Encrypting data helps protect it from unauthorized access.",
                    Choices = new List<string>{ "True", "False" },
                    CorrectAnswer = "True",
                    Explanation = "Encryption converts data into code so only authorized users can read it."
                }
            };
        }

        private void ShowQuestion()
        {
            var q = questions[currentIndex];
            QuestionNumberText.Text = $"Question {currentIndex + 1} of {questions.Count}";
            QuestionText.Text = q.Text;
            AnswersPanel.Children.Clear();
            FeedbackText.Text = "";
            FeedbackBorder.Visibility = Visibility.Collapsed;
            NextButton.IsEnabled = false;

            foreach (var choice in q.Choices)
            {
                var rb = new RadioButton { Content = choice, GroupName = "ChoicesGroup" };
                rb.Checked += Answer_Checked;
                AnswersPanel.Children.Add(rb);
            }
        }


        private void Answer_Checked(object sender, RoutedEventArgs e)
        {
            var selected = (sender as RadioButton)?.Content.ToString();
            var q = questions[currentIndex];

            foreach (RadioButton rb in AnswersPanel.Children)
                rb.IsEnabled = false;

            if (selected == q.CorrectAnswer)
            {
                FeedbackText.Text = $"✅ Correct! {q.Explanation}";
                score++;
            }
            else
            {
                FeedbackText.Text = $"❌ Incorrect! {q.Explanation}";
            }

            FeedbackBorder.Visibility = Visibility.Visible;
            NextButton.IsEnabled = true;
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            if (currentIndex == questions.Count)
            {
                NavigationService.Navigate(new Page5(score, questions.Count));
            }
            else
            {
                ShowQuestion();
            }
        }



    }
}

