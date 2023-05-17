using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailMessage : MonoBehaviour
{
    public Email email;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkedRead() {
        email.read = true;

    }

    public void Setup(Email _email) {
        email = _email;
        GetChildByName.Get(this.gameObject, "Image").GetComponent<Image>().sprite = UsefulFunctions.TextureToSprite(email.Author.icon);
        GetChildByName.Get(this.gameObject, "From").GetComponent<TMP_Text>().text = "From: " + email.Author.name;
        GetChildByName.Get(this.gameObject, "Subject").GetComponent<TMP_Text>().text = email.Subject;
        GetChildByName.Get(this.gameObject, "Message").GetComponent<TMP_Text>().text = email.Message;

    }
}
