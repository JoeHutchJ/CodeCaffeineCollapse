using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displaceObject : MonoBehaviour
{

    Vector3 pos;
    Quaternion rot;


    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;
        Displace();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Displace() {
        transform.position = new Vector3(10000,0,0);

    }

    public void Reset() {
        transform.position = pos;
        transform.rotation = rot;

    }
}
