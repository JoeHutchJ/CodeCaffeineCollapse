using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpObject : MonoBehaviour
{
    [SerializeField] private Collider currColl;
    [SerializeField] private GameObject cam;
    [SerializeField] private bool handFree;
    [SerializeField] private GameObject NewParent;


    private rayExample coll;
    private Vector3 prevPosition;

    private Vector3 prevRotation;

    private Pickupable objInHand;

    public InteractUIController UIController;
    


    private void Start()
    {
        coll = cam.GetComponent<rayExample>(); //accessing the cam ray script and getting the collider from that
        handFree = true;
        UIController = GetChildByName.Get(gameObject,"InteractionUI").GetComponent<InteractUIController>();
    }

    private GameObject getGameObject()
    {

        currColl = coll.hitColl;
        if (currColl != null) {
        GameObject pickupable = currColl.gameObject; //will get the collider in front of the raycast and will find the gameobject from that
        //Debug.Log(pickupable);

        return pickupable;
        } 
        return null;
    }

    private void setParentClass(Pickupable obj, GameObject newParent)
    {
        if (obj.canPickup) {
        obj.transform.parent = newParent.transform;
        obj.Pickup();
        obj.transform.localPosition = new Vector3(0,0,0);
        }
        //Debug.Log(child.transform.parent.name);

        //child.transform.Translate(1, (float)0.7, 0);

    }

    private void removeParentClass(Pickupable obj)
    {
        obj.Drop();
        //child.transform.rotation = new Quaternion(0, 0, 0,0);
    }

    private void getPrevPosition(GameObject pickupable)
    {
        /*prevPosition.x = pickupable.transform.position.x;
        prevPosition.y = pickupable.transform.position.y;
        prevPosition.z = pickupable.transform.position.z;*/
        prevPosition = pickupable.transform.position;

        prevRotation = pickupable.transform.rotation.eulerAngles;
    }

    public void Drop() {
        removeParentClass(objInHand);
        handFree = true;
        objInHand = null;
    }

    public void Give(Interactable recip) {
        objInHand.Give(recip);
        handFree = true;
        objInHand = null;
    }





    private void Update()
    {

        if (!Global.cursorMode) {
        GameObject obj = getGameObject();

            if (!handFree)
        {
            if (obj.GetComponent<Interactable>() != null) {
                if (obj.GetComponent<Interactable>().isGivable()) {
                    if (objInHand.GetComponent<Interactable>() != null) {
                
                    UIController.AddPrompt("Press F to give");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Give(obj.GetComponent<Interactable>());

            }



                } else {
            if (objInHand.GetComponent<Interactable>() != null) {
                
            UIController.AddPrompt("Press F to drop");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Drop();

            }
            }
            }
            
            

        }

        if (obj != null) {

            Pickupable pickupable = obj.GetComponent<Pickupable>();

        if (pickupable != null) {

        if(handFree)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log(obj.name);
                    objInHand = pickupable;
                    Interactable interactable = objInHand.GetComponent<Interactable>();
                    if (interactable != null) {
                        interactable.Interact();
                    }

                    //getPrevPosition(objInHand);
                    setParentClass(objInHand, NewParent);
                    handFree = false;
                }
            }
        }

    }

        }

    }

    }


