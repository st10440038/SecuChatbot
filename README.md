# 🛡️ SecuBot: Cybersecurity Chatbot

**SecuBot** is a smart, interactive chatbot designed to help users improve their cybersecurity knowledge and digital habits. It combines a friendly chat experience with educational tools such as quizzes, reminders, and sentiment-aware conversations.

---

## 🚀 What's New?

### ✅ GUI Version (WPF)
SecuBot now features a **Graphical User Interface** built using WPF for a more engaging and user-friendly experience.

### ✅ 🧠 Improved NLP (Natural Language Processing)
SecuBot understands flexible input like:
- "Can I take the quiz?"
- "Remind me in 3 days to check my email settings"
- "What's phishing?"

### ✅ 🎮 Quiz Game
Test your cybersecurity knowledge with a built-in **10-question multiple choice quiz**. Score is tracked, and personalized feedback is given.

### ✅ 📋 Task Assistant
Add, view, and manage security-related tasks:
- Tasks include title, description, and optional reminders.
- View your task list in the chat or in a dedicated management page.
- Mark tasks as completed ✅ or delete them.

### ✅ ⏰ Reminder System
Set reminders in natural language:
- “Remind me in 5 days”
- “Set a reminder on July 1”
- “Next week”

### ✅ 🧾 Activity Log
SecuBot remembers your recent activities and can display a log of actions you've taken (e.g., started quiz, added tasks).

### ✅ 😊 Sentiment Detection
Understands emotional cues like:
- “I'm confused”
- “This is frustrating”
- “I'm worried about scams”

…and responds empathetically.

### ✅ 🎵 Voice Greeting
Plays a friendly **audio welcome message** when the app starts.

### ✅ 🔄 Memory
SecuBot remembers your **name**, **interests**, and preferences throughout your session.

---

## ✨ Core Features

- **Cybersecurity Guidance** – Get helpful responses on phishing, passwords, 2FA, scams, malware, and more.
- **Personalization** – SecuBot remembers your name and preferred topics.
- **Randomized Replies** – For key topics like phishing, SecuBot provides randomized advice to keep conversations engaging.
- **Typing Delay** – Simulates realistic typing delays for a more human feel.
- **ASCII Art** – Console version includes a custom ASCII banner at startup.

---

## 🧪 Technologies Used

- **C# (.NET)** – Core language for logic
- **WPF (XAML)** – Modern GUI using Windows Presentation Foundation
- **Regex** – For parsing time and reminder phrases
- **SoundPlayer** – For audio greeting playback
- **List & Dictionary** – For dynamic memory and responses
- **LINQ & Binding** – To bind task data in the GUI
- **INotifyPropertyChanged** – To reflect task updates in real-time

---

## 🛠️ Installation & Setup

1. Clone this repository:

   ```bash
   git clone https://github.com/st10440038/SecuChatbot.git
2. Open the project in Visual Studio.

3. Build the solution and run the WPF application.

4. Ensure the file audioWelcomeChatbot.wav is present in the /bin/Debug/ folder.

   Right-click → Properties → Set Copy to Output Directory = Copy if newer

##🧠 How to Use SecuBot
Once running, just type naturally into the chatbot. SecuBot understands sentences like:

“Remind me in 3 days to update antivirus”

“What’s my task list?”

“I’m feeling confused about phishing”

“Start the quiz”

“Add task – enable 2FA”

“Show activity log”

##💬 Example Interactions
User: Remind me in 5 days to check my firewall
SecuBot: Got it! I'll remind you on [date]

User: Can we start the game?
SecuBot: Starting the quiz. Good luck!

User: What have I done so far?
SecuBot: Here's your recent activity:

[time] Quiz started

[time] Task added: Update antivirus

##✅ Exiting
To exit the console version of SecuBot, type any of the following:

exit

quit

bye

(Note: WPF version includes built-in navigation without command-line exit.)

##📁 Folder Contents

SecuBot/
├── audioWelcomeChatbot.wav       # Audio greeting
├── MainWindow.xaml               # Home/start page
├── Page1.xaml                    # Chatbot interface
├── Page2.xaml                    # Task manager
├── Page4.xaml                    # Quiz game
├── Page5.xaml                    # Quiz result screen
├── TaskItem.cs                   # Task data model
├── TaskManager.cs                # Task handling logic
├── ResponseHandler.cs            # Core chatbot logic
└── README.md                     # This file

##💡 Future Ideas
🧠 Save/load memory and tasks across sessions

🕶️ Themed UI (dark mode/light mode toggle)

⏰ Desktop reminders or notifications

🌐 Web version (Blazor / ASP.NET)

📊 User statistics & quiz leaderboards

🔗 Export activity log as a text file


##👨‍💻 Contributing
Want to contribute? Here's how:

Fork the repository

Create a new branch (git checkout -b feature-X)

Commit your changes

Push to your fork and open a pull request

🔐 License
This project is licensed under the MIT License for academic/educational use.

🧩 Credits
Developed by ST10440038 for the PROG6221 Practical POE.

“Stay safe, stay informed — with SecuBot!” 🔐
