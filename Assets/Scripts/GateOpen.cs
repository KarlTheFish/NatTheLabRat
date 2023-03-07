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
    private GameObject LeftGateGhost;
    private GameObject RightGateGhost;
    private bool Complete = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        LeftGate = GameObject.Find("LeftGate");
        RightGate = GameObject.Find("RightGate");
        LeftGateGhost = GameObject.Find("LeftGateGhost");
        RightGateGhost = GameObject.Find("RightGateGhost");
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
        }
        
        
    }

    private void OnMouseDown()
    {
        GateOpened = true;
        GameObject.Find("Mouse").GetComponent<MouseScript>().MoveY = -4;
    }

    IEnumerator MoveGates()
    {
        while (LeftGate.transform.position != LeftGateGhost.transform.position || RightGate.transform.position != RightGateGhost.transform.position)
        {
            LeftGate.transform.position = Vector3.MoveTowards(LeftGate.transform.position, LeftGateGhost.transform.position, Time.deltaTime * 0.2f);
            RightGate.transform.position = Vector3.MoveTowards(RightGate.transform.position, RightGateGhost.transform.position, Time.deltaTime * 0.2f);
            yield return null;
        }

        Complete = true;
        GateOpened = false;
        GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission = true;
    }

    void OpenGate()
    {
        StartCoroutine(MoveGates());
    }
}
