using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class EmailReply : MonoBehaviour
{
    Email email;
    string replyText;

    public bool finished;

    string displayText;
    int currentCharIndex;

    TMP_Text textBox;

    float timeSince = 0.0f;
    
    bool active;

    int lettersPer = 3;

    Button replyButton;

    EmailManager manager;

    public GameObject sentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetChildByName.Get(this.gameObject,"Message").GetComponent<TMP_Text>();
        replyButton = GetChildByName.Get(this.gameObject, "ReplyButton").GetComponent<Button>();
        enableButton(replyButton, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
        /*if (timeSince >= 0.05f) {
        AddLetter();
        timeSince = 0;
        }*/
        timeSince += Time.deltaTime;
        textBox.text = displayText;
        }
    }


    public void Send() {


    }

    public void Setup(Email _email, EmailManager mngr) {
        Start();
        email = _email;
        active = true;
        replyText = email.replyMessage;
        manager = mngr;

        if (email.replied) {
            textBox.text = email.replyMessage;
            active = false;
            Instantiate(sentPrefab, GetChildByName.Get(this.gameObject,"body").transform);
        }
    }

    public void AddLetter(int count) {
        if (count == 0) {
            count = lettersPer;
        }
        active = true;
        if (currentCharIndex < replyText.Length) {
            if (Char.IsWhiteSpace(replyText[currentCharIndex])) {
                displayText += replyText[currentCharIndex];
                currentCharIndex++;
                AddLetter(count);
            }
            displayText += replyText[currentCharIndex];
            currentCharIndex++;
            if (count > 1) {
                AddLetter(count - 1);
            }
        } else {
            finished = true;
            enableButton(replyButton, true);
        }

    }

    void enableButton(Button button, bool enable) {
        button.enabled = enable;
        button.interactable = enable;
    }

    public void Replied() {
        manager.emailReplied();
        Instantiate(sentPrefab, GetChildByName.Get(this.gameObject,"body").transform);
    }
}
