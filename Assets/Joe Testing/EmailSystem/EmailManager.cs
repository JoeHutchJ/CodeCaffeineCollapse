using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EmailManager : MonoBehaviour
{
    public GameObject emailSummaryPrefab;

    public Transform content;

    public GameObject EmailMessagePrefab;

    public GameObject ReplyMessagePrefab;

    public Transform Body;

    public List<Email> emails;

    public List<EmailSummary> emailSummarys;

    public EmailMessage currentMessage;

    public EmailReply currentReply;

    public int currentEmailID;

    public GameObject placeholderPrefab;

    public float estimatedEmailHeight; 

    
    // Start is called before the first frame update
    void Start()
    {
        emails = new List<Email>();
        content = GetChildByName.Get(this.gameObject,"Content").transform;
        Body = GetChildByName.Get(this.gameObject, "Email Body").transform;
        estimatedEmailHeight = emailSummaryPrefab.GetComponent<RectTransform>().sizeDelta.y;
        wipeEmails();
        wipeBody(true);
        for (int i = 0; i < 15; i++) {

            Email email = EmailBuilder.newEmail(UnityEngine.Random.value < .5 ? EmailSentiment.SPAM: EmailSentiment.SPAM, TaskType.REVIEW);
            
            newEmailSummary(email);
        }
       
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, emailSummarys.Count * estimatedEmailHeight);
        Scrollbar scrollbar = GetChildByName.Get(this.gameObject, "Scrollbar Vertical").GetComponent<Scrollbar>();
        scrollbar.value = 1;
        
        GetChildByName.Get(this.gameObject,"Scroll View").GetComponent<ScrollRect>().scrollSensitivity = 16.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void newEmailSummary(Email email) {
        GameObject EmailObj = Instantiate(emailSummaryPrefab, content);
        EmailObj.GetComponent<EmailSummary>().Setup(email, this);
        emails.Add(email);
        emailSummarys.Add(EmailObj.GetComponent<EmailSummary>());
    }

    void wipeEmails() {
         UsefulFunctions.deleteAllchildren(content);
          
    }

    void wipeBody(bool placeholder) {
        UsefulFunctions.deleteAllchildren(Body);
        if (placeholder) {
        Instantiate(placeholderPrefab, Body);
        }
    }

    public void Display(Email email) {
        if (email != null) {
        wipeBody(false);
        currentEmailID = email.id;
        currentReply = null;
        GameObject obj = Instantiate(EmailMessagePrefab, Body);
        if (email.reply) {
            GameObject replyObj = Instantiate(ReplyMessagePrefab, Body);
            replyObj.GetComponent<EmailReply>().Setup(email, this);
            currentReply = replyObj.GetComponent<EmailReply>();
        }
        obj.GetComponent<EmailMessage>().Setup(email, this);
        currentMessage = obj.GetComponent<EmailMessage>();
        } else {

            UsefulFunctions.deleteAllchildren(Body);

        }


        
    }

    public void keyPressed() {
        if (currentReply != null) {
            currentReply.AddLetter(0);
        }


    }

    public void emailReplied() {
        getEmailbyId(currentEmailID).replied = true;
        updateEmails(getEmailbyId(currentEmailID));
    }

    public void markRead() {

        getEmailbyId(currentEmailID).read = true;
        updateEmails(getEmailbyId(currentEmailID));
    }

    public void deleteSpam() {
        foreach(EmailSummary em in emailSummarys) {
            if (em.email.id == currentEmailID) {
            Destroy(em.gameObject);
            } 
        }
        Email toDelete = new Email();
        foreach(Email em in emails) {
            if (em.id == currentEmailID) {
                toDelete = em;
            }
        }
        emails.Remove(toDelete);

        currentEmailID = -1;
        wipeBody(true);
    }

    public void updateEmails(Email newEmail) {
        foreach (EmailSummary sum in emailSummarys) {
           if (sum.email.id == newEmail.id) {
            sum.Reset(newEmail);
           }
        }

    }

    public Email getEmailbyId(int id) {
        foreach (Email e in emails) {
            if (e.id == id) {
                return e;
            }
        }
        return null;
    }
}
