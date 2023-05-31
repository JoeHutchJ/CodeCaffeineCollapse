using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalHandler : MonoBehaviour
{
    public int jobQuota;

    public float percent;

    public int jobPoints;

    public GameObject buildDebugger;

    public BoolFlag caffeinePaused;

    public bool freeMode = false;

    // Start is called before the first frame update
    void Start()
    {
        jobQuota = Global.jobQuota;
        Global.BuildDebugger = buildDebugger;
        Global.freeMode = freeMode;
    }

    // Update is called once per frame
    void Update()
    {
        Global.Update();
        jobQuota = Global.jobQuota;
        percent = Global.QuotaPercent();
        jobPoints = Global.jobQuotaPoints;
        Global.UpdateCaffeine();
        




    }

    public void setCursorMode(bool locked) {
        Global.cursorMode = locked;
    }

    public void ObjectivesStarted() {
        Global.ObjectivesStarted = true;
    }

    public void LeftOffice() {
        Global.leftOffice = true;
        Debug.Log(Global.leftOffice);

    }

    public void ResetDay() {
        Global.ResetDay();
    }
}
