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
    public static int level;
    public GameObject Gates;
    public bool GameStarted;
    public GameObject Paused;
    public bool PlayerPressed;
    
    [CanBeNull] public GameObject Mirror;
    // Start is called before the first frame update
    void Start()
    {
        Mouse = GameObject.Find("Mouse");
        MouseScript = Mouse.GetComponent<MouseScript>();
        level = 1;
        
        Gates = GameObject.Find("Gates");
        
        GameStarted = false;
        
        if(GameObject.FindWithTag("Mirror") != null) {
            Mirror = GameObject.FindWithTag("Mirror");
        }
        
        Paused = GameObject.Find("Paused");
        Paused.SetActive(false);
        
        GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().CheckVolume();
        GameObject.Find("MenuSound").GetComponent<MenuVolume>().CheckMenuVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }

    public void PlayerPress()
    {
        PlayerPressed = true;
    }

    public void Play()
    {
        level = level + 1;
        SceneManager.LoadScene(level);
        PlayerPressed = false;
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
        }
    }

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
}
