using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "New Bool Event", menuName = "ScriptableObjects/TaskEvent")] //custom inspector
public class TaskEvent : BaseEvent
{
        public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Task> subscribed; 

    public void Raise(Task _task) {
        if (subscribed != null ) {
        subscribed.Invoke(_task);
        }
        

    }

    public void Register(Action<Task> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Task> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Task;
    }

    
}
