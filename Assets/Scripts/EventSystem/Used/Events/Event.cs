using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Sirenix.OdinInspector;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity





[CreateAssetMenu(fileName = "New Event", menuName = "ScriptableObjects/Events")] //custom inspector
//Adds a folder when I right-click, to create a "New Event", among other events.

public class Event: BaseEvent 
{

    //public virtual String type {get; set;}

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }
    
    public Action subscribed; //delegate, subscribe methods to it.
    
    

    
    public void Raise() {
        if (subscribed != null ) {
        subscribed.Invoke();
        }
    }

    public void Register(Action method) { //could return bool?
        //Debug.Log("event register " + this.name + " ");

        subscribed += method;


    }

    public void DeRegister(Action method) {
        subscribed -= method;

    }

    public override void assignType() { //makes it specific per type.
        
        Type = eventTypeenum.Event;
    }

    

    


}
