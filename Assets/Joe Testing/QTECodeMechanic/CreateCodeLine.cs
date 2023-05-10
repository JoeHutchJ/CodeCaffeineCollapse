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

    public List<int> QTEIndexes;

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
        QTEIndexes = new List<int>();
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
        GameObject qte = Instantiate(QTEIcon, QTEs);
        RectTransform rect = qte.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(xPos, 0, 0);
        QTEIndexes.Add(currentCharindex);

    }

    public bool isQTE(int index) {
        foreach (int i in  QTEIndexes) {
            if (i == index) {
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
        /*if (isQTE(currentCharindex)) {

        } else {*/
            for (int i = 0; i < QTEIndexes.Count; i++) {
                if (indexWithinRange(currentCharindex, QTEIndexes[i], range)) {
                    Transform QTEObj =  QTEs.GetChild(i);
                    QTEObj.gameObject.SetActive(true);
                    UsefulFunctions.HideAllChildren(QTEObj, false);
                    float howClose = 1 -  Mathf.Abs(QTEIndexes[i] - currentCharindex) / (float)range;
                    Color tempColor = QTEObj.Find("Image").GetComponent<Image>().color;
                    tempColor.a = Mathf.Lerp(0,1, howClose);

                    QTEObj.Find("Image").GetComponent<Image>().color = tempColor;

                    tempColor =  QTEObj.Find("Text").GetComponent<TMP_Text>().color;
                    tempColor.a = Mathf.Lerp(0,1, howClose);
                    QTEObj.Find("Text").GetComponent<TMP_Text>().color = tempColor;

                } else {
                    Transform QTEObj =  QTEs.GetChild(i);
                    Color tempColor = QTEObj.Find("Image").GetComponent<Image>().color;
                    tempColor.a = 0;

                    QTEObj.Find("Image").GetComponent<Image>().color = tempColor;

                    tempColor =  QTEObj.Find("Text").GetComponent<TMP_Text>().color;
                    tempColor.a = 0;
                    QTEObj.Find("Text").GetComponent<TMP_Text>().color = tempColor;
                }
            }

            if (currentCharindex >= text.Length) {
                finished = true;
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

         return totalLength;
 
         /*foreach(TMP_CharacterInfo c in chars)
         {
             
             float length = textBox.textBounds.size.x;
             
            
             totalLength += length;
         }
 
         return totalLength;*/
     }
}
