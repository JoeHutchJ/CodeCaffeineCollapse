using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public enum QTEKEY {
    NORTHW, WESTA, SOUTHS, EASTD,
}

public class ReviewCodeLine : MonoBehaviour
{
    public Boolean active;

    public Boolean QTEpossible;
    public Boolean QTEactive;

    public  QTEKEY QTEkey;

    public bool clicked;

    public string text;

    public TMP_Text textBox;

    public Transform QTEIcon;

    public TMP_Text QTEtextBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(string _text, float frequency) {
        QTEIcon = transform.Find("QTE Icon");
        textBox = transform.Find("Text").GetComponent<TMP_Text>();
        text = _text;
        
        textBox.text = text;

        if (text.Contains("}")) {

        } else {
            QTEpossible = true;
        }

        if (QTEpossible) {
            if (UnityEngine.Random.Range(0.0f, 1.0f) > frequency) {
            SetupQTE();
            }
        }
    }

    void SetupQTE() {
        QTEactive = true;
        QTEIcon.gameObject.SetActive(true);
        QTEtextBox = QTEIcon.Find("Text").GetComponent<TMP_Text>();

        switch (UnityEngine.Random.Range(0,4)) {
            case 0:
                QTEkey = QTEKEY.NORTHW;
                QTEtextBox.text = "North / W";
                break;
            case 1:
                QTEkey = QTEKEY.WESTA;
                QTEtextBox.text = "West / A";
                break;
            case 2:
                QTEkey = QTEKEY.SOUTHS;
                QTEtextBox.text = "South / S";
                break;
            case 3:
                QTEkey = QTEKEY.EASTD;
                QTEtextBox.text = "East / D";
                break;         
        }
        

    }

    public void outBounds() {
        active = false;
    }

    public void inBounds() {
        active = true;

        
    }

    public bool QTEPressed(QTEKEY key) {
        if (QTEkey == key) {
           QTEIcon.gameObject.SetActive(false);  
           QTEactive = false;
           active = false;
           clicked = true;
           return true;    
        }
        return false;
    }
}
