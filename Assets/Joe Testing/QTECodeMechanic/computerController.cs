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

    List<float> CodingRequests;
    List<float>ReviewRequests;
    List<float>ReportRequests;
    
    List<Email>emails; 
    // Start is called before the first frame update
    void Start()
    {
        Tabs = new List<Transform>();
        foreach(Transform child in GetChildByName.Get(this.gameObject, "TopBar").transform) {
            Tabs.Add(child);
        }

        emails = new List<Email>();
        CodingRequests = new List<float>();
        ReviewRequests = new List<float>();
        ReportRequests = new List<float>();

        CodingRequests.Add(0.5f);
        CodingRequests.Add(0.5f);

        ReviewRequests.Add(0.5f);

        ReportRequests.Add(0.5f);
        
        


        if (emails.Count <= 0) {
        populateEmails();
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

    void populateEmails() {
        for (int i = 0; i < 15; i++) {

            Email email = EmailBuilder.newEmail(UnityEngine.Random.value < .5 ? EmailSentiment.POSITIVE: EmailSentiment.NEGATIVE, TaskType.REVIEW);
            
            emails.Add(email);
        }
        //add weighting for start emails.



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
                    populateEmails(); 
                }
                    currentWindow.GetComponent<EmailManager>().setEmails(emails);
                    break;
                case "CodeCreation":
                    CodeCreationController create = currentWindow.GetComponent<CodeCreationController>();
                    foreach (float request in CodingRequests) {
                        create.AddRequest(request);
                    }
                    break;
                case "CodeReview":
                    ReviewController review = currentWindow.GetComponent<ReviewController>();
                    foreach (float request in ReviewRequests) {
                        review.AddRequest(request);
                    }
                    break;
                case "ReportCreation":
                    ReportCreationController report = currentWindow.GetComponent<ReportCreationController>();
                    foreach (float request in ReportRequests) {
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