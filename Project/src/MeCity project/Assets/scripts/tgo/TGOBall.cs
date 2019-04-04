using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOBall : MonoBehaviour
{
    public GameObject paddle;
    public GameObject breakoutPanel;
    public Canvas breakoutCanvas;
    public float ballInitialVelocity = 600f;

    private System.Random rnd = new System.Random();
    private float angle;

    private bool paddleIsPressed;
    private Rigidbody2D rb;
    [HideInInspector] public static bool ballInPlay;
    // Start is called before the first frame update
    void Start()
    {
        GetRandomAngle();
        paddle.GetComponent<Button>().onClick.AddListener(IsPressed);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballInPlay == false && breakoutCanvas.enabled == true && paddleIsPressed)
        {
            transform.parent = breakoutPanel.transform;
            ballInPlay = true;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(ballInitialVelocity * (Mathf.Cos(angle)), ballInitialVelocity * Mathf.Sin(angle));
            paddleIsPressed = false;
        }
    }

    public void IsPressed()
    {
        paddleIsPressed = true;
        paddle.GetComponentInChildren<Text>().text = "";
    }

    public void GetRandomAngle()
    {
        angle = rnd.Next(45, 136);
        while (angle < 95 && angle > 85)
        {
            angle = rnd.Next(45, 136);
        }

        angle *= (Mathf.PI / 180);
    }

}
