using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInteract : MonoBehaviour
{
    [SerializeField] Text txt;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject prompt;
    [SerializeField] GameObject intSymbol;

    private rayExample obj;
    private GameObject inSight;
    private bool handFree;

    private void Start()
    {
        obj = cam.GetComponent<rayExample>();

    }


    private void changeText(string newPrompt)
    {
        txt.text = newPrompt;
    }


    public void boxActive()
    {
        inSight = obj.hitColl.gameObject;
        handFree = gameObject.GetComponent<pickUpObject>().handFree;
        GameObject objInHand = gameObject.GetComponent<pickUpObject>().getObjectInHand();

        if (inSight.tag == "Pickupable")
        {
            if (handFree)
            {
                string text = "Press E to Pick Up";
                changeText(text);
                prompt.SetActive(true);
                intSymbol.SetActive(true);
            }
        }
        else
        {
            prompt.SetActive(false);
            intSymbol.SetActive(false);
        }

        if(handFree == false)
        {

            if (objInHand.GetComponent<pickupable>().isReady == false)
            {
                string text = "Press E To interact";
                changeText(text);
                prompt.SetActive(true);
                intSymbol.SetActive(false);
            }
            else
            {
                string text = "Press F to Put Down";
                changeText(text);
                prompt.SetActive(true);
                intSymbol.SetActive(false);
            }

        }

        if (inSight.GetComponent<pickupable>().canPickUp == false)
        {
            prompt.SetActive(false);
            intSymbol.SetActive(false);
        }
    }


    private void Update()
    {
        boxActive();
    }
}
