using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Sirenix.OdinInspector;

[Serializable]
public class EventInfo
{
    [EnumToggleButtons]
    public eventTypeenum EventType;

    public isAgent isAgentEvent = isAgent.NONE;

    [ShowIf("isAgentEvent", isAgent.NONE)]
    public BaseEvent Event; //main
    [HideIf("isAgentEvent", isAgent.NONE)]
    public BaseAgentEvent AgentEvent;
    [HideIf("isAgentEvent", isAgent.NONE)]
    public GameObject Agent;

     [ShowIf("EventType", eventTypeenum.Vector2)]
     public Vector2 Vec2input;

      [ShowIf("EventType", eventTypeenum.Boolean)]
     public Boolean boolinput;

      [ShowIf("EventType", eventTypeenum.String)]
     public String stringinput;

      [ShowIf("EventType", eventTypeenum.Int)]
     public int intinput;


      [ShowIf("EventType", eventTypeenum.Task)]
     public Task taskinput;

    
      [ShowIf("EventType", eventTypeenum.Vector3)]
     public Vector3 vector3input;

     
      [ShowIf("EventType", eventTypeenum.Conversation)]
      [InlineEditor]
     public Conversation conversationinput;

     
      [ShowIf("EventType", eventTypeenum.Email)]
     public Email emailinput;
    
     [ShowIf("EventType", eventTypeenum.Transform)]
     public Transform transforminput;


    public void Raise() {
        
            if (AgentEvent != null) {
            AgentEvent.assignType();
            switch (AgentEvent.Type) {
        case eventTypeenum.Vector2 :
            AgentVecEvent tempVecEvent = (AgentVecEvent)AgentEvent;
            tempVecEvent.Raise(Vec2input);
            break;
        case eventTypeenum.Boolean:
            
            AgentBoolEvent tempBoolEvent = (AgentBoolEvent)AgentEvent;
            tempBoolEvent.Agent = Agent;
            tempBoolEvent.Raise(boolinput);
            break;
        case eventTypeenum.String:
            AgentStringEvent tempStringEvent = (AgentStringEvent)AgentEvent;
            tempStringEvent.Agent = Agent;
            tempStringEvent.Raise(stringinput);
            break;    
        case eventTypeenum.Int:
            AgentIntEvent tempIntEvent = (AgentIntEvent)AgentEvent;
            tempIntEvent.Agent = Agent;
            tempIntEvent.Raise(intinput);
            break;  
        case eventTypeenum.Task:
            AgentTaskEvent tempTaskEvent = (AgentTaskEvent)AgentEvent;
            tempTaskEvent.Agent = Agent;
            tempTaskEvent.Raise(taskinput);
            break;  
        case eventTypeenum.Vector3:
            AgentVector3Event tempVec3Event = (AgentVector3Event)AgentEvent;
            tempVec3Event.Agent = Agent;
            tempVec3Event.Raise(vector3input);
            break;
        case eventTypeenum.Conversation:
            AgentConversationEvent tempConvEvent = (AgentConversationEvent)AgentEvent;
            tempConvEvent.Agent = Agent;
            tempConvEvent.Raise(conversationinput);
            break;
        case eventTypeenum.Email:
            AgentEmailEvent tempEmailEvent = (AgentEmailEvent)AgentEvent;
            tempEmailEvent.Agent = Agent;
            tempEmailEvent.Raise(emailinput);
            break;
        case eventTypeenum.Transform:
            AgentTransformEvent temtransformEvent = (AgentTransformEvent)AgentEvent;
            temtransformEvent.Agent = Agent;
            temtransformEvent.Raise(transforminput);
            break;
        case eventTypeenum.Event:
            AgentEvent tempEvent = (AgentEvent)AgentEvent;
            tempEvent.Agent = Agent;
            tempEvent.Raise();
            break;
        

        }
            
        }


        if (Event != null) {

            Event.assignType();
        switch (Event.Type) {
        case eventTypeenum.Vector2 :
            VecEvent tempVecEvent = (VecEvent)Event;
            tempVecEvent.Raise(Vec2input);
            break;
        case eventTypeenum.Boolean:
            
            BoolEvent tempBoolEvent = (BoolEvent)Event;
            tempBoolEvent.Raise(boolinput);
            break;
        case eventTypeenum.String:
            StringEvent tempStringEvent = (StringEvent)Event;
            tempStringEvent.Raise(stringinput);
            break;    
        case eventTypeenum.Int:
            IntEvent tempIntEvent = (IntEvent)Event;
            tempIntEvent.Raise(intinput);
            break;  
        case eventTypeenum.Task:
            TaskEvent tempTaskEvent = (TaskEvent)Event;
            tempTaskEvent.Raise(taskinput);
            break;  
        case eventTypeenum.Vector3:
            Vector3Event tempVec3Event = (Vector3Event)Event;
            tempVec3Event.Raise(vector3input);
            break;
        case eventTypeenum.Conversation:
            ConversationEvent tempConvEvent = (ConversationEvent)Event;
            tempConvEvent.Raise(conversationinput);
            break;
        case eventTypeenum.Email:
            EmailEvent tempEmailEvent = (EmailEvent)Event;
            tempEmailEvent.Raise(emailinput);
            break;
        case eventTypeenum.Transform:
            TransformEvent temtransformEvent = (TransformEvent)Event;
            temtransformEvent.Raise(transforminput);
            break;
        case eventTypeenum.Event:
            Event tempEvent = (Event)Event;
            tempEvent.Raise();
            break;
        

        }
        }

    }


    public bool Check() {
        return true;


    }

    public bool CheckAgent(GameObject agent) {
        if (agent == Agent) {
        return true;
        }
        return false;


    }

        public bool Check(Vector2 vec) {
        if (vec == Vec2input) {
        return true;
        }
        return false;


    }

    public bool Check(Boolean boo) {
        if (boo == boolinput) {
        return true;
        }
        return false;


    }

    public bool Check(String str) {
        if (str.Equals(stringinput)) {
        return true;
        }
        return false;


    }

    public bool Check(int i) {
        if (i == intinput) {
        return true;
        }
        return false;


    }

        public bool Check(Vector3 vec3) {
        if (vec3 == vector3input) {
        return true;
        }
        return false;


    }

    public bool Check(Conversation conv) {
        if (conv == conversationinput) { //maybe not work
        return true;
        }
        return false;


    }

        public bool Check(Email em) {
        if (em.id == emailinput.id) { //maybe not work
        return true;
        }
        return false;


    }

            public bool Check(Task tsk) {
        if (tsk.ID == taskinput.ID) { //maybe not work
        return true;
        }
        return false;


    }




}
