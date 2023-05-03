using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// due to problems with inheritance of functions between parent and children, this will need to be an abstract parent class of all Event types...


public class BaseEvent : ScriptableObject
{
    protected eventTypeenum type;

    public virtual eventTypeenum Type {
        get { return type;} 
        set {type = value;}
    }
    
    //public abstract void Raise();

    public eventTypeenum GetEventType() {

        return type;
    
    }

    public virtual void assignType() {

    }

}
