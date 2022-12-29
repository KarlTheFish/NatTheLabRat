using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorScript : MonoBehaviour
{
    private GameObject ghost;
    private bool button;
    private Quaternion OGrotation;
    private Vector2 OGposition;
    private bool inPosition;
    private GameObject Pivotpoint;
    private int ButtonsPressed;
    
    // Start is called before the first frame update
    void Start()
    {
        ghost = GameObject.Find("Ghost1" + gameObject.name);
        OGrotation = gameObject.transform.rotation;
        OGposition = gameObject.transform.position;
        inPosition = false;
        Pivotpoint = GameObject.Find(gameObject.name + "Pivotpoint");
        ButtonsPressed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        button = GameObject.Find("Mouse").GetComponent<MouseScript>().ButtonPressed;
        if (button == true && ButtonsPressed < 1)
        {
            DoorRotate();
        }

        if (inPosition == true)
        {
            GhostDoorRotate();
        }
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
            ButtonsPressed =+ 1;
        }
        
    }

    private void GhostDoorRotate()
    {
        ghost.transform.position = OGposition;
        ghost.transform.rotation = OGrotation;
    }

}
