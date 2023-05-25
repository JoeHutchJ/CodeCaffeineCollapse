using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity





[CreateAssetMenu(fileName = "AgentEvent", menuName = "AgentEvents/Events")] //custom inspector
//Adds a folder when I right-click, to create a "New Event", among other events.
public class AgentEvent: BaseAgentEvent 
{

    //public virtual String type {get; set;}

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }
    
    public Action<GameObject> subscribed; //delegate, subscribe methods to it.
    
    

    
    public void Raise() {
        if (subscribed != null ) {
        subscribed.Invoke(Agent);
        }
    }

    public void Register(Action<GameObject> method) { //could return bool?
        //Debug.Log("event register " + this.name + " ");

        subscribed += method;


    }

    public void DeRegister(Action<GameObject> method) {
        subscribed -= method;

    }

    public override void assignType() { //makes it specific per type.
        
        Type = eventTypeenum.Event;
    }

    

    


}
