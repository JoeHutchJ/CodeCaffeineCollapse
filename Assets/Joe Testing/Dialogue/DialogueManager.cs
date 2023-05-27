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

    bool dialoguePlaying = false;

    bool responseSelected = false;

    bool responsePlaying = false;

    Response currentResponse;

    public BoolEvent lockMouseEvent;

    public AgentTransformEvent cameraRotationEvent;

    public ConversationEvent FinishConvoEvent;

    // 

    void Start()
    {
        responsesBox = GetChildByName.Get(gameObject, "Responses").transform;
        dialogueBox =  GetChildByName.Get(gameObject, "Dialogue").transform;
        skipButton =  GetChildByName.Get(gameObject, "SkipDialogue").transform;
        instantiatedResponses = new List<ResponseContainer>();
        WipeAll();
        if (currentConversation != null && dialogueIndex == 0) {
        //nextDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("f")) {
            nextDialogue();
        }

        if (dialoguePlaying) {
            if (!currentConversation.audioSource.isPlaying) {
                setResponses(currentDialogue);
                dialoguePlaying = false;
            }
        }

        if (responsePlaying) {
            if (!currentConversation.audioSource.isPlaying) {
                responsePlaying = false;
                SelectResponse(currentResponse);
            }

        }
    }

    public void WipeAll() {
        GetChildByName.Get(gameObject,"DialogueText").GetComponent<TMP_Text>().text = "";
        hideMain(true);
        WipeResponses();

    }

    public void WipeResponses() {
        UsefulFunctions.deleteAllchildren(responsesBox);

    }

    public void hideSkip(bool hide) {
        skipButton.gameObject.SetActive(!hide);

    }

    public void hideMain(bool hide) {
        dialogueBox.gameObject.SetActive(!hide);
    }

    public void nextDialogue() {
        if (currentConversation != null) {
        if (dialogueIndex + 1 <= currentConversation.dialogues.Count) {
            currentDialogue = currentConversation.dialogues[dialogueIndex];
            dialogueIndex++;
            setDialogue(currentDialogue);
            
        } else {
            lockMouseEvent.Raise(false);
            WipeAll();
            FinishConvoEvent.Raise(currentConversation);
        }
        }

    }

    public void setDialogue(Dialogue dialogue) {
        WipeAll();
        hideMain(false);
        GetChildByName.Get(gameObject,"DialogueText").GetComponent<TMP_Text>().text = dialogue.dialogue;

        hideSkip(!dialogue.skippable);

        if (currentDialogue.audio != null) {

        currentConversation.audioSource.clip = currentDialogue.audio;
                currentConversation.audioSource.Play();
                dialoguePlaying = true;
        } else {

        setResponses(dialogue);
        }


    }

    public void setResponses(Dialogue dialogue) {
        if (dialogue.respondable) {
            dialogue.populateResponses();
            foreach(Response response in dialogue.responses) {
                addResponse(response);
            }
        }

    }

    public void addResponse(Response response) {

        GameObject obj = Instantiate(responsePrefab, responsesBox);
        obj.GetComponent<ResponseContainer>().Setup(response, this);
        instantiatedResponses.Add(obj.GetComponent<ResponseContainer>());

    }

    public void skipConversation() {
        if (currentDialogue.skippable) {
            currentConversation.audioSource.Stop();
        currentConversation = null;
        dialogueIndex = 0;
        currentDialogue = null;
        lockMouseEvent.Raise(false);
        
        dialoguePlaying = false;
        responsePlaying = false;
        currentResponse = null;
        WipeAll();
        }

    }

    public void SelectResponse(Response response) {
        if (!responseSelected) {
        responseSelected = true;
        currentResponse = response;
        if (response.responseAudio != null) {
            currentConversation.audioSource.clip = response.responseAudio;
            currentConversation.audioSource.Play();
            responsePlaying = true;
            WipeAll();

        } else {
        if (response.type == ResponseType.AFFIRM) {
            nextDialogue();

        } else {
            skipConversation();
        }
        }

    } else {
        currentResponse = null;
        responseSelected = false;
         if (response.type == ResponseType.AFFIRM) {
            nextDialogue();

        } else {
            skipConversation();
        }

    }
    }
    

    public void recieveConversation(Conversation convo) {

        //Global.BuildDebugger.GetComponent<DebugStuff.ConsoleToGUI>().Log(convo.dialogues[0].dialogue, "1",LogType.Log);
        currentConversation = convo;
        dialogueIndex = 0;
        currentDialogue = null;
        lockMouseEvent.Raise(true);
        cameraRotationEvent.Agent = gameObject;
        cameraRotationEvent.Raise(convo.audioSource.transform);
        nextDialogue();

    }
}
