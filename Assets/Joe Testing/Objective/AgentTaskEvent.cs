using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "A", menuName = "AgentEvents/TaskEvent")] //custom inspector
public class AgentTaskEvent : BaseAgentEvent
{
        public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Task, GameObject> subscribed; 

    public void Raise(Task _task) {
        if (subscribed != null ) {
        subscribed.Invoke(_task, Agent);
        }
        

    }

    public void Register(Action<Task, GameObject> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Task, GameObject> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Task;
    }

    
}
