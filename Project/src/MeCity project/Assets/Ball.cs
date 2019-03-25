using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Canvas breakoutCanvas;
    float BallForce = 200f;
    Rigidbody rb;
    bool ballInPlay;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false && breakoutCanvas.enabled == true)
        {
            rb.AddForce(BallForce, BallForce, 0);
        }
    }
}