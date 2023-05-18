using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EmailSentiment {POSITIVE, NEGATIVE, SPAM, INQUIRY}

public enum TaskType {EMAIL, CODING, REVIEW, REPORT, BUILD, PHONE, COFFEE, AUTHENTICATE};

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
                name = SpamNames[UnityEngine.Random.Range(0,SpamNames.Length)];
        
        }

        icon = _icon;
    }
}

public static class EmailBuilder
{
    static int emailID = 0;
    static UnityEngine.Object[] iconObjects;

    static List<Texture2D> icons;

    static List<Texture2D> Unusedicons;
    static List<Author> authors;

    static List<Author> spamAuthors;

    //positive & negative for: coding, review, report, build?

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

    static string[] positiveCodingSubject = {
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



    };

    static string[] positiveReviewSubject = {
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
    


    };

    static string[] positiveReportSubject = {
        "Well Executed on the Task at Hand!",
    "Keep up the Good Work on the Project!",
    "Appreciating Your Efforts and Contributions!",
    "Congratulations on a Successful Milestone!",
    "Well-Delivered Report - Well Done!",
    "Great Effort and Results - Keep it Up!",
    "Well Done on Meeting the Project Deadlines!",
    "Impressive Work on the Report!",
    "Excellent Report - Well Done!",
    "Kudos for the Outstanding Report!",
    "Congratulations on the Stellar Report!",
    "Bravo on the Exceptional Report!",
    "Fantastic Report - You Nailed It!",
    "Superb Work on the Report!",
    "Thumbs Up for the Remarkable Report!",
    "Well Executed Report - Bravo!",
    "Outstanding Report - Great Effort!",
    "Exceptional Report - Keep It Up!",
    "Top-notch Report - Congratulations!",
    "Job Well Done on the Report!",
    "Hats Off for the Impressive Report!",
    "Well-crafted Report - Congrats!",
    "Amazing Work on the Report!",
    "Terrific Report - Kudos!",
    "High-quality Report - Great Job!",
    "Excellent Analysis in the Report!",

    };

    static string[] positiveBuildSubject = {
        "Well Executed on the Task at Hand!",
    "Keep up the Good Work on the Project!",
    "Appreciating Your Efforts and Contributions!",
    "Congratulations on a Successful Milestone!",
    "Great Effort and Results - Keep it Up!",
    "Well Done on Meeting the Project Deadlines!",
    };

    static string[] negativeCodingSubject = {
        "Your Last Commit Broke Everything! We Need a Fix ASAP!",
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
    "Critical Situation: Your Updates Led to Data Corruption!",
    "Alert: Your Recent Code Is Causing Frequent Application Crashes!",
    "Frustrating Performance: Your Code Changes Slowed Everything Down!",
    "Buggy Code: Countless Issues Found in Your Recent Work!",
    "Data Chaos: Your Commit Introduced Inconsistent States!",
    "Code Nightmare: Poor Quality and Lack of Documentation in Your Work!",

    };

    static string[] negativeReviewSubject = {
        "Code Review Disaster: Your Changes Are Riddled with Bugs!",
           "Serious Concerns with the Code Review",
    "Critical Issues Found in the Code Review",
    "Code Review Reveals Troubling Problems",
    "Disappointing Code Review Findings",
    "Urgent Attention Required for Code Review",
    "Major Flaws Detected in the Code Review",
    "Code Review Raises Significant Concerns",
    "Critical Feedback on the Code Review",
    "Unsatisfactory Code Review Results",
    "Troubling Findings in the Code Review",
    "Code Review Uncovers Major Issues",
    "Code Review Falls Short of Expectations",
    "Code Review Requires Immediate Action",
    "Code Review Unveils Deeply Concerning Problems",
    "Critical Analysis of the Code Review",
    "Subpar Code Review - Room for Improvement",
    "Code Review Highlights Unacceptable Practices",
    "Code Review Raises Red Flags",
    "Code Review Raises Serious Red Flags",
    "Code Review Exposes Severe Weaknesses",

    };

    static string[] negativeReportSubject = {
            "Concerns About the Report",
    "Issues Found in the Report",
    "Report Requires Improvement",
    "Disappointing Report Findings",
    "Serious Problems with the Report",
    "Report Falls Short of Expectations",
    "Critical Feedback on the Report",
    "Important Report Revisions Needed",
    "Unsatisfactory Report Quality",
    "Troubling Findings in the Report",
    "Major Flaws in the Report",
    "Significant Concerns About the Report",
    "Urgent Attention Required for the Report",
    "Report Review Reveals Major Issues",
    "Report Inaccuracies and Weaknesses",
    "Deeply Displeased with the Report",
    "Subpar Report - Room for Improvement",
    "Report Lacks Essential Information",
    "Unacceptable Report Content",
    "Critical Assessment of the Report",


    };

    static string[] negativeBuildSubject = {
         "Concerns About Slow Progress",
    "Unsatisfactory Results and Progress",
    "Disappointing Lack of Progress",
    "Slow Progress - Cause for Concern",
    "Serious Concerns About the Pace of Progress",
    "Unacceptable Delays in Progress",
    "Deeply Dissatisfied with the Progress",
    "Poor Results - Room for Improvement",
    "Lackluster Progress and Results",
    "Inadequate Progress - Immediate Action Needed",
    "Troubling Lack of Progress and Results",
    "Insufficient Results - Urgent Attention Required",
    "Frustration Over Slow Progress",
    "Major Setbacks in Progress",
    "Poor Performance and Lack of Results",
    "Critical Assessment of Progress",
    "Unsatisfactory Outcome and Progress",
    "Concerning Lack of Productivity",
    "Progress Falls Short of Expectations",
    "Dismal Results and Slow Progress",
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

        static string[] positiveCodingMessage = {
              "Congratulations on the code you've written! Your implementation is clean, well-structured, and easy to understand. It demonstrates your strong problem-solving skills and attention to detail. Great job!",
    "I'm impressed with the code you've produced! It shows a deep understanding of the requirements and an elegant solution. Your code is robust, efficient, and follows best practices. Well done!",
    "Kudos to you for the exceptional code you've crafted! Your work reflects a high level of expertise and professionalism. The logic is sound, and your code is modular and reusable. Fantastic job!",
    "I wanted to express my appreciation for the code you've created. It's evident that you've put a lot of thought and effort into it. Your code is concise, well-documented, and easy to maintain. Excellent work!",
    "I'm thoroughly impressed with the code you've developed! It's evident that you've gone above and beyond to deliver a high-quality solution. Your code is elegant, optimized, and highly performant. Well done!",
    "I wanted to take a moment to acknowledge the outstanding code you've produced. It's evident that you've carefully thought through the design and implemented it flawlessly. Your code is efficient, scalable, and error-free. Great job!",
    "Hats off to you for the remarkable code you've written! It's evident that you've paid attention to every detail and followed industry best practices. Your code is clean, well-documented, and a joy to review. Congratulations!",
    "I'm delighted with the code you've created! It showcases your strong programming skills and your ability to deliver high-quality solutions. Your code is elegant, well-structured, and follows coding standards. Keep up the great work!",
    "I wanted to extend my appreciation for the code you've crafted. It's evident that you've put a lot of thought and effort into creating a well-designed solution. Your code is organized, readable, and easy to maintain. Well done!",
    "Great work on the code you've written! It's evident that you've followed best practices and adhered to coding standards. Your code is clean, efficient, and demonstrates your expertise. Congratulations!",
    "I'm truly impressed with the code you've produced. It's evident that you've dedicated time and effort to create a solid solution. Your code is well-structured, modular, and easy to understand. Keep up the fantastic work!",
    "Congratulations on delivering high-quality code! Your implementation is elegant, efficient, and meets all the specified requirements. It's clear that you've applied strong problem-solving skills and attention to detail. Well done!",
    "I wanted to commend you on the code you've written. It demonstrates your exceptional coding abilities and your commitment to delivering excellence. Your code is clean, concise, and well-tested. Keep up the fantastic job!",
    "I'm thrilled with the code you've developed. It's evident that you've invested time and effort into creating a robust and efficient solution. Your code is well-organized, thoroughly documented, and a pleasure to work with. Congratulations!",
    "Kudos on the code you've created! It showcases your technical prowess and ability to deliver high-quality results. Your code is readable, maintainable, and follows best practices. Great job!",
    "I wanted to express my admiration for the code you've crafted. It's evident that you've applied your expertise and attention to detail to create an exceptional solution. Your code is well-structured, error-free, and performs optimally. Well done!",
    "Congratulations on the code you've written! It's evident that you've put thought into the design and implementation. Your code is clean, well-documented, and demonstrates your strong coding skills. Keep up the great work!",
    "I'm highly impressed with the code you've produced. It's evident that you've gone above and beyond to deliver a high-quality solution. Your code is elegant, well-optimized, and thoroughly tested. Well done!",
    "Great job on the code you've developed! It's clear that you've put in the effort to ensure a reliable and efficient solution. Your code is well-structured, modular, and follows industry best practices. Congratulations!",
    "I wanted to send my compliments for the code you've written. Your solution is well-thought-out, clean, and demonstrates your mastery of coding. Your code is a pleasure to review and work with. Keep up the excellent work!",
    "Congratulations on the code you've created! It shows your expertise and commitment to delivering high-quality results. Your code is organized, efficient, and easy to understand. Well done!"




    };

    static string[] positiveReviewMessage = {
           "Great job on the code review and bug fixing! Your attention to detail and thoroughness have greatly improved the overall quality of the codebase. Thank you for your diligent efforts!",
    "I wanted to express my appreciation for your excellent coding review and bug fixing skills. Your insightful feedback and meticulous approach have been instrumental in resolving the issues. Well done!",
    "Kudos to you for the outstanding coding review and bug fixing work! Your keen eye for detail and problem-solving abilities have been invaluable in identifying and rectifying the issues. Fantastic job!",
    "I'm truly impressed with your coding review and bug fixing efforts. Your expertise and dedication have resulted in significant improvements to the codebase. Thank you for your diligent work!",
    "I wanted to extend my gratitude for your exceptional coding review and bug fixing contributions. Your meticulous analysis and efficient solutions have been instrumental in addressing the issues. Great job!",
    "Congratulations on the successful coding review and bug fixing! Your expertise and attention to detail have greatly improved the overall code quality and stability. Well done!",
    "I'm delighted with your coding review and bug fixing efforts. Your insights and proactive approach have been instrumental in enhancing the codebase and resolving the issues. Thank you for your valuable contributions!",
    "Great work on the coding review and bug fixing! Your thorough analysis and prompt action have greatly improved the codebase's reliability and performance. Thank you for your diligent work!",
    "I wanted to commend you for the excellent coding review and bug fixing. Your expertise and attention to detail have played a crucial role in identifying and rectifying the issues. Well done!",
    "Congratulations on your exceptional coding review and bug fixing work! Your in-depth analysis and efficient solutions have greatly contributed to the overall stability and quality of the codebase. Fantastic job!",
    "I'm thrilled with your coding review and bug fixing efforts. Your meticulous approach and expertise have been invaluable in ensuring the code's correctness and robustness. Thank you for your diligent work!",
    "Great job on the coding review and bug fixing! Your keen eye for detail and systematic approach have significantly improved the code quality and eliminated critical issues. Well done!",
    "I wanted to express my appreciation for your outstanding coding review and bug fixing contributions. Your dedication and expertise have been instrumental in enhancing the codebase's reliability and maintainability. Thank you for your valuable efforts!",
    "Kudos to you for the successful coding review and bug fixing! Your in-depth understanding and meticulous efforts have greatly enhanced the code quality and resolved critical issues. Fantastic job!",
    "Congratulations on your exceptional coding review and bug fixing work! Your comprehensive analysis and efficient solutions have greatly contributed to the codebase's stability and overall quality. Well done!",
    "I wanted to extend my gratitude for your diligent coding review and bug fixing efforts. Your attention to detail and proactive approach have played a vital role in improving the codebase's reliability and performance. Thank you for your valuable contributions!",
    "Great work on the coding review and bug fixing! Your expertise and meticulousness have resulted in significant improvements to the codebase's stability and functionality. Well done!",
    "I'm delighted with your coding review and bug fixing contributions. Your thorough analysis and effective solutions have been instrumental in addressing critical issues and improving the code quality. Thank you for your diligent work!",
    "Congratulations on your outstanding coding review and bug fixing work! Your extensive knowledge and careful attention to detail have greatly enhanced the codebase's reliability and performance. Fantastic job!",
    "I wanted to express my appreciation for your exceptional coding review and bug fixing efforts. Your expertise and dedication have been invaluable in resolving critical issues and improving the codebase's overall quality. Well done!",
    "Kudos to you for the successful coding review and bug fixing! Your meticulous analysis and efficient solutions have greatly contributed to the codebase's stability and functionality. Thank you for your valuable contributions!"



    };

    static string[] positiveReportMessage = {
            "Great job on the report! Your analysis and insights are thorough and well-presented. The report provides valuable information that will greatly assist in decision-making. Well done!",
    "I wanted to express my appreciation for the exceptional report you've written. It demonstrates a deep understanding of the subject matter and provides valuable insights. Thank you for your diligent work!",
    "Kudos to you for the outstanding report! Your research, analysis, and clear presentation of findings make it a valuable resource. Fantastic job!",
    "I'm truly impressed with the report you've produced. Your attention to detail and ability to convey complex information in a concise manner are commendable. Well done!",
    "I wanted to extend my gratitude for your exceptional report writing skills. Your report is well-structured, comprehensive, and presents the information in a clear and concise manner. Great job!",
    "Congratulations on the successful completion of the report! Your findings and recommendations are insightful and well-supported. Thank you for your diligent efforts!",
    "I'm delighted with the report you've written. It showcases your expertise and attention to detail. The report is well-organized, informative, and presents the data effectively. Well done!",
    "Great work on the report! Your thorough research and analysis have provided valuable insights and recommendations. Thank you for your diligent work!",
    "I wanted to commend you on the excellent report you've crafted. It demonstrates your strong research and writing skills. The report is comprehensive, well-structured, and easy to understand. Keep up the fantastic work!",
    "Congratulations on delivering a high-quality report! Your research, analysis, and presentation of findings are commendable. The report is informative, insightful, and well-written. Well done!",
    "I'm thrilled with the report you've produced. It reflects your meticulousness and attention to detail. The report is well-researched, well-organized, and provides valuable information. Great job!",
    "Great job on the report! Your in-depth research, insightful analysis, and clear communication make it a valuable resource. Well done!",
    "I wanted to express my appreciation for the exceptional report you've written. It showcases your expertise and ability to present complex information in a concise and understandable way. Thank you for your diligent work!",
    "Kudos to you for the outstanding report! Your thorough research, critical analysis, and well-structured presentation make it a valuable asset. Fantastic job!",
    "I'm truly impressed with the report you've produced. It's evident that you've put thought and effort into gathering and presenting the information. Your report is well-documented, informative, and easy to follow. Well done!",
    "I wanted to extend my gratitude for your exceptional report writing skills. Your report is well-researched, well-written, and provides valuable insights. Thank you for your diligent work!",
    "Congratulations on the successful completion of the report! Your findings, analysis, and recommendations are thorough and well-presented. Well done!",
    "I'm delighted with the report you've written. It demonstrates your expertise and ability to communicate complex concepts effectively. The report is well-structured, comprehensive, and insightful. Great job!",
    "Great work on the report! Your extensive research, critical analysis, and concise presentation make it a valuable resource. Thank you for your diligent work!",
    "I wanted to commend you on the excellent report you've crafted. Your attention to detail and ability to convey complex information in a clear and concise manner are commendable. Well done!",
    "Congratulations on delivering a high-quality report! Your research, analysis, and presentation skills are evident. The report is well-organized, informative, and provides valuable insights. Well done!"

    };

    static string[] positiveBuildMessage = {
          "Thank you for your hard work and dedication! Your contributions have been exceptional and greatly appreciated. Keep up the fantastic job!",
    "I wanted to take a moment to express my gratitude for your outstanding efforts. Your commitment and professionalism have not gone unnoticed. Thank you for your continuous dedication!",
    "Kudos to you for your outstanding performance! Your exceptional skills and positive attitude have made a significant impact. Thank you for going above and beyond!",
    "I'm truly grateful for your exceptional contributions. Your commitment to excellence and willingness to take on challenges have been invaluable. Thank you for your outstanding work!",
    "I wanted to extend my appreciation for your exceptional work. Your passion, expertise, and dedication have been instrumental in our success. Thank you for your outstanding contributions!",
    "Congratulations on your outstanding performance! Your hard work, attention to detail, and commitment to excellence have been exemplary. Well done!",
    "I wanted to express my sincere appreciation for your remarkable contributions. Your dedication, creativity, and positive attitude have made a real difference. Thank you for your outstanding work!",
    "Great job on your continued efforts! Your professionalism, expertise, and teamwork have been instrumental in our achievements. Thank you for your exceptional contributions!",
    "I wanted to commend you for your exceptional work. Your commitment, enthusiasm, and ability to deliver results have been outstanding. Thank you for your continuous dedication!",
    "Congratulations on your exceptional performance! Your skills, dedication, and hard work have set a new standard of excellence. Well done!",
    "I'm thrilled with the exceptional work you've done. Your contributions, expertise, and dedication have been invaluable to our team's success. Thank you for going above and beyond!",
    "Great work on your outstanding contributions! Your commitment, expertise, and positive attitude have been truly remarkable. Thank you for making a significant impact!",
    "I wanted to express my gratitude for your exceptional work. Your professionalism, perseverance, and ability to exceed expectations have been outstanding. Thank you for your continuous dedication!",
    "Kudos to you for your remarkable contributions! Your expertise, dedication, and innovative thinking have made a significant difference. Thank you for your exceptional work!",
    "I'm truly impressed with your exceptional work. Your dedication, skills, and commitment to excellence have been exemplary. Thank you for your outstanding contributions!",
    "Great job on your outstanding performance! Your hard work, dedication, and positive mindset have been invaluable. Thank you for your exceptional contributions!"

    };

    static string[] negativeCodingMessage = {
            "The code you've written requires significant improvement. It's poorly structured and difficult to understand. Please take the time to refactor and improve the quality.",
    "I'm disappointed with the code you've produced. It's evident that little effort went into understanding the requirements. Your code lacks organization, and the logic is convoluted.",
    "I regret to inform you that the code you've created is subpar. It fails to meet the expected standards, and the lack of documentation makes it challenging to maintain.",
    "I'm not satisfied with the code you've developed. It's evident that you rushed the implementation without considering best practices. Your code is error-prone and difficult to debug.",
    "I wanted to address the issues in the code you've written. It's apparent that you did not thoroughly test the solution, resulting in numerous bugs. Please revise and rectify the problems.",
    "I'm concerned about the quality of the code you've produced. It lacks proper error handling and doesn't adhere to coding standards. Significant improvements are necessary.",
    "I regret to inform you that the code you've created is far from satisfactory. It's messy, lacks structure, and is challenging to maintain. Please invest more time in improving it.",
    "I'm disappointed with the code you've written. It's evident that you didn't follow the recommended coding practices. Your code is inefficient, hard to read, and prone to errors.",
    "I wanted to express my concern regarding the code you've developed. It's evident that you didn't fully understand the requirements, resulting in an ineffective and error-prone solution.",
    "I'm not pleased with the code you've produced. It lacks modularity and proper documentation, making it difficult for other team members to collaborate. Please make the necessary improvements.",
    "I regret to inform you that the code you've created is below expectations. It lacks proper validation and error handling, resulting in a fragile and unreliable solution.",
    "I'm concerned about the code you've written. It's evident that you didn't thoroughly test the solution, leading to unexpected behavior and frequent crashes. Please address these issues.",
    "I wanted to address the shortcomings in the code you've produced. It's apparent that you didn't consider scalability and extensibility, resulting in a rigid and difficult-to-maintain solution.",
    "I'm disappointed with the code you've created. It's evident that you didn't pay attention to code readability and maintainability. Your code is convoluted and lacks proper documentation.",
    "I regret to inform you that the code you've developed falls short of expectations. It's riddled with inefficiencies and lacks proper optimization. Significant improvements are necessary.",
    "I'm not satisfied with the code you've produced. It lacks proper error handling and doesn't follow coding best practices. Please revise and improve the quality of your code."

    };

    static string[] negativeReviewMessage = {
            "Your coding review and bug fixing efforts have been insufficient. Many critical issues remain unresolved, and your feedback lacks depth. Please revisit the code and address the problems.",
    "I'm disappointed with the quality of your coding review and bug fixing. It's evident that you didn't thoroughly analyze the code, resulting in unresolved issues. Please take the time to reassess and provide more effective solutions.",
    "I regret to inform you that your coding review and bug fixing contributions have been unsatisfactory. Your analysis lacks thoroughness, and your proposed solutions are ineffective. Please improve the quality of your work.",
    "I'm not satisfied with the results of your coding review and bug fixing. Many critical issues have been overlooked, and your solutions are incomplete. Please dedicate more time and attention to detail.",
    "I wanted to address the shortcomings in your coding review and bug fixing. Your analysis is shallow, and you've failed to address the root causes of the issues. Please revisit the code and provide more comprehensive solutions.",
    "I'm concerned about the effectiveness of your coding review and bug fixing. You've missed several critical issues, and your proposed solutions are insufficient. Please reassess and provide more robust fixes.",
    "I regret to inform you that your coding review and bug fixing have fallen short of expectations. Your feedback lacks depth, and you've failed to address the core problems. Please invest more effort into improving your analysis and solutions.",
    "I'm disappointed with the quality of your coding review and bug fixing. It's evident that you haven't considered all possible scenarios, resulting in unresolved issues. Please reassess and provide more comprehensive solutions.",
    "I wanted to express my concern regarding your coding review and bug fixing efforts. Many critical issues have been left unresolved, and your proposed solutions are ineffective. Please revisit the code and address the problems.",
    "I'm not pleased with the results of your coding review and bug fixing. Your analysis is incomplete, and you've failed to address the underlying issues. Please invest more time and effort into improving your work.",
    "I regret to inform you that your coding review and bug fixing contributions have been below expectations. Your analysis lacks thoroughness, and your proposed solutions are ineffective. Please improve the quality of your work.",
    "I'm concerned about the effectiveness of your coding review and bug fixing. You've missed several critical issues, and your proposed solutions are insufficient. Please reassess and provide more robust fixes.",
    "I wanted to address the shortcomings in your coding review and bug fixing. Your analysis is shallow, and you've failed to address the root causes of the issues. Please revisit the code and provide more comprehensive solutions.",
    "I'm disappointed with the quality of your coding review and bug fixing. It's evident that you haven't considered all possible scenarios, resulting in unresolved issues. Please reassess and provide more comprehensive solutions.",
    "I regret to inform you that your coding review and bug fixing have fallen short of expectations. Your feedback lacks depth, and you've failed to address the core problems. Please invest more effort into improving your analysis and solutions.",
    "I'm not satisfied with the results of your coding review and bug fixing. Your analysis is incomplete, and you've failed to address the underlying issues. Please invest more time and effort into improving your work."

    };

    static string[] negativeReportMessage = {
            "I'm disappointed with the quality of the report you've written. It lacks thoroughness and fails to provide meaningful insights. Please revisit the content and improve its overall quality.",
    "I regret to inform you that your report falls short of expectations. It lacks proper research and analysis, making it superficial and unreliable. Please invest more effort into improving its content.",
    "I'm not satisfied with the report you've produced. It lacks clarity and fails to present the information effectively. Please revise the report and ensure that it provides valuable insights.",
    "I wanted to address the shortcomings in the report you've written. The analysis is shallow, and the conclusions drawn are unsubstantiated. Please revisit the content and provide more meaningful findings.",
    "I'm concerned about the quality of the report you've produced. It lacks coherence and fails to present a comprehensive picture. Please revise the report and ensure that it meets the required standards.",
    "I'm disappointed with the quality of the report you've written. The content is poorly organized, and the information provided is inadequate. Please revisit the report and improve its structure and depth.",
    "I regret to inform you that your report falls below expectations. It lacks critical insights and fails to address the key aspects of the subject matter. Please invest more time in improving its content.",
    "I'm not satisfied with the report you've produced. It lacks in-depth analysis and fails to draw meaningful conclusions. Please revise the report and provide more valuable insights.",
    "I wanted to express my concern regarding the quality of the report you've written. It lacks proper research and fails to present a comprehensive analysis. Please improve the content and ensure its accuracy.",
    "I'm concerned about the quality of the report you've produced. It lacks structure and fails to present the information in a coherent manner. Please revise the report and improve its overall readability.",
    "I'm disappointed with the quality of the report you've written. The analysis is superficial, and the recommendations provided lack substance. Please revisit the content and provide more meaningful insights.",
    "I regret to inform you that your report falls short of expectations. It lacks thorough research and fails to address the key issues adequately. Please invest more effort into improving its content.",
    "I'm not satisfied with the report you've produced. It lacks clarity and fails to present the information effectively. Please revise the report and ensure that it provides valuable insights.",
    "I wanted to address the shortcomings in the report you've written. The analysis is shallow, and the conclusions drawn are unsubstantiated. Please revisit the content and provide more meaningful findings.",
    "I'm concerned about the quality of the report you've produced. It lacks coherence and fails to present a comprehensive picture. Please revise the report and ensure that it meets the required standards.",
    "I'm disappointed with the quality of the report you've written. The content is poorly organized, and the information provided is inadequate. Please revisit the report and improve its structure and depth."


    };

    static string[] negativeBuildMessage = {
            "I wanted to address some concerns regarding your recent performance. Your contributions have been inconsistent and below expectations. Please take the necessary steps to improve your work.",
    "I'm disappointed with the quality of your recent contributions. Your attention to detail has been lacking, and you've failed to meet the required standards. Please make a conscious effort to improve your performance.",
    "I regret to inform you that your recent performance has been unsatisfactory. You've displayed a lack of initiative and commitment to excellence. Please reflect on your work and strive for improvement.",
    "I'm not satisfied with your recent performance. Your work has been subpar, and you've failed to meet the agreed-upon deadlines. Please prioritize your tasks and improve your time management skills.",
    "I wanted to express my concern regarding your recent performance. Your work has been inconsistent, and you've made several avoidable mistakes. Please pay more attention to detail and strive for accuracy.",
    "I'm concerned about the quality of your recent contributions. Your work lacks creativity and fails to meet the required standards. Please invest more time and effort into improving your performance.",
    "I regret to inform you that your recent performance has been below expectations. Your work has been error-prone, and you've shown a lack of accountability. Please take the necessary steps to improve your performance.",
    "I'm disappointed with the quality of your recent contributions. Your work has been sloppy and lacks attention to detail. Please revise your work and ensure its accuracy before submission.",
    "I wanted to address some concerns regarding your recent performance. Your work has been inconsistent, and you've failed to meet the expected standards. Please reflect on your work and strive for improvement.",
    "I'm not satisfied with your recent performance. Your work has been below par, and you've failed to deliver on your commitments. Please reevaluate your approach and improve your work ethic.",
    "I regret to inform you that your recent performance has been unsatisfactory. Your work lacks innovation and fails to meet the required standards. Please invest more effort into improving your performance.",
    "I'm concerned about the quality of your recent contributions. Your work has been uninspired, and you've shown a lack of dedication. Please challenge yourself and strive for excellence in your work.",
    "I wanted to express my disappointment with your recent performance. Your work has been inconsistent, and you've made avoidable mistakes. Please take the necessary steps to improve your performance.",
    "I'm disappointed with the quality of your recent contributions. Your attention to detail has been lacking, and you've failed to meet the required standards. Please make a conscious effort to improve your performance.",
    "I regret to inform you that your recent performance has been below expectations. You've displayed a lack of initiative and commitment to excellence. Please reflect on your work and strive for improvement.",
    "I'm not satisfied with your recent performance. Your work has been subpar, and you've failed to meet the agreed-upon deadlines. Please prioritize your tasks and improve your time management skills."

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
    
    public static  bool isPositive(EmailSentiment sentiment) {
        return sentiment == EmailSentiment.POSITIVE;
    }

     public static  bool isNegative(EmailSentiment sentiment) {
        return sentiment == EmailSentiment.NEGATIVE;
    }

    public static string randomChoice(string[] array) {
        return array[UnityEngine.Random.Range(0,array.Length)];
    }

    public static Email newEmail(EmailSentiment sentiment, TaskType taskType) {
        yep();

        Email email = new Email();

        switch(taskType) {
            case TaskType.CODING:
                if (isPositive(sentiment)) {
                    email.Subject = randomChoice(positiveCodingSubject);
                    email.Message = randomChoice(positiveCodingMessage);
                } else if (isNegative(sentiment)) {
                    email.Subject = randomChoice(negativeCodingSubject);
                    email.Message = randomChoice(negativeCodingMessage);
                }
                break;
            case TaskType.REVIEW:
                if (isPositive(sentiment)) {
                    email.Subject = randomChoice(positiveReviewSubject);
                    email.Message = randomChoice(positiveReviewMessage);
                }else if (isNegative(sentiment)) {
                    email.Subject = randomChoice(negativeReviewSubject);
                    email.Message = randomChoice(negativeReviewMessage);
                }
                break;
            case TaskType.REPORT:
                if (isPositive(sentiment)) {
                    email.Subject = randomChoice(positiveReportSubject);
                    email.Message = randomChoice(positiveReportMessage);
                }else if (isNegative(sentiment)) {
                    email.Subject = randomChoice(negativeReportSubject);
                    email.Message = randomChoice(negativeReportMessage);
                }
                break;
            case TaskType.BUILD:
                if (isPositive(sentiment)) {
                    email.Subject = randomChoice(positiveBuildSubject);
                    email.Message = randomChoice(positiveBuildMessage);
                }else if (isNegative(sentiment)) {
                    email.Subject = randomChoice(negativeBuildSubject);
                    email.Message = randomChoice(negativeBuildMessage);
                }
                break;
            

        }

        switch (sentiment) {
            case EmailSentiment.POSITIVE :
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];

            break;
            case EmailSentiment.NEGATIVE:
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];

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

        email.taskType = taskType;
        email.id = emailID;
        emailID++;
        return email;
        
    }


    public static Email newCustomEmail(string Subject, string Message, EmailSentiment sentiment, TaskType taskType) {
        Email email = new Email();
        
        switch (sentiment) {
            case EmailSentiment.POSITIVE :
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];

            break;
            case EmailSentiment.NEGATIVE:
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];

            break;
            case EmailSentiment.SPAM:
            email.Author = spamAuthors[UnityEngine.Random.Range(0,spamAuthors.Count)];

            email.reply = false;
            email.spam = true;
            break;
            case EmailSentiment.INQUIRY:
            int index = UnityEngine.Random.Range(0,inquirySubjects.Length);
            email.Author = authors[UnityEngine.Random.Range(0,authors.Count)];

            email.reply = true;

            break;
        }

        email.Subject = Subject;
        email.Message = Message;
        email.taskType = taskType;

        return email;

    }
}
