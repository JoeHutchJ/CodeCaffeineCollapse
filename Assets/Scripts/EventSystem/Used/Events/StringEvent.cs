using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity


[CreateAssetMenu(fileName = "New String Event", menuName = "ScriptableObjects/StringEvent")] //custom inspector
public class StringEvent: BaseEvent //derived from scriptable object class
{
    //public new String type = "Vector2";

    
    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }
    
    public Action<String> subscribed; 




    public void Raise(String str) {
        if (subscribed != null ) {
        subscribed.Invoke(str);
        }

    }

    public void Register(Action<String> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<String> method) {
        subscribed -= method;

    }
    

    public override void assignType() {
    
        Type = eventTypeenum.String;
    }

    

}
