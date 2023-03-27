using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public MouseScript MouseScript;
    public GameObject Mouse;
    public static int level;
    // Start is called before the first frame update
    void Start()
    {
        Mouse = GameObject.Find("Mouse");
        Debug.Log(Mouse);
        MouseScript = Mouse.GetComponent<MouseScript>();
        Debug.Log(MouseScript);
        level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() {
        MouseScript.MoveX = 0;
        MouseScript.MoveY = 0;
        Mouse.transform.rotation = Quaternion.Euler(0, 0, 0);
        MouseScript.MovePermission = false;
        Mouse.transform.position = MouseScript.StartPosition;
        MouseScript.LeftGate.transform.position = MouseScript.LeftGate.GetComponent<GateOpen>().LeftGateOGpos1;
        MouseScript.RightGate.transform.position = MouseScript.RightGate.GetComponent<GateOpen>().RightGateOGpos1;
        Mouse.GetComponent<Animator>().enabled = false;
    }

    public void Play()
    {
        level = level + 1;
        SceneManager.LoadScene(level);
    }
}
