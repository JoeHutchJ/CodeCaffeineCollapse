using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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

    public static void deleteAllchildren(Transform trans) {
        foreach (Transform child in trans) {
            GameObject.Destroy(child.gameObject);
        }
    }

    public static Transform FindParent(Transform trans, string name) {
        if (trans.parent != null) {
            if (trans.parent.name.Equals(name)) {
                return trans.parent;
            } else {
                return FindParent(trans.parent, name);
            }
        }
        if (trans.name.Equals(name)) {
            return trans;
        }
        return null;
    }

    public static GameObject FindParentWithName(GameObject childObject, string name)
 {
    Transform t = childObject.transform;
    while (t.parent != null)
    {
       if (t.parent.name == name)
       {
          return t.parent.gameObject;
       }
       t = t.parent.transform;
    }
    return null; // Could not find a parent with given tag.
 }

   public static GameObject FindParentWithTag(GameObject childObject, string tag)
 {
    Transform t = childObject.transform;
    while (t.parent != null)
    {
       if (t.parent.tag == tag)
       {
          return t.parent.gameObject;
       }
       t = t.parent.transform;
    }
    return null; // Could not find a parent with given tag.
 }

 public static Sprite TextureToSprite(Texture2D texture) {

                 Rect rec = new Rect(0, 0, texture.width, texture.height);
                 Sprite.Create(texture,rec,new Vector2(0,0),1);
                 return Sprite.Create(texture,rec,new Vector2(0,0),.01f);
                 
    }
 }

 



