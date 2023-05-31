using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playanim : MonoBehaviour
{

    public BoolFlag photoflippedFlag;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        GetComponent<Animation>().Play();
        photoflippedFlag.Value = true;
        GetComponent<Interactable>().primaryActive = false;
    }
}
