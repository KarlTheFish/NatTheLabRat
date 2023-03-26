using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private MouseScript MouseScript;
    public GameObject Mouse;
    // Start is called before the first frame update
    void Start()
    {
        Mouse = GameObject.Find("Mouse");
        MouseScript = Mouse.GetComponent<MouseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset() {
        Debug.Log("CYKA BLYAT!");
        MouseScript.MoveX = 0;
        MouseScript.MoveY = 0;
        Mouse.transform.rotation = Quaternion.Euler(0, 0, 0);
        MouseScript.MovePermission = false;
        Mouse.transform.position = MouseScript.StartPosition;
        MouseScript.LeftGate.transform.position = MouseScript.LeftGate.GetComponent<GateOpen>().LeftGateOGpos1;
        MouseScript.RightGate.transform.position = MouseScript.RightGate.GetComponent<GateOpen>().RightGateOGpos1;
        Mouse.GetComponent<Animator>().enabled = false;
    }
}
