using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoolFlag : MonoBehaviour
{
    public List<BoolFlag> flags;
    // Start is called before the first frame update
    void Start()
    {
        foreach ( BoolFlag flag in flags) {
            flag.Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
