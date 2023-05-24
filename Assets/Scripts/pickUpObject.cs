using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickUpObject : MonoBehaviour
{
    [SerializeField] private Collider currColl;
    [SerializeField] private GameObject cam;
    [SerializeField] private bool handFree;
    [SerializeField] private GameObject NewParent;


    private rayExample coll;
    private Vector3 prevObject;
    private GameObject objInHand;
    private Vector3 DeskPos;


    private void Start()
    {
        coll = cam.GetComponent<rayExample>(); //accessing the cam ray script and getting the collider from that
        handFree = true;
    }

    public GameObject getObjectInHand()
    {
        return objInHand;
    }

    private GameObject getGameObject()
    {

        currColl = coll.hitColl;
        GameObject pickupable = currColl.gameObject; //will get the collider in front of the raycast and will find the gameobject from that
        //Debug.Log(pickupable);

        return pickupable;
    }





    private void setParentClass(GameObject child, GameObject newParent)
    {
        child.transform.parent = newParent.transform;
        child.transform.localPosition = new Vector3(-0.5f,-0.1f,1.0f);
        child.transform.localRotation = new Quaternion(0, 0, 0, 0);


    }

    private void removeParentClass(GameObject child)
    {
        child.transform.parent = null;
        child.transform.position = new Vector3(prevObject.x, prevObject.y, prevObject.z);
        child.transform.rotation = new Quaternion(0, 0, 0,0);
    }





    private void getPrevPosition(GameObject pickUpable)
    {
        
        pickupable pos = pickUpable.GetComponent<pickupable>();
        pos.getPosition();
        prevObject = pos.prevPos;
    }

    public void changePutDownPos(Vector3 newPos)
    {
        prevObject = newPos;
    }




    public void putDown()
    {
        removeParentClass(objInHand);
        handFree = true;
        objInHand = null;
    }

    public void pickUp()
    {
        getPrevPosition(objInHand);
        setParentClass(objInHand, NewParent);
        handFree = false;
    }




    private void Update()
    {
        GameObject pickupable = getGameObject();

        if (handFree == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                putDown();
            }

        }


        if (pickupable.tag == "Pickupable") //must set tag of pickupable items in unity
        {
            if(handFree)
            {
                objInHand = pickupable;
                if (objInHand.GetComponent<pickupable>().canPickUp == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pickUp();
                    }
                }
            }
        }


    }

}
