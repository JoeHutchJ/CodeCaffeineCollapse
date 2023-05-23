using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public Conversation currentConversation;

    public Dialogue currentDialogue;

    public int dialogueIndex;

    Transform responsesBox; 

    Transform dialogueBox;

    Transform skipButton;

    List<ResponseContainer>instantiatedResponses;

    public GameObject responsePrefab;
    void Start()
    {
        responsesBox = GetChildByName.Get(gameObject, "Responses").transform;
        dialogueBox =  GetChildByName.Get(gameObject, "Dialogue").transform;
        skipButton =  GetChildByName.Get(gameObject, "SkipDialogue").transform;
        instantiatedResponses = new List<ResponseContainer>();
        nextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("f")) {
            nextDialogue();
        }
    }

    public void WipeAll() {
        GetChildByName.Get(gameObject,"DialogueText").GetComponent<TMP_Text>().text = "";
        WipeResponses();

    }

    public void WipeResponses() {
        UsefulFunctions.deleteAllchildren(responsesBox);

    }

    public void hideSkip(bool hide) {
        skipButton.gameObject.SetActive(!hide);

    }

    public void nextDialogue() {
        if (dialogueIndex + 1 <= currentConversation.dialogues.Count) {
            currentDialogue = currentConversation.dialogues[dialogueIndex];
            dialogueIndex++;
            setDialogue(currentDialogue);
        } else {
            WipeAll();
        }

    }

    public void setDialogue(Dialogue dialogue) {
        WipeAll();
        GetChildByName.Get(gameObject,"DialogueText").GetComponent<TMP_Text>().text = dialogue.dialogue;

        hideSkip(!dialogue.skippable);

        setResponses(dialogue);


    }

    public void setResponses(Dialogue dialogue) {
        if (dialogue.respondable) {
            dialogue.populateResponses();
            Debug.Log(dialogue.responses.Length);
            foreach(Response response in dialogue.responses) {
                Debug.Log(response.responseText);
                addResponse(response);
            }
        }

    }

    public void addResponse(Response response) {

        GameObject obj = Instantiate(responsePrefab, responsesBox);
        obj.GetComponent<ResponseContainer>().Setup(response);
        instantiatedResponses.Add(obj.GetComponent<ResponseContainer>());

    }
}
