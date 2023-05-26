using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity



[CreateAssetMenu(fileName = "New Trans Event", menuName = "ScriptableObjects/TransformEvent")] //custom inspector
public class TransformEvent: BaseEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Transform> subscribed; 

    public void Raise(Transform _bool) {
        if (subscribed != null ) {
        subscribed.Invoke(_bool);
        }
        

    }

    public void Register(Action<Transform> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Transform> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Transform;
    }

    

}
