using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity



[CreateAssetMenu(fileName = "New Bool Event", menuName = "AgentEvents/BoolEvent")] //custom inspector
public class AgentBoolEvent: BaseAgentEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Boolean, GameObject> subscribed; 

    public void Raise(Boolean _bool) {
        if (subscribed != null ) {
        subscribed.Invoke(_bool, Agent);
        }
        

    }

    public void Register(Action<Boolean, GameObject> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Boolean, GameObject> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Boolean;
    }

    

}
