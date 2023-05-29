using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGo : MonoBehaviour
{

    public Transform currentTarget;
    public float moveSpeed;
    public float rotationSpeed; 

    public bool active;

    public bool goback = false;

    public BoolEvent mainCamEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            GoToPos(currentTarget.position);
            RotateTo(currentTarget.rotation);

        }

        if (closeTo(transform.position,currentTarget.position) && transform.rotation == currentTarget.rotation) {
            active = false;
            if (goback) {
                goback = false;
                mainCamEvent.Raise(true);
            }
        }
    }

    public void Go(Transform target) {
        currentTarget = target;
        active = true;

    }

    public void GoBack(Transform target) {
        currentTarget = target;
        active = true;
        goback = true;
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
