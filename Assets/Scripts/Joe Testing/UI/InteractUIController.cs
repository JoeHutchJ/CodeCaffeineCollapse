using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class InteractUIController : MonoBehaviour
{
    Image interactIcon;

    public GameObject PromptBoxPrefab;

    public Transform prompts;

    int addedThisFrame = 0;

    public Image crosshair; 

    bool hidden;

    public Event leftOffice;

    // Start is called before the first frame update
    void Start()
    {
        interactIcon = GetChildByName.Get(gameObject, "InteractionIcon").GetComponent<Image>();
        prompts = GetChildByName.Get(gameObject, "Prompts").transform;
        crosshair = GetChildByName.Get(gameObject, "Crosshair").GetComponent<Image>();
        HideInteractIcon(true);
    }

    // Update is called once per frame
    void Update()
    {   
        WipePrompts();
            
        
    }

    public void HideAll(bool hide) {
        hidden = hide;
                interactIcon.enabled = !hide;
                crosshair.enabled = !hide;
                prompts.gameObject.SetActive(!hide);

    }
    

    public void HideInteractIcon(bool hide) {
        if (!hidden) {
        interactIcon.enabled = !hide;
        crosshair.enabled = hide;
        } else {
        crosshair.enabled = false;
        interactIcon.enabled = false;
        }
    }

    public void AddPrompt(string prompt) {
        if (prompt != null) {
        GameObject obj = Instantiate(PromptBoxPrefab, prompts);
        GetChildByName.Get(obj, "Prompt").GetComponent<TMP_Text>().text = prompt;
        addedThisFrame++;
        }
    }

    public void WipePrompts() {
        for (int i = prompts.childCount; i >= 0; i--) {
            if (i < prompts.childCount - addedThisFrame) {
                Destroy(prompts.GetChild(i).gameObject);
            }
        }
        addedThisFrame = 0;
    }

    public void HideUI(bool hide) {
        GetComponent<Canvas>().enabled = !hide;

    }

    public void FadeBlackScreen() {
        StartCoroutine(FadeInBlackScreen());


    }

    public void ResetBlackScreen() {
        GetChildByName.Get(gameObject, "BlackScreenFade").GetComponent<CanvasGroup>().alpha = 0;

    }

    IEnumerator FadeInBlackScreen() {
        CanvasGroup obj = GetChildByName.Get(gameObject, "BlackScreenFade").GetComponent<CanvasGroup>();
        float step = 0.7f;

        while (obj.alpha < 1) {
            obj.alpha += step * Time.deltaTime;
            yield return null;
        }

        leftOffice.Raise();





    }

    public void DisplayDay() {
        ResetBlackScreen();

        TMP_Text textBox = GetChildByName.Get(gameObject,"Day").GetComponent<TMP_Text>();
        Debug.Log("ui " + Global.dayIndex);
        textBox.text = Global.currentDay;
        StartCoroutine(fadeDay(textBox.gameObject.GetComponent<CanvasGroup>()));

    }

    IEnumerator fadeDay(CanvasGroup canvasGroup) {

            bool done = false;

            bool fadingIn = true;

            float step = 0.4f;

            while (!done) {
                if (fadingIn) {
                    if (canvasGroup.alpha < 1) {
                        canvasGroup.alpha += step * Time.deltaTime;
                    } else {
                        fadingIn = false;
                    }
                    yield return null;
                    
                } else {
                    if (canvasGroup.alpha > 0) {
                         canvasGroup.alpha -= step* Time.deltaTime;
                    } else {
                        done = true;
                    }
                    yield return null;


                }


            }


    }
}
