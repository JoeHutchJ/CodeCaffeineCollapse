using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public enum InteractableType {CAMERA, EQUIP, INTERACT};

public class Interactable : MonoBehaviour
{

    public InteractableType type;

    public UnityEvent action;

    public Vector3Event cameraMoveEvent;

    public Vector3Event cameraRotationEvent;

    public BoolEvent lockCameraRotation; 

    Vector3 cameraPosition;
    Quaternion cameraRotation;

    public bool lockRotation;
    
    

    // Start is called before the first frame update
    void Start()
    {
        switch (type) {
            case InteractableType.CAMERA:
                cameraPosition = GetChildByName.Get(this.gameObject,"CameraPosition").transform.position;
                cameraRotation = GetChildByName.Get(this.gameObject,"CameraPosition").transform.rotation;
                break;
            case InteractableType.EQUIP:

                break;
            case InteractableType.INTERACT:

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
                //action.Invoke();
                cameraMoveEvent.Raise(cameraPosition);
                if (cameraRotationEvent != null && cameraRotation != null) {
                cameraRotationEvent.Raise(cameraRotation.eulerAngles);
                }
                if (lockCameraRotation != null) {
                    lockCameraRotation.Raise(lockRotation);
                }
                break;
            case InteractableType.EQUIP:
            if (action != null) {
                action.Invoke();
            }
                break;
            case InteractableType.INTERACT:
                            if (action != null) {
                action.Invoke();
            }
                break;
        }

    }
}
