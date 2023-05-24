using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CoffeeStatus {
    NONE, BREWING, FILLING, READY


}
public class CoffeeMachine : MonoBehaviour
{

    public float brewTime;

    public float coffeeElapsed;

    public GameObject cup;

    CoffeeStatus status = CoffeeStatus.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (status) {
            case CoffeeStatus.BREWING:
                if (coffeeElapsed + Time.deltaTime > brewTime) {
                    coffeeElapsed = 0;
                    StartFilling();
                } else {
                    coffeeElapsed += Time.deltaTime;
                }
                break;
            case CoffeeStatus.FILLING:
                if (cup != null) {
                    if (cup.GetComponent<Animation>()) {
                if (!cup.GetComponent<Animation>().isPlaying) {
                    CoffeeReady();
                }
                
            }
            
        }
        break;

        }
    }

    void StartBrewing() {
        status = CoffeeStatus.BREWING;


    }

    void StartFilling() {
        status = CoffeeStatus.FILLING;
        if (cup != null) {
            if (cup.GetComponent<Animation>()) {
                cup.GetComponent<Animation>().Play();
            }
        }

    }

    void CoffeeReady() {
        status = CoffeeStatus.READY;

    }



    void PickupCoffee() {
        
    }


    public void Interact() {
        switch (status) {
            case CoffeeStatus.NONE: 
                StartBrewing();
                break;
            case CoffeeStatus.READY:
                PickupCoffee();
                break;

        }



    }
}
