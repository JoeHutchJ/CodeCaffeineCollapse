using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity


[CreateAssetMenu(fileName = "New Vector Event", menuName = "ScriptableObjects/VecEvent")] //custom inspector
public class VecEvent: BaseEvent //derived from scriptable object class
{
    //public new String type = "Vector2";

    
    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }
    
    public Action<Vector2> subscribed; 




    public void Raise(Vector2 vec) {
        if (subscribed != null ) {
        subscribed.Invoke(vec);
        }


    }

    public void Register(Action<Vector2> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public override void assignType() {
        
        Type = eventTypeenum.Vector2;
    }

    

}
