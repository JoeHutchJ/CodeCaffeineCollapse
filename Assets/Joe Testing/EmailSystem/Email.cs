using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Email", menuName = "ScriptableObjects/Email")] //custom inspector

public class Email
{
    public int id;
    public Author Author;
    public string Subject;

    public string Message;
    
    public bool reply;

    public string replyMessage;

    public bool read;

    public bool spam; //ie not important / related to a task.
    // Start is called before the first frame update
    public TaskType taskType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
