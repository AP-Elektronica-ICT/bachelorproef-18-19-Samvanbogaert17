using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOBreakoutController : MonoBehaviour
{
    public int lives = 3;
    public int bricks = 40;
    public GameObject paddle;
    public GameObject ball;
    public Text livesTxt;

    [HideInInspector] public int brickCounter;

    void Start()
    {
        brickCounter = bricks;
        livesTxt.text = lives.ToString();
        paddle.GetComponentInChildren<Text>().text = "Press me to start";
    }

    void Update()
    {
        if (bricks == 0)
        {
            ResetGame();
        }
    }

    public void Setup()
    {
        TGOBall.ballInPlay = false;
        paddle.GetComponentInChildren<Text>().text = "Press me to start";
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        ball.transform.SetParent(paddle.transform);
        ball.transform.localPosition = new Vector2(0, 40);
        paddle.transform.position = new Vector2(300, -175);
    }

    public void LoseLife()
    {
        lives--;
        if (lives == 0)
        {
            ResetGame();
            DataScript.AddScore(-2500);
        }
        else
        {
            livesTxt.text = lives.ToString();
            Setup();
        }
        FindObjectOfType<TGOBall>().GetRandomAngle();
    }

    public void ResetGame()
    {
        brickCounter = bricks;
        lives = 3;
        livesTxt.text = lives.ToString();
        FindObjectOfType<TGOBrickGrid>().Reset();
        FindObjectOfType<TGOBall>().GetRandomAngle();
        Setup();
    }
}
