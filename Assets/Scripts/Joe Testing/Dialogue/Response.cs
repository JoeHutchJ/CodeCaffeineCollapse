using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Response", menuName = "ScriptableObjects/Response")]
public class Response: ScriptableObject {

    public string responseText;

    public AudioClip responseAudio;
    public ResponseType type;



}

