using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskContainer : MonoBehaviour
{

    public Task task;
    public bool timeTask;

    public int height;

    public Color expiredColour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set(Task _task) {
        task = _task;
    }

    public void SetTimeBar(float val) {
        GetChildByName.Get(transform.gameObject, "TimeMask").GetComponent<Image>().fillAmount = val;

    }

    public void setExpired() {
        GetComponent<Image>().color = expiredColour;

    }
}
