using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class caffineMeter : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackFilter;
    [SerializeField] private GameObject gbl;
    [SerializeField] private Slider cfnMetr;

    private float alphaStep;

    private float getCaffineLvl()
    {
        global lvl = gbl.GetComponent<global>();
        return lvl.caffineVal;
    }

    private void setCaffineMeter(float newVal)
    {
        global lvl = gbl.GetComponent<global>();
        lvl.caffineVal = newVal;

    }

    private void decrementCaffine()
    {
        float val = getCaffineLvl();
        val -= 0.00004f;
        cfnMetr.value = val;
        setCaffineMeter(val);
        
    }



    private void fadeInBlack()
    {
        float val = getCaffineLvl();
        if(val < 0.4)
        {
            blackFilter.alpha += 0.0001f;
        }
    }


    private void Update()
    {
        decrementCaffine();
        fadeInBlack();
    }

}
