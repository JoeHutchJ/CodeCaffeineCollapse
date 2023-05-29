using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public bool enabled;

    public Camera mainCamera;

    public Camera pauseCamera;

    public Transform cameraTarget;

    public BoolEvent mainCamEvent;

    public BoolEvent hideUIEvent;

    public Transform pauseMenucontent;

    public Event displaceEvent;

    public Event returnEvent;

    public StartMenu startMenu;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        pauseCamera = GetComponentInChildren<Camera>();
        cameraTarget = GetChildByName.Get(gameObject, "CameraTarget").transform;
        pauseCamera.GetComponent<CameraGo>().hideCam(true);
        pauseMenucontent.gameObject.SetActive(false);
        //Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled) {
            enableCursor(true);
        }

        if (Input.GetKeyUp(KeyCode.Escape)) {
            Enable();
        }

        if (Input.GetKeyDown(KeyCode.G)) {
            Disable();
        }
    }


    public void Enable() {
        if (!enabled) {
        enabled = true;
        Global.paused = true;


        enabled = true;
        if (mainCamera  != null) {
            pauseCamera.transform.position = mainCamera.transform.position;
            pauseCamera.transform.rotation = mainCamera.transform.rotation;
            pauseCamera.GetComponent<CameraGo>().Go(cameraTarget);
            //mainCamEvent.Raise(false);
            mainCamera.enabled = false;
            pauseCamera.enabled = true;
            
            

        }
        hideUIEvent.Raise(true);
        pauseMenucontent.gameObject.SetActive(true);
        enableCursor(true);
        Global.paused = true;
        displaceEvent.Raise();

        }

    }


    public void Disable() {

        enabled = false;
        //move camera...
        pauseCamera.GetComponent<CameraGo>().GoBack(mainCamera.transform);

        //enable UI...
        hideUIEvent.Raise(false);

        returnEvent.Raise();

        //hide (hideAllchildren)
        pauseMenucontent.gameObject.SetActive(false);

        enableCursor(false);

        Global.paused = false;

        //unlock cursor




    }

    public void goToStart() {

        enabled = false;
        //move camera...
        
        startMenu.EnableFromPause(pauseCamera);

        pauseMenucontent.gameObject.SetActive(false);


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
