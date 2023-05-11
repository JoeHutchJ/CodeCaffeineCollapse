using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTEIcon : MonoBehaviour
{
    public QTEKEY key;
    public int charIndex;

    public Image imageObj;


    public TMP_Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        imageObj = transform.Find("Image").GetComponent<Image>();
        textBox = transform.Find("Text").GetComponent< TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRectPos(float xPos) {
        RectTransform rect = GetComponent<RectTransform>();
        rect.localPosition = new Vector3(xPos, 0, 0);
    }

    public void setOpacity(float opacity) {
        Color tempColor = transform.Find("Image").GetComponent<Image>().color;
                    tempColor.a = opacity;

                    transform.Find("Image").GetComponent<Image>().color = tempColor;

                    tempColor =  transform.Find("Text").GetComponent<TMP_Text>().color;
                    tempColor.a = opacity;
                    transform.Find("Text").GetComponent<TMP_Text>().color = tempColor;

    }

    public void Setup(int index) {
        Start();
        charIndex = index;

        switch (UnityEngine.Random.Range(0,4)) {
            case 0:
                key = QTEKEY.NORTHW;
                textBox.text = "North / W";
                break;
            case 1:
                key = QTEKEY.WESTA;
                textBox.text = "West / A";
                break;
            case 2:
                key = QTEKEY.SOUTHS;
                textBox.text = "South / S";
                break;
            case 3:
                key = QTEKEY.EASTD;
                textBox.text = "East / D";
                break;         
        }

    }
}
