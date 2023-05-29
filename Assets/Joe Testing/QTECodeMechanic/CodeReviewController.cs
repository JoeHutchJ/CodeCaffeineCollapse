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

    public List<Task> requests;

    public int numLines = 20;

    public float scrollSpeed = 0;

    float QTEFrequency;

    float estimatedCodeLineHeight = 3.6202f;

    public GameObject CodeLinesLayout;

    public RectTransform ContentBox;

    public ScrollManager scrollManager;

    public GameObject CodeLinePrefab;

    public GameObject NoCodeToReviewPrefab;

    public GameObject nextTaskbuttonPrefab;

    public RectTransform inBoundsline;

    public GameObject progressBar;

    public TMP_Text RequestsCountElement;

    List<GameObject> codeLines;

    int QTECount;

    float timeSincelastCheck = 0.0f;

    bool disabled = false;

    bool started = false;
    
    bool active = false;
    

    AudioManager audioManager;
     void Start()
    {   
        if (!started) {
        started = true;
        codeLines = new List<GameObject>();
        //requests.Add(0.0f); //this is test, this will be done using event, the float is the difficulty
        //requests.Add(0.5f);
        //requests.Add(1.0f);
        if (requests != null) {
            if (requests.Count <= 0) {
        requests = new List<Task>();
        }
        } else {
            requests = new List<Task>();
        }
        CodeLinesLayout = GetChildByName.Get(this.gameObject, "CodeLinesLayout");
        ContentBox = GetChildByName.Get(this.gameObject, "Content").GetComponent<RectTransform>();
        scrollManager = GetChildByName.Get(this.gameObject, "Scrollbar Vertical").GetComponent<ScrollManager>();
        progressBar = GetChildByName.Get(this.gameObject, "ProgressBar");
        RequestsCountElement = GetChildByName.Get(this.gameObject, "RequestsCount").GetComponent<TMP_Text>();
        inBoundsline = GetChildByName.Get(this.gameObject, "InBounds").GetComponent<RectTransform>();
        audioManager = GetComponent<AudioManager>();
        estimatedCodeLineHeight = CodeLinePrefab.GetComponent<RectTransform>().sizeDelta.y;

        displayIntermediate();

        }
        
        
        
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
    
    public bool isActive() {

        return active;
    }

    public void AddRequest(Task task) {
        if (task.taskType == TaskType.REVIEW && task.active) {
        Start();
        requests.Add(task);
        if (!active) {
            displayIntermediate();
        }
        }
    }

    public int getRequests() {
        if (requests != null) {
        if (requests.Count > 0) {
        return requests.Count;
        } else {
            return 0;
        }
        }
        return 0;
  
    }

    public void generateNew() {
        active = true;
        wipeContentbox();
        if (requests.Count >= 1) {
            newDifficulty(requests[0].difficulty);
        
        scrollManager.resetScrolling();
        string CodeString = generateGibberishCode.GenerateRandomCode(numLines);
        string[] LineArray = CodeString.Split("\n");
        ContentBox.sizeDelta = new Vector2(ContentBox.sizeDelta.x, LineArray.Length * estimatedCodeLineHeight + 100);
        foreach(string Line in LineArray) {
            addToContent(Line);

        }
        scrollManager.startScrolling();
        } 

        inBoundsline.gameObject.SetActive(true);
            
        


    }

    public void newDifficulty(float difficulty) {
        scrollManager.scrollSpeed = UsefulFunctions.Remap(difficulty, 0, 1, 1.0f, 1.8f);
        numLines = (int)UsefulFunctions.Remap(difficulty, 0, 1, 10, 70);
        QTEFrequency = UsefulFunctions.Remap(difficulty, 0, 1, 0.8f, 0.2f);

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
            if (Camera.main != null) {
            corners[i] = Camera.main.WorldToScreenPoint(corners[i]);
            if (RectTransformUtility.RectangleContainsScreenPoint(point, corners[i], Camera.main)) {
                return true;
            }
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
                            Debug.Log(audioManager == null);
                            audioManager.Play("QTEComplete");
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

    public void displayIntermediate() {
        active = false;
        inBoundsline.gameObject.SetActive(false);
        if (requests.Count > 0) {
            if (!GetChildByName.isInChilden(CodeLinesLayout.transform, "NextTaskButton(Clone)(Clone)")) {
                wipeContentbox();
            Instantiate(nextTaskbuttonPrefab, CodeLinesLayout.transform);
            

        } } else {
            if (!GetChildByName.isInChilden(CodeLinesLayout.transform, "NoCodeToReview(Clone)")) {
                wipeContentbox();
            Instantiate(NoCodeToReviewPrefab, CodeLinesLayout.transform);
            }
        }
        

    }


    public void completeCurrent() {
        wipeContentbox();
        progressBar.GetComponent<ProgressBar>().setProgress(0.0f);
        if (requests.Count > 0 ) {
            float percent = (float)QTEClicked() / (float)QTECount;
            requests[0].Complete(percent);
        requests.Remove(requests[0]);
        }
        //Debug.Log("Percentage: " + percent * 100 + "%"); //testing this will go
        
        
        if (requests.Count > 0 ) {
        requests.Remove(requests[0]);
        }

        QTECount = 0;
        codeLines.Clear();
        scrollManager.resetScrolling();
        displayIntermediate();

    }



    
}
