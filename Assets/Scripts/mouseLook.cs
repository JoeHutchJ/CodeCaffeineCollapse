using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {

    public enum RotationDirection
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }



    [SerializeField] private Vector2 acceleration;
    [SerializeField] private Vector2 sens;
    [SerializeField] private RotationDirection rotationDirections;
    [SerializeField] private float inputLagPeriod;
    [SerializeField] private float maxAngleFromHorizon;

    private Vector2 velocity; 
    public Vector2 rotation; // curr rotation in degs
    private Vector2 lastInputEvent;
    private float inputLagTimer;

    Vector3 rotationBeforeLock;

    bool isReturningRotation;

    [SerializeField] float moveSpeed;

    public Transform cam;

    bool cameraMoving;

    Vector3 movementTarget = new Vector3(0,0,0);

    bool rotationLocked = false;

    public float rotationSpeed = 100.0f;

    public BoolFlag isPcMode;

    public Event resetToDeskEvent;

    public BoolEvent lockRotationEvent;

    public Quaternion originalRot;

    public Quaternion prevRot;

    public Vector3 originalPos;


    private void Start()
    {
       enableCursor(false);
       originalRot = Quaternion.identity;
       originalPos = transform.position;

    }

    public void enableCursor(bool enable) {
        Debug.Log("enable cursor");
        if (enable) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

        } else {
             Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private float clampLookingangle(float angle)
    {
        return Mathf.Clamp(angle, -maxAngleFromHorizon, maxAngleFromHorizon);  //using unitys clamp to stop the y rotation from exceding the desired amount
        //preventing the user from being to "flip" the charater with the camera
    }


    private Vector2 GetInput()
    {
        inputLagTimer += Time.deltaTime; //will set the lag timer to stop jittering and outputs of 0 from the input system


        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
            );

        if((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod)
        {
            lastInputEvent = input;
            inputLagTimer = 0;
        }
        // will check the last input is valid and will output that insted of zero


        return lastInputEvent;
    }

    public void Reset() {
        transform.position = originalPos;
        transform.rotation = originalRot;

    }

    public void SetRotation(Vector3 rot) {
        velocity = new Vector2();
        rotation = new Vector2(-rot.y, -rot.x);
    }

    private void Update()
    {

        if (!rotationLocked) {
        Vector2 TargetVelocity = GetInput() * sens;



        if((rotationDirections & RotationDirection.Horizontal) == 0)
        {
            TargetVelocity.x = 0;
        }
        if((rotationDirections & RotationDirection.Vertical) == 0)
        {
            TargetVelocity.y = 0;
        }

        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, TargetVelocity.x, acceleration.x * Time.deltaTime * Global.GetMouseSensitivity()),
            Mathf.MoveTowards(velocity.y, TargetVelocity.y, acceleration.y * Time.deltaTime * Global.GetMouseSensitivity()));
            // the accelaration and the move towards function are used to smooth out the viewing speed
        rotation += velocity * Time.deltaTime;
        rotation.y = clampLookingangle(rotation.y);
        if (!isReturningRotation) {
        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
        }

        } 

        if (cameraMoving) {
            if (movementTarget != null) {
            moveTowardsPos(movementTarget);
            }

        }
    }

    public void UpdateRotation() {
        //StartCoroutine(RotateTowardsLocalRot(rotationBeforeLock));
        rotationBeforeLock = Vector3.zero;

    }

    public void moveTowardsPos(Vector3 pos) {
        cameraMoving = true;
        if (movementTarget == new Vector3(0,0,0)) {
            movementTarget = pos - getCamOffset();
        }
            
        //StartCoroutine(moveTowards(pos - getCamOffset()));
        float step = moveSpeed * Time.deltaTime;

        if (!closeTo(transform.position, movementTarget)) {
            transform.position = Vector3.MoveTowards(transform.position, movementTarget, step);



        } else {
            cameraMoving = false;
            movementTarget = new Vector3(0,0,0);
        }

    }


    IEnumerator moveTowards(Vector3 target) {
        float step = moveSpeed * Time.deltaTime;

        while (!closeTo(transform.position, target)) {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            yield return null;


        }

        StopAllCoroutines();


    }

    public void RotateBack() {
        Debug.Log("Roate bac");
        StopAllCoroutines();
        StartCoroutine(RotateBacktoPrev(prevRot));

    }

    IEnumerator RotateBacktoPrev(Quaternion prev) {
        float step = rotationSpeed * 4.0f ;

        while (transform.localRotation != prev) {
            Debug.Log("rotate prev");
            //Debug.Log(transform.localRotation == prev);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, prev, step * Time.deltaTime);
            Debug.Log(transform.localRotation == prev);

            yield return null;



        }

        Debug.Log("lock false");

        lockRotationEvent.Raise(false);

    }

    public void rotateTowardsTarget(Transform target) {

        if (rotationDirections == RotationDirection.Horizontal) {
            prevRot = transform.localRotation;
        StartCoroutine(RotateTowards(target));
        } else {
            if (isPcMode.Value) {
                originalRot = transform.localRotation;
                //resetToDeskEvent.Raise();
                //lockRotation(true);
            }
             StartCoroutine(RotateToLocalZero());
        }


    }

    


    public IEnumerator RotateTowards(Transform target) {
        float step = rotationSpeed;
        Vector3 direction = target.position - cam.transform.position;

        Quaternion horizontalRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
        Quaternion verticalRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, 0.0f));


        while (!CloseRotate(cam.transform, target.position)) {
            Debug.Log("rotate towards");
            Quaternion targetRotation = Quaternion.LookRotation(direction);

             //Quaternion targetHorizontalRotation = Quaternion.RotateTowards(transform.rotation, horizontalRotation, step);
            //Quaternion targetVerticalRotation = Quaternion.RotateTowards(transform.rotation, verticalRotation, step);
                transform.localRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step * Time.deltaTime);
            


            yield return null;
        } 



        }

      public IEnumerator RotateToLocalZero()
{
    Quaternion targetRotation = Quaternion.identity;
    float step = rotationSpeed * Time.deltaTime;

    while (transform.localRotation != targetRotation)
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, step);
        yield return null;
    }
}

        public IEnumerator RotateTowardsLocalRot(Vector3 rot) {
        float step = rotationSpeed * Time.deltaTime;
        isReturningRotation = true;
         while (transform.localEulerAngles != rot)
        {

            // Rotate towards the target rotation using Quaternion.RotateTowards
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(rot), step);

            yield return null;  // Wait for the next frame

            
        }

        isReturningRotation = false;

        } 


        public void GoToRotation (Vector3 rotation) {
            if (rotationDirections == RotationDirection.Horizontal) {
                 transform.localRotation = Quaternion.Euler(new Vector3 (rotation.x, 0.0f, rotation.z));

            } else {
                 transform.localRotation = Quaternion.Euler(new Vector3 (0.0f, rotation.y, 0.0f));
            }
            //transform.localRotation = Quaternion.Euler(rotation);

        }


    bool closeTo(Vector3 pos, Vector3 target) {

        if (Vector3.Distance(pos,target) < 40) {
            return true;
        }
        return false;
    }

    bool CloseRotate(Transform transform, Vector3 targetPosition)
    {
    
        Vector3 directionToTarget = targetPosition - transform.position;

       
        directionToTarget.Normalize();

     
        float dotProduct = Vector3.Dot(transform.forward, directionToTarget);


        float threshold = 0.99f;

        return dotProduct >= threshold;
    }

    bool closeRotate(Quaternion pos, Quaternion target) {
        //Debug.Log(Quaternion.Angle(Quaternion.Euler(pos), Quaternion.Euler(target)));
        if (Quaternion.Angle(pos, target) >= 179 && Quaternion.Angle(pos, target) <= 180) {
            return true;
        }
        //Debug.Log("closerotate");
        return false;
    }

    Vector3 getCamOffset() {
        return cam.localPosition;
    }

    


    public void lockRotation(bool Lock) {
        /*rotationLocked = Lock;
        enableCursor(Lock);
        if (!Lock) {
            //UpdateRotation();
        } else {
            //rotationBeforeLock = new Vector3(rotation.y, rotation.x, 0);
        }*/

        rotationLocked = Lock;
        enableCursor(Lock);
        if (!Lock) {
            //SetRotation(transform.localEulerAngles);
            //cam.GetComponent<CameraGo>().GoBack(prevRot);
            //cam.GetComponent<CameraGo>().enableCursor = false;
        } else {
            //rotationLocked = Lock;
            prevRot = transform.localRotation;
            //enableCursor(Lock);
        }
        


        if (originalRot != Quaternion.identity) {

            /*originalRot = Quaternion.identity;
            enableCursor(true);
            rotationLocked = true;*/
            originalRot = Quaternion.identity;
            resetToDeskEvent.Raise();
            
        } 

    }

    public void EnableCamera(bool enable) {
        cam.GetComponent<Camera>().enabled = enable;
        cam.GetComponent<AudioListener>().enabled = enable;

    }

    }


