using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public enum InteractableType {CAMERA, EQUIP, INTERACT};

public class Interactable : MonoBehaviour
{

    public InteractableType type;

    public UnityAction action;

    public Vector3Event cameraMoveEvent;

    public Vector3 cameraPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        switch (type) {
            case InteractableType.CAMERA:
                cameraPosition = GetChildByName.Get(this.gameObject,"CameraPosition").transform.position;
                break;
            case InteractableType.EQUIP:
                action.Invoke();
                break;
            case InteractableType.INTERACT:
                action.Invoke();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover() {
        //hover effect

    }

    public void Interact() {
        //do things

        switch (type) {
            case InteractableType.CAMERA:
                action.Invoke();
                cameraMoveEvent.Raise(cameraPosition);
                break;
            case InteractableType.EQUIP:
                action.Invoke();
                break;
            case InteractableType.INTERACT:
                action.Invoke();
                break;
        }

    }
}
