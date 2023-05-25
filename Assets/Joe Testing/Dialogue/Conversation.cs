using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Sirenix.OdinInspector;

[Serializable]

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Conversation")] //custom inspector
public class Conversation: ScriptableObject {

    public List<Dialogue> dialogues;

    public string name; 

    public AudioSource audioSource;

    public Vector3 source;



}

