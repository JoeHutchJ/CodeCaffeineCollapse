using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
[ExecuteAlways]
public class ObjectiveManager : MonoBehaviour
{
     ObjectiveBlock currentObjectiveBlock = null;

    Objective currentObjective = null;

    public Objective publicCurrentObjective;
    public List<ObjectiveBlock> ObjectiveBlocks;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.IsPlaying(gameObject)) {
            if (currentObjectiveBlock == null) {
                nextObjectiveBlock();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.IsPlaying(gameObject)) {
        publicCurrentObjective = currentObjective;
        } else {
            foreach (ObjectiveBlock block in ObjectiveBlocks) {
            foreach(Objective objective in block.objectives) {
                objective.setId();
            }
        }
        }
    }

    public void nextObjective() {
        
        if (currentObjective == null) {
            if (currentObjectiveBlock != null) {
                if (currentObjectiveBlock.objectives.Count > 0) {
                    Debug.Log("first objective");
                    SetupObjective(currentObjectiveBlock.objectives[0]);
                }
            }
        } else {
            int index  = ArrayUtility.IndexOf(currentObjectiveBlock.objectives.ToArray(), currentObjective);
            if (index + 1 < currentObjectiveBlock.objectives.Count) {
                Debug.Log("new Objective");
                WipeCurrentObjective();
                SetupObjective(currentObjectiveBlock.objectives[index + 1]);

            } else {
                Debug.Log("end of objective block");
                WipeCurrentObjective();
                currentObjectiveBlock = null;
            }
        }
    }

    public void SetupObjective(Objective obj) {
        //set currentObjective
        //set trigger Events to Event Listeners
        //WipeListeners();
        
        currentObjective = obj;
        SetTriggers(currentObjective.TriggerEvents);
        checkActivationConditions(obj);
        if (currentObjective == obj) {
            currentObjective.Activate();
        }

    }

    public void nextObjectiveBlock() {
        //find objective block next
        //if null, next in array
        //if end of array, nothing
        //set currentObjectiveBlock
        //setup first objective in currentObjectiveBlock
        if (currentObjectiveBlock == null) {
            Debug.Log(ObjectiveBlocks.Count);
            if (ObjectiveBlocks.Count > 0) {
                Debug.Log("set current objective block");
                currentObjectiveBlock = ObjectiveBlocks[0];
                nextObjective();
            }
        }
        
    }

    public void SetTriggers(List<EventInfo> triggerEvents) {
        if (triggerEvents.Count > 0) {
            for (int i = 0; i < triggerEvents.Count; i++) {
                SetListener(triggerEvents[i], newListener());
            }

        } else {
            if (currentObjective.delay > 0) {
                StartCoroutine(DelayToComplete(currentObjective.delay));
            }
        }
        


    }

    IEnumerator DelayToComplete(float delay) {
        yield return new WaitForSeconds(delay);
        Triggered(false);
    }

    public EventListener newListener() {
        return (EventListener)gameObject.AddComponent(typeof(EventListener));

    }

    public void SetListener(EventInfo trigger, EventListener listener) {
        //set event / agentEvent + agent
        //response per assignType. 
        if (trigger.isAgentEvent == isAgent.NONE) {
            listener.SetEvent(trigger.Event);
            
        } else {
            listener.SetEvent(trigger.AgentEvent, trigger.Agent);
        }
        switch (listener.eventType) {
            case eventTypeenum.Vector2 :
            listener.SetVec2Response(CheckTrigger);
            break;
        case eventTypeenum.Boolean:
            listener.SetBoolResponse(CheckBoolTrigger);
            break;
        case eventTypeenum.String:
            listener.SetStringResponse(CheckTrigger);
            break;    
        case eventTypeenum.Int:
            listener.SetIntResponse(CheckTrigger);
            break;  
        case eventTypeenum.Task:
            listener.SetTaskResponse(CheckTrigger);
            break;  
        case eventTypeenum.Vector3:
            listener.SetVec3Response(CheckTrigger);
            break;
        case eventTypeenum.Conversation:
            listener.SetConvResponse(CheckTrigger);
            break;  
        case eventTypeenum.Email:
            listener.SetEmailResponse(CheckTrigger);
            break;
        case eventTypeenum.Transform:
            listener.SetTransformResponse(CheckTrigger);
            break;
        case eventTypeenum.Event:
            listener.SetResponse(CheckTrigger);
            break;

        }
        
    }

    public void WipeListeners() {
        EventListener[] listeners = GetComponents<EventListener>();
        if (listeners.Length > 0) {
            for (int i = 0; i < listeners.Length; i++) {
                listeners[i].Destroy();
                Destroy(listeners[i]);
            }
        }

    }

    public void WipeCurrentObjective() {
        WipeListeners();
        currentObjective = null;
        
    }

    public (Objective, ObjectiveBlock) findObjectivebyId(int id) {
        if (ObjectiveBlocks.Count > 0) {
            foreach (ObjectiveBlock block in ObjectiveBlocks) {
                foreach(Objective objective in block.objectives) {
                    if (objective.id == id) {
                        return (objective, block);
                    }
                }
            }
        }
        return (null, null);

    }

    public void checkActivationConditions(Objective obj) {
        if (obj != null) {
            if (obj.ActivationConditions.Count > 0) {
                foreach (ConditionInfo info in obj.ActivationConditions) {
                    if (info.conditon.Check()) {
                        (Objective, ObjectiveBlock) tuple = findObjectivebyId(info.SkipObjectiveId);
                        if (tuple.Item1 != null) {
                            Debug.Log(tuple.Item1.id);
                            goToObjective(tuple.Item2, tuple.Item1);
                        }
                    }
                }
            }
        }

    }





    public void goToObjective(ObjectiveBlock block, Objective objective) {
        currentObjectiveBlock = block;
        WipeCurrentObjective();
        SetupObjective(objective);

    }

    public void Triggered(bool delay) {

        if (delay && currentObjective.delay > 0) {
                StartCoroutine(DelayToComplete(currentObjective.delay));
           
        } else {

        currentObjective.Triggered();

        if (currentObjective.nextObjective != -1) {
        nextObjective();
        } else {
            (Objective, ObjectiveBlock) tuple = findObjectivebyId(currentObjective.nextObjective);
                        if (tuple.Item1 != null) {

                            goToObjective(tuple.Item2, tuple.Item1);
                        } else {
                            nextObjective();
                        }
        }
        }

    }
    
    public void CheckTrigger() {
        if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check()) {
                Triggered(true);
            }
        }
        }

    }

                public void CheckTrigger(Vector2 val) {
                            if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                            }

    }

                    public void CheckTrigger(String val) {
                                if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                                }

    }

                    public void CheckBoolTrigger(Boolean val) {
                                if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                                }

    }

                    public void CheckTrigger(int val) {
                                if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                                }

    }

                    public void CheckTrigger(Vector3 val) {
                                if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                                }

    }

                        public void CheckTrigger(Conversation val) {
                                    if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            
            if (info.Check(val)) {
                Triggered(true);
            }

        }
                                    }

    }

                        public void CheckTrigger(Task val) {
                                    if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                                    }

    }

                        public void CheckTrigger(Email val) {
                                    if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                                    }

    }

                            public void CheckTrigger(Transform val) {
                                    if (currentObjective != null) {
        foreach (EventInfo info in currentObjective.TriggerEvents) {
            if (info.Check(val)) {
                Triggered(true);
            }
        }
                                    }

    }






}
