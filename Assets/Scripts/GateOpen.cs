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
    public bool GateOpened = false;
    private GameObject LeftGate;
    private GameObject RightGate;
    private GameObject LeftGateGhost;
    private GameObject RightGateGhost;

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
    }

    // Update is called once per frame
    void Update()
    {

        if (GateOpened == true)
        {
            OpenGate();
        }
        
        
    }

    IEnumerator MoveGates()
    {
        while (LeftGate.transform.position != LeftGateGhost.transform.position || RightGate.transform.position != RightGateGhost.transform.position)
        {
            LeftGate.transform.position = Vector3.MoveTowards(LeftGate.transform.position, LeftGateGhost.transform.position, Time.deltaTime * 0.2f);
            RightGate.transform.position = Vector3.MoveTowards(RightGate.transform.position, RightGateGhost.transform.position, Time.deltaTime * 0.2f);
            yield return null;
        }
        
        GateOpened = false;
        GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission = true;
    }

    public void OpenGate()
    {
        StartCoroutine(MoveGates());
    }
}
