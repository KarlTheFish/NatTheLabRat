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

    public bool moving = false;
    private int j;
    public GameObject ghost;
    public bool clicked;
    private Quaternion OGrotation;
    private Vector2 OGposition;
    private bool inPosition;
    private GameObject Pivotpoint;
    private List<GameObject> Ghosts = new List<GameObject>();
    public int GhostNr;
    private bool GhostReverse;

    public List<DoorPositionStart> ghostPositionStarts = new List<DoorPositionStart>();

    public DoorPositionStart doorPositionStart;
    public DoorPositionStart pivotpointPositionStart;
    public DoorPositionStart StartPosGhost;

    //TODO: Add sound effect when door has finished moving
    //TODO: Make mouse unable to move when the door is moving
    
    // Start is called before the first frame update
    void Start() {
        OGrotation = gameObject.transform.rotation;
        OGposition = gameObject.transform.position;
        inPosition = false;
        Pivotpoint = GameObject.Find(gameObject.name + "Pivotpoint");
        GhostNr = 1;
        GhostReverse = false;
        
        doorPositionStart.position = gameObject.transform.position;
        doorPositionStart.rotation = gameObject.transform.rotation;
        
        pivotpointPositionStart.position = Pivotpoint.transform.position;
        pivotpointPositionStart.rotation = Pivotpoint.transform.rotation;

        for (int i = 1; i < 4; i++) {
            if (GameObject.Find("Ghost" + i + gameObject.name) != null) {
                Ghosts.Add(GameObject.Find("Ghost" + i + gameObject.name));
                ghostPositionStarts.Add(new DoorPositionStart());
                StartPosGhost.position = Ghosts[i - 1].transform.position;
                StartPosGhost.rotation = Ghosts[i - 1].transform.rotation;
                ghostPositionStarts[i-1] = StartPosGhost;
            }
            else {
                break;
            }
        }
    }

    public void Reset() {
        j = 0;
        GhostNr = 1;
        GhostReverse = false;
        StopAllCoroutines();
        gameObject.transform.position = doorPositionStart.position;
        gameObject.transform.rotation = doorPositionStart.rotation;
        Pivotpoint.transform.position = pivotpointPositionStart.position;
        Pivotpoint.transform.rotation = pivotpointPositionStart.rotation;
        foreach(GameObject ghost1 in Ghosts){
            StartPosGhost = ghostPositionStarts[j];
            ghost1.transform.position = StartPosGhost.position;
            ghost1.transform.rotation = StartPosGhost.rotation;
            j++;
        }
        OGrotation = gameObject.transform.rotation;
        OGposition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update() {
        ghost = GameObject.Find("Ghost" + GhostNr + gameObject.name);
        if (clicked == true && GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission == false && moving == false) {
            if (Vector2.Distance(gameObject.transform.position, ghost.transform.position) < 0.01) {
                    if (GhostReverse) {
                        GhostNr = GhostNr + 1;
                    }
                    else if (GhostReverse == false) {
                        GhostNr = GhostNr - 1;
                    }
            }

            StartCoroutine(DoorRotate());
            clicked = false;
        }
        else {
            if (clicked == true &&
                GameObject.Find("Mouse").GetComponent<MouseScript>().MovePermission == true) {
                clicked = false;
            }
            else {
                if (clicked == true && moving == true) {
                    clicked = false;
                }
            }
        }
        
        //These if statements are to make sure the ghost number doesn't go out of bounds
        
        if (GhostNr > Ghosts.Count) {
            GhostNr = Ghosts.Count;
            GhostReverse = true;
        }
        else {
            if (GhostNr < 1) {
                GhostNr = 1;
                GhostReverse = false;
            }
        }

        if (inPosition == true) {
            GhostDoorRotate();
        }

    }
    
    private void OnMouseDown() {
        clicked = true;
    }

    //This coroutine is to rotate the door to the correct position
    IEnumerator DoorRotate() {
        while (gameObject.transform.rotation != ghost.transform.rotation) {
            moving = true;
            if (gameObject.transform.rotation.eulerAngles.z > ghost.transform.rotation.eulerAngles.z) {
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.back, 1);
                Pivotpoint.transform.RotateAround(Pivotpoint.transform.position, Vector3.back, 1);
                yield return new WaitForSecondsRealtime(0.02f);
            }
            if (gameObject.transform.rotation.eulerAngles.z < ghost.transform.rotation.eulerAngles.z) {
                gameObject.transform.RotateAround(Pivotpoint.transform.position, Vector3.forward, 1);
                Pivotpoint.transform.RotateAround(Pivotpoint.transform.position, Vector3.forward, 1);
                yield return new WaitForSecondsRealtime(0.02f);
            }
            if (gameObject.transform.rotation.eulerAngles.z - ghost.transform.rotation.eulerAngles.z < 1 &&
                gameObject.transform.rotation.eulerAngles.z - ghost.transform.rotation.eulerAngles.z > -1) {
                gameObject.transform.rotation = ghost.transform.rotation;
                break;
            }
        }
        if (Vector2.Distance(gameObject.transform.position, ghost.transform.position) < 1) {
            GhostDoorRotate();
            moving = false;
            OGrotation = gameObject.transform.rotation;
            OGposition = gameObject.transform.position;
            if (GhostReverse == false) {
                GhostNr = GhostNr + 1;
            }

            if (GhostReverse == true) {
                GhostNr = GhostNr - 1;
            }
            StopCoroutine(DoorRotate());
        }
        
    }

    private void GhostDoorRotate() {
        ghost.transform.position = OGposition;
        ghost.transform.rotation = OGrotation;
    }
    
}
