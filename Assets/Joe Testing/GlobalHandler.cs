using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalHandler : MonoBehaviour
{
    public int jobQuota;

    public float percent;

    public int jobPoints;

    public GameObject buildDebugger;
    // Start is called before the first frame update
    void Start()
    {
        jobQuota = Global.jobQuota;
        Global.BuildDebugger = buildDebugger;
    }

    // Update is called once per frame
    void Update()
    {
        Global.Update();
        jobQuota = Global.jobQuota;
        percent = Global.QuotaPercent();
        jobPoints = Global.jobQuotaPoints;


    }

    public void setCursorMode(bool locked) {
        Global.cursorMode = locked;
    }
}
