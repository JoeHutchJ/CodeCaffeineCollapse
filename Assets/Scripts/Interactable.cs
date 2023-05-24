using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cam;


    private pickUpObject inHand;
    private playerInteract gameArea;
    private bool startTimer = false;
    private float coffeeTimr = 5;
    private GameObject intObj;

    private void Start()
    {
        inHand = player.GetComponent<pickUpObject>();
        gameArea = player.GetComponent<playerInteract>();
    }

    private GameObject GetObjectInHand()
    {
        GameObject obj = inHand.getObjectInHand();

        return obj;
    }

    private GameObject GetArea()
    {
        GameObject gmeArea = gameArea.getArea();
        Debug.Log("game area : " + gameArea);
        return gmeArea;
    }

    private void changePosToInter()
    {
        Vector3 pos = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z + 0.5f
            );

        inHand.changePutDownPos(pos);
    }

  




    private void validateCoffee(GameObject obj, GameObject area)
    {
        if (area.name == "KitchenBox")
        {
            Debug.Log("kitchenbox");
            if (obj != null) {
            if (obj.name == "mug")
            {
                Debug.Log("mug");
                pickupable mug = obj.GetComponent<pickupable>();
                if (mug.isReady)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        Debug.Log("in coffee mach");
                        intObj = obj;
                        changePosToInter();
                        inHand.putDown();
                        startTimer = true;
                        mug.canPickUp = false;
                    }
                }
            }
        }
    }

    }

    private void getReadyCoffee()
    {
        startTimer = false;
        pickupable mug = intObj.GetComponent<pickupable>();
        mug.isReady = false;
        mug.canPickUp = true;
        Debug.Log("coffee ready");
        inHand.changePutDownPos(mug.prevPos);
        coffeeTimr = 5;
    }


    private void OnMouseOver()
    {
        Debug.Log("mouse over");
        GameObject obj = GetObjectInHand();
        GameObject area = GetArea();
        validateCoffee(obj, area);
    }


    private void Update()
    {
        if (startTimer)
        {
            if(coffeeTimr > 0)
            {
                coffeeTimr -= Time.deltaTime;
            }
            else
            {
                getReadyCoffee();
            }
        }
    }
}
