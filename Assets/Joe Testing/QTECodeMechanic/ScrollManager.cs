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
        if (scrollbar.value > 0) {
        scrollbar.value -= scrollSpeed * Time.deltaTime;
        } else {
            scrollbar.value = 0;
        }
    }

    public float getScrollValue() {
        return scrollbar.value;
    }
}
