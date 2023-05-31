using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is work from a previous project, used for MPIE Project Joseph Hutchinson


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

    public eventTypeenum getType() {
        assignType();
        return type;
      
    }

}
