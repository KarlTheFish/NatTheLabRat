using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class lvlButtonScript : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ButtonClick()
    {
        GameObject.Find("AudioPlayer").GetComponent<Script>().LevelIndexGlobal = index;
        GameObject.Find("GameManager").GetComponent<Manager>().FadeIn();
    }
}
