using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{

    public bool enabled; 

    public Camera pauseCamera;
    public Camera mainCamera; 

    public Transform cameraTarget;

    public Transform StartMenuContent;

    public BoolEvent mainCamEvent;

    public BoolEvent hideUIEvent;

    public Event displaceEvent;

    public Event returnEvent;


    
    // Start is called before the first frame update
    void Start()
    {
        pauseCamera = GetComponentInChildren<Camera>();
        mainCamera = Camera.main;
        StartMenuContent = GetChildByName.Get(gameObject, "start menu").transform;
        cameraTarget = GetChildByName.Get(gameObject, "CameraTarget").transform;
        Enable();
        enableCursor(true);
    }

    // Update is called once per frame
    void Update()
    {   
        if (enabled) {
        enableCursor(true);
        }

        /*if (Input.GetKeyDown(KeyCode.G)) {
            StartGame();
        }*/
    }

    public void Enable() {
        //move camera
        //disable main camera. 
        enabled = true;
        if (mainCamera  != null) {
            pauseCamera.transform.position = mainCamera.transform.position;
            pauseCamera.transform.rotation = mainCamera.transform.rotation;
            pauseCamera.GetComponent<CameraGo>().Go(cameraTarget);
            mainCamEvent.Raise(false);
            
            

        }
        hideUIEvent.Raise(true);
        StartMenuContent.gameObject.SetActive(true);
        enableCursor(true);
        Global.paused = true;
        displaceEvent.Raise();



    }

    public void StartGame() {

        enabled = false;
        //move camera...
        pauseCamera.GetComponent<CameraGo>().GoBack(mainCamera.transform);

        //enable UI...
        hideUIEvent.Raise(false);

        returnEvent.Raise();

        //hide (hideAllchildren)
        StartMenuContent.gameObject.SetActive(false);

        enableCursor(false);

        Global.paused = false;

        //unlock cursor

    }



    public void enableCursor(bool enable) {
        if (enable) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            

        } else {
             Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


}
