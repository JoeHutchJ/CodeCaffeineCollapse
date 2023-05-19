using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailSummary : MonoBehaviour
{
    public Email email;

    Color unread;
    Color read;

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
        unread = new Color(255,114,144,255);
        read = new Color(149,149,149,255);
        email = _email;
        emailManager = manager;
        GetChildByName.Get(this.gameObject, "Icon").GetComponent<Image>().sprite = UsefulFunctions.TextureToSprite(email.Author.icon);
        GetChildByName.Get(this.gameObject, "AuthorText").GetComponent<TMP_Text>().text = email.Author.name;

        if (!email.read) {
            //GetChildByName.Get(this.gameObject, "AuthorText").GetComponent<TMP_Text>().fontStyle = FontStyles.Underline;
            ColorBlock block = GetComponent<Button>().colors;
            block.normalColor = unread;
            GetComponent<Button>().colors = block;
            Debug.Log(GetComponent<Button>().colors.normalColor == unread);
        } else {
            //GetChildByName.Get(this.gameObject, "AuthorText").GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;
            ColorBlock block = GetComponent<Button>().colors;
            block.normalColor = read;
            GetComponent<Button>().colors = block;
            Debug.Log(GetComponent<Button>().colors.normalColor == read);
        }
        

    }

    public void Reset(Email _email) {
        email = _email;
        GetChildByName.Get(this.gameObject, "Icon").GetComponent<Image>().sprite = UsefulFunctions.TextureToSprite(email.Author.icon);
        GetChildByName.Get(this.gameObject, "AuthorText").GetComponent<TMP_Text>().text = email.Author.name;

        if (!email.read) {
            GetChildByName.Get(this.gameObject, "AuthorText").GetComponent<TMP_Text>().fontStyle = FontStyles.Underline;
        } else {
            GetChildByName.Get(this.gameObject, "AuthorText").GetComponent<TMP_Text>().fontStyle = FontStyles.Normal;
        }

    }

    public void Clicked() {
        emailManager.Display(email);
    }
}
