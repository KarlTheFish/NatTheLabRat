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
    public GameObject[] Doors;
    public bool ButtonPressed;
    public SpriteRenderer Sprite1;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        StartPosition = transform.position;
        RightGate = GameObject.Find("RightGate");
        LeftGate = GameObject.Find("LeftGate");
        Doors = GameObject.FindGameObjectsWithTag("Door");
        Sprite1 = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MovePermission == true)
        {
            gameObject.GetComponent<Animator>().enabled = true;
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
            LeftGate.transform.position = LeftGate.GetComponent<GateOpen>().LeftGateOGpos1;
            RightGate.transform.position = RightGate.GetComponent<GateOpen>().RightGateOGpos1;
            gameObject.GetComponent<Animator>().enabled = false;
            RotateReps = 0;
            foreach (var door in Doors )
            {
                door.GetComponent<BoxCollider2D>().isTrigger = true;
            }

        }

        if (boxCollider2D.GameObject().name == "Mirror")
        {
            MoveX = -MoveX;
            MoveY = -MoveY;
            Sprite1.flipY = !(Sprite1.flipY);
            Debug.Log("Mirror Hit");
        }
        
        else
        {
            //transform.position = new Vector2(transform.position.x - (float)0.01, transform.position.y - (float)0.01);
            switch ((MoveX, MoveY))
            {
                case (0, -4):
                    MoveX = 4;
                    MoveY = 0;
                    
                    break;
                case (4, 0):
                    MoveX = 0;
                    MoveY = 4;
                    transform.position = new Vector2(transform.position.x - (float)0.1,
                        transform.position.y);
                    break;
                case (0, 4):
                    MoveX = -4;
                    MoveY = 0;
                    transform.position =  new Vector2(transform.position.x,
                        transform.position.y - (float)0.1);
                    break;
                case (-4, 0):
                    MoveX = 0;
                    MoveY = -4;
                    transform.position = new Vector2(transform.position.x + (float)0.1,
                        transform.position.y);
                    break;
            }
        }
    }
}
