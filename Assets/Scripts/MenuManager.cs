using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private GameObject MainMenu;
    private GameObject CreditsMenu;
    private GameObject SettingsMenu;
    
    public GameObject AudioPlayer;

    public GameObject VolumeButton;

    private AudioClip Gameplay;

    private bool GameStarted;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        MainMenu = GameObject.Find("MainMenu");
        CreditsMenu = GameObject.Find("CreditsWindow");
        SettingsMenu = GameObject.Find("SettingsWindow");
        
        AudioPlayer = GameObject.Find("AudioPlayer");

        gameObject.GetComponent<Manager>().enabled = false;
        
        CreditsMenu.SetActive(false);
        SettingsMenu.SetActive(false);

    }

    void Update()
    {
        if (GameStarted == true)
        {
            gameObject.GetComponent<Manager>().enabled = true;
            SceneManager.LoadScene("Level1");
        }
        else
        {
            gameObject.GetComponent<Manager>().enabled = false;
        }
    }
    
    public void StartGame()
    {
        GameStarted = true;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().mute = true;
        AudioPlayer.GetComponent<AudioSource>().Play();
        AudioPlayer.GetComponent<MusicVolume>().VolumeButton = null;
    }

    public void CreditsMenuSet()
    {
        StartCoroutine(Credits());
    }
    
    public void SettingsMenuSet()
    {
        StartCoroutine(Settings());
    }

    IEnumerator Settings()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        MainMenu.SetActive(!MainMenu.activeSelf);
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
        if (SettingsMenu.activeSelf)
        {
            GameObject.Find("AudioPlayer").GetComponent<MusicVolume>().VolumeButton = GameObject.Find("MusicLevel");
            GameObject.Find("MenuSound").GetComponent<MenuVolume>().MenuVolumeButton = GameObject.Find("MenuLevel");
        }
    }

    IEnumerator Credits()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        MainMenu.SetActive(!MainMenu.activeSelf);
        CreditsMenu.SetActive(!CreditsMenu.activeSelf);
    }
}
