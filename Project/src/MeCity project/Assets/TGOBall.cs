using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOBall : MonoBehaviour
{
    public GameObject breakoutPanel;
    public Canvas breakoutCanvas;
    public float ballInitialVelocity = 600f;


    private Rigidbody2D rb;
    [HideInInspector] public static bool ballInPlay;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ballInPlay == false && breakoutCanvas.enabled == true)
        {
            transform.parent = breakoutPanel.transform;
            ballInPlay = true;
            //rb.isKinematic = false;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(ballInitialVelocity, ballInitialVelocity);
        }
    }
}
