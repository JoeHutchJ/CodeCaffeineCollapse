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

    public Event DropEvent;

    public float moveSpeed = 400.0f;

    public float rotationSpeed = 40.0f;
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
        //transform.position = prevPos;
        StartCoroutine(MoveToPos(prevPos));
    }

    public void goToPrevRotation() {
        //transform.rotation = prevRotation;
        StartCoroutine(RotateTo(prevRotation));

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

    public void Give(Interactable interactable) {
        Instantiate(gameObject, originalPos, originalRotation);
        Transform parent = interactable.getGiveParent();
        setPrevPos(parent.position);
        setPrevRotation(parent.rotation);
        DropEvent.Raise();
        setNewParent(parent);
        Global.setObjinHand(null);
        Debug.Log("interactable types");
        if (interactable.type == InteractableType.GIVE) {
            interactable.Interact();
        }
        if (interactable.secondaryType == InteractableType.GIVE) {
            interactable.secondaryInteract();
        } 
        

    }


    public void GoToPos(Vector3 pos) {
        StartCoroutine(MoveToPos(pos));

    }

    IEnumerator MoveToPos(Vector3 pos) {

        float step = moveSpeed * Time.deltaTime;
        while (!AtPos(pos)) {

            transform.position = Vector3.MoveTowards(transform.position, pos, step);

            yield return null;
        }


    }

    public void RotateToPos(Quaternion rotation) {
        StartCoroutine(RotateTo(rotation));

    }

        IEnumerator RotateTo(Quaternion rotation) {

        float step = rotationSpeed * Time.deltaTime;
        while (!AtRotation(rotation)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step);
            //Debug.Log(transform.rotation);
            yield return null;
        }


    }



    public bool AtPos(Vector3 pos) {
        return Vector3.Distance(transform.position, pos) < 10;




    }

    public bool AtRotation(Quaternion rotation) {
        return Quaternion.Angle(transform.rotation, rotation) < 5;




    }


}
