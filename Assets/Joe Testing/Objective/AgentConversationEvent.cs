using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity



[CreateAssetMenu(fileName = "New Convo Event", menuName = "AgentEvents/ConvoEvent")] //custom inspector
public class AgentConversationEvent: BaseAgentEvent //derived from scriptable object class
{
    

    public override eventTypeenum Type {
        get
        {return type;}
        set
        {type = value;}

    }

    
    public Action<Conversation, GameObject> subscribed; 

    public void Raise(Conversation _bool) {
        if (subscribed != null ) {
        subscribed.Invoke(_bool, Agent);
        }
        

    }

    public void Register(Action<Conversation, GameObject> method) { //could return bool?
        //Debug.Log("listener " + this.name );
        subscribed += method;
        

    }

     public void DeRegister(Action<Conversation, GameObject> method) {
        subscribed -= method;

    }

    public override void assignType() {
        
        Type = eventTypeenum.Conversation;
    }

    

}
