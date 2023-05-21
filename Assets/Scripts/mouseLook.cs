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
    private Vector2 rotation; // curr rotation in degs
    private Vector2 lastInputEvent;
    private float inputLagTimer;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    private void Update()
    {
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
            Mathf.MoveTowards(velocity.x, TargetVelocity.x, acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, TargetVelocity.y, acceleration.y * Time.deltaTime));
            // the accelaration and the move towards function are used to smooth out the viewing speed
        rotation += velocity * Time.deltaTime;
        rotation.y = clampLookingangle(rotation.y);

        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }

}
