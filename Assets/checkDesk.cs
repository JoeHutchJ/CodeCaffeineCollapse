using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDesk : MonoBehaviour
{
    public BoolFlag playerAtDesk;

    public Collider collider;

    Vector3 ogpos;
    Quaternion ogrot;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        ogpos = transform.position;
        ogrot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate() {
        transform.position = ogpos;
        transform.rotation = ogrot;
    }

    private void OnCollisionExit(Collision other) {
        Debug.Log("exit");
        if (other.gameObject.tag == "Player") {
            playerAtDesk.Value = false;
        }
    }

    private void OnCollisionStay(Collision other) {
                Debug.Log("stay");
        if (other.gameObject.tag == "Player") {
            playerAtDesk.Value = true;
        }
    }

    private void OnCollisionEnter(Collision other) {
                Debug.Log("enter");
        if (other.gameObject.tag == "Player") {
            playerAtDesk.Value = true;
        }
    }
}
