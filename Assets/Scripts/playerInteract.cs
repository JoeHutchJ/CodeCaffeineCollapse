using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{


    [SerializeField] GameObject cam;
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] private Collider currColl;

    private rayExample coll;
    private GameObject area;

    private void Start()
    {
        coll = cam.GetComponent<rayExample>();
        currColl = coll.hitColl;
        area = currColl.gameObject;
    }

    public GameObject getArea()
    {
        return area;
    }

    private Vector3 areaToMove(GameObject point)
    {
        Vector3 toMoveTo = new Vector3(
            point.transform.position.x,
            point.transform.position.y,
            point.transform.position.z);

        return toMoveTo;
    }


    private void Update()
    {
        var inc = moveSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            currColl = coll.hitColl;
            area = currColl.gameObject;
            Debug.Log(area.name);
        }

        if(area.tag == "Interactable")
        {
            Vector3 toMove = areaToMove(area);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, toMove, inc);
        }
    }
}
