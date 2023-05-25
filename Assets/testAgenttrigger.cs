using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
public class testAgenttrigger : MonoBehaviour
{
    public AgentEvent Event;
    public List<ObjectiveBlock> info;


    // Start is called before the first frame update
    void Start()
    {
        //Event.Agent = gameObject;
        //Event.Raise();
        Debug.Log("editor");
        if (Application.isEditor) {
        foreach (ObjectiveBlock block in info) {
            foreach(Objective objective in block.objectives) {
                objective.setId();
            }
        }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor) {
        foreach (ObjectiveBlock block in info) {
            foreach(Objective objective in block.objectives) {
                objective.setId();
            }
        }
        }
    }
}
