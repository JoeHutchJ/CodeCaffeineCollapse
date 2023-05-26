using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;
using System;

using Sirenix.OdinInspector;

//public  delegate void Vec2Delegate(Vector2 vec); //https://social.msdn.microsoft.com/Forums/en-US/0e4a2fc8-1db3-4093-8b83-83c598044917/syntax-help-calling-a-delegate-from-a-dictionary?forum=csharplanguage
//public  delegate void BaseEventDelegate();



//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity

public enum eventTypeenum {Vector2, Event, Boolean, String, Int, Task, Vector3, Conversation, Email, Transform};

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
    [ShowIf("@eventType == eventTypeenum.Event")]
    public UnityEvent response = new UnityEvent();
    [ShowIf("@eventType == eventTypeenum.Vector2 ")]
    public UnityEvent<Vector2> vector2response = new UnityEvent<Vector2>();
    [ShowIf("@eventType == eventTypeenum.Boolean ")]
    public UnityEvent<Boolean> boolresponse = new UnityEvent<Boolean>();
    [ShowIf("@eventType == eventTypeenum.String" )]
    public UnityEvent<String> stringresponse = new UnityEvent<String>();
    [ShowIf("@eventType == eventTypeenum.Int")]
    public UnityEvent<int> intresponse = new UnityEvent<int>();
    [ShowIf("@eventType == eventTypeenum.Task")]
    public UnityEvent<Task> taskresponse = new UnityEvent<Task>();
    [ShowIf("@eventType == eventTypeenum.Vector3 ")]
    public UnityEvent<Vector3> Vector3response=  new UnityEvent<Vector3>();
    [ShowIf("@eventType == eventTypeenum.Conversation ")]
    public UnityEvent<Conversation> Conversationresponse = new UnityEvent<Conversation>();
    [ShowIf("@eventType == eventTypeenum.Email")]
    public UnityEvent<Email> Emailresponse = new UnityEvent<Email>();
    [ShowIf("@eventType == eventTypeenum.Transform")]
    public UnityEvent<Transform> Transformresponse = new UnityEvent<Transform>();
    [EnumToggleButtons] //define the type of Event.
    
    
    //condittional field allows the inspector to hide all 'Response' sections that arent the correct type.
    


    bool enabled = false;

    private void OnEnable() {
        Do();
    }

    private void Update() {
    }
 
    private void  Do() //where the responses are registered to the Events.
    {   
        Destroy();
        if ( !enabled) {
            enabled = true;
        if (_AgentEvent != null) {
            _AgentEvent.assignType();
            eventType = _AgentEvent.getType();
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
        case eventTypeenum.Transform:
            AgentTransformEvent tempTransEvent = (AgentTransformEvent)_AgentEvent;
            tempTransEvent.Register(new Action<Transform, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Event:
            AgentEvent tempEvent = (AgentEvent)_AgentEvent;
            tempEvent.Register(new Action<GameObject>(EnactAgentEvent));
            break;
        

        }
            
        }


        if (_Event != null) {

            _Event.assignType();
            eventType = _Event.getType();
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
        case eventTypeenum.Transform:
            TransformEvent tempTransformEvent = (TransformEvent)_Event;
            tempTransformEvent.Register(new Action<Transform>(EnactEvent));
            break;
        case eventTypeenum.Event:
            Event tempEvent = (Event)_Event;
            tempEvent.Register(new Action(EnactEvent));
            break;
        

        }
        }
        }

    }

    public void Destroy() {
        if (_AgentEvent != null) {
            _AgentEvent.assignType();
            switch (_AgentEvent.Type) {
        case eventTypeenum.Vector2 :
            AgentVecEvent tempVecEvent = (AgentVecEvent)_AgentEvent;
            tempVecEvent.DeRegister(new Action<Vector2, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Boolean:
            
            AgentBoolEvent tempBoolEvent = (AgentBoolEvent)_AgentEvent;
            tempBoolEvent.Register(new Action<Boolean, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.String:
            AgentStringEvent tempStringEvent = (AgentStringEvent)_AgentEvent;
            tempStringEvent.DeRegister(new Action<String, GameObject>(EnactAgentEvent));
            break;    
        case eventTypeenum.Int:
            AgentIntEvent tempIntEvent = (AgentIntEvent)_AgentEvent;
            tempIntEvent.DeRegister(new Action<int, GameObject>(EnactAgentEvent));
            break;  
        case eventTypeenum.Task:
            AgentTaskEvent tempTaskEvent = (AgentTaskEvent)_AgentEvent;
            tempTaskEvent.DeRegister(new Action<Task, GameObject>(EnactAgentEvent));
            break;  
        case eventTypeenum.Vector3:
            AgentVector3Event tempVec3Event = (AgentVector3Event)_AgentEvent;
            tempVec3Event.DeRegister(new Action<Vector3, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Conversation:
            AgentConversationEvent tempConvEvent = (AgentConversationEvent)_AgentEvent;
            tempConvEvent.DeRegister(new Action<Conversation, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Email:
            AgentEmailEvent tempEmailEvent = (AgentEmailEvent)_AgentEvent;
            tempEmailEvent.DeRegister(new Action<Email, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Transform:
            AgentTransformEvent tempTransEvent = (AgentTransformEvent)_AgentEvent;
            tempTransEvent.Register(new Action<Transform, GameObject>(EnactAgentEvent));
            break;
        case eventTypeenum.Event:
            AgentEvent tempEvent = (AgentEvent)_AgentEvent;
            tempEvent.DeRegister(new Action<GameObject>(EnactAgentEvent));
            break;
        

        }
            
        }


        if (_Event != null) {

            _Event.assignType();
        switch (_Event.Type) {
        case eventTypeenum.Vector2 :
            VecEvent tempVecEvent = (VecEvent)_Event;
            tempVecEvent.DeRegister(new Action<Vector2>(EnactEvent));
            break;
        case eventTypeenum.Boolean:
            
            BoolEvent tempBoolEvent = (BoolEvent)_Event;
            tempBoolEvent.DeRegister(new Action<Boolean>(EnactEvent));
            break;
        case eventTypeenum.String:
            StringEvent tempStringEvent = (StringEvent)_Event;
            tempStringEvent.DeRegister(new Action<String>(EnactEvent));
            break;    
        case eventTypeenum.Int:
            IntEvent tempIntEvent = (IntEvent)_Event;
            tempIntEvent.DeRegister(new Action<int>(EnactEvent));
            break;  
        case eventTypeenum.Task:
            TaskEvent tempTaskEvent = (TaskEvent)_Event;
            tempTaskEvent.DeRegister(new Action<Task>(EnactEvent));
            break;  
        case eventTypeenum.Vector3:
            Vector3Event tempVec3Event = (Vector3Event)_Event;
            tempVec3Event.DeRegister(new Action<Vector3>(EnactEvent));
            break;
        case eventTypeenum.Conversation:
            ConversationEvent tempConvEvent = (ConversationEvent)_Event;
            tempConvEvent.DeRegister(new Action<Conversation>(EnactEvent));
            break;
        case eventTypeenum.Email:
            EmailEvent tempEmailEvent = (EmailEvent)_Event;
            tempEmailEvent.DeRegister(new Action<Email>(EnactEvent));
            break;
        case eventTypeenum.Transform:
            TransformEvent tempTransformEvent = (TransformEvent)_Event;
            tempTransformEvent.Register(new Action<Transform>(EnactEvent));
            break;
        case eventTypeenum.Event:
            Event tempEvent = (Event)_Event;
            tempEvent.DeRegister(new Action(EnactEvent));
            break;
        

        }
        }
        }

    
    

    void OnDisable()
    {
        
    }

    public void SetEvent(BaseEvent ev) {
        _Event = ev;
        isAgentEvent = isAgent.NONE;
        _Event.assignType();
        eventType = _Event.getType();
        OnEnable();

    }

    public void SetEvent(BaseAgentEvent ev, GameObject _Agent) {
        _AgentEvent = ev;
        Agent = _Agent;
        isAgentEvent = isAgent.AGENT;
        _AgentEvent.assignType();
        eventType = _AgentEvent.getType();
        Do();
        
    }

    public void Reset() {
        OnEnable();
    }

    public void Wipe() {
        _Event = null;
        _AgentEvent = null;
        Agent = null;
        WipeResponses();

    }
    

    public void WipeResponses() {
     response = null; 
     vector2response= null; 
     boolresponse= null; 
    stringresponse= null; 

    intresponse= null; 
    taskresponse= null; 
    Vector3response= null; 
    Conversationresponse= null; 
    Emailresponse= null; 
    

    }

    public void SetResponse(UnityAction res) {

            Do();
            
            UnityEventTools.AddPersistentListener(response, res);
            //response.AddListener(res);

        
    }


    public void SetBoolResponse(UnityAction<Boolean> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(boolresponse, res);
            //boolresponse.AddListener(res);

        
    }

    public void SetStringResponse(UnityAction<String> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(stringresponse, res);
            //response.AddListener(res);
        
    }

public void SetIntResponse(UnityAction<int> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(intresponse, res);
            //response.AddListener(res););
        
    }

public void SetVec2Response(UnityAction<Vector2> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(vector2response, res);
            //response.AddListener(res);
        
    }

public void SetVec3Response(UnityAction<Vector3> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(Vector3response, res);
            //response.AddListener(res);
        
    }

public void SetTaskResponse(UnityAction<Task> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(taskresponse, res);
            //response.AddListener(res);
        
    }

public void SetConvResponse(UnityAction<Conversation> res) {
           Do();
            
            UnityEventTools.AddPersistentListener(Conversationresponse, res);
            //response.AddListener(res);
        
    }

public void SetEmailResponse(UnityAction<Email> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(Emailresponse, res);
            //response.AddListener(res);
        
    }

public void SetTransformResponse(UnityAction<Transform> res) {
            Do();
            
            UnityEventTools.AddPersistentListener(Transformresponse, res);
            //response.AddListener(res);
        
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

    public void EnactEvent(Transform _bool) {
        Transformresponse.Invoke(_bool);
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
            Debug.Log("trigger ");
            response.Invoke();
        } 

    }

    public void EnactAgentEvent(Vector2 vec, GameObject agent) {
        if (Agent == agent) {
        vector2response.Invoke(vec);
        }
    }

    public void EnactAgentEvent(Boolean _bool, GameObject agent) {
        if (Agent == agent) {
        boolresponse.Invoke(_bool);
    }
    }

    public void EnactAgentEvent(Transform _bool, GameObject agent) {
        if (Agent == agent) {
        Transformresponse.Invoke(_bool);
    }
    }

    public void EnactAgentEvent(String str, GameObject agent) {
        if (Agent == agent) {
        stringresponse.Invoke(str);
        }
    }

    public void EnactAgentEvent(int Int, GameObject agent) {
        if (Agent == agent) {
        intresponse.Invoke(Int);
        }
    }

    public void EnactAgentEvent(Task task, GameObject agent) {
        if (Agent == agent) {
        taskresponse.Invoke(task);
        }
    }

    public void EnactAgentEvent(Vector3 vec, GameObject agent) {
        if (Agent == agent) {
        Vector3response.Invoke(vec);
        }
    }

    public void EnactAgentEvent(Conversation CONV, GameObject agent) {
        if (Agent == agent) {
        Conversationresponse.Invoke(CONV);
        }
    }

    public void EnactAgentEvent(Email CONV, GameObject agent) {
        if (Agent == agent) {
        Emailresponse.Invoke(CONV);
        }
    }


}
