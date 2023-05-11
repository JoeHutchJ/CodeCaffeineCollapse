using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{

    public Scrollbar scrollbar;

    public bool active;

    public float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
        if (scrollbar.value > 0) {
        scrollbar.value -= scrollSpeed * Time.deltaTime * 0.03f;
        } else {
            scrollbar.value = 0;
        }
        }
    }

    public void setScrollSpeed(float speed) {
        scrollSpeed = speed;
    }

    public float getScrollSpeed() {
        return scrollSpeed;
    }

    public float getScrollValue() {
        return scrollbar.value;
    }

    public void startScrolling() {
        active = true;
    }

    public void stopScrolling() {
        active = false;

    }

    public void resetScrolling() {
        active = false;
        scrollbar.value = 1;
    }
}
