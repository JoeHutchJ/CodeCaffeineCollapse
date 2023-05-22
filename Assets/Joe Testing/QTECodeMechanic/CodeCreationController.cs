using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class CodeCreationController : MonoBehaviour
{
    public List<Task> requests;

    public int numLines = 20;

    public float scrollSpeed = 0;

    float QTEFrequency;
    public GameObject CodeLinesLayout;

    public RectTransform ContentBox;

    public ScrollManager scrollManager;

    public GameObject CodeLinePrefab;

    public GameObject NoMoreCodingRequests;

    public GameObject nextTaskbuttonPrefab;

    public GameObject BufferPrefab;

    public RectTransform inBoundsline;

    public GameObject progressBar;

    public TMP_Text RequestsCountElement;

    

    float timeSincelastCheck = 0;

    float estimatedCodeLineHeight = 3.6202f;

    CreateCodeLine selectedLine;

    List<GameObject> codeLines;

    float typeSpeed = 4.0f;

    int combo;

    public bool disabled;

    public bool started;

    bool active = false;

    bool intermediateActive = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!started) {
            started = true;
        CodeLinesLayout = GetChildByName.Get(this.gameObject, "CodeLinesLayout");
        ContentBox = GetChildByName.Get(this.gameObject, "Content").GetComponent<RectTransform>();
        scrollManager = GetChildByName.Get(this.gameObject, "Scrollbar Vertical").GetComponent<ScrollManager>();
        progressBar = GetChildByName.Get(this.gameObject, "ProgressBar");
        RequestsCountElement = GetChildByName.Get(this.gameObject, "RequestsCount").GetComponent<TMP_Text>();
        inBoundsline = GetChildByName.Get(this.gameObject, "InBounds").GetComponent<RectTransform>();
        estimatedCodeLineHeight = CodeLinePrefab.GetComponent<RectTransform>().sizeDelta.y;
        codeLines = new List<GameObject>();
        if (requests != null) {
            if (requests.Count <= 0) {
        requests = new List<Task>();
        }
        } else {
            requests = new List<Task>();
        }
        //requests.Add(0.5f);
        displayIntermediate();
        } else {
            
        }
        
        
        
        //scrollManager.setScrollSpeed()
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSincelastCheck > 0.5) {
            timeSincelastCheck = 0;
            checkCodeLines(codeLines);
            selectedLine = firstActivenonFinished();
            if (selectedLine != null) {
            selectedLine.setTypeSpeed(typeSpeed);
            } else {
                scrollManager.addToscrolling((1.0f + (40.0f / (codeLines.Count * estimatedCodeLineHeight))) / (float)codeLines.Count);
                selectedLine = firstActivenonFinished();
            }
        }
        timeSincelastCheck += Time.deltaTime;


         progressBar.GetComponent<ProgressBar>().setProgress(1 - scrollManager.getScrollValue());
        RequestsCountElement.text = requests.Count.ToString();

        OnKeyUp();

    }

    public void AddRequest(Task task) {
        Start();
        requests.Add(task);
        if (!active) {
            displayIntermediate();
        }
    }

    public int getRequests() {
        if (requests.Count > 0) {
        return requests.Count;
        } else {
            return 0;
        }
    }

    public void newDifficulty(float difficulty) {
        scrollManager.scrollSpeed = UsefulFunctions.Remap(difficulty, 0, 1, 0.1f, 0.3f);
        numLines = (int)UsefulFunctions.Remap(difficulty, 0, 1, 5, 10);
        QTEFrequency = UsefulFunctions.Remap(difficulty, 0, 1, 0.8f, 0.6f);

    }

    public void generateNew() {
        active = true;
        wipeContentbox();
        if (requests.Count >= 1) {
            newDifficulty(requests[0].difficulty);
        ContentBox.position = new Vector3(ContentBox.position.x,ContentBox.position.y-20,ContentBox.position.z);
        scrollManager.resetScrolling();
        string CodeString = generateGibberishCode.GenerateRandomCode(numLines);
        string[] LineArray = CodeString.Split("\n");
        ContentBox.sizeDelta = new Vector2(ContentBox.sizeDelta.x, LineArray.Length * estimatedCodeLineHeight + 5);
        Instantiate(BufferPrefab, CodeLinesLayout.transform);
        foreach(string Line in LineArray) {
            addToContent(Line);

        }
        scrollManager.startScrolling();
        }



    }

    public void addToContent(string Line) {
        GameObject newCodeLine = Instantiate(CodeLinePrefab, CodeLinesLayout.transform);
        newCodeLine.GetComponent<CreateCodeLine>().Setup(Line, QTEFrequency);
        codeLines.Add(newCodeLine);

    }

    public void checkCodeLines(List<GameObject> lines) {
        foreach (GameObject line in lines) {
            if (line != null) {
            checkCodeLine(line);
            }
        }
    }

    public void checkCodeLine(GameObject line) {
        RectTransform rectTransform = line.GetComponent<RectTransform>();
        CreateCodeLine codeLine = line.GetComponent<CreateCodeLine>();
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



    void wipeContentbox() {
        foreach (Transform child in CodeLinesLayout.transform) {
            Destroy(child.gameObject);
        }
    }

    CreateCodeLine firstActivenonFinished() {
        CreateCodeLine first = null;
        bool firstGot = false;
        foreach (GameObject line in codeLines) {
            CreateCodeLine createCodeLine = line.GetComponent<CreateCodeLine>();
            if (createCodeLine.activeNonfinished()) {
                if (!firstGot) {
                first = createCodeLine;
                createCodeLine.selected = true;
                firstGot = true;
                } else {
                    createCodeLine.selected = false;
                }
            } else {
                createCodeLine.selected = false;
            }
        }
        return first;
    }


    public void keyPressed(QTEKEY key) {
        if (selectedLine != null) {
        float val = selectedLine.QTEPressed(key);
        float newSpeed = typeSpeed;
        if (val != -1.0f) {
            if (val == 1.0f) {
                combo++;
                newSpeed += 1.0f;
            } else if (val > 0.7f) {
                combo = 0;
                newSpeed += 0.5f;
            } else if (val > 0.4f) {
                combo = 0;
                //newSpeed += 0.1f;
            } else if (val >= 0 ) {
                combo = 0;
                newSpeed = 4.0f;
            }
               
            updateTypeSpeed(newSpeed + (combo * 0.2f));
            
        }

        }

    }

    public void updateTypeSpeed(float speed) {
        Debug.Log(speed);
        typeSpeed = speed;
        selectedLine.setTypeSpeed(typeSpeed);
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

    public void displayIntermediate() {
        active = false;
        if (requests.Count > 0) {
            if (!GetChildByName.isInChilden(CodeLinesLayout.transform, "NextTaskButton(Clone)(Clone)")) {
                wipeContentbox();
            Instantiate(nextTaskbuttonPrefab, CodeLinesLayout.transform);
            

        } } else {
            if (!GetChildByName.isInChilden(CodeLinesLayout.transform, "NoCodingRequests(Clone)")) {
                wipeContentbox();
            Instantiate(NoMoreCodingRequests, CodeLinesLayout.transform);
            }
        }
        

    }

    



    public void completeCurrent() {
        wipeContentbox();
        progressBar.GetComponent<ProgressBar>().setProgress(0.0f);
        
        /*Debug.Log("Percentage: " + (float)QTEClicked() / (float)QTECount * 100 + "%"); //testing this will go
        QTECount = 0;*/
        int count = 0;
        foreach (GameObject line in codeLines) {
            CreateCodeLine codeLine = line.GetComponent<CreateCodeLine>();
            if (codeLine.finished) {
                count++;
            }
        }

        float percent = (float)count / (float)codeLines.Count;
        Debug.Log("Percentage: " + percent  * 100 + "%");
        requests[0].Complete(percent);
        if (requests.Count > 0 ) {
            
            requests.Remove(requests[0]);
        }
        codeLines.Clear();
        scrollManager.resetScrolling();
        displayIntermediate();

    }

    public void anyKeyPressed() {
        if (selectedLine != null) {
        selectedLine.keyPressed();
        }

    }
}
