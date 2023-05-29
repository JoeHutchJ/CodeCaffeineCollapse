using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBoolFlag : MonoBehaviour
{
    public BoolFlag flag;

    public bool value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set() {
        flag.Value = value;
    }
}
