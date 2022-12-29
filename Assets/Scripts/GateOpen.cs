using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    public Quaternion LeftGateOGpos;
    public Quaternion RightGateOGpos;
    public Vector2 LeftGateOGpos1;
    public Vector2 RightGateOGpos1;
    private bool GateOpened = false;
    private GameObject LeftGate;
    private GameObject RightGate;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        LeftGate = GameObject.Find("LeftGate");
        RightGate = GameObject.Find("RightGate");
        LeftGateOGpos = LeftGate.transform.rotation;
        RightGateOGpos = RightGate.transform.rotation;
        LeftGateOGpos1 = LeftGate.transform.position;
        RightGateOGpos1 = RightGate.transform.position;
        GameObject.Find("Mouse").GetComponent<MouseScript>().RotateReps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GateOpened == false)
        {
            LeftGate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            RightGate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (GateOpened == true)
        {
            LeftGate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            RightGate.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            OpenGate();
            if (GameObject.Find("Mouse").GetComponent<MouseScript>().RotateReps == 18)
            {
                GateOpened = false;
            }
        }
        
        
    }

    private void OnMouseDown()
    {
        GateOpened = true;
        GameObject.Find("Mouse").GetComponent<MouseScript>().MoveY = -4;
    }

    void OpenGate()
    {
        while (GameObject.Find("Mouse").GetComponent<MouseScript>().RotateReps < 20)
        {
            //GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission = false;
            LeftGate.transform.Rotate(Vector3.forward, 5);
            RightGate.transform.Rotate(Vector3.forward, -5);
            GameObject.Find("Mouse").GetComponent<MouseScript>().RotateReps = GameObject.Find("Mouse").GetComponent<MouseScript>().RotateReps + 1;
            break;
        }
        GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission = true;
    }
}
