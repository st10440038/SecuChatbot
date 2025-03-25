using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


class Program
{
    static void Main()
    {
        Chatbot chatbot = new Chatbot();
        chatbot.Start();
    }
}

class Chatbot
{
    private User userObj;
    private ResponseHandler responseHandlerObj;


    public Chatbot()
    {
        userObj = new User();
        responseHandlerObj = new ResponseHandler();
    }


    public void Start()
    {
        try
        {
            Console.SetWindowSize(100, 30);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==============================================================================");
            Console.WriteLine("||                                                                          ||");
            Console.WriteLine("||                             SECUBOT V1.0                                 ||");
            Console.WriteLine("||                                                                          ||");
            Console.WriteLine("==============================================================================");
            Console.ResetColor();
            Console.WriteLine();



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

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(asciiArt);
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine("*********************************************************************");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            TypeText("Welcome to the Cybersecurity Chatbot!");
            TypeText("Type 'exit', 'quit', 'no' or 'bye' anytime to end the conversation with the bot.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("*********************************************************************");
            Console.WriteLine();

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
            userObj.Name = GetUserInput();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();


            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeText($"It's a pleasure to meet you, {userObj.Name}! Let's dive into some valuable cybersecurity insights.");
            TypeText("Feel free to ask me anything, or if you’d prefer, I can begin by offering a helpful security tip.");
            Console.ResetColor();
            Console.WriteLine();

            while (true)
            {
                Console.Write("----------> ");
                Console.ForegroundColor = ConsoleColor.Green;
                string? userInput = Console.ReadLine()?.ToLower();

                if (userInput == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeText("SecuBot: Oops! It seems like I missed that. Could you please try again?");
                    Console.ResetColor();
                    continue;
                }

                if (userInput == "no" || userInput == "exit" || userInput == "bye" || userInput == "quit" || userInput == "no more questions")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();
                    TypeText($"SecuBot: It was a pleasure assisting you today. Stay safe online, and don’t hesitate to reach out again. Goodbye {userObj.Name}!");
                    Console.ResetColor();
                    break;
                }

                var response = responseHandlerObj.GetResponse(userInput);
                if (string.IsNullOrEmpty(response))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeText("\nSecuBot: Hmm, I didn't quite catch that. Feel free to ask me anything related to online security, and I’ll be happy to help!");
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

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                TypeText("\nWould you like more information on any specific topic, or do you have any further questions?");
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

 
    private string GetUserInput()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        string? userName = Console.ReadLine();
        Console.ResetColor();

        while (string.IsNullOrEmpty(userName) || !System.Text.RegularExpressions.Regex.IsMatch(userName, @"^[a-zA-Z\s'-]+$"))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            TypeText("SecuBot: Please enter a valid name using only letters.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.Write("Your Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            userName = Console.ReadLine();
            Console.ResetColor();
        }
        return userName;
    }

    static void TypeText(string text, int delay = 15)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delay);
        }
        Console.WriteLine();
    }
}

public class User
{
    public string? Name { get; set; }
}

public class ResponseHandler
{ 

    private Dictionary<string, List<string>> keywordDictionary;
    private Dictionary<string, string> responses;

    public ResponseHandler()
    {
        keywordDictionary = new Dictionary<string, List<string>>()
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

        responses = new Dictionary<string, string>()
        {
            { "how_are_you", "I’m doing great, thanks for asking ! How about you ? How’s everything going on your end? I'm here to chat whenever you’re ready." },
            { "what_can_i_ask", "Oh, there's so much you can ask! I can provide tips on protecting your passwords, spotting phishing attacks, setting up two-factor authentication, and much more. Whatever’s on your mind regarding cybersecurity, feel free to ask!" },
            { "purpose", "I’m SecuBot, your cybersecurity guide. My goal is to help you stay safe online by answering your questions and providing tips on securing your data, devices, and accounts. Whether you're a beginner or already tech-savvy, I’ve got your back!" },
            { "cybersecurity", "Cybersecurity is all about protecting your digital life—your data, your devices, and your identity. It involves practices, tools, and technologies that keep your information safe from unauthorized access, damage, or attacks. In simple terms, it's making sure you're safe online." },
            { "passwords", "When creating a strong password, mix things up! Use a combination of upper and lowercase letters, numbers, and symbols. The longer, the better—12 characters or more is ideal. And please avoid common patterns or using personal info like birthdays. Password managers can also help you keep track of them securely." },
            { "phishing", "Phishing is a tricky scam where attackers pretend to be someone you trust, like your bank or a coworker, to steal sensitive information. Be wary of unsolicited emails or messages that ask you to click on links or open attachments. Always verify the source before taking any action." },
            { "2fa", "Two-factor authentication (2FA) is like adding an extra lock to your door. After entering your password, 2FA requires you to verify your identity by entering a code sent to your phone or email. It’s a super easy way to add an extra layer of protection to your accounts." },
            { "social_engineering", "Social engineering is when attackers manipulate people into giving up sensitive information. They might trick you into sharing your password or personal details through phone calls, emails, or even face-to-face interactions. Always stay cautious and verify who you're dealing with before disclosing any information." },
            { "malware", "Malware refers to any software intentionally designed to harm or exploit your device. This can include viruses, spyware, or ransomware. Always keep your software up-to-date, and avoid downloading files or clicking on links from untrusted sources. Anti-malware software is a great way to protect yourself." },
            { "safe_browsing", "Safe browsing is all about staying aware of the websites you visit. Look for 'HTTPS' in the URL—it's a sign the site encrypts your data. Also, avoid clicking on suspicious links and be cautious of pop-ups or ads that seem too good to be true. A VPN can also protect you when browsing on public Wi-Fi." },
            { "data_privacy", "Data privacy means keeping your personal information private and secure. This includes things like your name, email, browsing habits, and even where you are. Be mindful of the information you share online, and review your privacy settings on social media platforms regularly to ensure your data is safe." },
            { "secure_messaging", "When it comes to messaging securely, look for services that offer end-to-end encryption, like Signal or WhatsApp. This means that only you and the recipient can read your messages, keeping hackers or unwanted third parties from eavesdropping." },
            { "encryption", "Encryption is the process of scrambling your data so that only authorized people can read it. It’s like sending a locked box where only the recipient has the key. This technology protects everything from your emails to online banking transactions, ensuring your sensitive information stays private." }
        };
    }

    public string GetResponse(string userInput)
    {
        string matchedCategory = keywordDictionary
            .FirstOrDefault(entry => entry.Value.Any(keyword => userInput.Contains(keyword)))
            .Key;

        if (!string.IsNullOrEmpty(matchedCategory) && responses.ContainsKey(matchedCategory))
        {
            return responses[matchedCategory];
        }
        return string.Empty;
    }
}