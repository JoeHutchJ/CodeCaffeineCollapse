using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiptPrinterController : MonoBehaviour
{

    //public TaskEvent receiptEvent;

    public TaskEvent taskInfoEvent;

    public TaskEvent completeEvent;

    public List<Task> taskQueue;

    public Task currentTask;

    public Task interactableTask;

    public Animation anim;

    public AnimationClip def;

    public AnimationClip stopped;

    public AudioManager audioManager;

    bool animActive;
    // Start is called before the first frame update
    void Start()
    {
        taskQueue = new List<Task>();
        anim = GetComponent<Animation>();
        audioManager = GetComponent<AudioManager>();

        AddTask(newTask(TaskType.REVIEW, 0.5f, 0, true));

        AddTask(newTask(TaskType.REPORT, 0.5f, 0, true));

        AddTask(newTask(TaskType.CODING, 0.5f, 10, true));

        AddTask(newTask(TaskType.REVIEW, 0.5f, 20, true));

        AddTask(newTask(TaskType.REPORT, 0.5f, 0, true));

        AddTask(newTask(TaskType.COFFEE, 0.5f, 50, true));

        AddTask(newTask(TaskType.AUTHENTICATE, 0.5f, 30, true));
    }

    // Update is called once per frame
    void Update()
    {
        if (animActive) {
            if (!anim.isPlaying) {
                sendTask(currentTask);
                animActive = false;
                
            }
        } else {
            if (interactableTask == null) {
                if (taskQueue.Count > 0) {
                nextTask();
                }
            }
        }
    }

    public void AddTask(Task task) {
        task.active = true;
        taskQueue.Add(task);

    }

    void nextTask() {
        if (taskQueue.Count > 0) {
        Task task = taskQueue[0];
        taskQueue.Remove(taskQueue[0]);
        currentTask = task;
        animActive = true;
        anim.clip = def;
        anim.Play();
        audioManager.Play();
        }

    }

    void sendTask(Task task) {
        //taskInfoEvent.Raise(task);
        interactableTask = task;

    }

    public void Interact() {
        if (interactableTask != null) {
        taskInfoEvent.Raise(interactableTask);
        interactableTask = null;
        }
        anim.clip = stopped;
        //anim.playAutomatically = true;
        anim.Play();
        anim.Stop();
    }

     public Task newTask(TaskType type, float difficulty, int timeLimit, bool active) {

        return new Task(type, difficulty, "", "", timeLimit, completeEvent, active);

     }
}
