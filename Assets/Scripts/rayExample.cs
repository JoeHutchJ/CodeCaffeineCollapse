using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayExample : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    public Collider hitColl;

    void Update()
    {   
        if (Camera.main != null) {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            hitColl = hit.collider;
            //Debug.Log(hitColl);
        }
        }
    }


}
