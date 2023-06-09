using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueInfo {

    public static List<Response> getResponses(bool skippable, int num) {

    Object[] responseobjs = Resources.LoadAll("Responses");
    List<Response> allResponses = new List<Response>();
    List<Response> responses = new List<Response>();

    foreach (Object obj in responseobjs) {
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
public class Dialogue : ScriptableObject
{

    public string dialogue;
    public AudioClip audio;

    public bool skippable;

    public bool respondable;

    public float relativeVolume = 1.0f;

    public Response[] responses; 
    
    
    public void populateResponses() {
        if (respondable) {
             responses = DialogueInfo.getResponses(skippable, 3).ToArray();
        }
    }

}



