using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool pressed;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        pressed = true;
        Debug.Log("Button pressed");
    }
}
