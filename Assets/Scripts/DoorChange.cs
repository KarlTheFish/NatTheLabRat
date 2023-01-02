using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private List<GameObject> Ghosts = new List<GameObject>();
    private int GhostNr;
    private bool GhostReverse;

    // Start is called before the first frame update
    void Start()
    {
        OGrotation = gameObject.transform.rotation;
        OGposition = gameObject.transform.position;
        inPosition = false;
        Pivotpoint = GameObject.Find(gameObject.name + "Pivotpoint");
        GhostNr = 1;
        GhostReverse = false;

        for (int i = 1; i < 4; i++)
        {
            if (GameObject.Find("Ghost" + i + gameObject.name) != null)
            {
                Ghosts.Add(GameObject.Find("Ghost" + i + gameObject.name));
            }
            else
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked == true)
        {
            ghost = GameObject.Find("Ghost" + GhostNr + gameObject.name);
            DoorRotate();
            Debug.Log(GhostNr);
        }
        
        if (GhostNr > Ghosts.Count)
        {
            GhostNr = Ghosts.Count;
            GhostReverse = true;
        }

        if (GhostNr < 1)
        {
            GhostNr = 1;
            GhostReverse = false;
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
            if (GhostReverse == false)
            {
                GhostNr = GhostNr + 1;
            }

            if (GhostReverse == true)
            {
                GhostNr = GhostNr - 1;
            }
        }
        
    }

    private void GhostDoorRotate()
    {
        ghost.transform.position = OGposition;
        ghost.transform.rotation = OGrotation;
    }
    
}
