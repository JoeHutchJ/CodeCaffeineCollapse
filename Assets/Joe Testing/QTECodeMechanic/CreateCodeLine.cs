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

    public Image Background;

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

    float timePastQTE = 0;

    float textTotallength;

    // Start is called before the first frame update
    void Start()
    {
        textBox = transform.Find("Text").GetComponent<TMP_Text>();
        Background = transform.Find("Background").GetComponent<Image>();
        QTEs = transform.Find("QTEs");
        QTEIcons = new List<QTEIcon>();
        SetupQTEs();
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && displaytext != "") {
            Background.color = new Color(1,0f,0,1.0f); 
        }
        if (selected) {
            Background.color = new Color(0,1.0f,0,0.5f);        
            if (timeSince >= typeInterval) {
                timeSince = 0;
                addLetter(true);
            } else {
                timeSince += Time.deltaTime;
            }
        }
        if (finished) {
            Background.color = new Color(0,1.0f,0,1.0f); 
        }
        textBox.text = displaytext;

        if (isQTE(currentCharindex)) {
            timePastQTE += Time.deltaTime;
        }
    
    }

    public void Setup(string _text, float frequency) {

        textBox = transform.Find("Text").GetComponent<TMP_Text>();
        QTEs = transform.Find("QTEs");
        text = _text;

        
        

        if (text.Contains("}")) {

        } else {
            QTEpossible = true;
        }


    }

    public void SetupQTEs() {
        int sinceLastQTE = 0;

        for (int i=0; i < text.Length; i++) {
            addLetter(false);
            if (i > 5) {
            textBox.text = displaytext;
            textBox.ForceMeshUpdate(true);
             if (Char.IsWhiteSpace(text[i]) && sinceLastQTE >= 7) {
                if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f) {
                    SetupQTE(CalculateLengthOfText(), i);
                    sinceLastQTE = 0;
                }
             }  
             sinceLastQTE++;
            }

        }
        textTotallength = textBox.GetRenderedValues(false).x;
        displaytext = "";
        textBox.text = "";
        currentCharindex = 0;
        finished = false;
        foreach (Transform child in QTEs) {
        UsefulFunctions.HideAllChildren( child, true);
        }


    }

    public void SetupQTE(float widthOftext, int index) {

        float xPos = widthOftext - (GetComponent<RectTransform>().sizeDelta.x / 2);
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

    public float indexCloseness(int index, int target, int range) {
        float tempTimepastQTE = timePastQTE;
        timePastQTE = 0;
        if (indexWithinRange(index, target, range)) {
            if (index == target) {
                if (tempTimepastQTE < 0.5f) {
                return 1;
                } else {
                    if (tempTimepastQTE / 5.0f < 1) {
                        return 1 - (tempTimepastQTE / 5.0f);
                    } else {
                        return 0;
                    }
                }
            }
            return 1-(float)Mathf.Abs(target - index) / (float)range;
        }
        return -1.0f;

    }

    public void addLetter(bool skipWhiteSpace) {
        int range = 10;
        if (isQTE(currentCharindex)) {
        

        } else {
            for (int i = 0; i < QTEIcons.Count; i++) {
                if (indexWithinRange(currentCharindex, QTEIcons[i].charIndex, range)) {

                    float howClose = 1 -  Mathf.Abs(QTEIcons[i].charIndex - currentCharindex) / (float)range;

                    QTEIcons[i].setOpacity(Mathf.Lerp(0,1, howClose));
                    UsefulFunctions.HideAllChildren(QTEIcons[i].transform, false);

                } else {
                    QTEIcons[i].setOpacity(0);
                    UsefulFunctions.HideAllChildren(QTEIcons[i].transform, true);
                    
                }
            }

            if (currentCharindex >= text.Length) {
                finished = true;
                selected = false;
            } else {
                if (skipWhiteSpace) {
                if (Char.IsWhiteSpace(text[currentCharindex]) ) {
                    displaytext += text[currentCharindex];
                    currentCharindex++;
                    addLetter(true);
                    
                } else {
            displaytext += text[currentCharindex];
            
            currentCharindex++;
            
            } } else {
                displaytext += text[currentCharindex];
            
                currentCharindex++;
            }


            }
        
        }
   
        
        
    }

    public float QTEPressed(QTEKEY key) {
        foreach (QTEIcon QTE in QTEIcons) {
                if (indexWithinRange(currentCharindex, QTE.charIndex, 10)) {
                        if (key == QTE.key) {
                            removeQTE(QTE);
                            
                            return indexCloseness(currentCharindex, QTE.charIndex, 10);
                        }
                        
                    }
                }
        return -1.0f;
    }

    public void removeQTE(QTEIcon QTE) {
        UsefulFunctions.HideAllChildren(QTE.transform, true);
        QTEIcons.Remove(QTE);
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

         /*(foreach (Char c in displaytext) {
            if (!Char.IsWhiteSpace(c) ) {
                break;
            } else {
                
                totalLength += 7;
            }

         }*/

         totalLength += textBox.GetRenderedValues(false).x;

         //Debug.Log(totalLength + " " + textBox.bounds.size.x);

         return totalLength;
 
         /*foreach(TMP_CharacterInfo c in chars)
         {
             
             float length = textBox.textBounds.size.x;
             
            
             totalLength += length;
         }
 
         return totalLength;*/
     }
}
