using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class computerController : MonoBehaviour
{

    public GameObject CodeReview;
    public GameObject CodeCreation;

    public GameObject Email;

    public GameObject ReportCreation;


    Transform currentWindow;
    Transform currentTab;

    List<Transform> Tabs;

    List<Task> CodingRequests;
    List<Task>ReviewRequests;
    List<Task>ReportRequests;
    
    List<Email>emails; 

    public TaskEvent taskEvent;
    // Start is called before the first frame update
    void Start()
    {
        Tabs = new List<Transform>();
        foreach(Transform child in GetChildByName.Get(this.gameObject, "TopBar").transform) {
            Tabs.Add(child);
        }

        emails = new List<Email>();
        CodingRequests = new List<Task>();
        ReviewRequests = new List<Task>();
        ReportRequests = new List<Task>();

        CodingRequests.Add(new Task(TaskType.CODING, 0.5f, null, null, 0, taskEvent));
        CodingRequests.Add(new Task(TaskType.CODING, 0.5f, null, null, 0, taskEvent));

        ReviewRequests.Add(new Task(TaskType.REVIEW, 0.5f, null, null, 0, taskEvent));

        ReportRequests.Add(new Task(TaskType.REPORT, 0.5f, null, null, 0, taskEvent));
        
        


        if (emails.Count <= 0) {
        populateEmails(15);
        }

        createTab(Email); 


    }

    // Update is called once per frame
    void Update()
    {
        if (currentTab != null) {
            currentTab.GetComponent<Button>().Select();
        }
        updateTabs();
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
            
            emails.Add(email);
        }
        
        



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
        return currentTab.tag == tag;
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


    public int getUnreadEmails() {
        if (currentWindow.tag == "EmailTab") {
            return currentWindow.GetComponent<EmailManager>().getUnreadEmails();
        } else {
            int count = 0;
                foreach(Email email in emails) {
                        if (!email.read) {
                             count++;
                    }
                 }
                return count;
    }
        }
 
}