# SecuBot: Cybersecurity Chatbot

**SecuBot** is a professional, command-line chatbot that helps users understand and improve their cybersecurity knowledge. It gives quick, helpful advices about staying safe online, like how to manage passwords, recognize phishing attempts, and more.


## Features
***Cybersecurity Help:*** Ask the bot anything about digital security, and it’ll give an advice on everything from securing an account to spotting online threats.

***Interactive Experience:*** SecuBot responds to the user's questions in a conversational way, making learning about cybersecurity a little more interesting.

***ASCII Art:*** Every time the user starts the bot, a customized ASCII art appears.

***Voice Greeting:*** To make the interaction more immersive, SecuBot plays a short welcome audio message when launched.

***User-friendly Interaction:*** Ask questions, and SecuBot will reply with advice based on what the user said.


## Technologies Used
***C#:*** The chatbot is written in C# using basic console functionalities.

***Dictionary-based Responses:*** SecuBot looks for certain words or phrases the user type and responds with relevant info.

***Thread.Sleep for Typing Effect:*** Adds a "typing" effect to the chatbot's responses for a more interactive experience.

***System.Media.SoundPlayer:*** Plays a welcome audio clip upon startup.


## Steps for Installation & Usage
Clone this repository to your local machine: **git clone https://github.com/st10440038/SecuChatbot.git**

Open the project in your IDE or editor like *Visual Studio* or *Visual Studio Code*.

Build the solution and run the program.


## How It Works:
***Main Program:*** This starts by displaying a welcome banner, playing an audio greeting, and initializing the chatbot.

***Response Handling:*** The ResponseHandler class contains the logic to categorize user input (e.g., questions about "passwords," "cybersecurity," etc.) and provides relevant responses.

***User Input Handling:*** The program continuously listens for user input, matches it with predefined responses, and asks the user if they have more questions after each response.

***Exit Logic:*** The chatbot will end the conversation gracefully when the user types exit-related commands.


## How to Use
Once the chatbot is running, the user only has to ask a question about the topic wanted.
Some examples of all the topics users can ask SecuBot about:

- ***Cybersecurity*** (general information about online security)

- ***Passwords*** (tips on creating strong passwords and password management)

- ***Phishing*** (how to recognize and avoid phishing attacks)

- ***Two-factor Authentication (2FA)*** (how to set up and why it's important)

- ***Social Engineering*** (how attackers trick people into giving sensitive information)

- ***Malware*** (viruses, trojans, spyware, and how to avoid them)

- ***Safe Browsing*** (how to browse the internet securely)

- ***Data Privacy*** (how to protect personal information online)

- ***Secure Messaging*** (using encrypted messaging apps for privacy)

- ***Encryption*** (how encryption works to protect data)

When chatting with SecuBot, ask questions about cybersecurity and the bot will respond based on pre-programmed responses.


## Exiting the Chatbot with the console
To exit the chatbot, the user only has to type any of the following:

- "exit"

- "bye"

- "quit"


## Example Output
(Playing welcome audio...)

Welcome to the Cybersecurity Chatbot!

Type 'exit' or 'bye' anytime to end the conversation.

SecuBot: Hello, I’m SecuBot, here to assist you with all things cybersecurity.

SecuBot: What can I help you with today?

----------> What is phishing?

SecuBot: Phishing is a tricky scam where attackers pretend to be someone you trust, like your bank or a coworker, to steal sensitive information. Be wary of unsolicited emails or messages that ask you to click on links or open attachments. Always verify the source before taking any action.

Would you like more information on any specific topic, or do you have any further questions?
