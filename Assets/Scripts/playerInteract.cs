using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{


    [SerializeField] GameObject cam;
    [SerializeField] Vector3 deskPos;
    [SerializeField] Vector3 ktchPos;
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] private Collider currColl;

    private rayExample coll;

    private void Start()
    {
        coll = cam.GetComponent<rayExample>();
    }


    private void Update()
    {
        var inc = moveSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            currColl = coll.hitColl;
            Debug.Log(currColl);
        }

        if(currColl.name == "Desk")
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, deskPos, inc);
        }

        if(currColl.name == "kitchen")
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, ktchPos, inc);
        }
    }
}
