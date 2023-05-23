using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity



[CreateAssetMenu(fileName = "New Email Event", menuName = "ScriptableObjects/EmailEvent")] //custom inspector
public class EmailEvent: BaseEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Email> subscribed; 

    public void Raise(Email _bool) {
        if (subscribed != null ) {
        subscribed.Invoke(_bool);
        }
        

    }

    public void Register(Action<Email> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Email> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Email;
    }

    

}
