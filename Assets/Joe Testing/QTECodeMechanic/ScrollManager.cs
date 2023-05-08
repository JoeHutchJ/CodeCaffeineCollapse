using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{

    public Scrollbar scrollbar;

    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        
        scrollbar.value -= scrollSpeed * Time.deltaTime;
    }
}
