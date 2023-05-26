using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class clockController : MonoBehaviour
{
    TMP_Text hours;
    TMP_Text minutes;
    // Start is called before the first frame update
    void Start()
    {
        hours = GetChildByName.Get(gameObject,"hours").GetComponent<TMP_Text>();
        minutes = GetChildByName.Get(gameObject,"minutes").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        (int,float) time = Global.GetTime();

        if (time.Item1 < 10) {
            hours.text = "0" + time.Item1.ToString();
        } else {
           hours.text = time.Item1.ToString(); 
        }

        if (time.Item2 < 10.0f) {
            minutes.text = "0" + time.Item2.ToString();
        } else {
           minutes.text = time.Item2.ToString(); 
        }
    
    }
}
