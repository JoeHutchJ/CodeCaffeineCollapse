using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UsefulFunctions
{
    public static float Remap(float value, float from1, float to1, float from2, float to2) {
    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
}

    public static void HideAllChildren(Transform trans, bool hide) {
        if (trans.childCount > 0) {
        for (int i = 0; i < trans.childCount; i++) {
            HideAllChildren(trans.GetChild(i), hide);
            trans.GetChild(i).gameObject.SetActive(!hide);
        }
        
    }


    }


}


