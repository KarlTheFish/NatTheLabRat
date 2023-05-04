using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightScript : MonoBehaviour
{
    private bool GameStarted;
    bool colorBool = false;
    Color highlightColor;
    private Component component; 
    bool Clicked = false;
    
    // Start is called before the first frame update
    void Start() {
        GameStarted = GameObject.Find("GameManager").GetComponent<Manager>().GameStarted;
        highlightColor = Color.white;
        StartCoroutine(HighlightColor());
    }

    private void OnMouseDown()
    {
        Clicked = true;
        if (GameObject.FindWithTag("Door") != null) {
            GameObject.FindWithTag("Door").GetComponent<DoorChange>().clicked = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator HighlightColor() {
        Debug.Log("Highlight coroutine started");
        while (GameStarted == false && Clicked == false) {
            while (colorBool == false) {
                highlightColor.a += 0.05f;
                ColorChange();
                if(highlightColor.a >= 1) {
                    colorBool = true;
                    break;
                }
                yield return new WaitForSecondsRealtime(0.05f);
            }
            while (colorBool == true) {
                highlightColor.a -= 0.05f;
                ColorChange();
                if (highlightColor.a <= 0) {
                    colorBool = false;
                    break;
                }
                yield return new WaitForSecondsRealtime(0.05f);
            }
        }
    }

    void ColorChange() {
        if (TryGetComponent<Image>(out Image img)) {
            gameObject.GetComponent<Image>().color = highlightColor;
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().color = highlightColor;
        }
    }
}
