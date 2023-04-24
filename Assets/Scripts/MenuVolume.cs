using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuVolume : MonoBehaviour
{
    public int MenuVolumeState;
    [CanBeNull] public GameObject MenuVolumeButton;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = 0.5f;
        MenuVolumeState = 2;
    }

    public void ButtonClick()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void SetVolume()
    {
        Debug.Log(MenuVolumeButton.GetComponent<Image>().sprite);
        MenuVolumeState = MenuVolumeState + 1;
        if (MenuVolumeState == 4)
        {
            MenuVolumeState = 1;
        }

        if (MenuVolumeState == 1)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
            MenuVolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume1");
        }
        
        if (MenuVolumeState == 2)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.5f;
            MenuVolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume2");
        }
        
        if (MenuVolumeState == 3)
        {
            gameObject.GetComponent<AudioSource>().volume = 1;
            MenuVolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume3");
        }
    }

    public void CheckMenuVolumeSprite()
    {
        if (MenuVolumeState == 1)
        {
            MenuVolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume1");
        }
        
        if (MenuVolumeState == 2)
        {
            MenuVolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume2");
        }
        
        if (MenuVolumeState == 3)
        {
            MenuVolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume3");
        }
    }

    public void CheckMenuVolume()
    {
        if (MenuVolumeState == 1)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
        }
        
        if (MenuVolumeState == 2)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.5f;
        }
        
        if (MenuVolumeState == 3)
        {
            gameObject.GetComponent<AudioSource>().volume = 1;
        }
    }
}
