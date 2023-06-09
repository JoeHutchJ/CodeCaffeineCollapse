using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class Global 
{

    public static float currentTime;

    public static string currentDay = "Monday";

    public static int dayIndex = 0;

    public static Dictionary<int, string> days = new Dictionary<int, string>() { 
        {0, "Monday"},
        {1, "Tuesday"},
        {2, "Wednesday"},
        {3, "Thursday"},
        {4, "Friday"},

    };

    static float dayLength = 350.0f;

    static int hours = 9;

    static float minutes = 0;

    public static float caffeine = 1.0f;

    static float caffeinePerSecond = 0.013f;

    public static bool caffeineEnabled = true;

    //static float caffeinePerSecond = 0.07f;


    public static int jobQuota = 160;

    public static int jobQuotaPoints;

    public static bool cursorMode = false;

    public static float volume = 1.0f;

    public static float mouseSensitivity = 1.0f;

    public static GameObject ObjInHand;

    public static GameObject BuildDebugger;

    public static bool paused;

    public static bool ObjectivesStarted;

    public static bool freeMode = false;

    public static bool leftOffice;

    public static bool started;


    public static void Update() {
        if (!paused) {
        UpdateTime();
        }
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
        if (!paused && caffeineEnabled) {
        caffeine -= caffeinePerSecond * Time.deltaTime;
        if (caffeine < 0) {
            caffeine = 0;
        }
        }

    }


    public static void UpdateTime() {
        currentTime += Time.deltaTime;

        minutes += (480.0f/dayLength) * Time.deltaTime;

        if (minutes > 60.0f) {
            minutes = 0.0f;
            hours++;
        }

        if (hours >= 5.0f) {

        }




    }

    public static (int, float) GetTime() {

        return (hours, Mathf.Floor(minutes));


    }

    public static float GetMouseSensitivity() {
        float caffeineSens = UsefulFunctions.Remap(caffeine, 0.4f, 0.0f, 1.0f, 0.4f);
        return mouseSensitivity * caffeineSens;

    }

    public static void ResetTime() {
        currentTime = 0.0f;
        hours = 9;
        minutes = 0.0f;
    }

    public static void ResetDay() {
        ResetTime();
        caffeine = 1.0f;
        caffeineEnabled = true;
        jobQuotaPoints = 0;
        //ObjectivesStarted = false;
        leftOffice = false;
        started = false;

    }

    public static void nextDay() {
        
        ResetDay();
        dayIndex++;
        if (dayIndex == 5) {
            dayIndex = 0;
        }
        currentDay = days[dayIndex];
        Debug.Log(currentDay);

        if (dayIndex != 0) {
            freeMode = true;
        } else {
            freeMode = false;
        }


    }

    public static void resetToMonday() {
        dayIndex = 0;
        currentDay = days[dayIndex];
        ResetDay();


    }








    }

