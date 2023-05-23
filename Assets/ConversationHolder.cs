using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHolder : MonoBehaviour
{

    public ConversationEvent ConvEvent;

    public Conversation convo;

    AudioSource audio;

    public bool sent = false;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Send();
    }

    public void Send() {
        if (!sent) {
            sent = true;
        convo.audioSource = audio;
        convo.source = transform.position;
        ConvEvent.Raise(convo);
        }


    }
}
