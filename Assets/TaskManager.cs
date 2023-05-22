using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class TaskManager : MonoBehaviour
{

    List<Task>tasks;

    List<TaskContainer>instantiatedTasks;

    public bool randomActive; //random tasks can start being produced. 

    public GameObject taskUI;

    float taskUIheight;

    public GameObject taskUITime;

    float taskUItimeHeight;

    float taskContentHeight;

    public GameObject XMoreTasks;

    float XMoretasksHeight = 50;

    bool XMoretasksActive = false;

    public Transform taskContent;

    public TaskEvent taskEvent;

    void Start()
    {
        tasks = new List<Task>();
        instantiatedTasks = new List<TaskContainer>();

        taskContent = GetChildByName.Get(this.gameObject, "Vertical").transform;
        taskUIheight = taskUI.GetComponent<TaskContainer>().height;
        taskUItimeHeight = taskUITime.GetComponent<TaskContainer>().height;
        taskContentHeight = taskContent.GetComponent<RectTransform>().sizeDelta.y;
        clearAlltasks();

        addTask(newTask(TaskType.CODING, 0.5f, 10, true));

        addTask(newTask(TaskType.REVIEW, 0.5f, 20, true));

        addTask(newTask(TaskType.REPORT, 0.5f, 0, true));

        addTask(newTask(TaskType.REPORT, 0.5f, 1, true));

        addTask(newTask(TaskType.REPORT, 0.5f, 1, true));

        
    }

    // Update is called once per frame
    void Update()
    {
        updateTasks(); //maybe delay
    }

    public void clearAlltasks() {
        UsefulFunctions.deleteAllchildren(taskContent);
        taskContent.DetachChildren();

        XMoretasksActive = false;

    }

    public float getTaskUISize() {

        float height = 0;
        //Debug.Log(taskContent.childCount);
        foreach (Transform child in taskContent) {
            if (child.GetComponent<TaskContainer>() != null) {
                height += child.GetComponent<TaskContainer>().height;
        }
        }

        return height;

    }

    public int getNumberofTasks() {
        int count = 0;
        foreach (Transform child in taskContent) {
            if (child.GetComponent<TaskContainer>() != null) {
                count++;
            }
        }
        //Debug.Log("no " + count);
        return count;

    }

    public TaskContainer getInstantiatedTaskById(int id) {
        foreach (TaskContainer task in instantiatedTasks ) {

            if (task.task.ID == id) {
                return task;
            }
        }
        return null;
    }

    public void updateTasks() {
        foreach (Task task in tasks) {
            task.Update();
        }

        foreach (TaskContainer task in instantiatedTasks) {
            task.task.Update();
            if (task.task.timeTicking) {
                if (task.task.expired) {
                    task.setExpired();
                    StartCoroutine(DestroyAfterDelay(task.transform, 3));
                } else {
                    task.SetTimeBar(task.task.GetTimeElapsed());
                }
            }

        }

    }

    public IEnumerator DestroyAfterDelay(Transform trans, int Delay) {
        yield return new WaitForSeconds(Delay);

        instantiatedTasks.Remove(trans.GetComponent<TaskContainer>());
        trans.parent = null;
        Destroy(trans.gameObject);
        
        StopAllCoroutines();
    }



    public void addTask(Task task) {
        Task toChange = null;
        foreach (Task _task in tasks) {
            if (_task.ID == task.ID) {
                toChange = _task;
            }
        }
        if (toChange == null) {
        tasks.Add(task);
        } else {
        toChange = task;
        }

        if (task.active) {
            if (getInstantiatedTaskById(task.ID) == null) {
        if (task.timeTicking) {
           // Debug.Log(getTaskUISize() + " " + taskUItimeHeight + " " + taskContentHeight);
            if (getTaskUISize() + taskUItimeHeight   < taskContentHeight) {
                GameObject obj = Instantiate(taskUITime, taskContent);
                obj.GetComponent<TaskContainer>().Set(task);
                obj.transform.Find("TaskPrompt").GetComponent<TMP_Text>().text = task.prompt;
                obj.transform.Find("QuotaPoints").GetComponent<TMP_Text>().text = "+ " + task.points.ToString() + " Quota Points";
                instantiatedTasks.Add(obj.GetComponent<TaskContainer>());
            } else {
                if (!XMoretasksActive) {
                    XMoretasksActive = true;
                GameObject obj = Instantiate(XMoreTasks, taskContent);
                int num = tasks.Count - getNumberofTasks();
                string str = "";
                if (num > 1) {
                 str = tasks.Count - getNumberofTasks() + " More tasks";
                } else {
                     str = tasks.Count - getNumberofTasks() + " More task";
                }
                obj.transform.Find("NumberofTasks").GetComponent<TMP_Text>().text = str;
                } else {
                    int num = tasks.Count - getNumberofTasks();
                    Debug.Log(num);
                string str = "";
                if (num > 1) {
                 str = tasks.Count - getNumberofTasks() + " More tasks";
                } else {
                     str = tasks.Count - getNumberofTasks() + " More task";
                }
                    GetChildByName.Get(gameObject,"NumberofTasks").GetComponent<TMP_Text>().text = str;
                }
            }
        } else {
            if (getTaskUISize() + taskUIheight + XMoretasksHeight  < taskContentHeight) {
                GameObject obj = Instantiate(taskUI, taskContent);
                obj.GetComponent<TaskContainer>().Set(task);
                obj.transform.Find("TaskPrompt").GetComponent<TMP_Text>().text = task.prompt;
                obj.transform.Find("QuotaPoints").GetComponent<TMP_Text>().text = "+ " + task.points.ToString()  + " Quota Points";
                instantiatedTasks.Add(obj.GetComponent<TaskContainer>());
            } else {
                if (!XMoretasksActive) {
                    XMoretasksActive = true;
                GameObject obj = Instantiate(XMoreTasks, taskContent);
                int num = tasks.Count - getNumberofTasks();
                string str = "";
                if (num > 1) {
                 str = "+ " + (tasks.Count - getNumberofTasks()).ToString() + " More tasks";
                } else {
                     str = "+ " + (tasks.Count - getNumberofTasks()).ToString()  + " More task";
                }
                obj.transform.Find("NumberofTasks").GetComponent<TMP_Text>().text = str;
                } else {
                    int num = tasks.Count - getNumberofTasks();
                string str = "";
                if (num > 1) {
                 str = tasks.Count - getNumberofTasks() + " More tasks";
                } else {
                     str = tasks.Count - getNumberofTasks() + " More task";
                }
                    GetChildByName.Get(gameObject,"NumberofTasks").GetComponent<TMP_Text>().text = str;
                
                }
            }
        }
            }

        }

    }


    public Task newTask(TaskType type, float difficulty, int timeLimit, bool active) {

        return new Task(type, difficulty, "", "", timeLimit, taskEvent, active);

        /*switch (type) {
            case TaskType.CODING:
                return new Task(type, difficulty, "", "", timeLimit, taskEvent );
                break;
            case TaskType.REVIEW:
                return new Task(type, difficulty, "", "", timeLimit, taskEvent );
                break;
            case TaskType.REPORT:
                return new Task(type, difficulty, "", "", timeLimit, taskEvent );
                break;
            case TaskType.COFFEE:

                break;
            case TaskType.EMAIL:

                break;
            case TaskType.AUTHENTICATE:

                break;



        }*/
    }
}
