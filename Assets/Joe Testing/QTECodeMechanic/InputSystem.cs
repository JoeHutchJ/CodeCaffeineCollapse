using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using System;

/*
Hold all references to Events and link to Input system outputs...
i.e OnMovement, OnInteract, OnJump etc.
These events can then be passed to listeners...

OnMovement could be an event with no parameters, or pass the Vector2? 
May need to develop other types of events with parameters, Event Listener updated to be general purpose for all types of events
*/


public class InputSystem : MonoBehaviour
{
    //Many public Events for all Inputs

    public Event anyKeyPressed;
    
    ////////////////////////////////////

    public InputActionAsset playerControls;

    public PlayerInput playerInput;

    void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        
        
    }
    

    public void onAnyKeyPressed(InputAction.CallbackContext context) {

        if (context.performed) {

            anyKeyPressed.Raise();
        }
    }

}
