using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoffeeStatus {
    NONE, BREWING, FILLING, READY


}
public class CoffeeMachine : MonoBehaviour
{

    public float brewTime;

    public float coffeeElapsed;

    public GameObject cup;

    CoffeeStatus status = CoffeeStatus.NONE;

    public Event DropEvent;

    BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (status) {
            case CoffeeStatus.BREWING:
                if (coffeeElapsed + Time.deltaTime > brewTime) {
                    coffeeElapsed = 0;
                    StartFilling();
                } else {
                    coffeeElapsed += Time.deltaTime;
                }
                break;
            case CoffeeStatus.FILLING:
                if (cup != null) {
                    if (cup.GetComponent<Animation>()) {
                if (!cup.GetComponent<Animation>().isPlaying) {
                    CoffeeReady();
                }
                
            }
            
        }
        break;
        case CoffeeStatus.READY:
            if (GetChildByName.Get(gameObject, "targetPos").transform.childCount == 0) {
                PickupCoffee();
            }
            break;

        }
    }

    void StartBrewing() {
        GetComponent<AudioManager>().StopAll();
        GetComponent<AudioManager>().Play();
        Debug.Log("brewing");
        status = CoffeeStatus.BREWING;
        collider.enabled = false;
        cup = getCup();
        if (cup != null) {
            cup.GetComponent<Pickupable>().CanPickUp(false);
        }
        changePosToInter();


    }

    void StartFilling() {
        Debug.Log("filling");
        status = CoffeeStatus.FILLING;
        if (cup != null) {
            if (cup.GetComponent<Animation>()) {
                cup.GetComponent<Animation>().clip.SampleAnimation(cup, 0);
            }
        }

    }

    void CoffeeReady() {
        Debug.Log("ready");
        status = CoffeeStatus.READY;
        if (cup != null) {
            cup.GetComponent<Pickupable>().CanPickUp(false);
        }


    }



    void PickupCoffee() {
        status = CoffeeStatus.NONE;
        collider.enabled = true;
    }

    bool checkHand() {
        if (Global.ObjInHand != null) {
            if (Global.ObjInHand.GetComponent<Pickupable>() != null) {
                if (Global.ObjInHand.GetComponent<Pickupable>().identifier == "Cup") {
                    return true;
                }
            }   
        }
        return false;

    }

    GameObject getCup() {
        if (Global.ObjInHand != null) {
            if (Global.ObjInHand.GetComponent<Pickupable>() != null) {
                if (Global.ObjInHand.GetComponent<Pickupable>().identifier == "Cup") {
                    return Global.ObjInHand;
                }
            }   
        }
        return null;

    }


    public void Interact() {
        Debug.Log("coffee machine interacted");
        switch (status) {
            case CoffeeStatus.NONE: 
                if (checkHand()) {
                StartBrewing();
                }
                break;
            case CoffeeStatus.READY:
                PickupCoffee();
                break;

        }



    }

    void changePosToInter() {
        Transform target = GetChildByName.Get(gameObject, "targetPos").transform;
        Vector3 pos = target.transform.position;

        Quaternion rotation = GetChildByName.Get(gameObject, "targetPos").transform.rotation;

        if (Global.ObjInHand != null) {
            if (Global.ObjInHand.GetComponent<Pickupable>() != null) {
                Global.ObjInHand.GetComponent<Pickupable>().setPrevPos(pos);
                Global.ObjInHand.GetComponent<Pickupable>().setPrevRotation(rotation);
                GameObject objInhand = Global.ObjInHand;
                DropEvent.Raise();
                objInhand.GetComponent<Pickupable>().setNewParent(target);
            }
        }
    }
}
