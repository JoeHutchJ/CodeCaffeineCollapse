using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GetChildByName 
{
   

    public static GameObject GetDirect(GameObject obj, string name) {
     Transform trans = obj.transform;
     Transform childTrans = trans.Find(name);
     if (childTrans != null) {
         return childTrans.gameObject;
     } else {
         return null;
     }
 }

 public static GameObject Get(GameObject obj, string name) {
     Transform trans = obj.transform;
     Transform childTrans = trans.Find(name);
     if (childTrans != null) {
         return childTrans.gameObject;
     } else if (trans.childCount > 0) {
        for (int i = 0; i < trans.childCount; i++) {
            GameObject childObj = Get(trans.GetChild(i).gameObject, name);
            if (childObj != null) {
                return childObj;
            }
        }
        return null;
     } else {
         return null;
     }
 }
}
