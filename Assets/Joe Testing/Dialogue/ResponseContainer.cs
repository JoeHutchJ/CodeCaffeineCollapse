using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResponseContainer : MonoBehaviour
{
    Response response;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Response _response) {
        response = _response;
        TMP_Text textBox = transform.Find("ResponseText").GetComponent<TMP_Text>();
        textBox.text = response.responseText;
    }
}
