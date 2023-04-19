using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public GameObject button;
    public GameObject text;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        for (i = 1; i < 21; i++)
        {
            GameObject button = new GameObject("Button" + i);
            button.AddComponent<RectTransform>();
            button.AddComponent<Image>();
            button.AddComponent<Button>();
            button.transform.SetParent(GameObject.Find("Canvas").transform);
            GameObject text = new GameObject(i.ToString());
            text.AddComponent<CanvasRenderer>();
            text.AddComponent<TextMeshProUGUI>();
            //Destroy(text.GetComponent<MeshRenderer>());
            text.transform.SetParent(button.transform);
            text.GetComponent<RectTransform>().localScale = new Vector2(1f, 1f);
            text.GetComponent<TextMeshProUGUI>().text = i.ToString();

            button.GetComponent<RectTransform>().localScale = new Vector2(0.4f, 0.4f);
            button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Button1");
            
            text.GetComponent<RectTransform>().anchorMax = Vector2.one;
            text.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            text.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            
            text.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            text.GetComponent<TextMeshProUGUI>().fontSize = 60;
            text.GetComponent<TextMeshProUGUI>().font = Resources.Load<TMP_FontAsset>("Assets/Resources/NiceSugarFont");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
