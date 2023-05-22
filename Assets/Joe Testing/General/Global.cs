using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global 
{

    public static float currentTime;

    public static float caffeine;

    public static int jobQuota;

    public static int jobQuotaPoints;


    public static void Update() {
        currentTime += Time.deltaTime;
    }

    public static void AddPoints(int num) {
        jobQuotaPoints += num;
    }






    }

