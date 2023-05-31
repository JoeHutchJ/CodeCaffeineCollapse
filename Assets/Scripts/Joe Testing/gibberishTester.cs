using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gibberishTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.U)) {
            string code = generateGibberishCode.GenerateRandomCode(11);
            Debug.Log(code);
            Debug.Log(code.Split("\n").Length);

        }
    }
}
