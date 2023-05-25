using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectiveManager : MonoBehaviour
{
    public ObjectiveBlock currentObjectiveBlock;

    public Objective currentObjective;
    public List<ObjectiveBlock> ObjectiveBlocks;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextObjective() {


    }

    public void SetupObjective() {
        //set currentObjective
        //set trigger Events to Event Listeners


    }

    public void nextObjectiveBlock() {
        //find objective block next
        //if null, next in array
        //if end of array, nothing
        //set currentObjectiveBlock
        //setup first objective in currentObjectiveBlock

    }

    public void SetTriggers(List<EventInfo> triggerEvents) {
        if (triggerEvents.Count > 1) {
            //new Event listener

        }


    }

    public void SetListener(EventInfo trigger, EventListener listener) {
        //set event / agentEvent + agent
        //response per assignType. 

    }


}
