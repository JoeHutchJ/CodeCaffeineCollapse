using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[Serializable]
public class Objective 
{
    public string name;

    public bool skip;

    [SerializeField] bool completed;

    public List<EventInfo> TriggerEvents;

    public List<EventInfo> ActiviationEvents;

    public List<EventInfo> CompletionEvents;




}
