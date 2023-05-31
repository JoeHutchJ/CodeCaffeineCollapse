using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextTaskbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void next() {
        GameObject create = UsefulFunctions.FindParentWithTag(transform.gameObject, "CodeCreation");
        GameObject review = UsefulFunctions.FindParentWithTag(transform.gameObject, "CodeReview");
        GameObject report = UsefulFunctions.FindParentWithTag(transform.gameObject, "ReportCreation");
        if (create != null) {
            create.GetComponent<CodeCreationController>().generateNew();
        }

        if (review != null) {
            review.GetComponent<ReviewController>().generateNew();
        }
        if (report != null) {
            report.GetComponent<ReportCreationController>().generateNew();
        }

    }
}
