# SecuBot: Cybersecurity Chatbot

**SecuBot** is a professional, command-line chatbot that helps users understand and improve their cybersecurity knowledge. It gives quick, helpful advice about staying safe online, like how to manage passwords, recognize phishing attempts, and more.

---

## Features

### 💻 Cybersecurity Help
Ask the bot anything about digital security, and it’ll give advice on everything from securing an account to spotting online threats.

### 💬 Interactive Experience
SecuBot responds in a conversational way, making learning about cybersecurity more engaging.

### 🎨 ASCII Art
Every time the user starts the bot, a customized ASCII art appears.

### 🔊 Voice Greeting
To make the interaction more immersive, SecuBot plays a short welcome audio message when launched.

### 🧠 Random Responses
For topics like phishing, SecuBot selects from a variety of predefined responses to keep the conversation fresh and informative.

### 🔄 Conversation Flow
SecuBot can handle follow-up questions smoothly, maintaining the context of a conversation instead of resetting after every response.

### 📝 Memory and Recall
SecuBot remembers user preferences (like their name or favorite cybersecurity topic) and refers back to them for more personalized interactions.

### 😊 Sentiment Detection
Detects basic emotions such as worry or curiosity and adjusts responses to be more supportive and empathetic.

---

## Technologies Used

- **C#**: The chatbot is written in C# using basic console functionalities.
- **Dictionary-based Responses**: Recognizes keywords and maps them to relevant advice.
- **Randomized Reply Lists**: Provides varied responses using arrays/lists for certain topics.
- **Thread.Sleep**: Creates a typing effect in the chatbot’s responses.
- **System.Media.SoundPlayer**: Plays a welcome audio clip upon startup.

---

## Steps for Installation & Usage

1. Clone the repository:
   ```bash
   git clone https://github.com/st10440038/SecuChatbot.git
2. Open the project in your IDE (e.g., Visual Studio or Visual Studio Code).

3. Build and run the program.

---

## How It Works
**Main Program:** Displays a welcome banner, plays audio, and starts the chatbot.

**Response Handling:** The ResponseHandler class identifies keywords/topics and selects appropriate advice.

**Random Tips:** For key topics like phishing, responses are randomly selected from a list to keep conversations dynamic.

**Memory:** The chatbot remembers and recalls user-provided details to personalize responses.

**Sentiment:** Simple emotional cues like “worried” or “confused” trigger empathetic responses.

**Exit Logic:** The chatbot gracefully ends when a user types exit-related commands.

---

## How to Use
Once running, just ask a question about cybersecurity.

Topics include:

- Cybersecurity (general online safety)

- Passwords (creating strong ones and managing them)

- Phishing (identifying and avoiding phishing attacks)

- Two-Factor Authentication (2FA)

- Social Engineering

- Malware

- Safe Browsing

- Data Privacy

- Secure Messaging

- Encryption

---

## Example Interactions

**User:** What is phishing?

**SecuBot:** Phishing is a tricky scam where attackers pretend to be someone you trust, like your bank or a coworker... (randomly chosen tip)

**User:** I'm interested in privacy.

**SecuBot:** Great! I'll remember that you're interested in privacy. It's a crucial part of staying safe online.

**User:** I'm worried about online scams.

**SecuBot:** It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips...

---

## Exiting the Chatbot
Type any of the following to end the conversation:

- exit

- bye

- quit

---

 ## Example Output
(Playing welcome audio...)


Welcome to the Cybersecurity Chatbot!
Type 'exit' or 'bye' anytime to end the conversation.

**SecuBot:** Hello, I’m SecuBot, here to assist you with all things cybersecurity.

**SecuBot:** What can I help you with today?

**User:** What is phishing?

**SecuBot:** Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.

**User:** I'm worried about online scams.

**SecuBot:** It's completely normal to feel concerned. Let me help you feel more confident about staying safe online.


Stay safe, stay informed — with SecuBot! 🔐

