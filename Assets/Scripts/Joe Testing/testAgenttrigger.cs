using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class testAgenttrigger : MonoBehaviour
{
    public AgentEvent Event;

    public BoolEvent boolEvent;
    public List<ObjectiveBlock> info;


    // Start is called before the first frame update
    void Start()
    {
        
        if (Application.isEditor) {
        foreach (ObjectiveBlock block in info) {
            foreach(Objective objective in block.objectives) {
                objective.setId();
            }
        }
        } else {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
            
            if (Input.GetKeyUp("d")) {
                Event.Agent = gameObject;
                Event.Raise();
            }

                        if (Input.GetKeyUp("e")) {
                boolEvent.Raise(false);
            }




        }
    

    public void Testt(int val) {
        Debug.Log("Completion: " + val);
    }

}
