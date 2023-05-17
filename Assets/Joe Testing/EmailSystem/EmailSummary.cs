using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailSummary : MonoBehaviour
{
    public Email email;

    public EmailManager emailManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Email _email, EmailManager manager) {
        email = _email;
        emailManager = manager;
        GetChildByName.Get(this.gameObject, "Icon").GetComponent<Image>().sprite = UsefulFunctions.TextureToSprite(email.Author.icon);
        GetChildByName.Get(this.gameObject, "AuthorText").GetComponent<TMP_Text>().text = email.Author.name;

    }

    public void Clicked() {
        emailManager.Display(email);
    }
}
