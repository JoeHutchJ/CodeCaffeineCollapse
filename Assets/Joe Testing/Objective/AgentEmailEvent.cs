using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity


[CreateAssetMenu(fileName = "New Email Event", menuName = "ScriptableObjects/EmailEvent")] //custom inspector
public class AgentEmailEvent : BaseAgentEvent //derived from scriptable object class
{
    //public new String type = "Vector2";

    
    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }
    
    public Action<Email, GameObject> subscribed; 




    public void Raise(Email str) {
        if (subscribed != null ) {
        subscribed.Invoke(str, Agent);
        }

    }

    public void Register(Action<Email, GameObject> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Email, GameObject> method) {
        subscribed -= method;

    }

    public override void assignType() {
    
        Type = eventTypeenum.Email;
    }

    

}
