using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class computerController : MonoBehaviour
{

    public GameObject CodeReview;
    public GameObject CodeCreation;

    public GameObject Email;

    public GameObject ReportCreation;

    public GameObject AuthenticationPrefab;

    public bool authenticated = false;

    Transform currentWindow;
    Transform currentTab;

    List<Transform> Tabs;

    List<Task> CodingRequests;
    
    List<Task>ReviewRequests;
    List<Task>ReportRequests;
    
    List<Email>emails; 

    public TaskEvent taskEvent;

    public Event resetToDeskviewEvent;

    AudioManager audioManager;

    float timeSinceAuthentication;

    float timetilLogout;

    public BoolFlag isPcMode;
    // Start is called before the first frame update
    void Start()
    {
        Tabs = new List<Transform>();
        foreach(Transform child in GetChildByName.Get(this.gameObject, "TopBar").transform) {
            Tabs.Add(child);
        }

        audioManager = GetComponent<AudioManager>();

        emails = new List<Email>();
        CodingRequests = new List<Task>();
        ReviewRequests = new List<Task>();
        ReportRequests = new List<Task>();

        /*CodingRequests.Add(new Task(TaskType.CODING, 0.5f, null, null, 0, taskEvent, true));
        CodingRequests.Add(new Task(TaskType.CODING, 0.5f, null, null, 0, taskEvent, true));

        ReviewRequests.Add(new Task(TaskType.REVIEW, 0.5f, null, null, 0, taskEvent, true));

        ReportRequests.Add(new Task(TaskType.REPORT, 0.5f, null, null, 0, taskEvent, true));*/
        
        if (!authenticated) {
            AuthenticateWindow();
        }

        populateEmails(15);
    
        
        //createTab(Email); 


    }

    // Update is called once per frame
    void Update()
    {
        if (currentTab != null) {
            currentTab.GetComponent<Button>().Select();
        }
        updateTabs();

        if (authenticated) {
        if (timeSinceAuthentication >= timetilLogout) {
            AuthenticateWindow();
        }
        timeSinceAuthentication += Time.deltaTime;
        }


    }

    void populateEmails(int num) {
        emails = new List<Email>();
        for (int i = 0; i < num; i++) {
            EmailSentiment sentiment;
            TaskType type;
            float random = UnityEngine.Random.Range(0.0f,1.0f);

            if (random <= 0.5f) {
            float random2 = UnityEngine.Random.Range(0.0f,1.0f);
            if (random2 < 0.25f) {
                type = TaskType.CODING;
            } else if (random2 >= 0.25f && random2 < 0.5f) {
                type = TaskType.REVIEW;
            } else if (random2 >= 0.5f && random2 < 0.75f) {
                type= TaskType.REPORT;
            } else {
                type = TaskType.BUILD;
            }

            if (UnityEngine.Random.Range(0.0f,1.0f) > 0.5f) {
                sentiment = EmailSentiment.POSITIVE;
            } else {
                sentiment = EmailSentiment.NEGATIVE;
            }

        } else if (random > 0.5f && random <= 0.75f) {
            sentiment = EmailSentiment.SPAM;
            type = TaskType.EMAIL;
        } else {
            sentiment = EmailSentiment.INQUIRY;
            type = TaskType.EMAIL;
        }

            Email email = EmailBuilder.newEmail(sentiment, type);
            
            if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f) {
                email.read = true;
            }
            
            emails.Add(email);
        }
        
        



    }

    public void addEmail(Email email) {
        emails.Add(email);
        updateTabs();
        updateWindow();
        audioManager.Play("Notification");
    }



    public void deleteTabs() {
        foreach (Transform child in transform) {
            if (child.tag == "EmailTab") {
                emails = child.GetComponent<EmailManager>().emails;
            }
            if (child.gameObject.name != "TopBar") {
                Destroy(child.gameObject);
            }
        }
    }

    public void createTab(GameObject obj) {
        if (authenticated) {
        deleteTabs();
        currentWindow = Instantiate(obj, transform).transform;
        if (currentWindow.tag != "") {
        GameObject tab = GetChildByName.GetByTag(transform.gameObject, currentWindow.tag);
        if (tab != null) {
            currentTab = tab.transform;
        }
        }
        populateWindow();
        }

    }

    public void updateTabs() {
        foreach (Transform tab in GetChildByName.Get(this.gameObject, "TopBar").transform) {
            switch (tab.tag) {  
                case "EmailTab":
                    tab.GetComponent<TopTab>().setNotif(getUnreadEmails());
                    break;
                case "CodeCreation":
                    if (isCurrentTab(tab.tag)) {
                        tab.GetComponent<TopTab>().setNotif(currentWindow.GetComponent<CodeCreationController>().getRequests());
                    } else {
                    tab.GetComponent<TopTab>().setNotif(CodingRequests.Count);
                    }
                    break;
                case "CodeReview":
                if (isCurrentTab(tab.tag)) {
                        tab.GetComponent<TopTab>().setNotif(currentWindow.GetComponent<ReviewController>().getRequests());
                    } else {
                    tab.GetComponent<TopTab>().setNotif(ReviewRequests.Count);
                    }
                    break;
                case "ReportCreation":
                if (isCurrentTab(tab.tag)) {
                        tab.GetComponent<TopTab>().setNotif(currentWindow.GetComponent<ReportCreationController>().getRequests());
                    } else {
                    tab.GetComponent<TopTab>().setNotif(ReportRequests.Count);
                    }
                    break;
                    
    }

        }

    }


    public bool isCurrentTab(string tag) {
        if (currentTab != null) {
        return currentTab.tag == tag;
        } 
        return false;
    }

    public void populateWindow() {
        switch (currentWindow.tag) {  
                case "EmailTab":
                if (emails.Count <= 0) {
                    populateEmails(15); 
                }
                    currentWindow.GetComponent<EmailManager>().setEmails(emails);
                    break;
                case "CodeCreation":
                    CodeCreationController create = currentWindow.GetComponent<CodeCreationController>();
                    foreach (Task request in CodingRequests) {
                        create.AddRequest(request);
                    }
                    break;
                case "CodeReview":
                    ReviewController review = currentWindow.GetComponent<ReviewController>();
                    foreach (Task request in ReviewRequests) {
                        review.AddRequest(request);
                    }
                    break;
                case "ReportCreation":
                    ReportCreationController report = currentWindow.GetComponent<ReportCreationController>();
                    foreach (Task request in ReportRequests) {
                        report.AddRequest(request);
                    }
                    break;

    }

    }

        public void updateWindow() {
        switch (currentWindow.tag) {  
                case "EmailTab":

                    currentWindow.GetComponent<EmailManager>().setEmails(emails);
                    break;
                case "CodeCreation":
                    CodeCreationController create = currentWindow.GetComponent<CodeCreationController>();
                    int[] arr = getIdArray(CodingRequests);
                    foreach (Task request in create.requests) {
                        if (!arr.Contains(request.ID)) {
                            create.requests.Remove(request);
                        }
                    }
                    break;
                case "CodeReview":
                    ReviewController review = currentWindow.GetComponent<ReviewController>();
                    int[] arr2 = getIdArray(ReviewRequests);
                    foreach (Task request in review.requests) {
                        if (!arr2.Contains(request.ID)) {
                            review.requests.Remove(request);
                        }
                    }
                    break;
                case "ReportCreation":
                    ReportCreationController report = currentWindow.GetComponent<ReportCreationController>();
                    int[] arr3 = getIdArray(ReportRequests);
                    foreach (Task request in report.requests) {
                        if (!arr3.Contains(request.ID)) {
                            report.requests.Remove(request);
                        }
                    }
                    break;

    }

    }

    public int[] getIdArray(List<Task> list) {
        List<int>arr = new List<int>();
        foreach (Task task in list) {
            arr.Add(task.ID);
        }
        return arr.ToArray();
    }

    public void ClickCodeCreationTab() {
        createTab(CodeCreation);
    }

    public void ClickCodeReviewTab() {
        createTab(CodeReview);
    }

    public void ClickReportCreationTab() {
        createTab(ReportCreation);
    }

    public void ClickEmailTab() {
        createTab(Email);
    }

    public void clickQuitButton() {
        isPcMode.Value = false;
        resetToDeskviewEvent.Raise();
        
    }


    public int getUnreadEmails() {
        if (currentWindow.tag == "EmailTab") {
            return currentWindow.GetComponent<EmailManager>().getUnreadEmails();
        } else {
            int count = 0;
                if (emails != null) {
                foreach(Email email in emails) {
                        if (!email.read) {
                             count++;
                    }
                 }
                return count;

                }
            return 0;
    }
        }


    public void AuthenticateWindow() {
        deleteTabs();
        currentWindow = Instantiate(AuthenticationPrefab, transform).transform;
        currentTab = null;
        authenticated = false;
        timeSinceAuthentication = 0;
        timetilLogout = UnityEngine.Random.Range(50, 120);
        audioManager.Play("Logout");
        //Debug.Log("authenticated: " + authenticated);

    }

    public void Authenticate(bool auth) { 
        if (auth) {
        authenticated = true;
        
        createTab(Email);
        } else {
            if (!taskActive()) {
            AuthenticateWindow();
            }
        }
        audioManager.Play("Authenticate");
    }


    public bool taskActive() {
        if (currentWindow.GetComponent<CodeCreationController>() != null) {
            return currentWindow.GetComponent<CodeCreationController>().isActive();
        } else if (currentWindow.GetComponent<ReviewController>() != null) {
            return currentWindow.GetComponent<ReviewController>().isActive();

        } else if (currentWindow.GetComponent<ReportCreationController>() != null) {
            return currentWindow.GetComponent<ReportCreationController>().isActive();
        }
        return false;

    }




    public  void newTask(Task task) {
        if (task.active) {
        switch (task.taskType) {
            case TaskType.CODING:
                CodingRequests.Add(task);
                break;
            case TaskType.REVIEW:
                ReviewRequests.Add(task);
                break;
            case TaskType.REPORT:
                ReportRequests.Add(task);
                break;
            case TaskType.AUTHENTICATE:
                AuthenticateWindow();
                break;

        }
        }
    }

    public void updateTask(Task task) {
        if (task.active) {
        Task removeTask = null;
        switch (task.taskType) {
            case TaskType.CODING:
                foreach(Task tsk in CodingRequests) {
                    if (task.ID == tsk.ID) {
                        if (task.complete) {
                            taskEvent.Raise(task);
                        }
                        removeTask = tsk;
                    }
                }
                if (removeTask != null) {
                    CodingRequests.Remove(removeTask);
                    updateWindow();
                }
                break;
            case TaskType.REVIEW:
                foreach(Task tsk in ReviewRequests) {
                    if (task.ID == tsk.ID) {
                        if (task.complete) {
                            taskEvent.Raise(task);
                        }
                        removeTask = tsk;
                    }
                }
                if (removeTask != null) {
                    ReviewRequests.Remove(removeTask);
                    updateWindow();
                }
                break;
            case TaskType.REPORT:
                foreach(Task tsk in ReportRequests) {
                    if (task.ID == tsk.ID) {
                        if (task.complete) {
                            taskEvent.Raise(task);
                        }
                        removeTask = tsk;
                    }
                }
                if (removeTask != null) {
                    ReportRequests.Remove(removeTask);
                    updateWindow();
                }
                break;
            case TaskType.AUTHENTICATE:
                
                break;

        }
    }
    }

    public Task getTaskbyId(int id) {
        foreach(Task task in CodingRequests) {
            if (task.ID == id) {
                return task;
            }
        }
        foreach(Task task in ReviewRequests) {
            if (task.ID == id) {
                return task;
            }
        }
        foreach(Task task in ReportRequests) {
            if (task.ID == id) {
                return task;
            }
        }
        return null;
    }

    public void removeTaskbyId(int id) {
        List<Task> listToremove = null;
        Task toRemove = null;
        foreach(Task task in CodingRequests) {
            if (task.ID == id) {
                listToremove = CodingRequests;
                toRemove = task;
            }
        }
        foreach(Task task in ReviewRequests) {
            if (task.ID == id) {
                                listToremove = ReviewRequests;
                toRemove = task;
            }
        }
        foreach(Task task in ReportRequests) {
            if (task.ID == id) {
                                listToremove = ReportRequests;
                toRemove = task;
            }
        }

        if (listToremove != null && toRemove != null) {
            listToremove.Remove(toRemove);
        }
    }


    public void CompleteTask(Task task) {
        removeTaskbyId(task.ID);

    }
 
}