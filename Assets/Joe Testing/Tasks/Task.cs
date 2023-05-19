using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TaskInfo {

    public static int currentId = 0;

    public static Dictionary<TaskType, string> promptDict = new Dictionary<TaskType, string>() {
        {TaskType.COFFEE, "Make a cup of Coffee"},
        {TaskType.AUTHENTICATE, "Authenticate yourself at the Computer"},
        {TaskType.CODING, "Write some code"},
        {TaskType.REVIEW, "Review and Bug-fix some code"},
        {TaskType.REPORT, "Write a documentation report"},
        {TaskType.BUILD, "Make a build and publish"}

    };

}



public class Task
{
    public int ID;
    public TaskType taskType;
    public float difficulty;

    public string name;

    public string prompt;

    public bool timeTicking; 

    public float timeStarted;

    public float timeLimit;

    public float timeElapsed;

    public bool complete; 

    public float completePercent;

    public bool expired;

    public bool active; //is displayed / able to completed by user...

    public TaskEvent Event; 

    public Task(TaskType type, float _difficulty, string _name, string _prompt, float _timeLimit, TaskEvent _event) {
        taskType = type;
        difficulty = _difficulty;
        name = _name;
        prompt = _prompt;
        timeLimit = _timeLimit;

        Event = _event;
        TaskInfo.currentId++;
        ID = TaskInfo.currentId;

        if (name == null || name == "") {
            name = type.ToString();
            Debug.Log(name);
        }

        if (prompt == null || prompt == "") {
            prompt = TaskInfo.promptDict[taskType];
            Debug.Log(prompt);
        }

        if (timeLimit > 0) {
            timeTicking = false;
        }

        timeStarted = Global.currentTime;

        




    }

    public void Update() {
        //timeElapsed +=  Time.deltaTime;
        if (timeTicking) {
        if (Global.currentTime - timeStarted >= timeLimit ) {
            expired = true;

        }
        }
    }

    public void Complete(float percent) {
        complete = true;
        completePercent = percent;

        Event.Raise(this);
    }


}
