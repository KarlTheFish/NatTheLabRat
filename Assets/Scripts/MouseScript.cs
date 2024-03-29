using Unity.VisualScripting;
using UnityEngine;

// This script is attached to the Mouse object and handles the movement of the mouse. 
// It also handles the opening and closing of the gates.
// It also handles the animation of the mouse.
// It also handles the rotation of the mouse.
// It also handles the flipping of the mouse sprite.
// It is kind of a mess, but it works.

public class MouseScript : MonoBehaviour
{
    public float MoveX = 0;
    public float MoveY = -4;
    private Vector2 Movement;
    public bool MovePermission = false;
    public Vector2 StartPosition;
    public GameObject RightGate;
    public GameObject LeftGate;
    public GameObject[] Doors;
    public SpriteRenderer Sprite1;
    private GameObject GameManager;
    private Manager Manager;
    private float ColliderOffset;

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
        ColliderOffset = gameObject.GetComponent<CircleCollider2D>().offset.y;
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
    Debug.Log(boxCollider2D.GameObject().name + " hit");

    if (boxCollider2D.GameObject().name == "Cheese")
    {
        MoveX = 0;
        MoveY = 0;
        GameManager.GetComponent<Manager>().LevelComplete();
    }

    else
    {
        if (boxCollider2D.GameObject().CompareTag("Reset")) {
            Manager.Reset();
        } 

        else {
            if (boxCollider2D.GameObject().CompareTag("Mirror")) {
                ColliderOffset = -ColliderOffset;
                gameObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, ColliderOffset);
                MoveX = -MoveX;
                MoveY = -MoveY;
                Sprite1.flipY = !(Sprite1.flipY);
            }

            else {
                if (boxCollider2D.GameObject().CompareTag("Door")) {
                    transform.Rotate(new Vector3(0, 0, 90), Space.Self);
                    switch ((MoveX, MoveY)) {
                        case (0, -4):
                            transform.position += new Vector3(0, 0.1f, 0);
                            MoveX = 4;
                            MoveY = 0;
                            break;
                        case (4, 0):
                            transform.position -= new Vector3(0.1f, 0, 0);
                            MoveX = 0;
                            MoveY = 4;
                            break;
                        case (0, 4):
                            transform.position -= new Vector3(0, 0.1f, 0);
                            MoveX = -4;
                            MoveY = 0;
                            break;
                        case (-4, 0):
                            transform.position += new Vector3(0.1f, 0, 0);
                            MoveX = 0;
                            MoveY = -4;
                            break;
                    }
                }
                else {
                    transform.Rotate(new Vector3(0, 0, 90), Space.Self);
                    switch ((MoveX, MoveY)) {
                        case (0, -4):
                            transform.position += new Vector3(0, 0.05f, 0);
                            MoveX = 4;
                            MoveY = 0;
                            break;
                        case (4, 0):
                            transform.position -= new Vector3(0.05f, 0, 0);
                            MoveX = 0;
                            MoveY = 4;
                            break;
                        case (0, 4):
                            transform.position -= new Vector3(0, 0.05f, 0);
                            MoveX = -4;
                            MoveY = 0;
                            break;
                        case (-4, 0):
                            transform.position += new Vector3(0.05f, 0, 0);
                            MoveX = 0;
                            MoveY = -4;
                            break;
                    }
                }
            }
        }
    }
    }
}
