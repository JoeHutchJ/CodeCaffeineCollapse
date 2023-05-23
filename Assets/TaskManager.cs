using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class TaskManager : MonoBehaviour
{

    public List<Task>tasks;

    public List<TaskContainer>instantiatedTasks;

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

    public TaskEvent receiptEvent;

    public bool seeAll = false;

    void Start()
    {
        tasks = new List<Task>();
        instantiatedTasks = new List<TaskContainer>();

        taskContent = GetChildByName.Get(this.gameObject, "Vertical").transform;
        taskUIheight = taskUI.GetComponent<TaskContainer>().height;
        taskUItimeHeight = taskUITime.GetComponent<TaskContainer>().height;
        taskContentHeight = taskContent.GetComponent<RectTransform>().sizeDelta.y;
        clearAlltasks();

        
    }

    // Update is called once per frame
    void Update()
    {
        updateTasks(); //maybe delay


        if (Input.GetKeyUp("t")) {
            seeAll = !seeAll;
            UpdateAllTasks();
        }

        updateQuotaBar();
    }

    public void clearAlltasks() {
        UsefulFunctions.deleteAllchildren(taskContent);
        taskContent.DetachChildren();
        instantiatedTasks = new List<TaskContainer>();
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

    public int getNonExpiredTasks() {
        int count = 0;
        foreach (Task task in tasks) {
            if (!task.expired) {
                count++;
            }
        }
        return count;
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
    
        List<Task>toDelete = new List<Task>();
        foreach (Task task in tasks) {
            task.Update();
            if (task.expired) {
                toDelete.Add(task);
            }
        }
        foreach(Task task in toDelete.ToArray()) {
            tasks.Remove(task);
            UpdateAllTasks();
        }
        foreach (TaskContainer task in instantiatedTasks) {
            if (task != null) {
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

    }

    public IEnumerator DestroyAfterDelay(Transform trans, int Delay) {
        yield return new WaitForSeconds(Delay);

        DestroyTask(trans);
    }

    void DestroyTask(Transform trans) {
        /*if (trans != null) {
        instantiatedTasks.Remove(trans.GetComponent<TaskContainer>());
        tasks.Remove(trans.GetComponent<TaskContainer>().task);
        trans.parent = null;
        Destroy(trans.gameObject);
        } else {
            Debug.Log("what");
        }*/
        clearAlltasks();
        UpdateAllTasks();
        StopAllCoroutines();
    }

    public void UpdateAllTasks() {
        clearAlltasks();

        foreach(Task task in tasks) {
            addTasktoUI(task, seeAll);
        }
    }

    public void AddTask(Task task) {
        //check if task already in list and update
        task.ResetTime();
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

        if (!task.active) {
            receiptEvent.Raise(task);
        }

        

        //if not active, then send to receipt printer. 


        addTasktoUI(task, seeAll);

        
    }

    public void completeTask(Task task) {
        if (task.complete) {
            Debug.Log(task.getPoints());
            Global.AddPoints(task.getPoints())
    ;
        }
        
    }


    public void addTasktoUI(Task task, bool seeAll) {    

        if (task.active && !task.expired) {
            if (getInstantiatedTaskById(task.ID) == null) {
                if (!seeAll) {
        if (task.timeTicking) {
           // Debug.Log(getTaskUISize() + " " + taskUItimeHeight + " " + taskContentHeight);
            if (getTaskUISize() + taskUItimeHeight + XMoretasksHeight   < taskContentHeight) {
                GameObject obj = Instantiate(taskUITime, taskContent);
                obj.GetComponent<TaskContainer>().Set(task);
                obj.transform.Find("TaskPrompt").GetComponent<TMP_Text>().text = task.prompt;
                obj.transform.Find("QuotaPoints").GetComponent<TMP_Text>().text = "+ " + task.points.ToString() + " Quota Points";
                instantiatedTasks.Add(obj.GetComponent<TaskContainer>());
            } else {
                if (!XMoretasksActive) {
                    XMoretasksActive = true;
                GameObject obj = Instantiate(XMoreTasks, taskContent);
                int num = getNonExpiredTasks() - getNumberofTasks();
                string str = "";
                if (num > 1) {
                 str = getNonExpiredTasks() - getNumberofTasks() + " More tasks";
                } else {
                     str = getNonExpiredTasks() - getNumberofTasks() + " More task";
                }
                obj.transform.Find("NumberofTasks").GetComponent<TMP_Text>().text = str;
                } else {
                    int num = getNonExpiredTasks() - getNumberofTasks();
                string str = "";
                if (num > 1) {
                 str = getNonExpiredTasks() - getNumberofTasks() + " More tasks";
                } else {
                     str = getNonExpiredTasks() - getNumberofTasks() + " More task";
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
            } else {
                 if (task.timeTicking) {
GameObject obj = Instantiate(taskUITime, taskContent);
                obj.GetComponent<TaskContainer>().Set(task);
                obj.transform.Find("TaskPrompt").GetComponent<TMP_Text>().text = task.prompt;
                obj.transform.Find("QuotaPoints").GetComponent<TMP_Text>().text = "+ " + task.points.ToString() + " Quota Points";
                instantiatedTasks.Add(obj.GetComponent<TaskContainer>());
                 } else {
                    GameObject obj = Instantiate(taskUI, taskContent);
                obj.GetComponent<TaskContainer>().Set(task);
                obj.transform.Find("TaskPrompt").GetComponent<TMP_Text>().text = task.prompt;
                obj.transform.Find("QuotaPoints").GetComponent<TMP_Text>().text = "+ " + task.points.ToString()  + " Quota Points";
                instantiatedTasks.Add(obj.GetComponent<TaskContainer>());


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

    public void updateQuotaBar() {
        Image fill = GetChildByName.Get(gameObject,"QuotaBarFill").GetComponent<Image>();
        fill.fillAmount = Global.QuotaPercent();
        Debug.Log(fill.fillAmount + " " + Global.QuotaPercent());

    }
}
