using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public static class GibberishReport
{
    public static int numberOfLines = 40; // Number of lines to generate

    public static int numSections = 4;
    static string[] sectionTitles = new string[]
{
    "Introduction",
    "Features and Functionality",
    "User Interface",
    "Performance",
    "Collaboration and Teamwork",
    "Documentation and Support",
    "Extensibility",
    "Compatibility"
};

static string[] sectionContent = new string[]
{
    "This report presents an evaluation of the Co-Lapse programming software, aimed at analyzing its features, performance, and suitability for development projects. The software was tested extensively over a two-week period, and the following sections outline our findings and recommendations.",
    "Co-Lapse programming software offers a comprehensive set of features essential for modern software development. It includes a robust code editor with syntax highlighting, auto-completion, and code folding capabilities. These features enhance code readability and streamline the development process, allowing developers to write clean and efficient code. Additionally, the software supports version control integration, enabling seamless collaboration and efficient management of code changes. Co-Lapse also provides powerful debugging tools, empowering developers to identify and resolve issues quickly. Furthermore, the software supports a wide range of programming languages, making it versatile and suitable for diverse development projects.",
    "The Co-Lapse software boasts an intuitive and user-friendly interface, making it easy for developers to navigate and perform tasks efficiently. The well-organized layout allows for seamless code navigation, project management, and quick access to frequently used tools. The interface is highly customizable, enabling developers to personalize their workspace and tailor it to their preferences and workflow. Co-Lapse's intuitive design significantly reduces the learning curve, ensuring developers can quickly adapt to using the software effectively and efficiently.",
    "During our evaluation, the Co-Lapse software demonstrated excellent performance across different tasks. It exhibited swift response times even when handling large codebases, and its efficient memory management ensured a smooth development experience, minimizing the risk of crashes or slowdowns. Co-Lapse's performance remained consistent throughout our testing, indicating its reliability and ability to handle demanding development projects. These performance capabilities contribute to increased productivity and reduced development time, allowing developers to focus on their code rather than software limitations.",
    "Co-Lapse excels in facilitating collaboration among team members. Its built-in features for real-time code sharing, code reviews, and instant messaging streamline communication and foster effective teamwork, enhancing productivity in distributed development environments. Team members can collaborate seamlessly on the same codebase, providing feedback, suggesting changes, and resolving conflicts in real-time. Co-Lapse also supports integrations with popular collaboration tools, enabling teams to leverage their existing workflows and maximize productivity by seamlessly integrating the software into their established development processes.",
    "Co-Lapse is accompanied by comprehensive documentation that offers clear instructions, tutorials, and troubleshooting guidance. The documentation covers all aspects of the software, including installation, configuration, and advanced features. Additionally, Co-Lapse boasts an active and supportive user community where developers can seek assistance, share knowledge, and exchange ideas. Furthermore, the Co-Lapse customer support team is prompt and knowledgeable, providing quick resolutions to user queries and technical issues, ensuring a smooth experience for users.",
    "The Co-Lapse software provides a robust plugin ecosystem, enabling developers to extend its functionality and customize their development environment. The plugin marketplace offers a wide range of community-developed and officially supported extensions, allowing developers to enhance their workflow and tailor the software to their specific needs. This extensibility ensures that developers can maximize their productivity and leverage additional tools and features within the Co-Lapse environment.",
    "Co-Lapse seamlessly integrates with various operating systems, including Windows, macOS, and Linux. It also supports popular web browsers and mobile platforms, ensuring developers can work on their preferred devices without any compatibility issues. Co-Lapse's compatibility across different platforms enhances flexibility and allows developers to work in their preferred environments."

};


    private static string newSection() {
        StringBuilder text = new StringBuilder();
        int index = UnityEngine.Random.Range(0, sectionTitles.Length);
        string sectionTitle = sectionTitles[index];
        text.AppendLine(sectionTitle);
        string content = sectionContent[index];
        text.AppendLine(content);
        return text.ToString();

    }

    public static string Generate(int numSections)
    {
        StringBuilder text = new StringBuilder();
        for (int i = 0; i < numSections; i++)
        {
            text.Append(newSection());
        }
        return text.ToString();
    }

   

}
