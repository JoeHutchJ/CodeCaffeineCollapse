using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public GameObject Fill;
    // Start is called before the first frame update
    void Start()
    {
        Fill = GetChildByName.Get(transform.gameObject, "Fill");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setProgress(float val) {
        Fill.GetComponent<Image>().fillAmount = val;
    }
}
