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

    public Animator Anim;
    
    Quaternion targetRotation;

    AudioSource audioSource;

    public Vector3 originalPos;
    public Quaternion originalRot;

    bool talking;

    public bool coworker; 

    public BoolFlag playerAtDesk;

    public Transform otherTarget;

    // Start is called before the first frame update
    void Start()
    {
       //GoToPos(testTarget.position);
       //RotateToPos(testTarget.rotation);
       //Anim = GetChildByName.Get(gameObject,"boss_talk").GetComponent<Animator>();
       audioSource = GetComponent<AudioSource>();
       originalPos = transform.position;
       originalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsTarget();

        if (audioSource.isPlaying) {
            talking = true;
            Anim.SetTrigger("Talk");
        } else if (!audioSource.isPlaying && talking) {
            talking = false;
            Anim.SetTrigger("Idle");
        }
        
    }

    public void RotateTowardsTarget() {
        if (lookTowardsActive && lookTarget != null) {
            float step = rotationSpeed * Time.deltaTime;
         Vector3 direction = lookTarget.position - transform.position;
         direction.y = 0.0f;
         if (coworker) {
            transform.LookAt(lookTarget, Vector3.up);
         } else {
        if (direction != Vector3.zero) {
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        } 
         }



        }
    }

    public void LookTowards(Transform target) {
        //lookTowardsActive = !lookTowardsActive;
        lookTowardsActive = true;
        if (lookTowardsActive) {
            lookTarget = target;
        }

    }

    public void Move(Transform trans) {
        Debug.Log("move");
        Anim.SetTrigger("Walk");
        GoToPos(trans.position);
    }

    public void Rotate(Transform trans) {
        RotateToPos(trans.rotation);
    }

    public void GoToPos(Vector3 pos) {
        StartCoroutine(MoveToPos(pos));

    }

    public void GoToTarget(Transform trans) {
        StopAllCoroutines();
        StartCoroutine(MoveToPos(trans.position));
        LookTowards(trans);

    }

    IEnumerator MoveToPos(Vector3 pos) {

        if (!playerAtDesk.Value) {
            if (otherTarget != null) {
            pos = otherTarget.position;
            }
        }
        float step = moveSpeed;
        while (!AtPos(pos)) {

            transform.position = Vector3.MoveTowards(transform.position, pos, step * Time.deltaTime);

            yield return null;
        }

        Anim.SetTrigger("Idle");


    }

    IEnumerator ReturnToPos(Vector3 pos) {
        float step = moveSpeed;
        while (!AtPos(pos)) {

            transform.position = Vector3.MoveTowards(transform.position, pos, step * Time.deltaTime);

            yield return null;
        }
    }

    public void RotateToPos(Quaternion rotation) {
        StartCoroutine(RotateTo(rotation));

    }

        IEnumerator RotateTo(Quaternion rotation) {

        float step = rotationSpeed;
        while (!AtRotation(rotation)) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, step * Time.deltaTime);
            //Debug.Log(transform.rotation);
            yield return null;
        }


    }



    public bool AtPos(Vector3 pos) {
        return Vector3.Distance(transform.position, pos) < 20;




    }

    public bool AtRotation(Quaternion rotation) {
        return Quaternion.Angle(transform.rotation, rotation) < 10;




    }

    public void ReturnToOriginal() {
        StopAllCoroutines();
        StartCoroutine(ReturnToPos(originalPos));
        RotateToPos(originalRot);
        lookTowardsActive = false;
        lookTarget = null;

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
