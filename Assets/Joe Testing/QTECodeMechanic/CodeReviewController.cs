using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    float estimatedCodeLineHeight = 3.6202f;

    public GameObject CodeLinesLayout;

    public RectTransform ContentBox;

    public ScrollManager scrollManager;

    public GameObject CodeLinePrefab;

    public RectTransform inBoundsline;

    List<GameObject> codeLines;

    float timeSincelastCheck = 0.0f;
     void Start()
    {
        CodeLinesLayout = GetChildByName.Get(this.gameObject, "CodeLinesLayout");
        ContentBox = GetChildByName.Get(this.gameObject, "Content").GetComponent<RectTransform>();
        scrollManager = GetChildByName.Get(this.gameObject, "Scrollbar Vertical").GetComponent<ScrollManager>();
        estimatedCodeLineHeight = CodeLinePrefab.GetComponent<RectTransform>().sizeDelta.y;
        codeLines = new List<GameObject>();
        generateNew();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSincelastCheck > 0.5) {
            timeSincelastCheck = 0;
            checkCodeLines(codeLines);
        }
        timeSincelastCheck += Time.deltaTime;


        OnKeyUp();
    }

    public void generateNew() {
        //newDifficulty();
        string CodeString = generateGibberishCode.GenerateRandomCode(numLines);
        string[] LineArray = CodeString.Split("\n");
        ContentBox.sizeDelta = new Vector2(ContentBox.sizeDelta.x, LineArray.Length * estimatedCodeLineHeight);
        foreach(string Line in LineArray) {
            addToContent(Line);

        }


    }

    public void newDifficulty(float difficulty) {
        //input scroll speed & num lines here...

    }

    public void addToContent(string Line) {
        GameObject newCodeLine = Instantiate(CodeLinePrefab, CodeLinesLayout.transform);
        newCodeLine.GetComponent<CodeLine>().Setup(Line);
        codeLines.Add(newCodeLine);

    }

    public void checkCodeLines(List<GameObject> lines) {
        foreach (GameObject line in lines) {
            checkCodeLine(line);
        }
    }

    public void checkCodeLine(GameObject line) {
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        CodeLine codeLine = line.GetComponent<CodeLine>();
           if (LineIntersect(inBoundsline, rectTransform)) { 
            codeLine.inBounds();
        } else {
            codeLine.outBounds();
        }
    }


    public bool LineIntersect(RectTransform point, RectTransform line) {
        Vector3[] corners = new Vector3[4];
        line.GetWorldCorners(corners);
        for (int i = 0; i < corners.Length; i++) {
            corners[i] = Camera.main.WorldToScreenPoint(corners[i]);
            if (RectTransformUtility.RectangleContainsScreenPoint(point, corners[i], Camera.main)) {
                return true;
            }
        }
        return false;




    }

    public bool keyPressed(QTEKEY key) {
        foreach (GameObject line in codeLines) {
            CodeLine codeLine = line.GetComponent<CodeLine>();
            if (codeLine.active) {
                if (codeLine.QTEactive) {
                    if (codeLine.QTEkey == key) {
                        if (codeLine.QTEPressed(key)) {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }


    void OnKeyUp() {
        
        if (Input.GetKeyUp("w")) {
                keyPressed(QTEKEY.NORTHW);
        } else if (Input.GetKeyUp("a")) {
                keyPressed(QTEKEY.WESTA);

        } else if (Input.GetKeyUp("s")) {
                keyPressed(QTEKEY.SOUTHS);
        } else if (Input.GetKeyUp("d")) {
                keyPressed(QTEKEY.EASTD);
        }
        
    }



    
}
