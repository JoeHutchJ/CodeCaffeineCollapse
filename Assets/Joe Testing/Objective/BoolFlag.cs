using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Sirenix.OdinInspector;
[Serializable]
[CreateAssetMenu(fileName = "Flag", menuName = "Flags/BoolFlag")] //custom inspector
public class BoolFlag : ScriptableObject
{
    public Boolean initValue;
    public Boolean Value;

    public void Reset() {
        Value = initValue;
    }
}
