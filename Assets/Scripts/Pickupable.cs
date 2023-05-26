using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{

    public string identifier;
    public Vector3 prevPos;

    public Vector3 originalPos;

    public Quaternion originalRotation;

    public bool atOriginalPos;

    public Quaternion prevRotation;

    bool isReady;

    public bool canPickup = true;

    bool pickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CanPickUp(bool pickup) {
        canPickup = pickup;
    }

    public void setPrevPos(Vector3 pos) {
        Debug.Log("set pos");
        prevPos = pos;
    }

    public void setPrevRotation(Quaternion angle) {
        prevRotation = angle;
    }

    public void goToPrevPos() {
        transform.position = prevPos;
    }

    public void goToPrevRotation() {
        transform.rotation = prevRotation;

    }

    public void goToOriginalPos() {
        prevPos = originalPos;
        goToPrevPos();
    }

    public void goToOriginalRotation() {
        prevRotation= originalRotation;
        goToPrevRotation();

    }
    
    public void setOriginal() {
        Debug.Log("set original");
        atOriginalPos = true;
        prevPos = originalPos;
        prevRotation = originalRotation;
    }

    public void setNewParent(Transform parent) {
        transform.parent = parent;

    }

    public void Pickup() {
        if (canPickup) {
            if (atOriginalPos) {
        pickedUp = true;
        setPrevPos(transform.position);
        setPrevRotation(transform.rotation);
        Global.setObjinHand(this);
            } else {
                Global.setObjinHand(this);
                setOriginal();
                pickedUp = true;
            }
        }

    }

    public void Drop() {
        transform.parent = null;
        goToPrevPos();
        goToPrevRotation();
        pickedUp = false;
        Global.setObjinHand(null);

    }


}
