using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetChildByName.Get(this.gameObject,"Message").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
        if (timeSince >= 0.05f) {
        AddLetter();
        timeSince = 0;
        }
        timeSince += Time.deltaTime;
        textBox.text = displayText;
        }
    }


    public void Send() {


    }

    public void Setup(Email _email) {
        email = _email;
        active = true;
        replyText = email.replyMessage;
    }

    public void AddLetter() {

        if (currentCharIndex < replyText.Length) {
            displayText += replyText[currentCharIndex];
            currentCharIndex++;
        }

    }
}
