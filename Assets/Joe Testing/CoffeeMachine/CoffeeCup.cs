using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{

    public bool full;

    Interactable interactable;

    public Event drinkCoffeeEvent;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable != null) {
            interactable.secondaryActive = full;
        }
    }

    public void Drink() {
        if (full) {
            Debug.Log("drunk");
            full = false;
            Global.caffeine = 1;
            GetComponent<Animation>().Play();
            if (drinkCoffeeEvent != null) {
                drinkCoffeeEvent.Raise();
            }
        }
    }

    public void Fill() {
        full = true;

    }
}
