using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class ReviewController : MonoBehaviour
{
    //generate new code string based on difficutly (num lines ans scroll speed)
    //split string into lines array
    //estimate size of codeLines, and number of lines * to get content box height;
    //iterate through lines, instantiate codeLine to CodeLinesLayout
    //CodeLine active if not just }
    //Codeline if active random chance to be QTE

    //start scroll.

    public List<float> requests;

    public int numLines = 20;

    public float scrollSpeed = 0;

    float QTEFrequency;

    float estimatedCodeLineHeight = 3.6202f;

    public GameObject CodeLinesLayout;

    public RectTransform ContentBox;

    public ScrollManager scrollManager;

    public GameObject CodeLinePrefab;

    public GameObject NoCodeToReviewPrefab;

    public RectTransform inBoundsline;

    public GameObject progressBar;

    public TMP_Text RequestsCountElement;

    List<GameObject> codeLines;

    int QTECount;

    float timeSincelastCheck = 0.0f;
     void Start()
    {
        CodeLinesLayout = GetChildByName.Get(this.gameObject, "CodeLinesLayout");
        ContentBox = GetChildByName.Get(this.gameObject, "Content").GetComponent<RectTransform>();
        scrollManager = GetChildByName.Get(this.gameObject, "Scrollbar Vertical").GetComponent<ScrollManager>();
        progressBar = GetChildByName.Get(this.gameObject, "ProgressBar");
        RequestsCountElement = GetChildByName.Get(this.gameObject, "RequestsCount").GetComponent<TMP_Text>();
        estimatedCodeLineHeight = CodeLinePrefab.GetComponent<RectTransform>().sizeDelta.y;
        codeLines = new List<GameObject>();
        requests.Add(0.0f); //this is test, this will be done using event, the float is the difficulty
        requests.Add(0.5f);
        requests.Add(1.0f);
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
        progressBar.GetComponent<ProgressBar>().setProgress(1 - scrollManager.getScrollValue());
        RequestsCountElement.text = requests.Count.ToString();
    }

    public void generateNew() {
        wipeContentbox();
        if (requests.Count >= 1) {
            newDifficulty(requests[0]);
        
        scrollManager.resetScrolling();
        string CodeString = generateGibberishCode.GenerateRandomCode(numLines);
        string[] LineArray = CodeString.Split("\n");
        ContentBox.sizeDelta = new Vector2(ContentBox.sizeDelta.x, LineArray.Length * estimatedCodeLineHeight + 70);
        foreach(string Line in LineArray) {
            addToContent(Line);

        }
        scrollManager.startScrolling();
        } else {
            Instantiate(NoCodeToReviewPrefab, CodeLinesLayout.transform);
        }


    }

    public void newDifficulty(float difficulty) {
        scrollManager.scrollSpeed = UsefulFunctions.Remap(difficulty, 0, 1, 0.8f, 1.0f);
        numLines = (int)UsefulFunctions.Remap(difficulty, 0, 1, 10, 70);
        QTEFrequency = UsefulFunctions.Remap(difficulty, 0, 1, 0.8f, 0.6f);

    }

    public void addToContent(string Line) {
        GameObject newCodeLine = Instantiate(CodeLinePrefab, CodeLinesLayout.transform);
        newCodeLine.GetComponent<ReviewCodeLine>().Setup(Line, QTEFrequency);
        if (newCodeLine.GetComponent<ReviewCodeLine>().QTEactive) {
            QTECount++;
        }
        codeLines.Add(newCodeLine);

    }

    public void checkCodeLines(List<GameObject> lines) {
        foreach (GameObject line in lines) {
            checkCodeLine(line);
        }
    }

    public void checkCodeLine(GameObject line) {
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        ReviewCodeLine codeLine = line.GetComponent<ReviewCodeLine>();
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
            ReviewCodeLine codeLine = line.GetComponent<ReviewCodeLine>();
            if (codeLine.active) {
                if (codeLine.QTEactive) {
                        if (codeLine.QTEPressed(key)) {
                            return true;
                        }
                        return false;
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

    void wipeContentbox() {
        foreach (Transform child in CodeLinesLayout.transform) {
            Destroy(child.gameObject);
        }
    }

    int QTEClicked() {
        int count = 0;
        foreach (GameObject obj in codeLines) {
            if (obj.GetComponent<ReviewCodeLine>().clicked) {
                count++;
            }
        }
        return count;
    }


    public void completeCurrent() {
        wipeContentbox();
        progressBar.GetComponent<ProgressBar>().setProgress(0.0f);
        if (requests.Count > 0 ) {
        requests.Remove(requests[0]);
        }
        Debug.Log("Percentage: " + (float)QTEClicked() / (float)QTECount * 100 + "%"); //testing this will go
        QTECount = 0;
        codeLines.Clear();
        scrollManager.resetScrolling();
        generateNew();

    }



    
}
