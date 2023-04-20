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
    public Vector3 OGpos;
    bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Mirror";
        OGpos = transform.position;
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
        if (isColliding == false)
        {
            transform.position = transform.position + (Vector3)mouseMovement;
        }
        else
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, OGpos, Time.deltaTime * 0.2f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isColliding = true;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        isColliding = false;
    }
}
