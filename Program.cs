using System;
using System.Collections.Generic;
using System.Linq;
class Chatbot
{
    static void Main()
    {
        Console.WriteLine("SecuBot: Hello, I’m SecuBot, here to assist you with all things cybersecurity.");
        Console.WriteLine("In today’s connected world, online security is more crucial than ever, and I’m here to guide you through it.");
        Console.WriteLine("Whether you're seeking advice on securing accounts, recognizing online threats, or understanding best practices, I’m here to help.");
        Console.WriteLine();

        Console.Write("To get started, may I have your name? --------> ");
        string? userName = Console.ReadLine();
        Console.WriteLine();

        while (string.IsNullOrEmpty(userName) || !userName.All(Char.IsLetter))
        {
            Console.Write("SecuBot: Could you please provide your name again? --------> ");
            userName = Console.ReadLine();
            Console.WriteLine();
        }

        Console.WriteLine($"It's a pleasure to meet you, {userName}! Let's dive into some valuable cybersecurity insights.");
        Console.WriteLine("Feel free to ask me anything, or if you’d prefer, I can begin by offering a helpful security tip.");
        Console.WriteLine();

        Dictionary<string, List<string>> keywordDictionary = new Dictionary<string, List<string>>()
        {
            { "how_are_you", new List<string> { "how are you", "how's it going", "how are you doing", "how’s everything", "how do you feel" } },
            { "what_can_i_ask", new List<string> { "what can I ask you", "what topics can I discuss", "what are you here to help with", "what can I learn from you" } },
            { "purpose", new List<string> { "who are you", "what do you do", "what’s your purpose", "explain yourself", "tell me about your role" } },
            { "cybersecurity", new List<string> { "cybersecurity", "digital security", "online security", "cyber threats", "protecting my data" } },
            { "passwords", new List<string> { "password", "strong password", "password tips", "how to make a secure password", "password management" } },
            { "phishing", new List<string> { "phishing", "scam email", "fraudulent email", "phishing attacks", "how to recognize phishing" } },
            { "2fa", new List<string> { "2fa", "two-factor authentication", "MFA", "two-step verification", "how to set up 2fa" } },
            { "social_engineering", new List<string> { "social engineering", "human hacking", "manipulation", "fraudulent requests", "spotting social engineering" } },
            { "malware", new List<string> { "malware", "virus", "trojan", "spyware", "ransomware", "malicious software" } },
            { "safe_browsing", new List<string> { "safe browsing", "secure websites", "dangerous sites", "how to browse safely", "internet security" } },
            { "data_privacy", new List<string> { "data privacy", "personal data", "protecting personal information", "online privacy" } },
            { "secure_messaging", new List<string> { "secure messaging", "encrypted messages", "private messaging", "secure communication" } },
            { "encryption", new List<string> { "encryption", "encrypted data", "data encryption", "how encryption works", "secure communications" } }
        };

        Dictionary<string, string> responses = new Dictionary<string, string>()
        {
            { "how_are_you", "I’m doing well, thank you for asking! How about you? How is everything on your end?" },
            { "what_can_i_ask", "Feel free to ask me about various cybersecurity topics—everything from account protection to identifying phishing scams. I’m here to provide insights to help you stay safe online." },
            { "purpose", "I’m SecuBot, designed to assist you with understanding and improving your cybersecurity. I can answer questions on various topics like online safety, passwords, encryption, and more." },
            { "cybersecurity", "Cybersecurity refers to the practices and technologies used to protect digital information, devices, and systems from unauthorized access, attacks, and damage. It’s crucial to stay vigilant and informed in today’s digital age." },
            { "passwords", "Creating a strong password is key to protecting your accounts. Use a combination of upper and lowercase letters, numbers, and symbols. Aim for at least 12 characters, and avoid easily guessable information." },
            { "phishing", "Phishing attacks involve scammers pretending to be legitimate organizations to steal your personal information. Always check the sender’s email and avoid clicking on suspicious links or attachments." },
            { "2fa", "Two-factor authentication (2FA) is an additional layer of security that requires something you know (like your password) and something you have (like a code sent to your phone). It’s one of the simplest and most effective ways to secure your accounts." },
            { "social_engineering", "Social engineering involves manipulating people into divulging confidential information. Be cautious of unsolicited messages or requests, especially if they seem too good to be true." },
            { "malware", "Malware is software designed to harm or exploit any device or network. It can include viruses, spyware, and ransomware. Protect your devices by keeping your software up to date and avoiding suspicious downloads." },
            { "safe_browsing", "To browse safely, always check for 'HTTPS' in website URLs, avoid clicking on dubious links, and use a VPN when connecting to public Wi-Fi. Ensure that your browser and security software are up to date." },
            { "data_privacy", "Protecting your personal data is vital. Review your social media privacy settings, avoid sharing too much personal information online, and be cautious about what you disclose to websites and apps." },
            { "secure_messaging", "For secure communication, consider using encrypted messaging platforms like Signal or WhatsApp. These services ensure that your messages are private and protected from unauthorized access." },
            { "encryption", "Encryption is the process of converting data into a code to prevent unauthorized access. It’s widely used to secure sensitive information during transmission, ensuring that only authorized parties can decrypt and read it." }
        };

        while (true)
        {
            Console.Write("----------> ");
            string? userInput = Console.ReadLine()?.ToLower();

            if (userInput == null)
            {
                Console.WriteLine("SecuBot: Sorry, I didn’t quite catch that. Please try again.");
                continue;
            }

            if (userInput == "no" || userInput == "exit" || userInput == "bye" || userInput == "quit" || userInput == "no more questions")
            {
                Console.WriteLine("SecuBot: It was a pleasure assisting you today. Stay safe online, and don’t hesitate to reach out again. Goodbye!");
                break;
            }

            string matchedCategory = keywordDictionary
                .FirstOrDefault(entry => entry.Value.Any(keyword => userInput.Contains(keyword)))
                .Key;

            if (!string.IsNullOrEmpty(matchedCategory) && responses.ContainsKey(matchedCategory))
            {
                Console.WriteLine($"\nSecuBot: {responses[matchedCategory]}");
            }
            else
            {
                Console.WriteLine("\nSecuBot: I’m afraid I didn’t understand that. Please feel free to ask about any other cybersecurity topic.");
            }

            Console.WriteLine("\nWould you like more information on any specific topic, or do you have any further questions?");
        }
    }
}
