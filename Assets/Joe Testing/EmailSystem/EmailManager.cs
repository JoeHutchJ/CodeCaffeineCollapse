using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailManager : MonoBehaviour
{
    public GameObject emailSummaryPrefab;

    public Transform content;

    public GameObject EmailMessagePrefab;

    public GameObject ReplyMessagePrefab;

    public Transform Body;

    public List<Email> emails;

    public List<EmailSummary> emailSummarys;

    
    // Start is called before the first frame update
    void Start()
    {
        emails = new List<Email>();
        content = GetChildByName.Get(this.gameObject,"Content").transform;
        Body = GetChildByName.Get(this.gameObject, "Email Body").transform;
        wipeEmails();
        for (int i = 0; i < 5; i++) {

            Email email = EmailBuilder.newEmail(UnityEngine.Random.value < .5 ? EmailSentiment.SPAM: EmailSentiment.SPAM);
            
            newEmailSummary(email);
        }
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
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }
    }

    void wipeBody() {
        foreach (Transform child in Body) {
            Destroy(child.gameObject);
        }
    }

    public void Display(Email email) {
        wipeBody();
        GameObject obj = Instantiate(EmailMessagePrefab, Body);
        if (email.reply) {
            GameObject replyObj = Instantiate(ReplyMessagePrefab, Body);
            replyObj.GetComponent<EmailReply>().Setup(email);
        }
        obj.GetComponent<EmailMessage>().Setup(email);


        
    }
}
