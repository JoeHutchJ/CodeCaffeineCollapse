using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public bool enabled; 

    public bool defaultEnable = true;

    public Camera pauseCamera;
    public Camera mainCamera; 

    public PauseMenu pauseMenu;

    public Transform cameraTarget;

    public Transform StartMenuContent;

    public BoolEvent mainCamEvent;

    public BoolEvent hideUIEvent;

    public Event displaceEvent;

    public Event returnEvent;

    public Event startObjectives;

    public Event displayDay;

    public bool firstDay;

    public Event ResetDay;

    
    // Start is called before the first frame update
    void Start()
    {
        pauseCamera = GetComponentInChildren<Camera>();
        mainCamera = Camera.main;
        StartMenuContent = GetChildByName.Get(gameObject, "start menu").transform;
        StartMenuContent.gameObject.SetActive(false);
        cameraTarget = GetChildByName.Get(gameObject, "CameraTarget").transform;
        pauseCamera.GetComponent<CameraGo>().hideCam(true);
        if (defaultEnable) {
        Enable();
        } else {
            displayDay.Raise();
        }

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
        StopAllCoroutines();
        enabled = true;
        if (mainCamera  != null) {
            pauseCamera.transform.position = mainCamera.transform.position;
            pauseCamera.transform.rotation = mainCamera.transform.rotation;
            pauseCamera.GetComponent<CameraGo>().Go(cameraTarget);
            //mainCamEvent.Raise(false);
            mainCamera.enabled = false;
            mainCamera.gameObject.GetComponent<AudioListener>().enabled = false;
            pauseCamera.GetComponent<CameraGo>().hideCam(false);
            
            

        }
        //ResetDay.Raise();
        
        hideUIEvent.Raise(true);
        StartMenuContent.gameObject.SetActive(true);
        enableCursor(true);
        Global.paused = true;
        displaceEvent.Raise();



    }

    public void EnableFromPause(Camera cam) {
        //move camera
        //disable main camera. 
        StopAllCoroutines();
        enabled = true;
        if (cam  != null) {
            pauseCamera.transform.position = cam.transform.position;
            pauseCamera.transform.rotation = cam.transform.rotation;
            pauseCamera.GetComponent<CameraGo>().Go(cameraTarget);
            //mainCamEvent.Raise(false);

            cam.enabled = false;
            cam.gameObject.GetComponent<AudioListener>().enabled = false;
            pauseCamera.GetComponent<CameraGo>().hideCam(false);
            
            

        }
        hideUIEvent.Raise(true);
        StartMenuContent.gameObject.SetActive(true);
        enableCursor(true);
        Global.paused = true;
        displaceEvent.Raise();



    }

    public void GoToPause() {

        Time.timeScale = 1.0f;
        enabled = false;
        //move camera...
        pauseCamera.GetComponent<CameraGo>().hideCam(true);
        StopAllCoroutines();
        StartCoroutine(waitTilHide(1));
        pauseMenu.EnableFromStart(pauseCamera);
        
        //StartMenuContent.gameObject.SetActive(false);
        

    }

    public void StartMonday() {
        if (Global.started) {
            Global.started = false;
            Global.resetToMonday();
            SceneManager.LoadScene("Main");
        } else {
            StartGame();
        }


    }

    public void StartGame() {

        Global.started = true;

        enabled = false;

        if (Global.dayIndex == 0) {
            firstDay = true;
        } else {
            firstDay = false;
        }



        displayDay.Raise();
        //move camera...
        pauseCamera.GetComponent<CameraGo>().GoBack(mainCamera.transform);

        //enable UI...
        hideUIEvent.Raise(false);

        returnEvent.Raise();

        ResetDay.Raise();

        //hide (hideAllchildren)
        StartMenuContent.gameObject.SetActive(false);

        enableCursor(false);

        Global.paused = false;

        if (Global.currentDay == "Monday") {
        startObjectives.Raise();
        }

        //unlock cursor

    }

    IEnumerator waitTilHide(int delay) {

        Debug.Log("hide");

        yield return new WaitForSecondsRealtime(delay);

        Debug.Log("hide");

        StartMenuContent.gameObject.SetActive(false);

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

    public void QuitButton() {
        Application.Quit();

    }
    public void ContinueGame() {
        Debug.Log(Global.leftOffice);
        if (Global.leftOffice) {
            Global.nextDay();
            Global.freeMode = true;
            Debug.Log(Global.dayIndex);
            SceneManager.LoadScene("FreeModeDay");

        } else {
        
        StartGame();

        }

    }


}
