using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public Color color;
    public bool isMenu = true;
    
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(0, 0, 0, 0);
        gameObject.GetComponent<Image>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeIn()
    {
        while (gameObject.GetComponent<Image>().color.a < 1.0f)
        {
            color.a = color.a + 0.05f;
            gameObject.GetComponent<Image>().color = color;
            yield return new WaitForSecondsRealtime(0.15f);
            if (gameObject.GetComponent<Image>().color.a >= 1.0f)
            {
                break;
            }
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject.Find("GameManager").GetComponent<Manager>().GameStart();
        StopCoroutine(FadeIn());
    }
}
