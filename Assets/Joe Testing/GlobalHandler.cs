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
        if (!caffeinePaused.Value) {
        Global.UpdateCaffeine();
        }




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
