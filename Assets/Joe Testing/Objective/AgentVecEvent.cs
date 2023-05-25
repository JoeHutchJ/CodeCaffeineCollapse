using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity


[CreateAssetMenu(fileName = "New Vector Event", menuName = "AgentEvents/VecEvent")] //custom inspector
public class AgentVecEvent: BaseAgentEvent //derived from scriptable object class
{
    //public new String type = "Vector2";

    
    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }
    
    public Action<Vector2, GameObject> subscribed; 




    public void Raise(Vector2 vec) {
        if (subscribed != null ) {
        subscribed.Invoke(vec, Agent);
        }


    }

    public void Register(Action<Vector2, GameObject> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Vector2, GameObject> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Vector2;
    }

    

}
