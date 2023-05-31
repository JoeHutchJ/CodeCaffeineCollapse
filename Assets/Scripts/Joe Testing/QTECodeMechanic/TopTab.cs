using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TopTab : MonoBehaviour
{
    bool notifHidden;

    public bool noNotif;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setNotif(int num) {
        if (!noNotif) {
        if (num > 0) {
            if (notifHidden) {
                GetChildByName.Get(this.gameObject, "Notifs").SetActive(true);
            }
            GetChildByName.Get(this.gameObject, "NotfisText").GetComponent<TMP_Text>().text = num.ToString();
        } else {
            GetChildByName.Get(this.gameObject, "Notifs").SetActive(false);
            notifHidden = true;
        }
        }

    }
}
