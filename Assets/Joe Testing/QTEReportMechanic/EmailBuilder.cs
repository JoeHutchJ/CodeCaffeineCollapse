using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EmailSentiment {POSITIVE, NEGATIVE, SPAM, INQUIRY}

public class Author {
    public string name;
    public Texture2D icon;

    public string[] AuthorNames = {
            "john.doe@fastmail.com",
    "sarah.smith@protonmail.net",
    "michael.brown@gmx.co.uk",
    "emily.johnson@outlook.com",
    "david.wilson@icloud.co.uk",
    "olivia.martin@yahoo.net",
    "william.anderson@mail.com",
    "sophia.thomas@inbox.co.uk",
    "james.rodriguez@live.com",
    "ava.miller@zoho.co.uk",
    "ethan.lewis@protonmail.com",
    "emma.thompson@fastmail.net",
    "noah.jackson@gmx.co.uk",
    "mia.hall@outlook.com",
    "benjamin.wright@yahoo.net",
    "isabella.green@mail.co.uk",
    "samuel.carter@icloud.com",
    "sophie.harris@inbox.net",
    "alexander.clark@zoho.co.uk",
    "grace.evans@live.com"

    };

    public string[] SpamNames = {
         "spammyjunk1001@fakeemail.com",
    "cashprizewinner@scammail.net",
    "freestuffoffers@spambox.co.uk",
    "onlinebizopportunity@getrichquick.com",
    "nigerianprince419@scammail.com",
    "lotterywinnernotification@freemail.co.uk",
    "notarealperson@fakeinbox.com",
    "yourbankaccountinfo@phishingmail.net",
    "guaranteedloansapproval@spammyloans.co.uk",
    "malwarealertsecurity@fakemail.com",
    "urgentmessagefromceo@phishingmail.net",
    "secretinheritanceclaim@scammail.net",
    "sweepstakesprizewinner@fakeemail.com",
    "weightlossmiracleproducts@spammyinbox.com",
    "freeipadgiveaway@fakeinbox.net",
    "forexinvestmentopportunity@scammail.com",
    "lotteryticketclaim@spambox.net",
    "spammyoffersgalore@junkmail.co.uk",
    "moneymakingsecrets@scambox.com",
    "discountedmedications@spammyinbox.net"

    };

    public Author(bool spam, Texture2D _icon) {
        if (!spam) {
                name = AuthorNames[UnityEngine.Random.Range(0,AuthorNames.Length)];
        } else {
                name = AuthorNames[UnityEngine.Random.Range(0,SpamNames.Length)];
        
        }

        icon = _icon;
    }
}

public static class EmailBuilder
{
    static UnityEngine.Object[] iconObjects;

    static List<Texture2D> icons;

    static List<Texture2D> Unusedicons;
    static List<Author> authors;

    static List<Author> spamAuthors;

    static string[] positiveSubjects = {
            "Well Done on the Last Code Commit!",
    "Great Job with the Recent Updates!",
    "Congratulations on a Successful Task Completion!",
    "Impressive Work on the Coding Challenge!",
    "Excellent Progress on the Project!",
    "Kudos on a Job Well Done!",
    "Thumbs Up for Your Hard Work and Dedication!",
    "Nice Job on the Latest Code Refactoring!",
    "Congratulations on a Well-Written Piece of Code!",
    "Well Executed on the Task at Hand!",
    "Keep up the Good Work on the Project!",
    "Appreciating Your Efforts and Contributions!",
    "Congratulations on a Successful Milestone!",
    "Well-Delivered Report - Well Done!",
    "Good Job on the Recent Bug Fixing!",
    "Your Code Review was Exceptional!",
    "Impressed by Your Attention to Detail!",
    "Excellent Progress on the Development Tasks!",
    "Great Effort and Results - Keep it Up!",
    "Well Done on Meeting the Project Deadlines!",

    };

    static string[] negativeSubjects = {
         "Your Last Commit Broke Everything! We Need a Fix ASAP!",
    "Code Review Disaster: Your Changes Are Riddled with Bugs!",
    "Urgent: Your Code Changes Caused Chaos in the System!",
    "Attention Required: Your Recent Updates Created a Nightmare!",
    "Major Issues Detected: Your Code Changes Have Caused Havoc!",
    "Critical Problem Alert: Your Updates Have Wreaked Havoc!",
    "Warning: Your Code Modifications Resulted in a Catastrophe!",
    "Disastrous Code Quality: Your Work Is Frustratingly Poor!",
    "System Breakdown: Your Recent Code Is Full of Showstopper Bugs!",
    "Unacceptable Code: Your Changes Have Made a Mess of Things!",
    "Emergency: Your Commit Resulted in Server Meltdown!",
    "Big Trouble: Your Code Changes Have Brought Chaos!",
    "Unresolved Bugs Galore: Your Code Review Was a Disaster!",
    "Code Catastrophe: Your Modifications Created a Security Nightmare!",
    "Critical Situation: Your Updates Led to Data Corruption!",
    "Alert: Your Recent Code Is Causing Frequent Application Crashes!",
    "Frustrating Performance: Your Code Changes Slowed Everything Down!",
    "Buggy Code: Countless Issues Found in Your Recent Work!",
    "Data Chaos: Your Commit Introduced Inconsistent States!",
    "Code Nightmare: Poor Quality and Lack of Documentation in Your Work!",

    };

    static string[] spamSubjects = {
            "You've won a cash prize!",
    "Claim your free vacation package now!",
    "Exclusive business opportunity!",
    "URGENT: Important notification",
    "Your account has been selected for a special offer",
    "Get rich quick with this amazing opportunity",
    "Important message from a Nigerian Prince",
    "Congratulations! You've won a luxury car",
    "Limited time discount on top-brand products",
    "Increase your income with this secret method",
    "Your payment has been approved",
    "URGENT: Immediate action required",
    "Unlock the secrets to unlimited wealth",
    "Special offer: Buy one, get one free",
    "You're a lucky winner of a dream vacation",
    "Lose weight effortlessly with this miracle product",
    "Attention: Your bank account needs verification",
    "Exclusive investment opportunity with high returns",
    "Claim your lottery prize now!",
    "Amazing deals on top-rated electronics"

    };
    static string[] inquirySubjects = {
         "Status Update Request: Current Branch Progress",
    "Demo Request: Expected Timeline for Progress Review",
    "Deadline Inquiry: ETA for Task Completion",
    "Query: Next Steps in the Project",
    "Urgent: Clarification Needed on Requirements",
    "Meeting Request: Discuss Project Scope and Deliverables",
    "Progress Update: Milestone Accomplishments",
    "Budget Inquiry: Project Cost Summary",
    "Technical Support Needed: Troubleshooting Assistance",
    "Request for Documentation: System Architecture Overview",
    "Timeline Revision Request: Project Schedule Changes",
    "Collaboration Inquiry: Availability for Team Discussion",
    "Product Query: Feature Functionality and Usage",
    "Training Request: Workshop on New Technology Implementation",
    "Review Request: Quality Assurance and Bug Fixing",
    "Resource Allocation Inquiry: Staffing Requirements",
    "Vendor Inquiry: Pricing and Service Agreement Details",
    "Scope Clarification Needed: Project Requirements",
    "Legal Query: Contractual Terms and Agreements",
    "Data Access Request: Data Retrieval and Analysis",
    };

    static string[] positiveMessage = {
         "Good work on the last code commit! Your changes have greatly improved the functionality of the system. Keep up the excellent coding skills!",
    "That report is really great! It provides a comprehensive analysis and valuable insights. Your attention to detail and thoroughness is commendable. Well done!",
    "Congratulations on completing the task successfully! Your solution is well-structured and efficient. Your coding skills continue to impress us. Keep up the fantastic work!",
    "Impressive job on the coding challenge! Your logical approach and clean code demonstrate your expertise. Your problem-solving abilities are top-notch. Well done!",
    "Great effort on the project! Your contributions have been instrumental in achieving the desired outcomes. Your dedication and commitment are truly appreciated. Keep up the amazing work!",
    "Kudos on a job well done! Your recent code refactor has significantly improved the codebase's readability and maintainability. Your attention to code quality is commendable!",
    "Well executed on the task at hand! Your attention to detail and meticulousness have ensured a flawless implementation. Your hard work is recognized and appreciated!",
    "Congratulations on reaching a successful milestone! Your consistent effort, determination, and teamwork have brought us closer to our project goals. Keep up the outstanding work!",
    "Great job on the recent bug fixing! Your swift action and thoroughness have resolved critical issues. Your problem-solving skills are highly valued and appreciated!",
    "Well-written piece of code! Your clean and concise code style, along with meaningful comments, makes it easy to understand and maintain. Your coding practices are commendable!",
    "Fantastic progress on the development tasks! Your consistent efforts and timely deliverables have been instrumental in keeping the project on track. Keep up the excellent work!",
    "Congratulations on meeting the project deadlines! Your strong time management skills and focused approach have ensured timely completion. Your commitment to quality is commendable!",
    "Thumbs up for your hard work and dedication! Your contributions have been invaluable, and your positive attitude has had a significant impact on the team. Keep up the great work!",
    "Well-delivered report - well done! Your comprehensive analysis, clear presentation, and insightful recommendations make it a valuable resource for decision-making. Excellent job!",
    "Impressed by your attention to detail! Your meticulous testing approach has uncovered critical issues, ensuring a robust and reliable system. Your thoroughness is appreciated!",
    "Excellent progress on the project! Your proactive approach, effective collaboration, and consistent productivity have contributed significantly to the project's success. Well done!",
    "Your code review was exceptional! Your insightful feedback and constructive suggestions have greatly improved the overall code quality. Your expertise is highly valued!",
    "Keep up the good work on the project! Your consistent focus, creativity, and problem-solving abilities continue to drive the project forward. Your dedication is inspiring!",
    "Appreciating your efforts and contributions! Your positive attitude, teamwork, and willingness to go the extra mile have made a significant difference in achieving our goals. Thank you!",
    "Impressive work on the coding challenge! Your elegant and efficient solution showcases your deep understanding of programming concepts. Your coding skills are commendable!",
    "Well done on the successful task completion! Your attention to requirements, thorough testing, and seamless integration have ensured a high-quality deliverable. Congratulations!",
    "Great job with the recent updates! Your enhancements and optimizations have significantly improved the system's performance and user experience. Your efforts are highly valued!",

    };

    static string[] negativeMessage = {
         "Your last commit broke the server! We're experiencing downtime and angry users. You need to fix it immediately and be more careful in the future.",
    "Your code review was bad; there are tons of bugs and performance issues. It's frustrating to see such poor quality work. Please improve and pay attention to details.",
    "We found critical flaws in your recent code changes. The system is unstable, and it's causing headaches for everyone. You must address these issues promptly and thoroughly.",
    "Your updates introduced numerous security vulnerabilities. It's alarming and unacceptable. We need you to prioritize security and follow best practices when coding.",
    "The report you submitted is disappointing. It lacks crucial information, and the analysis is flawed. We expected better quality work from you. Please revise it accordingly.",
    "Your recent code modifications have caused frequent crashes and data corruption. It's causing significant disruptions and frustration. We need immediate resolution and better code quality.",
    "Your coding style is chaotic and hard to understand. It makes maintenance a nightmare. We expect clean and well-structured code. Please improve your coding practices.",
    "Your recent changes have regressed multiple functionalities. It's frustrating to see features that used to work perfectly now broken. We need a quick fix and thorough testing.",
    "Your code lacks proper documentation, making it challenging for others to understand and collaborate. We need comprehensive and clear comments. Improve your documentation skills.",
    "Your recent commit has introduced memory leaks and performance degradation. It's impacting the system's performance and efficiency. Optimize your code and fix these issues immediately.",
    "Your code changes have led to inconsistent data states, creating confusion and errors. We expect reliable and consistent data handling. Pay attention to data integrity in your code.",
    "Your work on the project has been disappointing. Your lack of commitment and missed deadlines are causing delays. We need better focus and timely delivery of tasks.",
    "Your recent modifications have resulted in poor user experience. It's frustrating for our customers. We need you to prioritize usability and consider the end-user's perspective.",
    "Your recent updates lack proper testing. It's causing unexpected issues and disruptions. We need comprehensive and thorough testing before deploying any changes.",
    "Your code quality is subpar, with countless bugs and poor performance. It's causing frustration and impacting productivity. We expect higher standards of coding from you.",
    "Your recent code changes have violated coding standards and best practices. It's essential to adhere to guidelines for consistency and maintainability. Improve your code quality.",
    "Your recent updates have introduced unnecessary complexity, making the code difficult to maintain. Simplicity and clarity should be your focus. Refactor and simplify your code.",
    "Your lack of attention to detail is evident in your recent work. It's causing errors and requiring constant fixes. We need you to be more thorough and precise in your coding.",
    "Your recent code review lacked constructive feedback. It's important to provide insightful comments and suggestions for improvement. Take the time to review code thoroughly.",
    "Your recent modifications have created compatibility issues across different platforms. It's hindering deployment and causing frustration. We need you to ensure cross-platform compatibility.",
    "Your recent changes have violated version control practices, leading to confusion and conflicts. It's crucial to follow proper branching and merging strategies. Learn and apply them.",

    };

    static string[] spamMessage = {
         "Congratulations! You're the Lucky Winner of a Luxurious 7-Day Vacation to an Exotic Tropical Paradise - Claim Your Prize Now!",
    "URGENT: Take Immediate Action to Secure Your Financial Future and Achieve Lasting Wealth with Our Revolutionary Investment Program!",
    "Exclusive Limited Time Offer: Double Your Investment Returns with Our Proven System - Don't Miss Out on This Opportunity!",
    "Unlock the Secrets to Effortless Weight Loss and Get the Body You've Always Dreamed Of - Try Our Revolutionary Product Today!",
    "Special Invitation: Join Our Elite Club and Gain Access to Exclusive VIP Benefits, Luxury Events, and Exciting Perks!",
    "Claim Your Guaranteed Cash Prize Now and Fulfill Your Wildest Dreams - Act Fast to Secure Your Reward!",
    "Attention: Important Notification Regarding Your Bank Account Security - Protect Yourself from Unauthorized Access!",
    "Act Fast and Secure Your Spot in Our High-Earning Affiliate Program Today - Start Earning Big Commission!",
    "Discover the Fountain of Youth: Rejuvenate Your Skin and Look Years Younger with Our Powerful Anti-Aging Formula!",
    "Limited Time Only: Buy One, Get One Free on All Top-Brand Products - Don't Miss Out on This Amazing Deal!",
    "Attention: Your Online Safety is at Risk! Protect Yourself with Our Cutting-Edge Security Solution - Stay Protected Today!",
    "Become a Millionaire in Just 30 Days with Our Revolutionary Money-Making System - Start Your Journey to Financial Freedom!",
    "Your Opinion Matters: Take Our Short Survey and Get Rewarded with Exciting Prizes - Share Your Thoughts Now!",
    "Exclusive Discount: Save Big on the Latest Electronics and Gadgets - Upgrade Your Tech Collection Today!",
    "Congratulations! You're a Lucky Winner of a Brand New Luxury Car - Claim Your Prize and Hit the Road in Style!",
    "Supercharge Your Love Life with Our Powerful Enhancement Products - Experience Unparalleled Satisfaction Tonight!",
    "URGENT: Don't Miss Out on the Opportunity to Triple Your Income - Take Action Now and Transform Your Finances!",
    "Special Offer: Get a Free Trial of Our Premium Service for 30 Days - Experience the Benefits Firsthand!",
    "Claim Your Inheritance: Act Now to Receive a Life-Changing Sum of Money - Secure Your Financial Future Today!",
    "Limited Availability: Reserve Your Dream Vacation Now at Unbeatable Prices - Book Your Trip of a Lifetime!",

    };

    static string[] inquiryMessage = {
         "Could you please provide an update on the progress of the current branch? I'm eager to know the status and any pending tasks.",
    "Hello, I was wondering when we can expect a demo to review the progress. It would be great to see the latest developments in action.",
    "I'm following up on the task completion deadline. Can you confirm the expected time of delivery for the pending tasks?",
    "I need clarification on the next steps in the project. Could you please outline the upcoming milestones and their objectives?",
    "I have some urgent questions regarding the project requirements. Could you clarify a few points for better understanding?",
    "I would like to schedule a meeting to discuss the project scope, deliverables, and any potential challenges we may encounter.",
    "Can you provide an update on the progress made so far? I would like to know the key accomplishments and milestones achieved.",
    "I need a summary of the project cost to ensure that we are within the budget. Could you please provide an overview of the expenses?",
    "I'm experiencing technical difficulties and require troubleshooting assistance. Could you please allocate resources to help resolve the issue?",
    "Could you share documentation or an overview of the system architecture? It would be beneficial for better understanding and future maintenance.",
    "There have been changes in the project schedule. Could you revise the timeline and provide an updated version?",
    "I would like to schedule a team discussion to collaborate on specific tasks. Please let me know your availability for the meeting.",
    "I have some questions about the functionality and usage of certain features. Can you provide more details or clarify their purpose?",
    "I'm requesting a workshop or training session to learn about the implementation of the new technology. When can this be arranged?",
    "I kindly request a thorough quality assurance review and bug fixing session to ensure a high-quality product. Please schedule accordingly.",
    "We need to discuss the staffing requirements for the upcoming phases. Can you provide an analysis or recommendations?",
    "I'm interested in your pricing and service agreement details. Could you please share information regarding costs and contractual terms?",
    "I need some clarification on the project requirements. Can we set up a meeting to go over the details?",
    "I have a few questions about the contractual terms and agreements. Could you please provide clarity on specific clauses?",
    "I require access to certain data for analysis purposes. Can you assist me in retrieving the required data and ensuring its accuracy?",
    };

    static string[] inquiryResponse = {
        "We're making good progress on the current branch. Most of the tasks are completed, and we're working on the remaining items. Expect an update soon.",
    "Thank you for your interest. We're aiming to have a demo ready by the end of the week. We'll reach out to schedule a convenient time for the review.",
    "Apologies for the delay. We had unexpected hurdles, but we're confident in completing the tasks by next week. We appreciate your patience.",
    "Here is the outline of the upcoming milestones: [Outline]. If you have any questions or suggestions, feel free to share. Let's discuss it further.",
    "Sure, I'll be happy to provide clarification on the project requirements. Let's set up a call or meeting to address your questions in detail.",
    "I appreciate your initiative. Let's schedule a meeting next Tuesday to discuss the project scope, deliverables, and potential challenges.",
    "Great news! We have achieved several key milestones, including [Milestones]. We're progressing well and are on track with the project plan.",
    "Certainly, I will provide a detailed breakdown of the project costs, including expenses for resources, licenses, and additional services.",
    "We're sorry to hear about the technical difficulties. Our support team will reach out shortly to assist you in troubleshooting the issue.",
    "Attached is the system architecture overview document. It contains detailed information about the system components and their interactions.",
    "We understand the need for timeline revisions. We'll analyze the changes and propose an updated schedule by the end of this week.",
    "Thank you for initiating the collaboration discussion. Let's set up a meeting next Monday to address the tasks and align our efforts.",
    "Sure, I'll be glad to provide more information about the feature functionality and usage. I'll prepare a detailed document and share it with you.",
    "We acknowledge your request for a workshop on the new technology. We'll organize a session next month and notify you of the details.",
    "We understand the importance of quality assurance. We'll conduct a thorough review and address any identified bugs promptly. Expect updates soon.",
    "We appreciate your attention to staffing requirements. Our team will evaluate the needs and provide a comprehensive analysis by the end of this week.",
    "Thank you for your interest in our services. We'll send you the pricing details and service agreement documentation shortly. Stay tuned!",
    "Certainly, let's schedule a meeting next Wednesday to clarify the project requirements and address any ambiguities or concerns.",
    "We understand the importance of clear contractual terms. We'll provide a response to your questions and address any concerns in a timely manner.",
    "We acknowledge your data access request. Our team will assist you in retrieving the required data and ensure its accuracy. Expect further communication.",
    };

    


    static void yep() {
        authors = new List<Author>();
        spamAuthors = new List<Author>();
        iconObjects = Resources.LoadAll("Icons",typeof(Texture2D));
        Unusedicons = new List<Texture2D>();
        icons = new List<Texture2D>();
        
        foreach (UnityEngine.Object obj in iconObjects) {
            icons.Add((Texture2D)obj);
        }
        Unusedicons = new List<Texture2D>(icons);
        for (int i = 0; i < 5; i++) {
            Texture2D _icon; 
            if (Unusedicons.Count > 0) {
             _icon = Unusedicons[UnityEngine.Random.Range(0, Unusedicons.Count)];
            } else {
             _icon = icons[UnityEngine.Random.Range(0, icons.Count)];   
            }
            Author auth = new Author(false, _icon);
            authors.Add(auth);
            Unusedicons.Remove(_icon);
        }

        for (int i = 0; i < 10; i++) {
            Texture2D _icon; 
            if (Unusedicons.Count > 0) {
            _icon = Unusedicons[UnityEngine.Random.Range(0, Unusedicons.Count)];
            } else {
             _icon = icons[UnityEngine.Random.Range(0, icons.Count)];   
            }
            
            Author auth = new Author(true, _icon);
            spamAuthors.Add(auth);
            Unusedicons.Remove(_icon);
        }
    }
    

    public static Email newEmail(EmailSentiment sentiment) {
        yep();

        Email email = new Email();

        switch (sentiment) {
            case EmailSentiment.POSITIVE :
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];
            email.Subject = positiveSubjects[UnityEngine.Random.Range(0,positiveSubjects.Length)];
            email.Message = positiveMessage[UnityEngine.Random.Range(0,positiveMessage.Length)];
            break;
            case EmailSentiment.NEGATIVE:
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];
            email.Subject = negativeSubjects[UnityEngine.Random.Range(0,negativeSubjects.Length)];
            email.Message = negativeMessage[UnityEngine.Random.Range(0,negativeMessage.Length)];
            break;
            case EmailSentiment.SPAM:
            email.Author = spamAuthors[UnityEngine.Random.Range(0,spamAuthors.Count)];
            email.Subject = spamSubjects[UnityEngine.Random.Range(0,spamSubjects.Length)];
            email.Message = spamMessage[UnityEngine.Random.Range(0,spamMessage.Length)];
            email.reply = false;
            email.spam = true;
            break;
            case EmailSentiment.INQUIRY:
            int index = UnityEngine.Random.Range(0,inquirySubjects.Length);
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];
            email.Subject = inquirySubjects[index];
            email.Message = inquiryMessage[index];
            email.reply = true;
            email.replyMessage = inquiryResponse[index];
            break;
        }

        return email;
        
    }
}
