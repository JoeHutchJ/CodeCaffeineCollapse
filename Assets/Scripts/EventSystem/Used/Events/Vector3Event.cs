using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "New Vector Event", menuName = "ScriptableObjects/Vector3Event")] //custom inspector
public class Vector3Event : BaseEvent
{
        public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Vector3> subscribed; 

    public void Raise(Vector3 _vec) {
        if (subscribed != null ) {
        subscribed.Invoke(_vec);
        }
        

    }

    public void Register(Action<Vector3> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Vector3> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Vector3;
    }

    
}
