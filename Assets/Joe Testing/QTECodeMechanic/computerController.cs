using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class computerController : MonoBehaviour
{

    public GameObject CodeReview;
    public GameObject CodeCreation;

    public GameObject Email;

    public GameObject ReportCreation;


    Transform currentTab;

    Transform[] tabs;
    // Start is called before the first frame update
    void Start()
    {

        createTab(CodeCreation); //will be email by default

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void deleteTabs() {
        foreach (Transform child in transform) {
            if (child.gameObject.name != "TopBar") {
                Destroy(child.gameObject);
            }
        }
    }

    public void createTab(GameObject obj) {
        deleteTabs();
        currentTab = Instantiate(obj, transform).transform;
    }

    public void ClickCodeCreationTab() {
        createTab(CodeCreation);
    }

    public void ClickCodeReviewTab() {
        createTab(CodeReview);
    }

    public void ClickReportCreationTab() {
        createTab(ReportCreation);
    }
}
