using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity


[CreateAssetMenu(fileName = "New String Event", menuName = "AgentEvents/StringEvent")] //custom inspector
public class AgentStringEvent: BaseAgentEvent //derived from scriptable object class
{
    //public new String type = "Vector2";

    
    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }
    
    public Action<String, GameObject> subscribed; 




    public void Raise(String str) {
        if (subscribed != null ) {
        subscribed.Invoke(str, Agent);
        }

    }

    public void Register(Action<String, GameObject> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public override void assignType() {
    
        Type = eventTypeenum.String;
    }

    

}
