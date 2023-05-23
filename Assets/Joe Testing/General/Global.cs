using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global 
{

    public static float currentTime;

    public static float caffeine;

    public static int jobQuota = 100;

    public static int jobQuotaPoints;

    public static bool cursorMode = false;


    public static void Update() {
        currentTime += Time.deltaTime;
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







    }

