using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public MouseScript MouseScript;
    public GameObject Mouse;
    public GameObject Gates;
    public bool GameStarted;
    public GameObject Paused;
    public GameObject Success;
    public GameObject SuccessWindow;
    public bool PlayerPressed;
    public GameObject Fade;
    public int Seconds;
    public int Minutes;
    private bool colorBool;
    private bool fadeComplete = false; // This checks if UI helper fadeout is complete

    [CanBeNull] private Color highlightColor;
    
    private List<GameObject> Doors = new List<GameObject>();
    private List<GameObject> Highlights = new List<GameObject>();

    [CanBeNull] public GameObject Mirror;
    [CanBeNull] public GameObject guideText;

    // Start is called before the first frame update
    void Start() {
        Fade = GameObject.Find("FadeOverlay");
    }

    public void FadeIn() {
        Fade.SetActive(true);
        Fade.GetComponent<FadeScript>().StartCoroutine(nameof(FadeScript.FadeIn));
    }

    public void GameStart() {
        Debug.Log("gamestart");
        Seconds = 0;
        Minutes = 0;
        Mouse = GameObject.Find("Mouse");
        MouseScript = Mouse.GetComponent<MouseScript>();

        Gates = GameObject.Find("Gates"); // Get the Gates object
        
        GameStarted = false;
        
        // This is a bit of a hacky way to get the Mirror object, but it works
        if(GameObject.FindWithTag("Mirror") != null) {
            Mirror = GameObject.FindWithTag("Mirror"); //REMINDER: This only finds one object, modify to make it find all of them
        }
        
        // Find the Paused and Success objects and set them to inactive
        Paused = GameObject.Find("Paused");
        Paused.SetActive(false);

        Success = GameObject.Find("Success");
        SuccessWindow = GameObject.Find("SuccessWindow");
        Success.SetActive(false);

        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door")) {
            Doors.Add(door);
        }
        foreach (GameObject highlight in GameObject.FindGameObjectsWithTag("Highlight")) {
            Highlights.Add(highlight);
        }
        
        if(GameObject.Find("GuideText") != null) {
            guideText = GameObject.Find("GuideText");
            guideText.transform.SetAsFirstSibling();
        }
        // Volume stuff
        GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().CheckVolume();
        GameObject.Find("MenuSound").GetComponent<MenuVolume>().CheckMenuVolume();

        StartCoroutine(Timer());
    }

    // This is called when the player presses the reset button and resets the level
    public void Reset() {
        MouseScript.MoveX = 0;
        MouseScript.MoveY = 0;
        Mouse.transform.rotation = Quaternion.Euler(0, 0, 0);
        MouseScript.MovePermission = false;
        Mouse.transform.position = MouseScript.StartPosition;
        Mouse.GetComponent<CircleCollider2D>().offset = new Vector2(0, -0.2f);
        MouseScript.LeftGate.transform.position = MouseScript.LeftGate.GetComponent<GateOpen>().LeftGateOGpos1;
        MouseScript.RightGate.transform.position = MouseScript.RightGate.GetComponent<GateOpen>().RightGateOGpos1;
        Mouse.GetComponent<Animator>().enabled = false;
        GameStarted = false;
        Mouse.GetComponent<SpriteRenderer>().flipY = false;
        if(PlayerPressed == true && Mirror != null) {
            Mirror.transform.position = Mirror.GetComponent<MirrorScript>().OGpos;
        }
        if(Success.activeSelf == true) {
            Success.SetActive(false);
        }
        foreach (GameObject door in Doors)
        {
            door.GetComponent<DoorChange>().Reset();
        }
    }
    
    public void PlayerPress() {
        PlayerPressed = true;
    }
    
    public void Play() {
        PlayerPressed = false;
        GameObject.Find("AudioPlayer").GetComponent<Script>().LevelIndexGlobal = SceneManager.GetActiveScene().buildIndex + 1;
        FadeIn();
    }
    
    public void StartGame() {
        if (GameStarted == false) { 
            StartCoroutine(GuideFadeOut()); //TODO: Make it so this also starts when door is rotated
            Gates.GetComponent<GateOpen>().GateOpened = true; 
            Mouse.GetComponent<MouseScript>().MoveY = -4;
            GameStarted = true;
        }
        else {
            Debug.Log("You can't start it again, you silly silly silly silly silly silly silly goose!");
            // This is a debug message that I put in to make sure that the player can't start the game twice
        }
    }

    //This is called when the player presses the pause button
    public void PauseMenu() {
        Paused.SetActive(!Paused.activeSelf);
        if (Paused.activeSelf) {
            StopCoroutine(Timer());
            GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().VolumeButton = GameObject.Find("MusicLevel");
            GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().CheckVolumeSprite();
            GameObject.Find("MenuSound").GetComponent<MenuVolume>().MenuVolumeButton = GameObject.Find("MenuLevel");
            GameObject.Find("MenuSound").GetComponent<MenuVolume>().CheckMenuVolumeSprite();
            MouseScript.MovePermission = false;
        }
        else {
            StartCoroutine(Timer());
            if(GameStarted) {
                MouseScript.MovePermission = true;
            }
        }
    }

    public void LevelMenu() {
        GameObject.Find("AudioPlayer").GetComponent<Script>().LevelIndexGlobal = 17; //REMINDER: CHANGE THIS TO 21 ONCE DONE
        //REMINDER: ANOTHER PLACE TO BE CHANGED IN Script.cs LINE 33
        FadeIn();
    }

    public void VolumeChange() {
        GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().SetVolume();
    }
    
    public void MenuVolumeChange() {
        GameObject.Find("MenuSound").GetComponent<MenuVolume>().SetVolume();
    }

    public void PlayButtonSound() {
        GameObject.Find("MenuSound").GetComponent<AudioSource>().Play();
    }
    
    public void LevelComplete() {
        //StopCoroutine(Timer());
        Success.SetActive(true);
        GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission = false;
        StartCoroutine(SuccessWindowMove());
        GameObject.Find("MenuSound").GetComponent<LevelsCompleted>().LogLevel();
        if(Seconds < 10) {
            GameObject.Find("Seconds").GetComponent<TextMeshProUGUI>().text = "0" + Seconds.ToString();
        }
        else {
            GameObject.Find("Seconds").GetComponent<TextMeshProUGUI>().text = Seconds.ToString();
        }
        if(Minutes < 10) {
            GameObject.Find("Minutes").GetComponent<TextMeshProUGUI>().text = "0" + Minutes.ToString();
        }
        else {
            GameObject.Find("Minutes").GetComponent<TextMeshProUGUI>().text = Minutes.ToString();
        }
    }
    
    public IEnumerator Timer() {
        while (Mouse.activeSelf == true)
        {
            yield return new WaitForSecondsRealtime(1);
            Seconds++;
            if (Seconds == 60)
            {
                Minutes++;
                Seconds = 0;
            }
        }
    }

    public IEnumerator SuccessWindowMove() {
        while (SuccessWindow.GetComponent<RectTransform>().anchoredPosition != Vector2.zero) {
            SuccessWindow.GetComponent<RectTransform>().anchoredPosition = Vector2.MoveTowards(SuccessWindow.GetComponent<RectTransform>().anchoredPosition, Vector2.zero, 3000 * Time.deltaTime);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    IEnumerator GuideFadeOut() {
        while (fadeComplete == false && guideText != null) {
            guideText.GetComponent<CanvasGroup>().alpha -= 0.05f;
            yield return new WaitForSecondsRealtime(0.02f);
            if (guideText.GetComponent<CanvasGroup>().alpha <= 0) {
                fadeComplete = true;
                break;
            }
        }
        StopCoroutine(GuideFadeOut());
    }
}
