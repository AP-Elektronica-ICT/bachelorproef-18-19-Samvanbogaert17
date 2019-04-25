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
            float xPos = Input.mousePosition.x;
            float x1 = screen.position.x;
            float x2 = screen.position.x + screen.rect.width;
            playerPos = new Vector2(Mathf.Clamp(xPos, x1, x2), screen.position.y - screen.rect.height + 50);
            player.position = playerPos;
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
