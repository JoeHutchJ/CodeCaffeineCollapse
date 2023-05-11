using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;



public class CreateCodeLine : MonoBehaviour
{
    public bool active;

    public bool finished;
    public TMP_Text textBox;

    public TMP_Text QTEtextBox;

    public GameObject QTEIcon;

    public Transform QTEs;

    public List<QTEIcon> QTEIcons;

    public bool selected;

    public float typeSpeed; //letters per second

    public float typeInterval;

    public string text;

    public string displaytext;
    public Boolean QTEpossible;

    int currentCharindex = 0;

    float timeSince = 0;


    // Start is called before the first frame update
    void Start()
    {
        textBox = transform.Find("Text").GetComponent<TMP_Text>();
        QTEs = transform.Find("QTEs");
        setTypeSpeed(4.0f);
        QTEIcons = new List<QTEIcon>();
        SetupQTEs();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected) {
                
            if (timeSince >= typeInterval) {
                timeSince = 0;
                addLetter();
            } else {
                timeSince += Time.deltaTime;
            }
        }
        textBox.text = displaytext;
    
    }

    public void Setup(string _text, float frequency) {

        textBox = transform.Find("Text").GetComponent<TMP_Text>();
        QTEs = transform.Find("QTEs");
        text = _text;
        
        //textBox.text = text;

        if (text.Contains("}")) {

        } else {
            QTEpossible = true;
        }


    }

    public void SetupQTEs() {
        int sinceLastQTE = 0;

        for (int i=0; i < text.Length; i++) {
            if (i > 10) {
            addLetter();
            textBox.text = displaytext;
            textBox.ForceMeshUpdate(true);
             if (Char.IsWhiteSpace(text[i]) && sinceLastQTE >= 7) {
                if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f) {
                    SetupQTE(CalculateLengthOfText(), i);
                    sinceLastQTE = 0;
                    break;
                }
             }  
             sinceLastQTE++;
            }

        }

        displaytext = "";
        textBox.text = "";
        currentCharindex = 0;
        finished = false;
        UsefulFunctions.HideAllChildren( QTEs, true);


    }

    public void SetupQTE(float widthOftext, int index) {

        float xPos = widthOftext - (GetComponent<RectTransform>().sizeDelta.x / 2);
        //Debug.Log(widthOftext + " xPos " + xPos);
        foreach (QTEIcon QTE in QTEIcons) {
            if (QTE.charIndex == index) {
                return;
            }
        }
        GameObject qte = Instantiate(QTEIcon, QTEs);
        QTEIcon qteIcon = qte.GetComponent<QTEIcon>();
        qteIcon.Setup(index);
        qteIcon.setRectPos(xPos);
        QTEIcons.Add(qteIcon);


    }

    public bool isQTE(int index) {
        foreach (QTEIcon i in  QTEIcons) {
            if (i.charIndex == index) {
                return true;
            }
        }
        return false;
    }

    public bool indexWithinRange(int index, int target, int range) {
        return index > target - range && index < target + range;

    }

    public void addLetter() {
        int range = 10;
        if (isQTE(currentCharindex)) {

        } else {
            for (int i = 0; i < QTEIcons.Count; i++) {
                if (indexWithinRange(currentCharindex, QTEIcons[i].charIndex, range)) {

                    float howClose = 1 -  Mathf.Abs(QTEIcons[i].charIndex - currentCharindex) / (float)range;

                    QTEIcon QTEObj =  QTEs.GetChild(i).GetComponent<QTEIcon>();
                    QTEObj.gameObject.SetActive(true);
                    QTEObj.setOpacity(Mathf.Lerp(0,1, howClose));
                    UsefulFunctions.HideAllChildren(QTEObj.transform, false);

                } else {
                    QTEIcon QTEObj =  QTEs.GetChild(i).GetComponent<QTEIcon>();
                    QTEObj.setOpacity(0);
                    UsefulFunctions.HideAllChildren(QTEObj.transform, true);
                    
                }
            }

            if (currentCharindex >= text.Length) {
                finished = true;
                selected = false;
            } else {
                if (Char.IsWhiteSpace(text[currentCharindex]) ) {
                    displaytext += text[currentCharindex];
                    currentCharindex++;
                    addLetter();
                } else {
            displaytext += text[currentCharindex];
            
            currentCharindex++;
            
            }

            }
        
        }
   
        
        
    }

    public bool QTEPressed(QTEKEY key) {
        foreach (QTEIcon QTE in QTEIcons) {
                if (indexWithinRange(currentCharindex, QTE.charIndex, 10)) {
                        if (key == QTE.key) {
                            QTEIcons.Remove(QTE);
                            return true;
                        }
                        
                    }
                }
        return false;
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

    public void setTypeSpeed(float perSecond) {
        typeSpeed = perSecond;
        typeInterval = 1.0f / typeSpeed;
    }

    float CalculateLengthOfText()
     {
         float totalLength = 0;
 
         TMP_CharacterInfo [] chars = textBox.textInfo.characterInfo;

         foreach (Char c in displaytext) {
            if (!Char.IsWhiteSpace(c) ) {
                break;
            } else {
                
                totalLength += 7;
            }

         }

         totalLength += textBox.textBounds.size.x;

         Debug.Log(totalLength);

         return totalLength;
 
         /*foreach(TMP_CharacterInfo c in chars)
         {
             
             float length = textBox.textBounds.size.x;
             
            
             totalLength += length;
         }
 
         return totalLength;*/
     }
}
