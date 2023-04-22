using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public GameObject button;
    public GameObject text;

    private float xPos, yPos;
    private int i;
    private int j;
    // Start is called before the first frame update
    void Start()
    {
        xPos = -120;
        yPos = 120;
        for (i = 1; i < 21; i++)
        {
            GameObject button = new GameObject("Button" + i);
            button.AddComponent<RectTransform>();
            button.AddComponent<Image>();
            button.AddComponent<Button>();
            button.AddComponent<lvlButtonScript>();
            button.GetComponent<lvlButtonScript>().index = i;
            button.transform.SetParent(GameObject.Find("Canvas").transform);
            GameObject text = new GameObject(i.ToString());
            text.AddComponent<CanvasRenderer>();
            text.AddComponent<TextMeshProUGUI>();
            text.transform.SetParent(button.transform);
            text.GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            text.GetComponent<TextMeshProUGUI>().text = i.ToString();

            button.GetComponent<RectTransform>().localScale = new Vector2(0.4f, 0.4f);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button1");
            
            if(i % 4 == 0) {
                xPos = -120;
                yPos -= 60;
            } else {
                xPos += 80;
            }
            
            text.GetComponent<RectTransform>().anchorMax = Vector2.one;
            text.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            text.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            text.GetComponent<TextMeshProUGUI>().fontSize = 60;
            text.GetComponent<TextMeshProUGUI>().font = Resources.Load<TMP_FontAsset>("NiceSugarFont");
            button.GetComponent<Transform>().SetAsFirstSibling();

            if (!GameObject.Find("MenuSound").GetComponent<LevelsCompleted>().CompletedList
                .Contains(i))
            {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
