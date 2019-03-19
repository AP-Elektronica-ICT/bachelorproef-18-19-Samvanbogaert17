using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOPaddle : MonoBehaviour
{
    public float paddleSpeed = 1f;
    public RectTransform player;
    public RectTransform screen;
    private Vector3 playerPos = new Vector3(0, 20f, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transform.localPosition.x + (Input.GetAxis("Horizontal") * paddleSpeed);
        playerPos = new Vector3(Mathf.Clamp(xPos, -screen.sizeDelta.x/2 + player.sizeDelta.x/2, screen.sizeDelta.x/2 - player.sizeDelta.x / 2), -175f, 0);
        transform.localPosition = playerPos;
    }
}
