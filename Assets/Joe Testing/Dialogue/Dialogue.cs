using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using Sirenix.OdinInspector;

public static class DialogueInfo {

    public static List<Response> getResponses(bool skippable, int num) {

    UnityEngine.Object[] responseobjs = Resources.LoadAll("Responses");
    List<Response> allResponses = new List<Response>();
    List<Response> responses = new List<Response>();

    foreach (UnityEngine.Object obj in responseobjs) {
        allResponses.Add((Response)obj);

    }
    List<Response> shuffled = UsefulFunctions.Shuffle<Response>(allResponses);
    if (!skippable) {
        foreach(Response response in shuffled) {
            if (response.type == ResponseType.AFFIRM) {
                if (responses.Count < num) {
                responses.Add(response);
                }
            }
        }
        

    } else {
        bool negativeGot = false;
        foreach(Response response in shuffled) {
            if (response.type == ResponseType.AFFIRM) {
                if ((responses.Count < num && negativeGot) || (responses.Count < num - 1 && !negativeGot)) {
                responses.Add(response);
                }
            } else {
                
                if (!negativeGot) {
                    responses.Add(response);
                }
                negativeGot = true;
            }
        }

    }

    return responses;


    }

}

public enum ResponseType {AFFIRM, NEGATIVE };


[CreateAssetMenu(fileName = "New Dialogue", menuName = "ScriptableObjects/Dialogue")] //custom inspector
[Serializable]
public class Dialogue : SerializedScriptableObject
{
    [SerializeReference]
    [SerializeField] public string dialogue;
    public AudioClip audio;

    public bool skippable;

    public bool respondable;

    public Response[] responses; 
    
    public void populateResponses() {
        Debug.Log(respondable);
        if (respondable) {
             responses = DialogueInfo.getResponses(skippable, 3).ToArray();
        }
    }

}


[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Conversation")] //custom inspector
[Serializable]
public class Conversation: SerializedScriptableObject {
    [SerializeReference]
    [SerializeField, InlineEditor] public List<Dialogue> dialogues;

    public string name; 

    public AudioSource audioSource;

    public Vector3 source;



}
