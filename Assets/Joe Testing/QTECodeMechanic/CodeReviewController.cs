using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewController : MonoBehaviour
{
    //generate new code string based on difficutly (num lines ans scroll speed)
    //split string into lines array
    //estimate size of codeLines, and number of lines * to get content box height;
    //iterate through lines, instantiate codeLine to CodeLinesLayout
    //CodeLine active if not just }
    //Codeline if active random chance to be QTE

    //start scroll.

    public int numLines = 20;

    public float scrollSpeed = 0;

    public GameObject CodeLinesLayout;

    public GameObject ContentBox;

    public ScrollManager scrollManager;

    public GameObject CodeLinePrefab;
     void Start()
    {
        CodeLinesLayout = GetChildByName.Get(this.gameObject, "CodeLinesLayout");
        ContentBox = GetChildByName.Get(this.gameObject, "Content");
        scrollManager = GetChildByName.Get(this.gameObject, "Scrollbar Vertical").GetComponent<ScrollManager>();
        generateNew();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateNew() {
        //newDifficulty();
        string CodeString = generateGibberishCode.GenerateRandomCode(numLines);
        Debug.Log(CodeString);
        string[] LineArray = CodeString.Split("\n");
        foreach(string Line in LineArray) {
            

        }


    }

    public void newDifficulty(float difficulty) {
        //input scroll speed & num lines here...

    }

    public void addToContent(string Line) {
        GameObject newCodeLine = Instantiate(CodeLinePrefab, ContentBox.transform);
        newCodeLine.GetComponent<CodeLine>().text = Line;
        

    }

    
}
