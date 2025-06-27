using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using SecuBot.GUI;



namespace SecuBot.GUI
{
    public partial class Page1 : Page
    {
        private Chatbot bot = new Chatbot();
        private ResponseHandler handlerObj = new ResponseHandler();
        private List<string> activityLog => MainWindow.GlobalActivityLog;
        private List<TaskItem> tasks => TaskManager.Tasks;
        private TaskItem pendingTask = null;

        private string userName = null;
        private string favoriteTopic = null;

        private readonly Dictionary<string, List<string>> intentKeywords = new()
        {
            ["add_task_with_reminder"] = new() { "remind me", "set a reminder", "add a reminder" },
            ["add_task"] = new() { "add task", "set", "create task", "new task", "enable", "check", "setup", "configure" },
            ["show_tasks"] = new() { "show tasks", "my tasks", "list tasks" },
            ["start_quiz"] = new() { "start quiz", "quiz", "play quiz", "take quiz", "game" },
            ["phishing_info"] = new() { "phishing", "scam", "scammer", "email attack", "fake email" },
            ["show_log"] = new() { "activity log", "show log", "activities" }
        };

        public Page1()
        {
            InitializeComponent();
            ChatListBox.Items.Add("Welcome to the Cybersecurity Chatbot!");
            ChatListBox.Items.Add("SecuBot: Hello, I’m SecuBot. What's your name?");
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
            {
                ChatListBox.Items.Add("SecuBot: Please enter something.");
                return;
            }

            ChatListBox.Items.Add(""); // Add a blank line for better readability
            ChatListBox.Items.Add("You: " + input);
            UserInputTextBox.Text = "";
            ChatListBox.Items.Add("");
            string lower = input.ToLower();

            // Ask for name at start
            if (userName == null)
            {
                userName = input;
                ChatListBox.Items.Add($"SecuBot: Nice to meet you, {userName}! What cybersecurity topic interests you most?");
                activityLog.Add($"[{DateTime.Now:HH:mm}] User name set: {userName}");
                return;
            }


            if (lower.Contains("manage tasks") || lower.Contains("task manager"))
            {
                NavigationService.Navigate(new Page2());
                return;
            }



            // ✅ Handle reminder follow-up
            if (pendingTask != null)
            {
                if (lower == "no" || lower.Contains("no thanks"))
                {
                    ChatListBox.Items.Add("");
                    ChatListBox.Items.Add("SecuBot: Got it. No reminder set for that task.");
                    activityLog.Add($"[{DateTime.Now:HH:mm}] Task added without reminder: {pendingTask.Title}");
                    ChatListBox.Items.Add("");
                    pendingTask = null;
                    return;
                }

                DateTime? reminder = TryParseReminder(input);
                if (reminder.HasValue)
                {
                    pendingTask.ReminderDate = reminder;
                    ChatListBox.Items.Add("");
                    ChatListBox.Items.Add($"SecuBot: Got it! I'll remind you on {reminder.Value.ToShortDateString()}.");
                    activityLog.Add($"[{DateTime.Now:HH:mm}] Reminder set for task: {pendingTask.Title} → {reminder.Value.ToShortDateString()}");
                    ChatListBox.Items.Add("");
                    pendingTask = null;
                    return;
                }

                return;
            }


            // ✅ Chatbot Response
            string botReply = handlerObj.GetBotResponse(input); // This uses DetectCategory() and keywordDictionary
            if (!string.IsNullOrEmpty(botReply))
            ChatListBox.Items.Add("");
            ChatListBox.Items.Add($"SecuBot: " + botReply);
            ChatListBox.Items.Add("");


            // ✅ Sentiment Detection (separate from chatbot)
            string sentiment = bot.DetectSentiment(input);
            string empatheticReply = bot.GetEmpatheticResponse(sentiment, userName);


            if (!string.IsNullOrEmpty(empatheticReply))
            {
                ChatListBox.Items.Add("");
                ChatListBox.Items.Add("SecuBot: " + empatheticReply);
                ChatListBox.Items.Add($"Would you like more information on any specific topic, or do you have any further questions?");
                ChatListBox.Items.Add("");
            }
        }



        private DateTime? TryParseReminder(string input)
        {
            string lower = input.ToLower().Trim();
            var inMatch = Regex.Match(lower, @"in (\d+)\s*(day|days|week|weeks|hour|hours|minute|minutes|month|months)");
            if (inMatch.Success)
            {
                int value = int.Parse(inMatch.Groups[1].Value);
                string unit = inMatch.Groups[2].Value;
                return unit switch
                {
                    "day" or "days" => DateTime.Now.AddDays(value),
                    "week" or "weeks" => DateTime.Now.AddDays(value * 7),
                    "hour" or "hours" => DateTime.Now.AddHours(value),
                    "minute" or "minutes" => DateTime.Now.AddMinutes(value),
                    "month" or "months" => DateTime.Now.AddMonths(value),
                    _ => null
                };
            }

            var onMatch = Regex.Match(lower, @"on\s+(.+)");
            if (onMatch.Success && DateTime.TryParse(onMatch.Groups[1].Value.Trim(), out DateTime parsed))
                return parsed;

            if (Regex.IsMatch(lower, @"\btomorrow\b")) return DateTime.Now.AddDays(1);
            if (Regex.IsMatch(lower, @"next\s+week")) return DateTime.Now.AddDays(7);

            return null;
        }


        public void ReceiveGameResultMessage(int score, int total)
        {
            ChatListBox.Items.Add("");
            ChatListBox.Items.Add("SecuBot: 🎉 The quiz is complete! You can now continue asking questions or try again.");
            activityLog.Add($"[{DateTime.Now:HH:mm}] Quiz completed with a score of {score} out of {total}.");
            ChatListBox.Items.Add("");
        }

    }
}
