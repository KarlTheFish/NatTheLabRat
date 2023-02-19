using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    private float mouseX = 0;
    private float mouseY = 0;
    private Vector2 mouseMovement;
	bool canMove = true;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Mirror";
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)gameObject.transform.rotation.eulerAngles.z == 90)
        {
            mouseY = Input.GetAxis("Mouse Y") * (float)0.1;
        }
        else
        {
            mouseX = Input.GetAxis("Mouse X") * (float)0.1;
        }
        mouseMovement = new Vector2(mouseX, mouseY);
    }

    private void OnMouseDrag()
    {
        if (canMove == true)
        {
            transform.position = transform.position + (Vector3)mouseMovement;
        }

        if (canMove == false)
        {
            transform.position = transform.position - (Vector3)mouseMovement;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        canMove = false;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        canMove = true;
    }
}
