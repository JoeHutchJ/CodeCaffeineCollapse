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

    public BoolFlag isPCMode;


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
            pauseCamera.GetComponent<CameraGo>().waitingforpause = true;
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
        Time.timeScale = 1.0f;
        //move camera...
        pauseCamera.GetComponent<CameraGo>().GoBack(mainCamera.transform);

        //enable UI...
        hideUIEvent.Raise(false);

        returnEvent.Raise();

        //hide (hideAllchildren)
        pauseMenucontent.gameObject.SetActive(false);
        if(!isPCMode.Value) {
        enableCursor(false);

        } else {
            enableCursor(true);
        }

        Global.paused = false;

        //unlock cursor




    }

    public void goToStart() {


        Time.timeScale = 1.0f;
        enabled = false;
        //move camera...
        
        startMenu.EnableFromPause(pauseCamera);

        pauseMenucontent.gameObject.SetActive(false);


    }


    public void EnableFromStart(Camera cam) {

        enabled = true;

        if (cam  != null) {
            pauseCamera.transform.position = cam.transform.position;
            pauseCamera.transform.rotation = cam.transform.rotation;
            pauseCamera.GetComponent<CameraGo>().Go(cameraTarget);
            pauseCamera.GetComponent<CameraGo>().waitingforpause = true;
            //mainCamEvent.Raise(false);
            
            

        }
        hideUIEvent.Raise(true);
        pauseMenucontent.gameObject.SetActive(true);
        enableCursor(true);
        Global.paused = true;
        displaceEvent.Raise();

    }








    public void enableCursor(bool enable) {
        if (enable) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Global.cursorMode = true;
            

        } else {
             Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Global.cursorMode = false;
        }
    }
}
