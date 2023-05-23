using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioClip", menuName = "ScriptableObjects/CustomAudioClip")] //custom inspector
public class CustomAudioClip : ScriptableObject
{

    public AudioClip clip;

    public string name;

    public bool loop;

    public float relativeVolume;

    public bool spatial;

    public float range;


}
