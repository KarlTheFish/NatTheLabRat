using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public MouseScript MouseScript;
    public GameObject Mouse;
    public GameObject Gates;
    public bool GameStarted;
    public GameObject Paused;
    public GameObject Success;
    public bool PlayerPressed;
    public GameObject Fade;

    [CanBeNull] public GameObject Mirror;

    // Start is called before the first frame update
    void Start()
    {
        Fade = GameObject.Find("FadeOverlay");
    }

    public void FadeIn()
    {
        Fade.SetActive(true);
        Fade.GetComponent<FadeScript>().StartCoroutine(nameof(FadeScript.FadeIn));
    }

    public void GameStart()
    {
        Mouse = GameObject.Find("Mouse");
        MouseScript = Mouse.GetComponent<MouseScript>();

        Gates = GameObject.Find("Gates"); // Get the Gates object
        
        GameStarted = false;
        
        // This is a bit of a hacky way to get the Mirror object, but it works
        if(GameObject.FindWithTag("Mirror") != null) {
            Mirror = GameObject.FindWithTag("Mirror");
        }
        
        // Find the Paused and Success objects and set them to inactive
        Paused = GameObject.Find("Paused");
        Paused.SetActive(false);

        Success = GameObject.Find("Success");
        Success.SetActive(false);
        
                
        // Volume stuff
        GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().CheckVolume();
        GameObject.Find("MenuSound").GetComponent<MenuVolume>().CheckMenuVolume();
    }

    // This is called when the player presses the reset button and resets the level
    public void Reset() {
        MouseScript.MoveX = 0;
        MouseScript.MoveY = 0;
        Mouse.transform.rotation = Quaternion.Euler(0, 0, 0);
        MouseScript.MovePermission = false;
        Mouse.transform.position = MouseScript.StartPosition;
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
    }
    
    public void PlayerPress()
    {
        PlayerPressed = true;
    }
    
    public void Play()
    {
        PlayerPressed = false;
        FadeIn();
        //Fade.SetActive(false);
    }
    
    public void StartGame()
    {
        if (GameStarted == false) {
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
    public void PauseMenu()
    {
        Paused.SetActive(!Paused.activeSelf);
        if (Paused.activeSelf)
        {
            GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().VolumeButton = GameObject.Find("MusicLevel");
            GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().CheckVolumeSprite();
            GameObject.Find("MenuSound").GetComponent<MenuVolume>().MenuVolumeButton = GameObject.Find("MenuLevel");
            GameObject.Find("MenuSound").GetComponent<MenuVolume>().CheckMenuVolumeSprite();
        }
    }

    public void VolumeChange()
    {
        GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().SetVolume();
    }
    
    public void MenuVolumeChange()
    {
        GameObject.Find("MenuSound").GetComponent<MenuVolume>().SetVolume();
    }

    public void PlayButtonSound()
    {
        GameObject.Find("MenuSound").GetComponent<AudioSource>().Play();
    }
    
    public void LevelComplete()
    {
        Success.SetActive(true);
        GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission = false;
        GameObject.Find("MenuSound").GetComponent<LevelsCompleted>().LogLevel();
    }
}
