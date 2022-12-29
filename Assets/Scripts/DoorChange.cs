using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorChange : MonoBehaviour
{
    private GameObject ghost;
    private bool clicked;
    private Quaternion OGrotation;
    private Vector2 OGposition;
    private bool inPosition;
    private GameObject Pivotpoint;

    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.Find("Ghost1" + gameObject.name);
        OGrotation = gameObject.transform.rotation;
        OGposition = gameObject.transform.position;
        inPosition = false;
        Pivotpoint = GameObject.Find(gameObject.name + "Pivotpoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked == true)
        {
            DoorRotate();
        }

        if (inPosition == true)
        {
            GhostDoorRotate();
        }

    }

    private void OnMouseDown()
    {
        clicked = true;
    }

    void DoorRotate()
    {
        while (gameObject.transform.rotation != ghost.transform.rotation)
        {
            if (gameObject.transform.rotation.eulerAngles.z > ghost.transform.rotation.eulerAngles.z)
            {
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.forward, -1);
                break;
            }
            if (gameObject.transform.rotation.eulerAngles.z < ghost.transform.rotation.eulerAngles.z)
            {
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.forward, 1);
                break;
            }
        }

        if (gameObject.transform.rotation == ghost.transform.rotation)
        {
            GhostDoorRotate();
            OGrotation = gameObject.transform.rotation;
            OGposition = gameObject.transform.position;
            clicked = false;
        }
        
    }

    private void GhostDoorRotate()
    {
        ghost.transform.position = OGposition;
        ghost.transform.rotation = OGrotation;
    }
    
}
