using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOBucket : MonoBehaviour
{
    public RectTransform player;
    public RectTransform screen;
    private Vector2 playerPos;

    // Update is called once per frame
    void Update()
    {
        if (TGOCatcher.playerIsReady)
        {
            float xPos = Input.mousePosition.x -  screen.rect.width;
            float x1 = 0f;
            float x2 = screen.rect.width;
            playerPos = new Vector2(Mathf.Clamp(xPos, x1, x2), -380f);
            player.localPosition = playerPos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.rigidbody.gameObject);
        TGOCatcher.lives++;
        TGOCatcher.lightningSpeed += 5f;
        DataScript.AddScore(100);
    }
}
