using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity

//This is work from a previous project, used for MPIE Project Joseph Hutchinson

[CreateAssetMenu(fileName = "New Bool Event", menuName = "ScriptableObjects/BoolEvent")] //custom inspector
public class BoolEvent: BaseEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Boolean> subscribed; 

    public void Raise(Boolean _bool) {
        if (subscribed != null ) {
        subscribed.Invoke(_bool);
        }
        

    }

    public void Register(Action<Boolean> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Boolean> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Boolean;
    }

    

}
