using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity



[CreateAssetMenu(fileName = "New Convo Event", menuName = "ScriptableObjects/ConvoEvent")] //custom inspector
public class ConversationEvent: BaseEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Conversation> subscribed; 

    public void Raise(Conversation _bool) {
        if (subscribed != null ) {
        subscribed.Invoke(_bool);
        }
        

    }

    public void Register(Action<Conversation> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

    public void DeRegister(Action<Conversation> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Conversation;
    }

    

}
