using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Sirenix.OdinInspector;

[Serializable]

public class Condition
{   
    [InlineEditor]
    public BoolFlag flag;

    public Boolean value;

    public Boolean Check() {
        Debug.Log("flag " + flag.Value + " " + value);
        return flag.Value == value;

    }

    public void Set() {
        flag.Value = value;
    }
}
