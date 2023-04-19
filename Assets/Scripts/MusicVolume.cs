using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    public static int VolumeState;
    [CanBeNull] public GameObject VolumeButton;


    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0.5f;
        gameObject.GetComponent<AudioSource>().volume = 0.5f;
        gameObject.GetComponent<AudioSource>().Stop();
        
        VolumeState = 2;
    }

    void Update()
    {
    }

    public void SetVolume()
    {
        Debug.Log(VolumeButton.GetComponent<Image>().sprite);
        VolumeState = VolumeState + 1;
        if (VolumeState == 4)
        {
            VolumeState = 1;
        }

        if (VolumeState == 1)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0;
            VolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume1");
        }
        
        if (VolumeState == 2)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.5f;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 0.5f;
            VolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume2");
        }
        
        if (VolumeState == 3)
        {
            gameObject.GetComponent<AudioSource>().volume = 1;
            GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = 1;
            VolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume3");
        }
    }

    public void CheckVolumeSprite()
    {
        if (VolumeState == 1)
        {
            VolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume1");
        }
        
        if (VolumeState == 2)
        {
            VolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume2");
        }
        
        if (VolumeState == 3)
        {
            VolumeButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("volume3");
        }
    }

    public void CheckVolume()
    {
        if (VolumeState == 1)
        {
            gameObject.GetComponent<AudioSource>().volume = 0;
        }
        
        if (VolumeState == 2)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.5f;
        }
        
        if (VolumeState == 3)
        {
            gameObject.GetComponent<AudioSource>().volume = 1;
        }
    }
}
