using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

using Sirenix.OdinInspector;

//public  delegate void Vec2Delegate(Vector2 vec); //https://social.msdn.microsoft.com/Forums/en-US/0e4a2fc8-1db3-4093-8b83-83c598044917/syntax-help-calling-a-delegate-from-a-dictionary?forum=csharplanguage
//public  delegate void BaseEventDelegate();



//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity

public enum eventTypeenum {Vector2, Event, Boolean, String, Int, Task, Vector3, Conversation, Email};

public enum isAgent {NONE, AGENT};


public class EventListener: MonoBehaviour //key class, which interprets all kinds of events and supports response to them.  
{
  
    
    //Dictionary<string,Delegate> delegateTypesDict = new Dictionary<string, Delegate>();

    

    [SerializeField]
    [EnumToggleButtons]
    public isAgent isAgentEvent = isAgent.NONE;

    [ShowIf("isAgentEvent", isAgent.NONE)]
    public BaseEvent _Event; //main
    [HideIf("isAgentEvent", isAgent.NONE)]
    public BaseAgentEvent _AgentEvent;
    [HideIf("isAgentEvent", isAgent.NONE)]
    public GameObject Agent;

    //private VecEvent VecEvent;
    

    [Space(10)]
    [SerializeField]

    public eventTypeenum eventType = eventTypeenum.Event;
    [ShowIf("isAgentEvent", isAgent.NONE)]
    [EnumToggleButtons]
     //define the type of Event.
    
    
    //condittional field allows the inspector to hide all 'Response' sections that arent the correct type.
    [ShowIf("@eventType == eventTypeenum.Event && isAgentEvent == isAgent.NONE")]
    public UnityEvent response; 
    [ShowIf("@eventType == eventTypeenum.Vector2 && isAgentEvent == isAgent.NONE")]
    public UnityEvent<Vector2> vector2response;
    [ShowIf("@eventType == eventTypeenum.Boolean && isAgentEvent == isAgent.NONE")]
    public UnityEvent<Boolean> boolresponse;
    [ShowIf("@eventType == eventTypeenum.String && isAgentEvent == isAgent.NONE")]
    public UnityEvent<String> stringresponse;
    [ShowIf("@eventType == eventTypeenum.Int && isAgentEvent == isAgent.NONE")]
    public UnityEvent<int> intresponse;
    [ShowIf("@eventType == eventTypeenum.Task && isAgentEvent == isAgent.NONE")]
    public UnityEvent<Task> taskresponse;
    [ShowIf("@eventType == eventTypeenum.Vector3 && isAgentEvent == isAgent.NONE")]
    public UnityEvent<Vector3> Vector3response;
    [ShowIf("@eventType == eventTypeenum.Conversation && isAgentEvent == isAgent.NONE")]
    public UnityEvent<Conversation> Conversationresponse;
    [ShowIf("@eventType == eventTypeenum.Email && isAgentEvent == isAgent.NONE")]
    public UnityEvent<Email> Emailresponse;
    [EnumToggleButtons] //define the type of Event.
    
    
    //condittional field allows the inspector to hide all 'Response' sections that arent the correct type.
    
    [ShowIf("@eventType == eventTypeenum.Event && isAgentEvent == isAgent.AGENT")]
    public UnityEvent agentresponse; 
    [ShowIf("@eventType == eventTypeenum.Vector2 && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<Vector2> agentvector2response;
    [ShowIf("@eventType == eventTypeenum.Boolean && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<Boolean> agentboolresponse;
    [ShowIf("@eventType == eventTypeenum.String && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<String> agentstringresponse;
    [ShowIf("@eventType == eventTypeenum.Int && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<int> agentintresponse;
    [ShowIf("@eventType == eventTypeenum.Task && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<Task> agenttaskresponse;
    [ShowIf("@eventType == eventTypeenum.Vector3 && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<Vector3> agentVector3response;
    [ShowIf("@eventType == eventTypeenum.Conversation && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<Conversation> agentConversationresponse;
    [ShowIf("@eventType == eventTypeenum.Email && isAgentEvent == isAgent.AGENT")]
    public UnityEvent<Email> agentEmailresponse;
    




    
 
    private void  OnEnable() //where the responses are registered to the Events.
    {
        

        if (_AgentEvent != null) {
            _AgentEvent.assignType();
            switch (_AgentEvent.Type) {
        case eventTypeenum.Vector2 :
            AgentVecEvent tempVecEvent = (AgentVecEvent)_AgentEvent;
            tempVecEvent.Register(new Action<Vector2, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Boolean:
            
            AgentBoolEvent tempBoolEvent = (AgentBoolEvent)_AgentEvent;
            tempBoolEvent.Register(new Action<Boolean, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.String:
            AgentStringEvent tempStringEvent = (AgentStringEvent)_AgentEvent;
            tempStringEvent.Register(new Action<String, GameObject>(EnactAgentEvent));
            break;    
        case eventTypeenum.Int:
            AgentIntEvent tempIntEvent = (AgentIntEvent)_AgentEvent;
            tempIntEvent.Register(new Action<int, GameObject>(EnactAgentEvent));
            break;  
        case eventTypeenum.Task:
            AgentTaskEvent tempTaskEvent = (AgentTaskEvent)_AgentEvent;
            tempTaskEvent.Register(new Action<Task, GameObject>(EnactAgentEvent));
            break;  
        case eventTypeenum.Vector3:
            AgentVector3Event tempVec3Event = (AgentVector3Event)_AgentEvent;
            tempVec3Event.Register(new Action<Vector3, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Conversation:
            AgentConversationEvent tempConvEvent = (AgentConversationEvent)_AgentEvent;
            tempConvEvent.Register(new Action<Conversation, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Email:
            AgentEmailEvent tempEmailEvent = (AgentEmailEvent)_AgentEvent;
            tempEmailEvent.Register(new Action<Email, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Event:
            AgentEvent tempEvent = (AgentEvent)_AgentEvent;
            tempEvent.Register(new Action<GameObject>(EnactAgentEvent));
            break;
        

        }
            
        }


        if (_Event != null) {

            _Event.assignType();
        switch (_Event.Type) {
        case eventTypeenum.Vector2 :
            VecEvent tempVecEvent = (VecEvent)_Event;
            tempVecEvent.Register(new Action<Vector2>(EnactEvent));
            break;
        case eventTypeenum.Boolean:
            
            BoolEvent tempBoolEvent = (BoolEvent)_Event;
            tempBoolEvent.Register(new Action<Boolean>(EnactEvent));
            break;
        case eventTypeenum.String:
            StringEvent tempStringEvent = (StringEvent)_Event;
            tempStringEvent.Register(new Action<String>(EnactEvent));
            break;    
        case eventTypeenum.Int:
            IntEvent tempIntEvent = (IntEvent)_Event;
            tempIntEvent.Register(new Action<int>(EnactEvent));
            break;  
        case eventTypeenum.Task:
            TaskEvent tempTaskEvent = (TaskEvent)_Event;
            tempTaskEvent.Register(new Action<Task>(EnactEvent));
            break;  
        case eventTypeenum.Vector3:
            Vector3Event tempVec3Event = (Vector3Event)_Event;
            tempVec3Event.Register(new Action<Vector3>(EnactEvent));
            break;
        case eventTypeenum.Conversation:
            ConversationEvent tempConvEvent = (ConversationEvent)_Event;
            tempConvEvent.Register(new Action<Conversation>(EnactEvent));
            break;
        case eventTypeenum.Email:
            EmailEvent tempEmailEvent = (EmailEvent)_Event;
            tempEmailEvent.Register(new Action<Email>(EnactEvent));
            break;
        case eventTypeenum.Event:
            Event tempEvent = (Event)_Event;
            tempEvent.Register(new Action(EnactEvent));
            break;
        

        }
        }

    }

    void OnDisable()
    {
        
    }
    
    //when Raise() is called, these function are called. 

    public void EnactEvent() {
        response.Invoke();

    }

    public void EnactEvent(Vector2 vec) {
        vector2response.Invoke(vec);
    }

    public void EnactEvent(Boolean _bool) {
        boolresponse.Invoke(_bool);
    }

    public void EnactEvent(String str) {
        stringresponse.Invoke(str);
    }

    public void EnactEvent(int Int) {
        intresponse.Invoke(Int);
    }

    public void EnactEvent(Task task) {
        taskresponse.Invoke(task);
    }

    public void EnactEvent(Vector3 vec) {
        Vector3response.Invoke(vec);
    }

    public void EnactEvent(Conversation CONV) {
        Conversationresponse.Invoke(CONV);
    }

    public void EnactEvent(Email CONV) {
        Emailresponse.Invoke(CONV);
    }



    public void EnactAgentEvent(GameObject _agent) {
        if (Agent == _agent) {
            Debug.Log("correct agent");
        agentresponse.Invoke();
        } else {
            Debug.Log("wrong agent");
        }

    }

    public void EnactAgentEvent(Vector2 vec, GameObject agent) {
        vector2response.Invoke(vec);
    }

    public void EnactAgentEvent(Boolean _bool, GameObject agent) {
        boolresponse.Invoke(_bool);
    }

    public void EnactAgentEvent(String str, GameObject agent) {
        stringresponse.Invoke(str);
    }

    public void EnactAgentEvent(int Int, GameObject agent) {
        intresponse.Invoke(Int);
    }

    public void EnactAgentEvent(Task task, GameObject agent) {
        taskresponse.Invoke(task);
    }

    public void EnactAgentEvent(Vector3 vec, GameObject agent) {
        Vector3response.Invoke(vec);
    }

    public void EnactAgentEvent(Conversation CONV, GameObject agent) {
        Conversationresponse.Invoke(CONV);
    }

    public void EnactAgentEvent(Email CONV, GameObject agent) {
        Emailresponse.Invoke(CONV);
    }


}
