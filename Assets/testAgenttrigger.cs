using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class testAgenttrigger : MonoBehaviour
{
    public AgentEvent Event;
    public List<ObjectiveBlock> info;
    // Start is called before the first frame update
    void Start()
    {
        Event.Agent = gameObject;
        Event.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
