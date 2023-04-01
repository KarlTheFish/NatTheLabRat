using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameObject MainMenu;
    private GameObject CreditsMenu;
    private GameObject SettingsMenu;

    private bool GameStarted;
    
    
    // Start is called before the first frame update
    void Start()
    {
        MainMenu = GameObject.Find("MainMenu");
        CreditsMenu = GameObject.Find("CreditsWindow");
        //SettingsMenu = GameObject.Find("SettingsMenu");
        
        gameObject.GetComponent<Manager>().enabled = false;
        
        CreditsMenu.SetActive(false);
        
    }

    void Update()
    {
        if (GameStarted == true)
        {
            gameObject.GetComponent<Manager>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Manager>().enabled = false;
        }
    }

    public void CreditsMenuSet()
    {
        MainMenu.SetActive(!MainMenu.activeSelf);
        CreditsMenu.SetActive(!CreditsMenu.activeSelf);
    }
}
