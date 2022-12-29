using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseScript : MonoBehaviour
{
    private float MoveX = 0;
    public float MoveY = -4;
    private Vector2 Movement;
    public bool MovePermission = false;
    private Vector2 StartPosition;
    GameObject RightGate;
    GameObject LeftGate;
    public int RotateReps;
    static int level = 1;
    private GameObject[] Doors;
    public bool ButtonPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        RightGate = GameObject.Find("RightGate");
        LeftGate = GameObject.Find("LeftGate");
        Doors = GameObject.FindGameObjectsWithTag("Door");
    }

    // Update is called once per frame
    void Update()
    {
        if (MovePermission == true)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Movement = new Vector2(MoveX, MoveY);
            transform.position = (Vector2)transform.position + Movement * Time.deltaTime;
        }

        if (MovePermission == false)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D boxCollider2D)
    {
        Debug.Log(boxCollider2D.GameObject().name);

        if (boxCollider2D.GameObject().name == "Cheese")
        {
            MoveX = 0;
            MoveY = 0;
            Debug.Log("Cheesed!");
            level = level + 1;
            SceneManager.LoadScene(level);
        }
        
        if (boxCollider2D.GameObject().CompareTag("Reset"))
        {
            MoveX = 0;
            MoveY = 0;
            MovePermission = false;
            transform.position = StartPosition;
            LeftGate.transform.rotation =
                LeftGate.GetComponent<GateOpen>().LeftGateOGpos;
            RightGate.transform.rotation =
                RightGate.GetComponent<GateOpen>().RightGateOGpos;
            LeftGate.transform.position = LeftGate.GetComponent<GateOpen>().LeftGateOGpos1;
            RightGate.transform.position = RightGate.GetComponent<GateOpen>().RightGateOGpos1;
            RotateReps = 0;
            foreach (var door in Doors )
            {
                door.GetComponent<BoxCollider2D>().isTrigger = true;
            }

        }

        if (boxCollider2D.GameObject().name == "Button")
        {
            ButtonPressed = true;
            MoveX = (float)1.001 * MoveX;
            MoveY = (float)1.001 * MoveY;
        }
        
        if (boxCollider2D.GameObject().name == "Mirror")
        {
            MoveX = -MoveX;
            MoveY = -MoveY;
            Debug.Log("Mirror Hit");
        }
        
        else
        {
            switch ((MoveX, MoveY))
            {
                case (0, -4):
                    MoveX = 4;
                    MoveY = 0;
                    break;
                case (4, 0):
                    MoveX = 0;
                    MoveY = 4;
                    break;
                case (0, 4):
                    MoveX = -4;
                    MoveY = 0;
                    break;
                case (-4, 0):
                    MoveX = 0;
                    MoveY = -4;
                    break;
            }
            if (boxCollider2D.GameObject().CompareTag("Door"))
            {
                boxCollider2D.GameObject().GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}
