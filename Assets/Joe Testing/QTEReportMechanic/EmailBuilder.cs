using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EmailSentiment {POSITIVE, NEGATIVE, SPAM}

public class Author {
    public string name;
    public Texture2D icon;

    public string[] AuthorNames = {


    };

    public string[] SpamNames = {


    };

    public Author(bool spam, Texture2D _icon) {
        if (!spam) {
                name = AuthorNames[UnityEngine.Random.Range(0,AuthorNames.Length)];
        } else {
                name = AuthorNames[UnityEngine.Random.Range(0,SpamNames.Length)];
        
        }

        icon = _icon;
    }
}

public static class EmailBuilder
{
    static UnityEngine.Object[] iconObjects;

    static List<Texture2D> icons;

    static List<Texture2D> Unusedicons;
    static List<Author> authors;

    static List<Author> spamAuthors;

    static string[] positiveMessage = {


    };

    static string[] negativeMessage = {


    };

    static string[] spamMessage = {


    };

    


    static void OnEnable() {
        authors = new List<Author>();
        spamAuthors = new List<Author>();
        iconObjects = Resources.LoadAll("Assets/Image Textures/Icons",typeof(Texture2D));
        icons = new List<Texture2D>();
        
        foreach (UnityEngine.Object obj in iconObjects) {
            icons.Add((Texture2D)obj);
        }
        Unusedicons = icons;
        for (int i = 0; i < 5; i++) {
            Texture2D _icon = icons[UnityEngine.Random.Range(0, icons.Count)];
            Author auth = new Author(false, _icon);
            authors.Add(auth);
            Unusedicons.Remove(_icon);
        }

        for (int i = 0; i < 10; i++) {
            Texture2D _icon = icons[UnityEngine.Random.Range(0, icons.Count)];
            Author auth = new Author(true, _icon);
            spamAuthors.Add(auth);
            Unusedicons.Remove(_icon);
        }
    }
    

    static void newEmail(EmailSentiment sentiment) {
        if (authors.Count <= 0 || spamAuthors.Count <= 0) {
            OnEnable();
            
        } 
        Email email = new Email();

        switch (sentiment) {
            case EmailSentiment.POSITIVE :
            email.
            break;
            case EmailSentiment.NEGATIVE:

            break;
            case EmailSentiment.SPAM:

            break;
        }
        
    }
}
