using System;

class Chatbot
{
    static void Main()
    {
        Console.WriteLine("SecuBot: Hey there! Welcome to SecuBot, your friendly guide to cybersecurity awareness.");
        Console.WriteLine("In today’s digital world, staying safe online is more important than ever, and that’s exactly what I’m here to help with!");
        Console.WriteLine("Whether you need tips on protecting your accounts, spotting scams, or just learning more about cybersecurity, I’ve got you covered.");
        Console.WriteLine();

        Console.Write("Let’s make this chat more personal ! What’s your name ? ----------> ");
        string userName = Console.ReadLine();
        Console.WriteLine();
        
        while (string.IsNullOrEmpty(userName) || !userName.All(Char.IsLetter))
        {
            Console.Write("SecuBot: Oops, please enter a valid name. ----------> ");
            userName = Console.ReadLine();
            Console.WriteLine();
        }

        Console.WriteLine($"Nice to meet you, {userName}! I’m glad you’re here.");
        Console.WriteLine("Now that we’re officially on a first-name basis, let’s dive into the world of cybersecurity!");
        Console.WriteLine();
        Console.WriteLine("Do you have any burning questions, or should I hit you with a cool security fact to get started?");
        Console.WriteLine();

        Dictionary<string, List<string>> keywordDictionary = new Dictionary<string, List<string>>()
        {
            { "purpose", new List<string> { "purpose", "about", "what is", "who are you", "your role", "you", "explain yourself", "describe", "tell me about", "what do you do" } },
            { "cybersecurity", new List<string> { "cybersecurity", "hacking", "protection", "safety", "online security", "digital security", "secure", "threats", "dangers", "online threats", "why cybersecurity" } },
            { "passwords", new List<string> { "password", "secure password", "strong password", "password tips", "password security", "password management", "how to make a strong password", "best password", "weak password", "password best practices" } },
            { "phishing", new List<string> { "phishing", "email scam", "fake email", "fraud", "scam", "phishing attack", "how to avoid phishing", "phishing example", "how to spot phishing", "phishing signs" } },
            { "2fa", new List<string> { "2fa", "two factor authentication", "multi factor authentication", "MFA", "login security", "secure login", "authentication", "how to enable 2fa", "what is 2fa" } },
            { "social_engineering", new List<string> { "social engineering", "manipulation", "psychological attack", "trick", "fake identity", "scammer", "social engineering examples", "human hacking", "deception" } },
            { "malware", new List<string> { "malware", "virus", "trojan", "spyware", "ransomware", "adware", "types of malware", "infected computer", "computer virus", "malware protection" } },
            { "safe_browsing", new List<string> { "safe browsing", "secure browsing", "dangerous websites", "how to browse safely", "internet safety", "protect my data", "safe internet use", "how to stay safe online", "public wifi", "vpn", "safe downloads", "secure websites", "private browsing", "safe surfing", "browser security", "avoid malware websites" } },
            { "data_privacy", new List<string> { "data privacy", "personal information", "protect my data", "online privacy", "how to protect my data", "digital privacy", "privacy settings", "data breach", "identity theft", "private data", "how to keep my data safe" } },
            { "secure_messaging", new List<string> { "secure messaging", "private chat", "encrypted messages", "safe texting", "best messaging app", "how to send secure messages", "whatsapp security", "telegram security" } },
            { "encryption", new List<string> { "encryption", "encrypted", "how does encryption work", "secure communication", "data encryption", "message encryption", "what is encryption", "cryptography" } }
        };


        // Responses dictionary with detailed answers
        Dictionary<string, string> responses = new Dictionary<string, string>()
        {
            { "purpose", "I’m SecuBot, your friendly cybersecurity assistant! My job is to help you stay safe online by giving advice on security best practices." },
            { "cybersecurity", "Cybersecurity is about protecting your devices, data, and online identity from threats like hackers, scams, and malware. It’s crucial in today’s digital world." },
            { "passwords", "A strong password should have at least 12 characters, including uppercase, lowercase, numbers, and special symbols. Avoid using personal info like your name or birthdate." },
            { "phishing", "Phishing attacks trick you into giving away personal information through fake emails, messages, or websites. Always check the sender’s email, avoid clicking unknown links, and verify requests before entering credentials." },
            { "2fa", "Two-Factor Authentication (2FA) adds an extra layer of security by requiring a second form of verification (like a code sent to your phone) when logging in. Always enable 2FA on important accounts!" },
            { "social_engineering", "Social engineering is when hackers manipulate people into revealing sensitive information. Be cautious of unexpected messages or phone calls asking for passwords or financial info!" },
            { "malware", "Malware is malicious software designed to harm or steal data. Types include viruses, ransomware, and spyware. Protect yourself by keeping your software updated and avoiding suspicious downloads." },
            { "safe_browsing", "To browse safely, always check for HTTPS in website URLs, avoid downloading unknown files, and use a VPN when on public Wi-Fi." },
            { "data_privacy", "Your personal data is valuable. Use strong passwords, adjust privacy settings on social media, and be cautious about sharing sensitive info online." },
            { "secure_messaging", "For private conversations, use encrypted messaging apps like Signal or Telegram. Avoid sharing personal info over unencrypted messages!" },
            { "encryption", "Encryption secures data by converting it into a secret code. This protects messages and files from being read by unauthorized users. Want to learn how encryption is used in online security?" }
        };

        while (true)
        {
            Console.Write("----------> ");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "no" || userInput == "no more questions" || userInput == "exit" || userInput == "bye")
            {
                Console.WriteLine("SecuBot: It was great chatting with you! Stay safe online. Goodbye!");
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
                Console.WriteLine("\nSecuBot: Hmm, I’m not sure I understood. Could you rephrase or ask something else?");
            }

            Console.WriteLine("\nDo you have any other questions?");
        }

    }
}
