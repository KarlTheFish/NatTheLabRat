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
    public float MoveX = 0;
    public float MoveY = -4;
    private Vector2 Movement;
    public bool MovePermission = false;
    public Vector2 StartPosition;
    public GameObject RightGate;
    public GameObject LeftGate;
    static int level = 1;
    public GameObject[] Doors;
    public SpriteRenderer Sprite1;
    private GameObject GameManager;
    private Manager Manager;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        StartPosition = transform.position;
        RightGate = GameObject.Find("RightGate");
        LeftGate = GameObject.Find("LeftGate");
        Doors = GameObject.FindGameObjectsWithTag("Door");
        Sprite1 = gameObject.GetComponent<SpriteRenderer>();
        GameManager = GameObject.Find("GameManager");
        Manager = GameManager.GetComponent<Manager>();
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

    private static IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(20);
    }

    private void OnTriggerEnter2D(Collider2D boxCollider2D)
    {
    Debug.Log(boxCollider2D.GameObject().name);

    if (boxCollider2D.GameObject().name == "Cheese")
    {
        MoveX = 0;
        MoveY = 0;
        Debug.Log("Cheesed!");
        StartCoroutine(Wait());
        level = level + 1;
        SceneManager.LoadScene(level);
    }

    else
    {
        if (boxCollider2D.GameObject().CompareTag("Reset"))
        {
            Manager.Reset();
        }

        else
        {
            if (boxCollider2D.GameObject().name == "Mirror")
            {
                MoveX = -MoveX;
                MoveY = -MoveY;
                Sprite1.flipY = !(Sprite1.flipY);
            }

            else
            {
                transform.Rotate(new Vector3(0, 0, 90), Space.Self);
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
            }
        }
    }
    }
}
