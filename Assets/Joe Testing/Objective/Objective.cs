using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System;

public static class ObjectiveId {

    public static int Id = 0;

    public static int getId() {
        Debug.Log(Id);
        Id++;
        return Id;

    }
}

[Serializable]
[ExecuteInEditMode]
public class Objective 
{
    [Header("Objective")]
    public int id = 0;
    public string name;

    public bool skip;

    [SerializeField] bool completed;

    public List<EventInfo> TriggerEvents;

    public float delay;

    public List<ConditionInfo> ActivationConditions;

    public List<EventInfo> ActiviationEvents;

    public List<EventInfo> CompletionEvents;

    public List<Condition> CompletionFlags;
    

    public int nextObjective = -1;

    public void setId() {
        if (id == 0) {
        Debug.Log("Objective");
        id = ObjectiveId.getId();
        }

    }
    Objective() {
        //id = ObjectiveId.getId();
    }

    public void Activate() {
        foreach(EventInfo info in ActiviationEvents) {
            info.Raise();
        }

    }
    
    public void Triggered() {
        Debug.Log("Triggered");
        foreach(EventInfo info in CompletionEvents) {
            info.Raise();
        }
        foreach (Condition condition in CompletionFlags) {
            condition.Set();
        }

    }



}
