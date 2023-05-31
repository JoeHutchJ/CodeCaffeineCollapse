using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGo : MonoBehaviour
{

    public Transform currentTarget;

    public Quaternion currentTargetRot;
    
    public float moveSpeed;
    
    public float rotationSpeed; 

    public float timeTocomplete = 1.0f;

    public bool active;

    public bool goback = false;

    public bool enableCursor;

    public bool waitingforpause;

    public BoolEvent mainCamEvent;

    public bool mainCam = false;

    public BoolEvent cameraLockEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            if (currentTarget != null) {
            GoToPos(currentTarget.position);
            RotateTo(currentTarget.rotation);
            } else {
                RotateTo(currentTargetRot);

            }

        
        if (currentTarget != null) {
        if (closeTo(transform.position,currentTarget.position) && transform.rotation == currentTarget.rotation) {
            active = false;
            Debug.Log("camera go close to");
            if (goback) {
                goback = false;
                if (!mainCam) {
                mainCamEvent.Raise(true);
                }
                Debug.Log(enableCursor);
                cameraLockEvent.Raise(!enableCursor);
                
                
            }
            if (waitingforpause) {
                Time.timeScale = 0.0f;
                waitingforpause = false;
            }
        }
        } else {
             if (transform.rotation == currentTargetRot) {
                active = false;
            Debug.Log("camera go close to");
            if (goback) {
                goback = false;
                if (!mainCam) {
                mainCamEvent.Raise(true);
                }
                Debug.Log(enableCursor);
                cameraLockEvent.Raise(enableCursor);
                
                
            }
            if (waitingforpause) {
                Time.timeScale = 0.0f;
                waitingforpause = false;
            }
             }

        }
        }
    }

    public void Go(Transform target) {
        goback = false;
        currentTarget = target;
        active = true;
        
        moveSpeed = Vector3.Distance(target.position, transform.position) / timeTocomplete;
        rotationSpeed = Quaternion.Angle(target.rotation, transform.rotation) / timeTocomplete;


    }

    public void GoBack(Transform target) {
        currentTarget = target;
        active = true;
        goback = true;

                moveSpeed = Vector3.Distance(target.position, transform.position) / ( timeTocomplete * 0.25f);
        rotationSpeed = Quaternion.Angle(target.rotation, transform.rotation) / ( timeTocomplete * 0.25f);
        enableCursor = true;
        
    }

    public void GoBack(Quaternion rot) {
        active = true;
        goback = true;
        currentTargetRot = rot;
         rotationSpeed = Quaternion.Angle(currentTargetRot, transform.rotation) / ( timeTocomplete * 0.25f);
         enableCursor = true;



    }



    public void GoToPos(Vector3 pos) {

        float step = moveSpeed * Time.deltaTime;

        if (!closeTo(transform.position, pos)) {
            transform.position = Vector3.MoveTowards(transform.position, pos, step);

            


        } 

    }


    public void RotateTo(Quaternion rot) {
        float step = rotationSpeed * Time.deltaTime;
         if (transform.rotation != rot) {

            // Rotate towards the target rotation using Quaternion.RotateTowards
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, step);

           

            
        }

        }

    bool closeTo(Vector3 pos, Vector3 target) {
        if (Vector3.Distance(pos,target) < 1) {
            return true;
        }
        return false;
    }

    public void hideCam(bool hide) {
        GetComponent<Camera>().enabled = !hide;
        GetComponent<AudioListener>().enabled = !hide;

    }


    
}
