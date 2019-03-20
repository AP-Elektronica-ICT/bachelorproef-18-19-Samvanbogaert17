using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOBreakoutController : MonoBehaviour
{
    public int bricks = 20;
    public int lives = 3;
    public GameObject paddle;
    public GameObject ball;
    public static TGOBreakoutController instance = null;
    // Start is called before the first frame update
    void Start()
    {
        if(instance = null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void Setup()
    {

    }

    public void LoseLife()
    {
        lives--;
        SetupPaddle();
    }

    void SetupPaddle()
    {
        TGOBall.ballInPlay = false;
        ball.transform.SetParent(paddle.transform);
        ball.transform.localPosition = new Vector2(0, 40);
        paddle.transform.position = new Vector2(300, -175);
    }

    void Reset()
    {
        
    }
}
