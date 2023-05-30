using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEvent : MonoBehaviour
{
    public Event ev;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Raise() {

        ev.Raise();
    }
}
