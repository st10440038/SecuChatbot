using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SecuBot.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Media;
    using System.IO;
    using System.Reflection;
    using Microsoft.VisualBasic;
    using static ResponseHandler;
    using System.Text.RegularExpressions;
    using System.Windows.Documents;
    using System.Windows.Navigation;




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


        // All the keywords and responses are stored in dictionaries (Responses and Keywords recognition)
        public ResponseHandler()
        {
            // Initialize keyword dictionary to map categories to user input (Keywords recognition 1)
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



            // Initialize first responses for each category (Responses - Keywords recognition 2)
            responses = new Dictionary<string, string>()
        {
            { ResponseCategories.HowAreYou, "I’m doing great, thanks for asking ! How about you ? How’s everything going on your end? I'm here to chat whenever you’re ready." },
            { ResponseCategories.WhatCanIAsk, "Oh, there's so much you can ask! I can provide tips on protecting your passwords, spotting phishing attacks, setting up two-factor authentication, and much more. Whatever’s on your mind regarding cybersecurity, feel free to ask!" },
            { ResponseCategories.Purpose, "I’m SecuBot, your cybersecurity guide. My goal is to help you stay safe online by answering your questions and providing tips on securing your data, devices, and accounts. Whether you're a beginner or already tech-savvy, I’ve got your back!" },
            { ResponseCategories.Cybersecurity, "Cybersecurity is all about protecting your digital life—your data, your devices, and your identity. It involves practices, tools, and technologies that keep your information safe from unauthorized access, damage, or attacks. In simple terms, it's making sure you're safe online." },
            { ResponseCategories.Passwords, "When creating a strong password, mix things up! Use a combination of upper and lowercase letters, numbers, and symbols. The longer, the better—12 characters or more is ideal. And please avoid common patterns or using personal info like birthdays. Password managers can also help you keep track of them securely." },
            { ResponseCategories.Phishing, "Phishing is when someone tries to trick you into revealing personal information, like passwords or credit card numbers, by pretending to be someone trustworthy. Often this happens via email or text messages with suspicious links or attachments." },
            { ResponseCategories.TwoFA, "Two-factor authentication (2FA) adds an extra layer of security by requiring two forms of verification—usually a password and a temporary code sent to your phone or email. It’s one of the easiest ways to secure your accounts from unauthorized access." },
            { ResponseCategories.SocialEngineering, "Social engineering is when attackers manipulate people into revealing sensitive information or performing actions they wouldn’t normally do. They might pretend to be someone you trust, like a co-worker or IT support, to get you to share login credentials or other private details." },
            { ResponseCategories.Malware, "Malware is malicious software designed to harm your device or steal information. It includes viruses, ransomware, spyware, and trojans. Always keep your software updated, use antivirus programs, and avoid downloading files from untrusted sources." },
            { ResponseCategories.SafeBrowsing, "Safe browsing means being cautious of the websites you visit. Stick to reputable sites, especially when entering sensitive information. Make sure the website’s URL begins with ‘https’ (the ‘s’ stands for secure), and be wary of pop-up ads or unsolicited offers." },
            { ResponseCategories.DataPrivacy, "Data privacy is about protecting your personal information online. Be mindful of what you share on social media, and ensure that the services you use are respecting your privacy. You should also be aware of the laws and regulations surrounding data protection, like GDPR." },
            { ResponseCategories.SecureMessaging, "Secure messaging platforms use encryption to protect the content of your conversations. Examples include apps like Signal and WhatsApp. Encrypted messaging ensures that only the intended recipient can read your messages, protecting your privacy." },
            { ResponseCategories.Encryption, "Encryption is a process that transforms your data into an unreadable format unless the recipient has the key to decrypt it. It’s widely used to secure sensitive information, such as communications, banking transactions, and personal data." },
            { ResponseCategories.Scam, "Scams are deceptive schemes designed to trick people into giving away money or sensitive information. They can happen through emails, phone calls, or fake websites. Be cautious of offers that sound too good to be true and always verify the legitimacy of the source before acting." }
        };



            // Initialize random responses for each category if the user wants to know more (Random responses)
            keywordResponses = new Dictionary<string, List<string>>()
        {
            { ResponseCategories.Phishing, new List<string>
                {
                    "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                    "Always check the sender's email address carefully. Scammers often use fake email addresses that look similar to legitimate ones.",
                    "If an email contains urgent language asking for sensitive data, it could be a phishing attempt. Take a moment to think before clicking any links.",
                    "Never click on suspicious links or download attachments from unknown sources. These could be used to steal your personal data.",
                    "Phishing scams often create a sense of urgency to trick users into reacting quickly without thinking. This emotional pressure is a common tactic used by attackers.",
                    "Phishers may create fake websites that look identical to real ones. Always double-check the web address before entering login credentials.",
                    "Even text messages (known as 'smishing') and phone calls (known as 'vishing') can be phishing attempts. It’s not just limited to email.",
                    "Hover over links before clicking to see the real URL. If it looks suspicious, don’t click it.",
                    "Grammar mistakes and odd language in messages are red flags of phishing attempts.",
                    "Use spam filters and report phishing attempts to your email provider to help protect others."
                }
            },

            { ResponseCategories.Cybersecurity, new List<string>
                {
                    "Always use strong, unique passwords for every account. Enable two-factor authentication where possible to add an extra layer of protection.",
                    "Stay up-to-date with the latest security patches. Cybercriminals often exploit vulnerabilities in outdated software to gain access to your system.",
                    "Cybersecurity includes everything from protecting your Wi-Fi network to ensuring the apps on your phone are secure and updated.",
                    "Think of cybersecurity as digital hygiene—just like washing hands prevents illness, good cybersecurity habits prevent digital threats.",
                    "Human error is one of the biggest security risks. Staying informed and cautious is just as important as using technical tools.",
                    "Install firewalls and antivirus software to build a strong first line of defense against attacks.",
                    "Avoid public Wi-Fi for sensitive transactions unless using a VPN.",
                    "Educate yourself about common threats like phishing, ransomware, and identity theft.",
                    "Don’t ignore system warnings and updates—they often fix security holes.",
                    "Back up your important data regularly to avoid loss from cyberattacks."
                }
            },

            { ResponseCategories.Passwords, new List<string>
                {
                    "Use a mix of uppercase, lowercase, numbers, and symbols in your passwords. The longer the password, the better—aim for at least 12 characters.",
                    "Never reuse passwords across different accounts. If one account is compromised, it can lead to others being hacked too.",
                    "Consider using a password manager to keep track of all your passwords securely. This way, you don’t need to remember each one.",
                    "Avoid using personal information like names or birthdates in your passwords—they're easy to guess or find online.",
                    "A passphrase made up of random words (like 'GreenHorseBatteryRiver!') can be easier to remember and harder to crack.",
                    "Changing your passwords regularly helps reduce the chances of long-term unauthorized access if a password is compromised.",
                    "Avoid writing down passwords where others can find them—use secure apps or vaults.",
                    "Do not share your passwords, even with people you trust.",
                    "Enable login notifications to be alerted of unauthorized attempts.",
                    "Always logout from shared or public computers after use."
                }
            },

            { ResponseCategories.TwoFA, new List<string>
                {
                    "When setting up 2FA, choose an authentication method that works for you, whether it’s a text message, app, or hardware key.",
                    "2FA significantly reduces the risk of your account being hacked, even if someone manages to steal your password.",
                    "2FA works like a double lock—if a hacker has your password, they’d still need the second factor to access your account.",
                    "Authenticator apps like Google Authenticator or Authy are more secure than SMS because text messages can be intercepted.",
                    "Using 2FA is one of the simplest ways to significantly improve your online security with minimal effort.",
                    "Biometric authentication like fingerprints or facial recognition can also serve as a second factor.",
                    "Some services offer backup codes—store them in a safe place for account recovery.",
                    "Enable 2FA on all critical accounts: email, banking, and cloud storage.",
                    "Be cautious when approving login requests—only approve ones you recognize.",
                    "Use multi-factor authentication (MFA) when possible for even stronger protection."
                }
            },

            { ResponseCategories.SocialEngineering, new List<string>
                {
                    "Social engineering relies on manipulating people into divulging confidential information. Be cautious if someone pressures you for sensitive data.",
                    "Attackers may pose as trusted individuals—like IT support or a colleague—to trick you into sharing private information. Always verify their identity first.",
                    "Never respond to unsolicited emails or phone calls asking for personal information. Take your time to evaluate the situation.",
                    "Social engineering isn't about hacking computers—it's about hacking people by exploiting trust, fear, or urgency.",
                    "Always question unusual requests, even if they come from someone you know. Their account may have been compromised.",
                    "Be alert for phrases like 'just this once' or 'don’t tell anyone'—these are common social engineering red flags.",
                    "If something feels off during a conversation or interaction, trust your instincts and verify the source.",
                    "Don’t give out security codes, passwords, or personal info over the phone or chat.",
                    "Be wary of surveys that ask for sensitive information—they might be fake.",
                    "Companies rarely ask for sensitive details out of the blue. Confirm requests through official channels."
                }
            },

            { ResponseCategories.Scam, new List<string>
                {
                    "Scams can take many forms, including fake emails, phone calls, or websites. Always be skeptical of offers that seem too good to be true.",
                    "If someone asks for personal information or money upfront, it’s likely a scam. Legitimate organizations will never ask for sensitive data in this way.",
                    "Research any company or individual before engaging with them. Look for reviews or reports of scams associated with them.",
                    "Be cautious of unsolicited messages claiming you’ve won a prize or lottery. Scammers often use these tactics to lure victims.",
                    "If you feel pressured to act quickly, take a step back. Scammers often create a sense of urgency to trick you into making hasty decisions.",
                    "Trust your instincts—if something feels off, it probably is. Always err on the side of caution.",
                    "Use official websites and contact information when verifying claims.",
                    "Avoid clicking links in suspicious messages—go directly to the source instead.",
                    "Report scams to your local authorities or consumer protection agencies.",
                    "Keep up with common scam trends by checking cybersecurity news."
                }
            },

            { ResponseCategories.Malware, new List<string>
                {
                    "Malware is any software intentionally designed to cause damage. Viruses, ransomware, and spyware are all types of malware.",
                    "To protect against malware, keep your operating system and software up-to-date, use antivirus software, and avoid downloading files from untrusted sources.",
                    "If you suspect your device is infected with malware, run a full system scan with trusted antivirus software and disconnect from the internet to prevent further damage.",
                    "Malware can hide inside innocent-looking software or files. Always download apps from official sources only.",
                    "Some malware operates silently in the background, stealing data without obvious symptoms. Regular scans are important.",
                    "Ransomware is a type of malware that locks your files and demands payment to unlock them. Backups are your best defense.",
                    "Be cautious of pop-up warnings urging immediate software downloads—they can be fake.",
                    "Avoid pirated software—it’s a common way malware spreads.",
                    "Use a secure browser and disable unnecessary plugins.",
                    "Set your antivirus software to update automatically to stay protected."
                }
            },

            { ResponseCategories.SafeBrowsing, new List<string>
                {
                    "Safe browsing is essential for protecting your personal data. Always make sure the website URL starts with 'https' (the 's' stands for secure).",
                    "Avoid clicking on pop-up ads or suspicious links, even if they appear to be from a reputable source. These could lead to harmful websites.",
                    "Use an ad blocker and privacy-focused browser extensions to minimize the risk of encountering malicious sites while browsing.",
                    "Legitimate websites usually have clear, professional layouts and don’t flood your screen with pop-ups or spelling errors.",
                    "Look for security indicators like a padlock symbol in the address bar to confirm a site is encrypted.",
                    "If a site asks for personal information too soon or without reason, that’s a red flag. Leave the site immediately.",
                    "Avoid downloading browser extensions that aren't well-reviewed or from unknown developers.",
                    "Clear your browser cache and cookies regularly to maintain privacy.",
                    "Use private browsing or incognito mode when accessing sensitive information.",
                    "Educate yourself and others on safe browsing habits."
                }
            },

            { ResponseCategories.DataPrivacy, new List<string>
                {
                    "Data privacy is about keeping your personal information safe. Be selective about the data you share online, and only provide necessary details to trusted services.",
                    "Make sure you're familiar with the privacy policies of the websites and apps you use. Understand what data they collect and how they protect it.",
                    "Consider using a VPN (Virtual Private Network) to encrypt your internet connection and protect your online privacy when using public networks.",
                    "Your digital footprint includes everything from search history to app permissions. Managing it protects your privacy.",
                    "Companies may collect more data than they need. Review app permissions and turn off those you don’t use.",
                    "Always log out of public or shared computers to prevent others from accessing your private information.",
                    "Use encrypted email services if you're sharing confidential data.",
                    "Disable location tracking for apps that don’t need it.",
                    "Think before you post—social media is a big part of your online identity.",
                    "Be careful with cloud storage—encrypt sensitive files before uploading."
                }
            },

            { ResponseCategories.SecureMessaging, new List<string>
                {
                    "Secure messaging ensures that only the intended recipient can read your messages. End-to-end encryption is a key feature of secure messaging apps like Signal and WhatsApp.",
                    "Consider using encrypted messaging services to safeguard sensitive information. These services use encryption algorithms to protect your messages from eavesdropping.",
                    "Whenever possible, avoid sending sensitive data over unsecured channels like SMS. Use secure, encrypted messaging apps instead.",
                    "End-to-end encryption means not even the service provider can read your messages. Only you and your recipient can.",
                    "Secure messaging is especially important for discussing financial, legal, or medical matters.",
                    "Some apps claim to be secure but don’t offer full encryption. Always check for end-to-end encryption in their features.",
                    "Check app permissions—some messaging apps request access to more data than they need.",
                    "Use disappearing messages or self-destruct timers for highly confidential information.",
                    "Always update your messaging apps to patch potential security flaws.",
                    "Avoid using public/shared devices for secure conversations."
                }
            },

            { ResponseCategories.Encryption, new List<string>
                {
                    "Encryption turns your data into an unreadable format, making it secure from unauthorized access. Only those with the decryption key can read it.",
                    "End-to-end encryption ensures that data is encrypted from the sender to the recipient, preventing anyone else from intercepting it during transit.",
                    "Encryption is used in many aspects of digital life, including securing emails, banking transactions, and messaging. It’s one of the best ways to protect sensitive information.",
                    "Encryption is like sealing a message in a locked box. Even if someone intercepts it, they can’t read it without the key.",
                    "There are different types of encryption—symmetric (same key to encrypt and decrypt) and asymmetric (public/private key pairs).",
                    "Most websites use encryption through SSL/TLS, which protects your data during transmission. That’s what the 'https' in URLs means.",
                    "File encryption protects sensitive documents stored on your device or cloud.",
                    "Using full-disk encryption protects data even if your device is lost or stolen.",
                    "Encrypted backups are crucial for secure data recovery.",
                    "Avoid sending encryption keys through the same channel as the data itself."
                }
            }

        };
        }



        // List of confusion keywords (Random responses)
        public List<string> confusionKeywords = new List<string>
        {
            "explain", "not sure", "don't understand", "confused", "more info", "more details", "tell me more", "clarify", "say again", "don't get it"
        };


        // To detect the category of user input (Memory and Recall for interests)
        public string DetectCategory(string input)
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


        // To detect the last topic of the conversation (Random responses based on the same topic)
        string lastTopic = null;


        // To get the bot response based on user input
        public string GetBotResponse(string input)
        {
            input = input.ToLower();

            string category = DetectCategory(input);  // To get the category of user input after it has been detected by the method DetectCategory
            bool confused = IsConfused(input);


            // If the user is confused but we know the same topic (Random responses)
            if (confused && lastTopic != null && keywordResponses.ContainsKey(lastTopic))
            {
                var clarifications = keywordResponses[lastTopic];
                var random = new Random();
                return clarifications[random.Next(clarifications.Count)];  // It returns a random response abot the topic
            }


            // If input contains a known category like "phishing" - (Random responses)
            if (category != null)
            {
                lastTopic = category; // Update the last topic


                // If the user is confused about the topic    
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


    }



    // Chatbot class that handles the conversation with the user
    public class Chatbot
    {
        private User userObj;
        private ResponseHandler handlerObj;


        public Chatbot()
        {
            userObj = new User();
            handlerObj = new ResponseHandler();
        }



        // Dictionary for interest-based tips (Memory and Recall)
        Dictionary<string, List<string>> interestTips = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { ResponseCategories.Cybersecurity, new List<string>
                {
                    "Use strong, unique passwords for every account you own.",
                    "Install updates for your system and apps as soon as they're available.",
                    "Be skeptical of unsolicited messages or emails with links.",
                    "Connect to public Wi-Fi only when necessary, and use a VPN when you do.",
                    "Back up your important data to both physical and cloud storage regularly.",
                    "Turn on firewalls and antivirus protection on all devices.",
                    "Log out of websites after finishing your session, especially on shared computers.",
                    "Don't install software unless you trust the source and know it's safe.",
                    "Stay updated on common scams and digital threats through trusted sources.",
                    "Use security questions that are hard to guess—not easily found online."
                }
            },

            { ResponseCategories.DataPrivacy, new List<string>
                {
                    "Limit how much personal info you share online, especially on social media.",
                    "Disable location tracking unless it's absolutely needed for the app.",
                    "Use browsers and extensions that prioritize privacy, like DuckDuckGo or Brave.",
                    "Carefully read privacy policies for apps before giving them access.",
                    "Be mindful of what permissions mobile apps are asking for.",
                    "Use aliases or nicknames on websites that don’t require legal names.",
                    "Delete old accounts you no longer use to reduce your digital footprint.",
                    "Regularly clear your browser's cookies and history.",
                    "Turn off ad personalization in your browser or device settings.",
                    "Avoid sharing sensitive personal info in public forums or comment sections."
                }
            },

            { ResponseCategories.Phishing, new List<string>
                {
                    "Never click on links or attachments from unknown senders.",
                    "Phishing emails often create a false sense of urgency—stay calm and double-check.",
                    "Check the domain name in the sender’s email—fake ones are often slightly off.",
                    "Avoid entering sensitive data on sites accessed via email links.",
                    "Use spam filters and keep them enabled to block phishing attempts.",
                    "If an email seems odd—even from someone you know—verify before responding.",
                    "Be cautious with messages asking for money, especially in cryptocurrency.",
                    "Look for spelling and grammar mistakes—scammers make them often.",
                    "Hover over links before clicking to check the actual destination URL.",
                    "Report suspicious emails to your provider or company’s security team."
                }
            },

            { ResponseCategories.Passwords, new List<string>
                {
                    "Create passwords with a mix of letters, numbers, and special characters.",
                    "Avoid using birthdays, names, or common words in your passwords.",
                    "Use a password manager to store and generate secure passwords.",
                    "Change passwords immediately if you suspect an account is compromised.",
                    "Enable login alerts on your accounts to detect unauthorized access.",
                    "Don’t reuse the same password across different websites.",
                    "Set up password recovery options like security questions and backup emails.",
                    "Avoid saving passwords in browsers—use secure tools instead.",
                    "Regularly update your passwords every few months.",
                    "Use passphrases (like song lyrics or unique sentences) for better strength."
                }
            },

            { ResponseCategories.Malware, new List<string>
                {
                    "Don’t download attachments from unknown emails.",
                    "Only install software from official or verified sources.",
                    "Use antivirus software and scan your devices regularly.",
                    "Ignore pop-up warnings offering 'free virus scans'.",
                    "Avoid pirated content—it often comes bundled with malware.",
                    "Check file extensions before opening (.exe files can be risky).",
                    "Disable macros in Office documents from unknown sources.",
                    "Be wary of browser extensions—they can carry hidden threats.",
                    "Set your system to automatically scan downloaded files.",
                    "Keep plugins and flash players updated or remove them if not needed."
                }
            },

            { ResponseCategories.TwoFA, new List<string>
                {
                    "Enable two-factor authentication (2FA) on all your important accounts.",
                    "Use an authenticator app like Google Authenticator or Authy instead of SMS.",
                    "Don’t share your 2FA codes with anyone, even if they claim to be support.",
                    "Keep a copy of your recovery codes in a secure offline place.",
                    "Use hardware-based tokens like YubiKey for stronger protection.",
                    "Check that each service supports 2FA and activate it where possible.",
                    "Be cautious of fake 2FA prompts after entering passwords.",
                    "Never approve a login request if you didn’t initiate it yourself.",
                    "Add a biometric lock on your 2FA app for extra protection.",
                    "Avoid backing up 2FA credentials to cloud services unless encrypted."
                }
            },

            { ResponseCategories.SocialEngineering, new List<string>
                {
                    "Always double-check the identity of someone asking for private info.",
                    "Never let urgency rush your decision to share sensitive details.",
                    "Legitimate institutions don’t ask for passwords over phone or email.",
                    "Be wary of overly friendly messages from strangers asking for help.",
                    "Don’t assume someone is trustworthy because they sound professional.",
                    "Avoid clicking links in DMs unless you’ve confirmed the sender.",
                    "Hang up and call official numbers if you suspect a scammer.",
                    "Ignore messages that play on fear, guilt, or flattery.",
                    "Be cautious of fake support agents or HR reps online.",
                    "Stay calm and seek advice when unsure—social engineers rely on pressure."
                }
            },

            { ResponseCategories.SafeBrowsing, new List<string>
                {
                    "Always look for the padlock symbol and 'https' in your browser.",
                    "Don't download files unless you're sure the site is safe.",
                    "Use browsers that regularly update security features.",
                    "Avoid adult or pirated content websites—they’re malware hotspots.",
                    "Don’t allow push notifications from random sites.",
                    "Install anti-malware browser extensions for extra safety.",
                    "Avoid logging into accounts on shared or public computers.",
                    "Turn off auto-fill for forms in public or risky environments.",
                    "Be skeptical of online quizzes asking for too much personal info.",
                    "Use incognito or private browsing mode when researching sensitive topics."
                }
            },

            { ResponseCategories.SecureMessaging, new List<string>
                {
                    "Use messaging apps with end-to-end encryption like Signal or WhatsApp.",
                    "Avoid sending private information through regular SMS.",
                    "Enable screen lock and biometric access to your messaging apps.",
                    "Delete old messages that contain sensitive information.",
                    "Avoid clicking on links received in group chats.",
                    "Use disappearing messages if discussing confidential topics.",
                    "Verify contact identity using built-in security codes.",
                    "Update messaging apps frequently to fix security bugs.",
                    "Review permissions granted to messaging apps.",
                    "Don’t allow your app to back up messages to unencrypted cloud storage."
                }
            },

            { ResponseCategories.Encryption, new List<string>
                {
                    "Enable full-disk encryption on laptops and smartphones.",
                    "Use encrypted USB drives for transporting sensitive data.",
                    "Encrypt email using tools like PGP or ProtonMail.",
                    "Use services that encrypt data both in transit and at rest.",
                    "Double-check cloud storage services for encryption features.",
                    "Use messaging apps that offer built-in encryption.",
                    "Avoid free Wi-Fi if you’re handling unencrypted information.",
                    "Encrypt backup files before storing them online or offline.",
                    "Use strong passwords for encrypted files and containers.",
                    "When sharing files, use encrypted transfer platforms or zipped folders with passwords."
                }
            },

            { ResponseCategories.Scam, new List<string>
                {
                    "Don’t send money to people or causes without verifying their identity.",
                    "Be cautious of job offers or prizes you didn’t apply for.",
                    "Scammers often impersonate banks or tax agencies—always double-check.",
                    "Don’t trust unknown messages with urgent financial requests.",
                    "Use caller ID to screen strange phone numbers.",
                    "Never give out banking or card details over email or unsolicited calls.",
                    "Check URLs closely—scam websites often use tiny changes in spelling.",
                    "Look up phone numbers or emails online before responding.",
                    "Block and report scammers on messaging platforms.",
                    "When in doubt, talk to someone you trust before taking action."
                }
            }
        };




        // List of keywords to make sure they match the dictionary keys (Interest detection - Memory and Recall)
        List<string> knownInterests = new List<string>
        {
            "phishing", "data privacy", "passwords", "malware", "2fa",
            "social engineering", "safe browsing", "secure messaging",
            "encryption", "scam", "cybersecurity"
        };



        // To detect the interest of user input (Memory and Recall)
        public string DetectInterestKeyword(string userInput)
        {
            foreach (string interest in knownInterests)
            {
                if (userInput.IndexOf(interest, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return interest;
                }
            }
            return null;
        }



        // Method to show interest tips every 3 turns (Recall)
        public void ShowInterestBasedTip(User user, int turn)
        {
            if (!string.IsNullOrEmpty(user.Interest) && turn % 3 == 0)
            {
                if (interestTips.TryGetValue(user.Interest, out List<string> tips) && tips.Count > 0)
                {
                    // Ensure the user has a tip history for this interest
                    if (!user.ShownTipsByInterest.ContainsKey(user.Interest))
                    {
                        user.ShownTipsByInterest[user.Interest] = new HashSet<string>();
                    }


                    // Get a random tip that hasn't been shown yet
                    var shownTips = user.ShownTipsByInterest[user.Interest];
                    var availableTips = tips.Where(t => !shownTips.Contains(t)).ToList();


                    // Reset if all tips were already shown
                    if (availableTips.Count == 0)
                    {
                        shownTips.Clear();
                        availableTips = new List<string>(tips);
                    }


                    // Select a random tip from the available tips
                    Random rnd = new Random();
                    string randomTip = availableTips[rnd.Next(availableTips.Count)];
                    shownTips.Add(randomTip); // Mark as shown


                    // Display the tip
                    string[] templates = new string[]
                    {
                    $"As someone interested in {user.Interest}, you might want to {{0}}",
                    $"Since you're focused on {user.Interest}, consider this: {{0}}",
                    $"Your interest in {user.Interest} is important, {userObj.Name}. Here's a tip: {{0}}"
                    };

                    // Select a random template
                    string selectedTemplate = templates[rnd.Next(templates.Length)];
                    string finalTip = string.Format(selectedTemplate, randomTip);

                    // Display the tip in a different color
                    Console.WriteLine("\n" + finalTip);
                    Console.WriteLine();
                }
            }
        }



        private string userName = "";
        private string interest = "";
        private Dictionary<string, DateTime> reminders = new Dictionary<string, DateTime>();
        private List<(string question, string answer)> quizQuestions;
        private int quizIndex = -1;
        private int quizScore = 0;


        public string ProcessInput(string input, List<string> activityLog)
        {
            input = input.ToLower();


            if (input == null)
            {
                return($"SecuBot: Oops {userObj.Name}! It seems like I missed that. Could you please try again?");
            }


            if (input == "exit" || input == "bye" || input == "quit" || input == "no more questions")
            {
                return ($"SecuBot: It was a pleasure assisting you today. Stay safe online, and don’t hesitate to reach out again. Goodbye {userObj.Name}!");
                
            }


            // Handle name
            if (input.Contains("my name is"))
            {
                userObj.Name = input.Trim();
                string userName = userObj.Name;
                return $"Nice to meet you, {userName}!";
            }




            // Interest
            if (input.ToLower().StartsWith("i'm interested in"))
            {
                userObj.Interest = input.Substring("i'm interested in ".Length).Trim();
                string interest = userObj.Interest;

                string detectedInterest = DetectInterestKeyword(interest);

                if (!string.IsNullOrEmpty(detectedInterest))
                {
                    userObj.Interest = detectedInterest;
                    return $"SecuBot: Great! I'll remember that you're interested in {detectedInterest}.";
                }
                else
                {
                    return "SecuBot: Could you repeat that? I didn't catch the topic you're interested in.";
                }
            }



            // Sentiment
            string sentiment = DetectSentiment(input);
            string empatheticReply = GetEmpatheticResponse(sentiment, input);






            // Default fallback
            return "I'm not sure I understand. Try asking about phishing, setting reminders, or starting the quiz.";
        }


        public static bool IsQuizStartIntent(string input)
        {
            input = input.ToLower();

            string[] quizPhrases =
            {
                "start quiz", "play quiz", "take quiz", "do quiz", "quiz time",
                "start the test", "test me", "give me questions", "launch quiz",
                "start game", "play game", "cybersecurity game", "try a game",
                "challenge me", "begin quiz", "run the quiz", "start the challenge",
                "begin the game", "do the test", "i want to do a quiz", "quiz now",
                "ready for the quiz", "quiz me", "let's play", "fire up the quiz",
                "give me a quiz", "ask me questions", "start my knowledge test",
                "start knowledge check", "run game", "start cybersecurity game",
                "open the quiz", "show me quiz", "go to quiz", "initiate quiz",
                "i’d like to try the quiz", "can i try the quiz", "can we begin the quiz",
                "start quiz challenge", "ready for game", "ready to play"
            };

            return quizPhrases.Any(p => input.Contains(p));
        }


        public DateTime? TryParseReminder(string input)
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


        // Quiz flow
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




        // To detect the sentiment of user input with synonyms for a large range of inputs 
        public string DetectSentiment(string input)
        {

            input = input.ToLower();


            // Worried Sentiment
            if (input.Contains("worried") || input.Contains("anxious") || input.Contains("scared") || input.Contains("concerned") || input.Contains("nervous") || input.Contains("uneasy"))
                return "worried";


            // Curious Sentiment
            if (input.Contains("curious") || input.Contains("interested") || input.Contains("wondering") || input.Contains("inquiring") || input.Contains("questioning") || input.Contains("keen"))
                return "curious";


            // Frustrated Sentiment
            if (input.Contains("frustrated") || input.Contains("confused") || input.Contains("annoyed") || input.Contains("irritated") || input.Contains("angry") || input.Contains("disappointed"))
                return "frustrated";


            // Confident Sentiment
            if (input.Contains("confident") || input.Contains("sure") || input.Contains("positive") || input.Contains("certain") || input.Contains("self-assured") || input.Contains("optimistic"))
                return "confident";


            // Overwhelmed Sentiment
            if (input.Contains("overwhelmed") || input.Contains("stressed") || input.Contains("lost") || input.Contains("swamped") || input.Contains("burdened") || input.Contains("flooded"))
                return "overwhelmed";


            // Happy Sentiment
            if (input.Contains("happy") || input.Contains("excited") || input.Contains("glad") || input.Contains("joyful") || input.Contains("elated") || input.Contains("cheerful"))
                return "happy";


            // Default to Neutral if no sentiment is detected
            return "neutral";
        }



        // To get an empathetic response based on the user's sentiment detected
        public string GetEmpatheticResponse(string sentiment, string input)
        {

            ResponseHandler categoryObj = new ResponseHandler();
            string category = categoryObj.DetectCategory(input);  // To get the category of user input 

            switch (sentiment)
            {
                case "worried":
                    return $"It's completely understandable to feel that way. Tell me all your questions about that {category}, and I will share some tips to help you stay safe.";

                case "curious":
                    return $"I love that you're curious about {category}, ! It's a great way to protect yourself and learn.";

                case "frustrated":
                    return $"I know it can feel frustrating. Especially that the whole cybersecurity field can be tricky, but I'm here to help you with this {category}.";

                case "confident":
                    return $"That's great to hear ! Confidence is key in cybersecurity. Keep up the good work!";

                case "overwhelmed":
                    return $"I understand that {category} can feel overwhelming at times. Don't worry, we'll break it down together, step by step.";

                case "happy":
                    return $"I'm glad to hear you're feeling happy ! Cybersecurity in general should give you peace of mind, not stress.";

                default:
                    return null;
            }
        }
    }



    // User class to store user information and preferences (Memory and Recall)
    public class User
    {
        public string Name { get; set; }
        public string Interest { get; set; }


        // Tracks shown tips by interest
        public Dictionary<string, HashSet<string>> ShownTipsByInterest { get; set; } = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
    
    }


    

}
