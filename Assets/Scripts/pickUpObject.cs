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

    private GameObject objInHand;


    private void Start()
    {
        coll = cam.GetComponent<rayExample>(); //accessing the cam ray script and getting the collider from that
        handFree = true;
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

    private void setParentClass(GameObject child, GameObject newParent)
    {

        child.transform.parent = newParent.transform;

        child.transform.localPosition = new Vector3(0,0,0);
        //Debug.Log(child.transform.parent.name);

        //child.transform.Translate(1, (float)0.7, 0);

    }

    private void removeParentClass(GameObject child)
    {
        child.transform.parent = null;
        child.transform.position = new Vector3(prevPosition.x, prevPosition.y, prevPosition.z);
        child.transform.rotation = Quaternion.Euler(prevRotation);
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





    private void Update()
    {
        GameObject pickupable = getGameObject();

        if (pickupable != null) {

        if (handFree == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                removeParentClass(objInHand);
                handFree = true;
                objInHand = null;
            }

        }


        if (pickupable.tag == "Pickupable") //must set tag of pickupable items in unity
        {
            if(handFree)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    objInHand = pickupable;
                    Interactable interactable = objInHand.GetComponent<Interactable>();
                    if (interactable != null) {
                        interactable.Interact();
                    }

                    getPrevPosition(objInHand);
                    setParentClass(objInHand, NewParent);
                    handFree = false;
                }
            }
        }

    }

    }

}
