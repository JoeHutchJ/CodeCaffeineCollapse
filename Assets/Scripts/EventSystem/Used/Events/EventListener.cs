using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

//public  delegate void Vec2Delegate(Vector2 vec); //https://social.msdn.microsoft.com/Forums/en-US/0e4a2fc8-1db3-4093-8b83-83c598044917/syntax-help-calling-a-delegate-from-a-dictionary?forum=csharplanguage
//public  delegate void BaseEventDelegate();



//using Inspiration:
//https://www.youtube.com/watch?v=raQ3iHhE_Kk&ab_channel=Unity

public enum eventTypeenum {Vector2, Event, Boolean, String, Int, Task, Vector3, Conversation, Email};




public class EventListener: MonoBehaviour //key class, which interprets all kinds of events and supports response to them.  
{
  
    
    //Dictionary<string,Delegate> delegateTypesDict = new Dictionary<string, Delegate>();

    

    [SerializeField]
    public BaseEvent _Event; //main

    //private VecEvent VecEvent;
    

    [Space(10)]
    [SerializeField]
    public eventTypeenum eventType = eventTypeenum.Event; //define the type of Event.
    
    [Space(10)]

    //condittional field allows the inspector to hide all 'Response' sections that arent the correct type.
    public UnityEvent response; 
    
    
    public UnityEvent<Vector2> vector2response;
    
    public UnityEvent<Boolean> boolresponse;

    public UnityEvent<String> stringresponse;

    public UnityEvent<int> intresponse;

    public UnityEvent<Task> taskresponse;

    public UnityEvent<Vector3> Vector3response;

    public UnityEvent<Conversation> Conversationresponse;

    public UnityEvent<Email> Emailresponse;
    




    
 
    private void  OnEnable() //where the responses are registered to the Events.
    {
        _Event.assignType();
        if (_Event != null) {
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


}
