using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using Sirenix.OdinInspector;

[Serializable]
public class ObjectiveBlock
{

    public string name;
    public List<Objective> objectives;

    public ObjectiveBlock nextObjectiveBlock;

}
