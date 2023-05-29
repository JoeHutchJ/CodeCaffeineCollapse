using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class printerController : MonoBehaviour
{
    // Start is called before the first frame update
    List<Task>toprint;

    Task currentPrint;

    bool printing;

    bool interactable;

    public TaskEvent taskInfoEvent;

    public float timeSinceStart = 0.0f;

    public GameObject paperPrefab;
    


    void Start()
    {
        toprint=  new List<Task>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toprint.Count > 0) {
            Print(toprint[0]);

        }

        if (printing) {
            timeSinceStart += Time.deltaTime; 
            if (timeSinceStart >= 5.0f) {
                interactable = true;
                Instantiate(paperPrefab, GetChildByName.Get(gameObject, "PaperTarget").transform);
                GetComponent<Interactable>().primaryActive = true;
                printing = false;
                timeSinceStart = 0.0f;
            }
        
    
        }
    }

    public void Recieve(Task _task) {
        GetComponent<Interactable>().primaryActive = false;
        toprint.Add(_task);

    }

    public void RemovePaper() {

        UsefulFunctions.deleteAllchildren(GetChildByName.Get(gameObject, "PaperTarget").transform);

    }

    public void Print(Task _task) {
        if (!printing) {
        GetComponent<AudioManager>().StopAll();
        GetComponent<AudioManager>().Play();
        currentPrint = _task;
        printing = true;
        }

    }

    public void Interact() {
        if (interactable) {
            currentPrint.Complete(currentPrint.completePercent);
            toprint.Remove(toprint[0]);
            RemovePaper();
            GetComponent<Interactable>().primaryActive = false;




        }

    }
}
