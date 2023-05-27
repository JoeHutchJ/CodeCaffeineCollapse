using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPC : MonoBehaviour
{

    public float moveSpeed;

    public float rotationSpeed;

    public Transform testTarget;

    public AgentEvent coffeeRecievedEvent;

    public bool lookTowardsActive;

    public Transform lookTarget;
    
    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
       //GoToPos(testTarget.position);
       //RotateToPos(testTarget.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();
    }

    public void RotateTowardsTarget() {
        if (lookTowardsActive && lookTarget != null) {
            float step = rotationSpeed * Time.deltaTime;
         Vector3 direction = lookTarget.position - transform.position;
         direction.y = 0.0f;
        if (direction != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        } 



        }
    }

    public void LookTowards(Transform target) {
        Debug.Log("look towards");
        //lookTowardsActive = !lookTowardsActive;
        lookTowardsActive = true;
        if (lookTowardsActive) {
            lookTarget = target;
        }

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

    public void GoToTarget(Transform trans) {
        StartCoroutine(MoveToPos(trans.position));
        LookTowards(trans);

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

    public void ReceiveCoffee() {
        CoffeeCup coffee = GetChildByName.Get(transform.gameObject, "CoffeeCup").GetComponent<CoffeeCup>();

        if (coffee != null) {
            Debug.Log("coffee not null");
            if (coffee.full) {
                if (coffeeRecievedEvent != null) {
                    coffeeRecievedEvent.Agent = this.gameObject;
                    coffeeRecievedEvent.Raise();
                }
            }
        }

    }
}
