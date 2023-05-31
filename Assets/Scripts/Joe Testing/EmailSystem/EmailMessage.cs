using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailMessage : MonoBehaviour
{
    public Email email;

    EmailManager manager;

    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetChildByName.Get(this.gameObject, "MarkRead").GetComponent<Button>();
        if (email != null) {
        if (email.read) {
            enableButton(btn, false);
        }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkedRead() {
        if (!email.spam) {
        email.read = true;
        manager.markRead();
        enableButton(btn, false);
        } else {
            manager.deleteSpam();
        }

    }

    void enableButton(Button button, bool enable) {
        button.enabled = enable;
        button.interactable = enable;
    }

    public void Setup(Email _email,EmailManager mnger ) {
        email = _email;
        manager = mnger;
        GetChildByName.Get(this.gameObject, "Image").GetComponent<Image>().sprite = UsefulFunctions.TextureToSprite(email.Author.icon);
        GetChildByName.Get(this.gameObject, "From").GetComponent<TMP_Text>().text = "From: " + email.Author.name;
        GetChildByName.Get(this.gameObject, "Subject").GetComponent<TMP_Text>().text = email.Subject;
        GetChildByName.Get(this.gameObject, "Message").GetComponent<TMP_Text>().text = email.Message;

        if (email.spam) {
            GetChildByName.Get(this.gameObject, "ButtonText").GetComponent<TMP_Text>().text = "Delete";
        }

    }
}
