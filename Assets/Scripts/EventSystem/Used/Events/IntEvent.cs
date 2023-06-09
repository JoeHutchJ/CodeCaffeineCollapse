using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity

//This is work from a previous project, used for MPIE Project Joseph Hutchinson

[CreateAssetMenu(fileName = "New Int Event", menuName = "ScriptableObjects/IntEvent")] //custom inspector
public class IntEvent: BaseEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<int> subscribed; 

    public void Raise(int _int) {
        if (subscribed != null ) {
        subscribed.Invoke(_int);
        }
        

    }

    public void Register(Action<int> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<int> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Int;
    }

    

}
