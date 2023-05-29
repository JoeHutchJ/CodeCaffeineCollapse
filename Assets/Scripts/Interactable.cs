using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public enum InteractableType {NONE, CAMERA, EQUIP, INTERACT, GIVE};

public class Interactable : MonoBehaviour
{

    public InteractableType type;

    public string InteractPrompt;

    public bool primaryActive= true;

    public UnityEvent action;

    //public UnityEvent<GameObject> Objaction;

    public InteractableType secondaryType;

    public string secondaryInteractPrompt;

    public bool secondaryActive;

    public UnityEvent secondaryAction; //only when equipped

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
        if (primaryActive) {
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

                if (action != null) {
                    action.Invoke();
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

        public void secondaryInteract() {
        if (secondaryActive) {
        switch (secondaryType) {
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
                secondaryAction.Invoke();
            }
                break;
            case InteractableType.INTERACT:
            if (action != null) {
                secondaryAction.Invoke();
            }
                break;
            case InteractableType.GIVE:
            if (action != null) {
                secondaryAction.Invoke();
            }
                break;
        }

    }
        }


   /*public void Interact(GameObject obj) {
        //do things
        Interact();
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
            if (Objaction != null) {
                Objaction.Invoke(obj);
            }
                break;
            case InteractableType.INTERACT:
            Debug.Log(gameObject.name);
            if (Objaction != null) {
                Objaction.Invoke(obj);
            }
                break;
        }

    }*/


    public string getPrompt() {
        
        if (primaryActive) {
        if (InteractPrompt != "") {
            return InteractPrompt;
        } else {
            switch (type) {
                case InteractableType.CAMERA:
                    return "Click to Move";

                case InteractableType.EQUIP:
                    return "Press E to Equip";

                case InteractableType.INTERACT:
                    return "Click to Interact";


            }
        }
        }
        return null;


    }

        public string getSecondaryPrompt() {
        
        if (secondaryInteractPrompt != "") {
            return secondaryInteractPrompt;
        } else {
            switch (secondaryType) {
                case InteractableType.CAMERA:
                    return "E to Move";

                case InteractableType.EQUIP:
                    return "E to Equip";

                case InteractableType.INTERACT:
                    return "E to Interact";
                    


            }
        }
        return "Interact";


    }

    public bool isGivable() {
        return type == InteractableType.GIVE || secondaryType == InteractableType.GIVE;

    }

    public Transform getGiveParent() {
        if (GetChildByName.Get(gameObject,"GiveParent") != null) {
            return GetChildByName.Get(gameObject,"GiveParent").transform;
        } else {
            return transform;
        }

    }
}
