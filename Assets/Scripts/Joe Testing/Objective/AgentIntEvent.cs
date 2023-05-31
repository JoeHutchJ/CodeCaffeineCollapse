using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity



[CreateAssetMenu(fileName = "New Int Event", menuName = "AgentEvents/IntEvent")] //custom inspector
public class AgentIntEvent: BaseAgentEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<int, GameObject> subscribed; 

    public void Raise(int _int) {
        if (subscribed != null ) {
        subscribed.Invoke(_int, Agent);
        }
        

    }

    public void Register(Action<int, GameObject> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<int, GameObject> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Int;
    }

    

}
