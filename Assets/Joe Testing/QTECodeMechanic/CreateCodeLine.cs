using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CreateCodeLine : MonoBehaviour
{
    public bool active;

    public bool finished;
    public TMP_Text textBox;

    public Transform QTEIcon;

    public TMP_Text QTEtextBox;

    public string text;

    public string displaytext;
    public Boolean QTEpossible;

    int currentCharindex = 0;

    float timeSince = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        textBox.text = displaytext;
    }

    public void Setup(string _text, float frequency) {
        QTEIcon = transform.Find("QTE Icon");
        textBox = transform.Find("Text").GetComponent<TMP_Text>();
        text = _text;
        
        //textBox.text = text;

        if (text.Contains("}")) {

        } else {
            QTEpossible = true;
        }



    }

    public void addLetter() {
            if (currentCharindex >= text.Length) {
                finished = true;
            } else {
                if (Char.IsWhiteSpace(text[currentCharindex]) ) {
                    Debug.Log("space");
                    displaytext += text[currentCharindex];
                    currentCharindex++;
                    addLetter();
                } else {
            displaytext += text[currentCharindex];
            
            currentCharindex++;
            }

            }
        
        
    }

    public void outBounds() {
        active = false;
    }

    public void inBounds() {
        active = true;

        
    }

    public bool activeNonfinished() {
        return (active && !finished);

        
    }
}
