using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaffeineMeterController : MonoBehaviour
{
    public Image fill;

    public CanvasGroup blackScreen;
    private void Start() {
        fill = GetChildByName.Get(gameObject, "FillCaffeineMeter").GetComponent<Image>();
        blackScreen = GetChildByName.Get(transform.parent.gameObject, "BlackScreen").GetComponent<CanvasGroup>();
    }
    void Update()
    {
        fill.fillAmount = Global.caffeine;

        if (Global.caffeine < 0.4f) {
            blackScreen.alpha = UsefulFunctions.Remap(Global.caffeine, 0.4f, 0, 0, 1);
        }

        

    }
}
