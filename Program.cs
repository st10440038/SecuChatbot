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
}



// ResponseHandler class that handles responses based on user input
public class ResponseHandler
{
    private Dictionary<string, List<string>> keywordDictionary;
    private Dictionary<string, string> responses;

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
            { ResponseCategories.DataPrivacy, new List<string> { "data privacy", "personal data", "protecting personal information", "online privacy" } },
            { ResponseCategories.SecureMessaging, new List<string> { "secure messaging", "encrypted messages", "private messaging", "secure communication" } },
            { ResponseCategories.Encryption, new List<string> { "encryption", "encrypted data", "data encryption", "how encryption works", "secure communications" } }
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
            { ResponseCategories.Encryption, "Encryption is a process that transforms your data into an unreadable format unless the recipient has the key to decrypt it. It’s widely used to secure sensitive information, such as communications, banking transactions, and personal data." }
        };
    }


    // Method to get a response based on user input
    public string GetResponse(string userInput)
    {
        // Loop through each category and check for a match with user input
        foreach (var category in keywordDictionary)
        {
            foreach (var keyword in category.Value)
            {
                if (userInput.Contains(keyword.ToLower()))
                {
                    return responses[category.Key];  // Return the corresponding response
                }
            }
        }
        return string.Empty;  // Return empty if no match is found
    }
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


            // Start the conversation loop
            while (true)
            {
                Console.Write("----------> ");
                Console.ForegroundColor = ConsoleColor.Green;
                string? userInput = Console.ReadLine()?.ToLower();  // Get user input

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


                // Get and display the response based on user input
                var response = responseHandlerObj.GetResponse(userInput);
                if (string.IsNullOrEmpty(response))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeText($"\nSecuBot: Hmm, I didn't quite catch that. Feel free to ask me anything related to online security, and I’ll be happy to help you {userObj.Name}!");
                    Console.WriteLine();
                    Console.ResetColor();
                    Console.WriteLine("---------------------------------------------------------------------");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    TypeText($"\nSecuBot: {response}");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------------------------");
                }


                // Ask if the user has more questions
                Console.WriteLine();
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



