using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorChange : MonoBehaviour
{
    public GameObject ghost;
    private bool clicked;
    private Quaternion OGrotation;
    private Vector2 OGposition;
    private bool inPosition;
    private GameObject Pivotpoint;
    private List<GameObject> Ghosts = new List<GameObject>();
    public int GhostNr;
    private bool GhostReverse;
    private List<GameObject> Doors = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        OGrotation = gameObject.transform.rotation;
        OGposition = gameObject.transform.position;
        inPosition = false;
        Pivotpoint = GameObject.Find(gameObject.name + "Pivotpoint");
        GhostNr = 1;
        GhostReverse = false;
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            Doors.Add(door);
        }
        Debug.Log(Doors.Count);

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

        //Doors.Remove(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ghost = GameObject.Find("Ghost" + GhostNr + gameObject.name);
        if (clicked == true && GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission == false)
        {
            Debug.Log(GhostNr);
            foreach (GameObject door in Doors)
            {
                if (Vector2.Distance(door.transform.position, ghost.transform.position) < 0.01 && door.name != gameObject.name)
                {
                    if (GhostReverse)
                    {
                        GhostNr = GhostNr + 1;
                    }

                    if (GhostReverse == false)
                    {
                        GhostNr = GhostNr - 1;
                    }
                    Debug.Log(door.name + Vector2.Distance(door.transform.position, ghost.transform.position));
                    Debug.Log("Something in the way!");
                }
            }
            DoorRotate();
        }
        else
        {
            if (clicked == true &&
                GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission == true)
            {
                clicked = false;
            }
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
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.back, 1);
                Pivotpoint.transform.Rotate(Vector3.back, Space.Self);
                break;
            }
            if (gameObject.transform.rotation.eulerAngles.z < ghost.transform.rotation.eulerAngles.z)
            {
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.forward, 1);
                Pivotpoint.transform.Rotate(Vector3.forward, Space.Self);
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
