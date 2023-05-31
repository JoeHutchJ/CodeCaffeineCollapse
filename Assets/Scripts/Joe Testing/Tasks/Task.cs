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

    public static int getPoint(float difficulty) {
        return (int)UsefulFunctions.Remap(difficulty, 0, 1, 10, 100);
    }

}


[Serializable]

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

    public int points;

    public bool expired;

    public bool active; //is displayed / able to completed by user...

    public TaskEvent CompleteEvent; 

    public Task(TaskType type, float _difficulty, string _name, string _prompt, float _timeLimit, TaskEvent _event, bool _active) {
        taskType = type;
        difficulty = _difficulty;
        name = _name;
        prompt = _prompt;
        timeLimit = _timeLimit;
        active = _active;
        CompleteEvent = _event;
        TaskInfo.currentId++;
        ID = TaskInfo.currentId;

        if (name == null || name == "") {
            name = type.ToString();
        }

        if (prompt == null || prompt == "") {
            prompt = TaskInfo.promptDict[taskType];
        }

        if (timeLimit > 0) {
            timeTicking = true;
        } 
        
        points = TaskInfo.getPoint(difficulty);

        timeStarted = Global.currentTime;


        




    }

    public void Create() {


        if (name == null || name == "") {
            name = taskType.ToString();
        }

        if (prompt == null || prompt == "") {
            prompt = TaskInfo.promptDict[taskType];
        }

        if (timeLimit > 0) {
            timeTicking = true;
        } 

        timeStarted = Global.currentTime;

        if (points == 0) {
            if (difficulty != null) {
             points = TaskInfo.getPoint(difficulty);
            } else {
                difficulty = 0.5f;
                points = TaskInfo.getPoint(difficulty);
            }
        }




    }

    public void Update() {
        //timeElapsed +=  Time.deltaTime;
        if (timeTicking && !complete) {
        if (Global.currentTime - timeStarted >= timeLimit ) {
            expired = true;

        }
        }
    }

    public void Complete(float percent) {
        Debug.Log("complete");
        complete = true;
        completePercent = percent;

        CompleteEvent.Raise(this);
    }

    public float GetTimeElapsed() {
        return 1.0f - ((Global.currentTime - timeStarted) / timeLimit);
    }

    public void ResetTime() {
       timeStarted = Global.currentTime;
    }

    public int getPoints() {

        return (int)(completePercent * points);
    }


}
