using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPC : MonoBehaviour
{

    public float moveSpeed;

    public float rotationSpeed;

    public Transform testTarget;

    // Start is called before the first frame update
    void Start()
    {
       //GoToPos(testTarget.position);
       //RotateToPos(testTarget.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Transform trans) {
        GoToPos(trans.position);
    }

    public void Rotate(Transform trans) {
        RotateToPos(trans.rotation);
    }

    public void GoToPos(Vector3 pos) {
        StartCoroutine(MoveToPos(pos));

    }

    IEnumerator MoveToPos(Vector3 pos) {

        float step = moveSpeed * Time.deltaTime;
        while (!AtPos(pos)) {

            transform.position = Vector3.MoveTowards(transform.position, pos, step);

            yield return null;
        }


    }

    public void RotateToPos(Quaternion rotation) {
        StartCoroutine(RotateTo(rotation));

    }

        IEnumerator RotateTo(Quaternion rotation) {

        float step = rotationSpeed * Time.deltaTime;
        while (!AtRotation(rotation)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step);
            //Debug.Log(transform.rotation);
            yield return null;
        }


    }



    public bool AtPos(Vector3 pos) {
        return Vector3.Distance(transform.position, pos) < 100;




    }

    public bool AtRotation(Quaternion rotation) {
        return Quaternion.Angle(transform.rotation, rotation) < 10;




    }
}
