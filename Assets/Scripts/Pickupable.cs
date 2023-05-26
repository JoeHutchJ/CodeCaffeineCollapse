using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupable : MonoBehaviour
{
    public bool isReady = true; // can be used as a variable for coffee cup and other equipibles which need to be interacted with
    //is true when empty

    public bool canPickUp = true;
    private bool firstTime = true;
    public Vector3 prevPos;

    public Vector3 getPosition()
    {
        Vector3 pos = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z);

        if (firstTime)
        {
            prevPos = pos;
            firstTime = false;
        }

        return pos;
    }


    
}
