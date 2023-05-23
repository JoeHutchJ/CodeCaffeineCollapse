using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseContainer : MonoBehaviour
{
    Response response;

    DialogueManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Response _response, DialogueManager _manager) {
        response = _response;
        manager = _manager;
        TMP_Text textBox = transform.Find("ResponseText").GetComponent<TMP_Text>();
        textBox.text = response.responseText;
    }

    public void Select() {
        manager.SelectResponse(response);

    }
}
