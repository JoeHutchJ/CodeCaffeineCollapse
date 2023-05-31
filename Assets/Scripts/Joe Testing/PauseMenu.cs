using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public BoolEvent cameraLockEvent;

    public BoolEvent PauseEvent;

    float volume;

    float mouseSensitivity;

    bool cursorModeatEnable = false;


    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
        pauseCamera = GetComponentInChildren<Camera>();
        cameraTarget = GetChildByName.Get(gameObject, "CameraTarget").transform;
        pauseCamera.GetComponent<CameraGo>().hideCam(true);
        pauseMenucontent.gameObject.SetActive(false);
        UpdateBars();
        //Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled) {
            enableCursor(true);
        }

        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (enabled) {
                if (Global.dayIndex == 0 && Global.ObjectivesStarted) {
                Disable();
                }
            } else {
                cursorModeatEnable = Global.cursorMode;
            Enable();
            }
            
        }

    }


    public void Enable() {

        if (!startMenu.enabled) {
        if (!enabled) {
            if (!Global.leftOffice) {
        enabled = true;
        PauseEvent.Raise(true);
        Global.paused = true;
        StopAllCoroutines();
        checkObjectiveStarted();

        enabled = true;
        if (mainCamera  != null) {
            pauseCamera.transform.position = mainCamera.transform.position;
            pauseCamera.transform.rotation = mainCamera.transform.rotation;
            pauseCamera.GetComponent<CameraGo>().Go(cameraTarget);
            pauseCamera.GetComponent<CameraGo>().waitingforpause = true;
            //mainCamEvent.Raise(false);
            mainCamera.enabled = false;
            mainCamera.gameObject.GetComponent<AudioListener>().enabled = false;
            
            pauseCamera.GetComponent<CameraGo>().hideCam(false);
            
            

        }
        hideUIEvent.Raise(true);
        pauseMenucontent.gameObject.SetActive(true);
        enableCursor(true);
        
        displaceEvent.Raise();


        }

        }

        }

    }


    public void Disable() {

        StopAllCoroutines();
        Debug.Log("disable");
        enabled = false;
        Time.timeScale = 1.0f;
        PauseEvent.Raise(false);
        //move camera...
        pauseCamera.GetComponent<CameraGo>().GoBack(mainCamera.transform);
        if (cursorModeatEnable) {
            pauseCamera.GetComponent<CameraGo>().enableCursor = false;
        }
        //mainCamEvent.Raise(true);
        pauseMenucontent.gameObject.SetActive(false);
        //enable UI...
        hideUIEvent.Raise(false);

        returnEvent.Raise();


        //hide (hideAllchildren)
        
        if(!isPCMode.Value) {
        //enableCursor(false);

        } else {
            pauseCamera.GetComponent<CameraGo>().enableCursor = false;
        }

        Global.paused = false;

        //unlock cursor




    }

    public void checkObjectiveStarted() {
        if (Global.dayIndex == 0) {
        GetChildByName.Get(pauseMenucontent.gameObject, "Resume").GetComponent<Button>().interactable = Global.ObjectivesStarted;
        } else {
            if (!Global.leftOffice) {
            GetChildByName.Get(pauseMenucontent.gameObject, "Resume").GetComponent<Button>().interactable = true;
            }
        }


    }

    public void goToStart() {

        StopAllCoroutines();
        Time.timeScale = 1.0f;
        enabled = false;
        //move camera...
        pauseCamera.GetComponent<CameraGo>().hideCam(true);
        startMenu.EnableFromPause(pauseCamera);
        PauseEvent.Raise(true);

        StartCoroutine(waitTilHide(1));


    }


    public void EnableFromStart(Camera cam) {

        enabled = true;

        if (cam  != null) {
            pauseCamera.transform.position = cam.transform.position;
            pauseCamera.transform.rotation = cam.transform.rotation;
            pauseCamera.GetComponent<CameraGo>().Go(cameraTarget);
            pauseCamera.GetComponent<CameraGo>().waitingforpause = true;
            //mainCamEvent.Raise(false);
            pauseCamera.GetComponent<CameraGo>().hideCam(false);

            

        }
        hideUIEvent.Raise(true);
        pauseMenucontent.gameObject.SetActive(true);
        enableCursor(true);
        Global.paused = true;
        PauseEvent.Raise(true);
        checkObjectiveStarted();
        displaceEvent.Raise();

    }


    IEnumerator waitTilHide(int delay) {

        yield return new WaitForSeconds(delay);

        pauseMenucontent.gameObject.SetActive(false);

    }






    public void enableCursor(bool enable) {
        if (enable) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Global.cursorMode = true;
            cameraLockEvent.Raise(true);
            

        } else {
             Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Global.cursorMode = false;
            cameraLockEvent.Raise(false);
        }
    }

    public void UpdateBars() {
        if (mouseSensitivity == 0) {
            mouseSensitivity = Global.mouseSensitivity;
        }

        if (volume == 0) {
            volume = Global.volume;
        }
        Global.mouseSensitivity = mouseSensitivity;
        Global.volume = volume;
        GetChildByName.Get(gameObject, "Volume").transform.Find("Fill").GetComponent<Image>().fillAmount = volume;
        GetChildByName.Get(gameObject, "Mouse Sensitivity").transform.Find("Fill").GetComponent<Image>().fillAmount = mouseSensitivity;

        GetChildByName.Get(gameObject, "CaffeineMeter").transform.GetComponentInChildren<Toggle>().isOn = Global.caffeineEnabled;

    }


    public void IncrementVolume(bool increment) {
        if (increment) {
            volume += 0.05f;
            if (volume > 1.0f) {
                volume = 1.0f;
            }
        } else {
            volume -= 0.05f;
            if (volume < 0.0f) {
                volume = 0.0f;
            }

        }

        UpdateBars();

    }


        public void IncrementMouseSens(bool increment) {
        if (increment) {
            mouseSensitivity += 0.05f;
            if (mouseSensitivity > 1.0f) {
                mouseSensitivity = 1.0f;
            }
        } else {
            mouseSensitivity -= 0.05f;
            if (mouseSensitivity < 0.0f) {
                mouseSensitivity = 0.0f;
            }

        }

        UpdateBars();

    }


    public void CaffeineMeterToggled(bool toggle) {
        Global.caffeineEnabled = toggle;

    }
}
