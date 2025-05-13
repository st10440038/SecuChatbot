using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Media;
using System.IO;
using System.Reflection;




// Static class to define response categories
public static class ResponseCategories
{
    public const string HowAreYou = "how_are_you";
    public const string WhatCanIAsk = "what_can_i_ask";
    public const string Purpose = "purpose";
    public const string Cybersecurity = "cybersecurity";
    public const string Passwords = "passwords";
    public const string Phishing = "phishing";
    public const string TwoFA = "2fa";
    public const string SocialEngineering = "social_engineering";
    public const string Malware = "malware";
    public const string SafeBrowsing = "safe_browsing";
    public const string DataPrivacy = "data_privacy";
    public const string SecureMessaging = "secure_messaging";
    public const string Encryption = "encryption";
    public const string Scam = "scam";
}



// ResponseHandler class that handles responses based on user input
public class ResponseHandler
{
    private Dictionary<string, List<string>> keywordDictionary;
    private Dictionary<string, string> responses;
    public Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>();



    public ResponseHandler()
    {
        // Initialize keyword dictionary to map categories to possible user input
        keywordDictionary = new Dictionary<string, List<string>>()
        {
            { ResponseCategories.HowAreYou, new List<string> { "how are you", "how's it going", "how are you doing", "how’s everything", "how do you feel", "what's up" }  },
            { ResponseCategories.WhatCanIAsk, new List<string> { "what can I ask you", "what topics can I discuss", "what are you here to help with", "what can I learn from you", "what should I ask", "what topics do you cover", "what can I talk to you about"} },
            { ResponseCategories.Purpose, new List<string> { "who are you", "what do you do", "what’s your purpose", "explain yourself", "tell me about your role" } },
            { ResponseCategories.Cybersecurity, new List<string> { "cybersecurity", "digital security", "online security", "cyber threats", "protecting my data" } },
            { ResponseCategories.Passwords, new List<string> { "password", "strong password", "password tips", "secure password", "password management" } },
            { ResponseCategories.Phishing, new List<string> { "phishing", "scam email", "fraudulent email", "phishing attacks", "how to recognize phishing" } },
            { ResponseCategories.TwoFA, new List<string> { "2fa", "two-factor authentication", "MFA", "two-step verification", "how to set up 2fa" } },
            { ResponseCategories.SocialEngineering, new List<string> { "social engineering", "human hacking", "manipulation", "fraudulent requests", "spotting social engineering" } },
            { ResponseCategories.Malware, new List<string> { "malware", "virus", "trojan", "spyware", "ransomware", "malicious software" } },
            { ResponseCategories.SafeBrowsing, new List<string> { "safe browsing", "secure websites", "dangerous sites", "how to browse safely", "internet security" } },
            { ResponseCategories.DataPrivacy, new List<string> { "data privacy", "personal data", "protecting personal information", "privacy", "online privacy", "data privacy protection", "protecting privacy", "privacy policies" } },
            { ResponseCategories.SecureMessaging, new List<string> { "secure messaging", "encrypted messages", "private messaging", "secure communication" } },
            { ResponseCategories.Encryption, new List<string> { "encryption", "encrypted data", "data encryption", "how encryption works", "secure communications" } },
            { ResponseCategories.Scam, new List<string> { "scam", "online scam", "fraud", "internet scam", "scam websites" } }
        };


        // Initialize responses for each category
        responses = new Dictionary<string, string>()
        {
            { ResponseCategories.HowAreYou, "I’m doing great, thanks for asking ! How about you ? How’s everything going on your end? I'm here to chat whenever you’re ready." },
            { ResponseCategories.WhatCanIAsk, "Oh, there's so much you can ask! I can provide tips on protecting your passwords, spotting phishing attacks, setting up two-factor authentication, and much more. Whatever’s on your mind regarding cybersecurity, feel free to ask!" },
            { ResponseCategories.Purpose, "I’m SecuBot, your cybersecurity guide. My goal is to help you stay safe online by answering your questions and providing tips on securing your data, devices, and accounts. Whether you're a beginner or already tech-savvy, I’ve got your back!" },
            { ResponseCategories.Cybersecurity, "Cybersecurity is all about protecting your digital life—your data, your devices, and your identity. It involves practices, tools, and technologies that keep your information safe from unauthorized access, damage, or attacks. In simple terms, it's making sure you're safe online." },
            { ResponseCategories.Passwords, "When creating a strong password, mix things up! Use a combination of upper and lowercase letters, numbers, and symbols. The longer, the better—12 characters or more is ideal. And please avoid common patterns or using personal info like birthdays. Password managers can also help you keep track of them securely." },
            { ResponseCategories.Phishing, "Phishing is when someone tries to trick you into revealing personal information, like passwords or credit card numbers, by pretending to be someone trustworthy. Often this happens via email or text messages with suspicious links or attachments. Always double-check the sender’s details and look out for suspicious language!" },
            { ResponseCategories.TwoFA, "Two-factor authentication (2FA) adds an extra layer of security by requiring two forms of verification—usually a password and a temporary code sent to your phone or email. It’s one of the easiest ways to secure your accounts from unauthorized access." },
            { ResponseCategories.SocialEngineering, "Social engineering is when attackers manipulate people into revealing sensitive information or performing actions they wouldn’t normally do. They might pretend to be someone you trust, like a co-worker or IT support, to get you to share login credentials or other private details." },
            { ResponseCategories.Malware, "Malware is malicious software designed to harm your device or steal information. It includes viruses, ransomware, spyware, and trojans. Always keep your software updated, use antivirus programs, and avoid downloading files from untrusted sources." },
            { ResponseCategories.SafeBrowsing, "Safe browsing means being cautious of the websites you visit. Stick to reputable sites, especially when entering sensitive information. Make sure the website’s URL begins with ‘https’ (the ‘s’ stands for secure), and be wary of pop-up ads or unsolicited offers." },
            { ResponseCategories.DataPrivacy, "Data privacy is about protecting your personal information online. Be mindful of what you share on social media, and ensure that the services you use are respecting your privacy. You should also be aware of the laws and regulations surrounding data protection, like GDPR." },
            { ResponseCategories.SecureMessaging, "Secure messaging platforms use encryption to protect the content of your conversations. Examples include apps like Signal and WhatsApp. Encrypted messaging ensures that only the intended recipient can read your messages, protecting your privacy." },
            { ResponseCategories.Encryption, "Encryption is a process that transforms your data into an unreadable format unless the recipient has the key to decrypt it. It’s widely used to secure sensitive information, such as communications, banking transactions, and personal data." },
            { ResponseCategories.Scam, "Scams are deceptive schemes designed to trick people into giving away money or sensitive information. They can happen through emails, phone calls, or fake websites. Be cautious of offers that sound too good to be true and always verify the legitimacy of the source before acting." }
        };




        // Initialize random responses for each category
        keywordResponses = new Dictionary<string, List<string>>()
        {
            { ResponseCategories.Phishing, new List<string>
            {
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                "Always check the sender's email address carefully. Scammers often use fake email addresses that look similar to legitimate ones.",
                "If an email contains urgent language asking for sensitive data, it could be a phishing attempt. Take a moment to think before clicking any links.",
                "Never click on suspicious links or download attachments from unknown sources. These could be used to steal your personal data."
            }
            },
            { ResponseCategories.Cybersecurity, new List<string>
            {
                "Cybersecurity is all about protecting your digital life—your data, your devices, and your identity. It involves practices, tools, and technologies that keep your information safe from unauthorized access, damage, or attacks.",
                "Always use strong, unique passwords for every account. Enable two-factor authentication where possible to add an extra layer of protection.",
                "Stay up-to-date with the latest security patches. Cybercriminals often exploit vulnerabilities in outdated software to gain access to your system."
            }
            },
            { ResponseCategories.Passwords, new List<string>
            {
                "Use a mix of uppercase, lowercase, numbers, and symbols in your passwords. The longer the password, the better—aim for at least 12 characters.",
                "Never reuse passwords across different accounts. If one account is compromised, it can lead to others being hacked too.",
                "Consider using a password manager to keep track of all your passwords securely. This way, you don’t need to remember each one."
            }
            },
            { ResponseCategories.TwoFA, new List<string>
            {
                "Two-factor authentication (2FA) adds an extra layer of security by requiring something you know (your password) and something you have (a code sent to your phone).",
                "When setting up 2FA, choose an authentication method that works for you, whether it’s a text message, app, or hardware key.",
                "2FA significantly reduces the risk of your account being hacked, even if someone manages to steal your password."
            }
            },
            { ResponseCategories.SocialEngineering, new List<string>
            {
                "Social engineering relies on manipulating people into divulging confidential information. Be cautious if someone pressures you for sensitive data.",
                "Attackers may pose as trusted individuals—like IT support or a colleague—to trick you into sharing private information. Always verify their identity first.",
                "Never respond to unsolicited emails or phone calls asking for personal information. Take your time to evaluate the situation."
            }
            },
            { ResponseCategories.Malware, new List<string>
            {
                "Malware is any software intentionally designed to cause damage. Viruses, ransomware, and spyware are all types of malware.",
                "To protect against malware, keep your operating system and software up-to-date, use antivirus software, and avoid downloading files from untrusted sources.",
                "If you suspect your device is infected with malware, run a full system scan with trusted antivirus software and disconnect from the internet to prevent further damage."
            }
            },
            { ResponseCategories.SafeBrowsing, new List<string>
            {
                "Safe browsing is essential for protecting your personal data. Always make sure the website URL starts with 'https' (the 's' stands for secure).",
                "Avoid clicking on pop-up ads or suspicious links, even if they appear to be from a reputable source. These could lead to harmful websites.",
                "Use an ad blocker and privacy-focused browser extensions to minimize the risk of encountering malicious sites while browsing."
            }
            },
            { ResponseCategories.DataPrivacy, new List<string>
            {
                "Data privacy is about keeping your personal information safe. Be selective about the data you share online, and only provide necessary details to trusted services.",
                "Make sure you're familiar with the privacy policies of the websites and apps you use. Understand what data they collect and how they protect it.",
                "Consider using a VPN (Virtual Private Network) to encrypt your internet connection and protect your online privacy when using public networks."
            }
            },
            { ResponseCategories.SecureMessaging, new List<string>
            {
                "Secure messaging ensures that only the intended recipient can read your messages. End-to-end encryption is a key feature of secure messaging apps like Signal and WhatsApp.",
                "Consider using encrypted messaging services to safeguard sensitive information. These services use encryption algorithms to protect your messages from eavesdropping.",
                "Whenever possible, avoid sending sensitive data over unsecured channels like SMS. Use secure, encrypted messaging apps instead."
            }
            },
            { ResponseCategories.Encryption, new List<string>
            {
                "Encryption turns your data into an unreadable format, making it secure from unauthorized access. Only those with the decryption key can read it.",
                "End-to-end encryption ensures that data is encrypted from the sender to the recipient, preventing anyone else from intercepting it during transit.",
                "Encryption is used in many aspects of digital life, including securing emails, banking transactions, and messaging. It’s one of the best ways to protect sensitive information."
            }
            }

        };


    }


    // List of keywords that indicate confusion
    public List<string> confusionKeywords = new List<string>
    {
        "explain", "not sure", "don't understand", "confused", "more info", "more details", "tell me more", "clarify", "say again", "don't get it"
    };


    // Tracks last used response index for each topic to avoid repetition
    private Dictionary<string, int> lastUsedIndex = new Dictionary<string, int>();


    // Random generator
    private Random rand = new Random();



    string DetectCategory(string input)
    {
        foreach (var category in keywordDictionary)
        {
            foreach (string keyword in category.Value)
            {
                if (input.Contains(keyword))
                    return category.Key;
            }
        }
        return null;
    }


    string lastTopic = null;


    string GetBotResponse(string input)
    {
        input = input.ToLower();
        string category = DetectCategory(input);
        bool confused = IsConfused(input);

        // If the user is confused but we know the last topic
        if (confused && lastTopic != null && keywordResponses.ContainsKey(lastTopic))
        {
            var clarifications = keywordResponses[lastTopic];
            var random = new Random();
            return clarifications[random.Next(clarifications.Count)];
        }

        // If input contains a known category (like "phishing")
        if (category != null)
        {
            lastTopic = category; // Update the last topic

            if (confused && keywordResponses.ContainsKey(category))
            {
                var clarifications = keywordResponses[category];
                var random = new Random();
                return clarifications[random.Next(clarifications.Count)];
            }

            if (responses.ContainsKey(category))
            {
                return responses[category];
            }
        }

        // If just confused and no topic yet
        if (confused)
        {
            return "Could you clarify what you're asking about? I can help with phishing, malware, passwords, and more.";
        }

        return "I'm not sure I understand your question, but feel free to ask me anything related to cybersecurity.";
    }

    // Check if user is confused
    public bool IsConfused(string userInput)
    {
        return confusionKeywords.Any(word => userInput.ToLower().Contains(word));
    }




    class Program
    {
        static void PlayVoiceGreeting()
        {
            try
            {
                string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audioWelcomeChatbot.wav");

                if (File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("[Playing audio greeting...]");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Welcome to the Cybersecurity Awareness Bot!");
                        Console.ResetColor();
                        player.PlaySync();
                        Thread.Sleep(2000);
                    }
                    return;
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Audio error: {ex.Message}");
                Console.ResetColor();
            }
        }



        static void Main()
        {
            // Set up console window size and appearance
            Console.SetWindowSize(100, 50);

            // Play voice greeting
            PlayVoiceGreeting();

            // Instantiate the Chatbot class and start the interaction
            Chatbot chatbot = new Chatbot();
            chatbot.Start();
        }
    }



    // Chatbot class that handles the conversation with the user
    class Chatbot
    {
        private User userObj;
        private ResponseHandler responseHandlerObj;


        // Constructor to initialize User and ResponseHandler objects
        public Chatbot()
        {
            userObj = new User();  // User object to hold user details
            responseHandlerObj = new ResponseHandler();  // ResponseHandler object to manage responses
        }



        // Method that starts the chatbot conversation
        public void Start()
        {
            try
            {

                // Display the welcome banner
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("==============================================================================");
                Console.WriteLine("||                                                                          ||");
                Console.WriteLine("||                             SECUBOT V1.0                                 ||");
                Console.WriteLine("||                                                                          ||");
                Console.WriteLine("==============================================================================");
                Console.ResetColor();
                Console.WriteLine();


                // ASCII art logo of the chatbot
                string asciiArt = @"
                                                                                       

                           *%%%%%%%%%%%%%%%%%%%%,                          
                      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%                     
                  %%%%%%%%%%%%%%(          #%%%%%%%%%%%%%%                   
               %%%%%%%%%%.                        *%%%%%%%%%%                
            %%%%%%%%%     %&&&&&&&&&&&                 %%%%%%%%%           
          %%%%%%%%.      &&&&&&&&&&& &&              .%%%%%%%%%%%%         
         %%%%%%%        &&& *&&&&&&&   &           %%%%%%%%%%%%%%%%        
       %%%%%%%          &    &&#%&&    %        .%%%%%%%%    .%%%%%%%      
      %%%%%%%          & &&&&&&&&&&&&&& %     %%%%%%%%%        %%%%%%%     
     %%%%%%%            & &&&&&&&&&&&& &   ,%%%%%%%%            %%%%%%#    
     %%%%%%               & &&&&&&&& %   %%%%%%%%%               %%%%%%    
    %%%%%%,                  &&&&&&   .%%%%%%%%                  (%%%%%%   
    %%%%%%           %&&&&&&/ (&&/  %%%%%%%#%                     %%%%%%   
    %%%%%%        &&&&&&&%&%&%&& .%##%%%%# #&%                    %%%%%%   
    %%%%%%      #&&&&&&&%%&&&# %###%%##%                          %%%%%%   
    %%%%%%,     &&&&&&&&&&& .%%%%%%%% (&&&&&&&&&&&&&&&&&         #%%%%%%   
     %%%%%%     %&&&&&&&& %%%%%%%%% & &&&&&&&&&&&&&&&&&&         %%%%%%    
     %%%%%%%           .%%%%%%%%      &&&&&&&&&&&&&&&&&&        %%%%%%#    
      %%%%%%%        %%%%%%%%%        &&&&&&&&&&&&&&&&&&       %%%%%%%     
       %%%%%%%    .%%%%%%%%           &&&&&&&&&&&&&&&&&&     *%%%%%%#      
         %%%%%%%%%%%%%%%%                   &&&&&&          %%%%%%%        
          %%%%%%%%%%%%                                   #%%%%%%%#         
            %%%%%%%%%.                                /%%%%%%%%#           
               %%%%%%%%%%(                        #%%%%%%%%%%              
                  %%%%%%%%%%%%%%%.        ,%%%%%%%%%%%%%%%                   
                      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%                       
                            %%%%%%%%%%%%%%%%%%%%                            
                                                                   
            ";


                // Display the ASCII art logo in the console
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(asciiArt);
                Console.ResetColor();
                Console.WriteLine();


                // Display introductory message
                Console.WriteLine("*********************************************************************");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                TypeText("Welcome to the Cybersecurity Chatbot!");
                TypeText("Type 'exit', 'quit', 'no' or 'bye' anytime to end the conversation with the bot.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("*********************************************************************");
                Console.WriteLine();


                // Greet the user and explain the chatbot's purpose
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeText("SecuBot: Hello, I’m SecuBot, here to assist you with all things cybersecurity.");
                TypeText("In today’s connected world, online security is more crucial than ever, and I’m here to guide you through it.");
                TypeText("Whether you're seeking advice on securing accounts, recognizing online threats, or understanding best practices, I’m here to help.");
                Console.WriteLine();

                TypeText("To get started, may I have your name?");
                Console.WriteLine();
                Console.ResetColor();

                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.Write("Your Name: ");
                userObj.Name = GetUserInput();  // Get the user's name
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();


                // Greet the user by name and offer assistance
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeText($"It's a pleasure to meet you, {userObj.Name}! Let's dive into some valuable cybersecurity insights.");
                TypeText("Feel free to ask me anything, or if you’d prefer, I can begin by offering a helpful security tip.");
                Console.ResetColor();
                Console.WriteLine();


                ResponseHandler handlerObj = new ResponseHandler();
                string currentTopic = "";


                // Start the conversation loop
                while (true)
                {
                    Console.Write("----------> ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string? userInput = Console.ReadLine()?.ToLower();  // Get user input
                    Console.ResetColor();


                    if (userInput == null)
                    {
                        // Handle empty input
                        Console.ForegroundColor = ConsoleColor.Red;
                        TypeText($"SecuBot: Oops {userObj.Name}! It seems like I missed that. Could you please try again?");
                        Console.ResetColor();
                        continue;
                    }


                    // Check if the user wants to exit the conversation
                    if (userInput == "no" || userInput == "exit" || userInput == "bye" || userInput == "quit" || userInput == "no more questions")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine();
                        TypeText($"SecuBot: It was a pleasure assisting you today. Stay safe online, and don’t hesitate to reach out again. Goodbye {userObj.Name}!");
                        Console.ResetColor();
                        break;
                    }



                    string botReply = handlerObj.GetBotResponse(userInput);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeText($"\nSecuBot: " + botReply);
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------------------------");



                    // Ask if the user has more questions
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeText($"\nWould you like more information on any specific topic {userObj.Name}, or do you have any further questions?");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeText($"SecuBot: Oh no, something went wrong! I encountered an issue: {ex.Message}. Please {userObj.Name} try again later, and we’ll get back on track.");
                Console.ResetColor();
            }
        }



        // Method to get user input with error handling for invalid or empty inputs
        private string GetUserInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string? userName = Console.ReadLine();
            Console.ResetColor();

            try
            {
                // Validate that the name contains only valid characters
                while (string.IsNullOrEmpty(userName) || !System.Text.RegularExpressions.Regex.IsMatch(userName, @"^[a-zA-Z\s'-]+$"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    TypeText("SecuBot: Please enter a valid name using only letters, spaces, apostrophes, or dashes.");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine();
                    Console.Write("Your Name: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    userName = Console.ReadLine();
                    Console.ResetColor();
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeText($"SecuBot: An error occurred while processing your input. Please try again. Error details: {ex.Message}");
                Console.ResetColor();
                return GetUserInput();  // Recursively call to get valid input
            }

            return userName;

        }



        // Method to simulate typing text with a delay
        static void TypeText(string text, int delay = 15)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);  // Pause for a short duration between characters
            }
            Console.WriteLine();
        }
    }



    // User class to hold the user's data (The user's name for now)
    public class User
    {
        public string? Name { get; set; }
    }
}



