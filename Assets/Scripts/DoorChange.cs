using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorChange : MonoBehaviour
{    
    public struct DoorPositionStart {
         public Vector2 position;
         public Quaternion rotation;
    }
    
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
    
    public DoorPositionStart doorPositionStart;

    //TODO: Add sound effect when door has finished moving
    //TODO: is the doors list really necessary? Try removing it and see if it still works 
    //TODO: It seems that it would be much better to put the list of doors into manager script and use that for resetting the doors

    // Start is called before the first frame update
    void Start()
    {
        OGrotation = gameObject.transform.rotation;
        OGposition = gameObject.transform.position;
        inPosition = false;
        Pivotpoint = GameObject.Find(gameObject.name + "Pivotpoint");
        GhostNr = 1;
        GhostReverse = false;
        
        doorPositionStart.position = gameObject.transform.position;
        doorPositionStart.rotation = gameObject.transform.rotation;
        
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            Doors.Add(door);
        }

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
        ghost = GameObject.Find("Ghost" + GhostNr + gameObject.name);
        if (clicked == true && GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission == false)
        {
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
                }
            }

            StartCoroutine(DoorRotate());
            clicked = false;
        }
        else
        {
            if (clicked == true &&
                GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission == true)
            {
                clicked = false;
            }
        }
        
        //These if statements are to make sure the ghost number doesn't go out of bounds
        
        if (GhostNr > Ghosts.Count)
        {
            GhostNr = Ghosts.Count;
            GhostReverse = true;
        }
        else
        {
            if (GhostNr < 1)
            {
                GhostNr = 1;
                GhostReverse = false;
            }
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

    //This coroutine is to rotate the door to the correct position
    IEnumerator DoorRotate()
    {
        while (gameObject.transform.rotation != ghost.transform.rotation)
        {
            if (gameObject.transform.rotation.eulerAngles.z > ghost.transform.rotation.eulerAngles.z)
            {
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.back, 1);
                Pivotpoint.transform.Rotate(Vector3.back, Space.Self);
                yield return new WaitForSecondsRealtime(0.02f);
                //break;
            }
            if (gameObject.transform.rotation.eulerAngles.z < ghost.transform.rotation.eulerAngles.z)
            {
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.forward, 1);
                Pivotpoint.transform.Rotate(Vector3.forward, Space.Self);
                yield return new WaitForSecondsRealtime(0.02f);
                //break;
            }
            if (gameObject.transform.rotation.eulerAngles.z - ghost.transform.rotation.eulerAngles.z < 1 &&
                gameObject.transform.rotation.eulerAngles.z - ghost.transform.rotation.eulerAngles.z > -1)
            {
                Pivotpoint.transform.Rotate(Vector3.forward, Space.Self);
                gameObject.transform.rotation = ghost.transform.rotation;
                break;
            }
        }
        Debug.Log(ghost);
        //This if statement is to make sure the door is in the correct position
        if (Vector2.Distance(gameObject.transform.position, ghost.transform.position) < 1)
        {
            Debug.Log("Distance");
            GhostDoorRotate();
            OGrotation = gameObject.transform.rotation;
            OGposition = gameObject.transform.position;
            //clicked = false;
            if (GhostReverse == false)
            {
                GhostNr = GhostNr + 1;
            }

            if (GhostReverse == true)
            {
                GhostNr = GhostNr - 1;
            }
            StopCoroutine(DoorRotate());
        }
        
    }

    private void GhostDoorRotate()
    {
        ghost.transform.position = OGposition;
        ghost.transform.rotation = OGrotation;
    }
    
}
