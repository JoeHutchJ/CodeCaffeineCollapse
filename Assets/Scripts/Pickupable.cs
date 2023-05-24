using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{

    public string identifier;
    public Vector3 prevPos;

    public Quaternion prevRotation;

    bool isReady;

    bool canPickup = true;

    bool pickedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CanPickUp(bool pickup) {
        canPickup = pickup;
    }

    public void setPrevPos(Vector3 pos) {
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

    public void setNewParent(Transform parent) {
        transform.parent = parent;

    }

    public void Pickup() {
        pickedUp = true;
        setPrevPos(transform.position);
        setPrevRotation(transform.rotation);
        Global.setObjinHand(this);

    }

    public void Drop() {
        transform.parent = null;
        goToPrevPos();
        goToPrevRotation();
        pickedUp = false;
        Global.setObjinHand(null);

    }
}
