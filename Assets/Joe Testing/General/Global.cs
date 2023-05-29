using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global 
{

    public static float currentTime;

    static float dayLength = 200.0f;

    static int hours = 9;

    static float minutes = 0;

    public static float caffeine = 1.0f;

    static float caffeinePerSecond = 0.007f;

    public static int jobQuota = 100;

    public static int jobQuotaPoints;

    public static bool cursorMode = false;

    public static float volume = 1.0f;

    public static float mouseSensitivity = 1.0f;

    public static GameObject ObjInHand;

    public static GameObject BuildDebugger;


    public static void Update() {
        UpdateTime();
    }

    public static void AddPoints(int num) {
        jobQuotaPoints += num;
    }

    public static float QuotaPercent() {
        float amount =  ((float)jobQuotaPoints / (float)jobQuota);
        if (amount > 1.0f) {
            return 1.0f;
        } else if (amount < 0.0f) {
            return 0;
        } else {
        return amount;
        }
    }

    public static void setObjinHand(Pickupable obj) {
        if (obj == null) {
            ObjInHand = null;
        } else {
        ObjInHand = obj.gameObject;
        }

    }

    public static void UpdateCaffeine() {
        caffeine -= caffeinePerSecond * Time.deltaTime;
        if (caffeine < 0) {
            caffeine = 0;
        }

    }


    public static void UpdateTime() {
        currentTime += Time.deltaTime;

        minutes += (480.0f/dayLength) * Time.deltaTime;

        if (minutes > 60.0f) {
            minutes = 0.0f;
            hours++;
        }




    }

    public static (int, float) GetTime() {

        return (hours, Mathf.Floor(minutes));


    }

    public static float GetMouseSensitivity() {
        return mouseSensitivity;

    }








    }

