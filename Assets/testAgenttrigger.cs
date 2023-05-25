using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAgenttrigger : MonoBehaviour
{
    public AgentEvent Event;
    // Start is called before the first frame update
    void Start()
    {
        Event.Raise(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
