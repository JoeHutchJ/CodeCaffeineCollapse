using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GibberishReport : MonoBehaviour
{
    public int numberOfLines = 40; // Number of lines to generate

    private string[] sectionTitles = {
        "Features and Functionality",
        "User Interface",
        "Performance",
        "Collaboration and Teamwork",
        "Documentation and Support",
        "Extensibility",
        "Compatibility",
        "Security",
        "Learning Curve",
        "Integration",
        "Licensing and Cost",
        "Reliability and Stability",
        "Performance Optimization Tools",
        "Accessibility",
        "Industry Adoption and Reputation"
    };

    private string[] sectionSentences = {
        "The XYZ programming software provides an extensive array of features crucial for contemporary software development.",
        "The software interface offers an intuitive and user-friendly experience, facilitating effortless navigation and efficient task execution.",
        "Throughout our evaluation, the XYZ programming software consistently showcased exceptional performance across various tasks.",
        "The software excels in promoting collaboration and fostering effective teamwork among developers.",
        "The comprehensive documentation and support resources accompanying the XYZ programming software offer clear instructions, tutorials, and troubleshooting assistance.",
        "The software's extensibility is a notable strength, allowing developers to enhance its functionality and personalize their development environment.",
        "The XYZ programming software seamlessly integrates with popular operating systems such as Windows, macOS, and Linux.",
        "The software implements robust security measures to safeguard sensitive code and project data.",
        "While the XYZ programming software boasts a rich set of features, it maintains a gentle learning curve suitable for both novice and experienced developers.",
        "The seamless integration capabilities of the XYZ programming software with other development tools, such as issue trackers and continuous integration servers, streamline the development workflow.",
        "The XYZ programming software offers flexible licensing options, including free, open-source, and commercial versions.",
        "Throughout our evaluation, the XYZ programming software demonstrated high reliability and stability, ensuring uninterrupted workflow and minimizing the risk of data loss.",
        "The software includes a suite of performance optimization tools that empower developers to identify and resolve performance bottlenecks in their code effectively.",
        "The XYZ programming software prioritizes accessibility by providing features such as screen reader compatibility, customizable color schemes, and keyboard shortcuts.",
        "The XYZ programming software has gained substantial industry adoption, boasting a large and active user community."
    };

    private List<string> paraphrasedSentences; // List to store paraphrased sentences

    private void Start()
    {
        GenerateParaphrasedSentences();
        GenerateTechnicalReport(numberOfLines);
    }

    private void GenerateTechnicalReport(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            string line = GenerateRandomLine();
        }
    }

    private void GenerateParaphrasedSentences()
    {
        paraphrasedSentences = new List<string>();

        foreach (string sentence in sectionSentences)
        {
            string paraphrasedSentence = GenerateRandomLine();
            paraphrasedSentences.Add(paraphrasedSentence);
        }
    }

    private string GenerateRandomLine()
{
    string line = "";

    if (Random.value < 0.3f)
    {
        // Randomly select a section title
        string sectionTitle = sectionTitles[Random.Range(0, sectionTitles.Length)];
        line = sectionTitle + ":";
    }
    else
    {
        // Randomly select a paraphrased sentence
        string paraphrasedSentence = paraphrasedSentences[Random.Range(0, paraphrasedSentences.Count)];
        line = paraphrasedSentence;
    }

    return line;
}
}
