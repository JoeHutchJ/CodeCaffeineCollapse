using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    List<Task>tasks;

    public bool randomActive; //random tasks can start being produced. 
    // Start is called before the first frame update
    void Start()
    {
        tasks = new List<Task>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
