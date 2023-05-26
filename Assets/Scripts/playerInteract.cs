using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerInteract : MonoBehaviour
{


    [SerializeField] GameObject cam;
    [SerializeField] Vector3 deskPos;
    [SerializeField] Vector3 ktchPos;
    [SerializeField] float moveSpeed = 8.0f;
    [SerializeField] private Collider currColl;

    public rayExample coll;

    InteractUIController UIcontroller;

    private void Start()
    {
        coll = cam.GetComponent<rayExample>();
        UIcontroller = GetChildByName.Get(gameObject,"InteractionUI").GetComponent<InteractUIController>();
    }


    private void Update()
    {
        var inc = moveSpeed * Time.deltaTime;

        if (coll.hitColl != null) {

        currColl = coll.hitColl;

        UIcontroller.HideInteractIcon(true);

        if (currColl.GetComponent<Interactable>() != null) {
            AddNewPrompt(currColl.GetComponent<Interactable>());
            UIcontroller.HideInteractIcon(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            
            //Debug.Log(currColl);
        if (!Global.cursorMode) {

        if (currColl != null) {



        /*if(currColl.name == "Desk")
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, deskPos, inc);
        }

        if(currColl.name == "kitchen")
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, ktchPos, inc);
        }*/
        Interactable obj = currColl.gameObject.GetComponent<Interactable>();

        if (obj != null) {
            obj.Interact();
        }

        }
        }
    }

        }

    }

    void AddNewPrompt(Interactable interactable) {
        UIcontroller.AddPrompt(interactable.getPrompt());
        }



    }


