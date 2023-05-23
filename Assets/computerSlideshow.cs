using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class computerSlideshow : MonoBehaviour
{
    Sprite[] imageArray;
    int index;

    Image imageUI;

    float delay = 5.0f;

    float elapsed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        imageUI = GetChildByName.Get(gameObject, "Image").GetComponent<Image>();
        UnityEngine.Object[] Objects = Resources.LoadAll("ComputerSlideshow",typeof(Texture2D));
        List<Sprite> images = new List<Sprite>();
        
        foreach (UnityEngine.Object obj in Objects) {
            images.Add(UsefulFunctions.TextureToSprite((Texture2D)obj));
        }

        imageArray = images.ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        if (elapsed + Time.deltaTime >= delay) {
            elapsed = 0;
            nextImage();
        }

        elapsed += Time.deltaTime;
    }

    void nextImage() {
        if (index >= imageArray.Length - 1) {
            index = 0;
            
        } else {
        index++;
        }
        displayImage(imageArray[index]);

    }

    void displayImage(Sprite sprite) {
        imageUI.sprite = sprite;

    }
}
