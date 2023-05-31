using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public GameObject Fade;

    public int LevelIndexGlobal;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        Fade = GameObject.Find("FadeOverlay");
        Fade.GetComponent<FadeScript>().color = new Color(0, 0, 0, 0);
        Invoke("FadeInactive", 0.1f);
    }

    void FadeInactive()
    {
        Fade.SetActive(false);
    }

    public IEnumerator FadeManager()
    {
        StopCoroutine(GameObject.Find("FadeOverlay").GetComponent<FadeScript>().FadeIn());
        yield return new WaitForSecondsRealtime(0.1f);
        Fade = GameObject.Find("FadeOverlay");
        Fade.GetComponent<FadeScript>().color = new Color(0, 0, 0, 1);
        Fade.GetComponent<Image>().color = Fade.GetComponent<FadeScript>().color;
        if (GameObject.Find("AudioPlayer").GetComponent<Script>().LevelIndexGlobal < 18) //REMINDER: SET TO 21 WHEN DONE!!!!!!!!
        {
            GameObject.Find("GameManager").GetComponent<Manager>().GameStart();
        }
        while (Fade.GetComponent<Image>().color.a > 0.0f)
        {
            Fade.GetComponent<FadeScript>().color.a = Fade.GetComponent<FadeScript>().color.a - 0.07f;
            Fade.GetComponent<Image>().color = Fade.GetComponent<FadeScript>().color;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        if (Fade.GetComponent<Image>().color.a <= 0.0f)
        {
            Fade.GetComponent<FadeScript>().isMenu = false;
            Fade.SetActive(false);
            StopCoroutine(FadeManager());
        }
    }
}
