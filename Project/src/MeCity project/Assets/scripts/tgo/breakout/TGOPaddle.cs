using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOPaddle : MonoBehaviour
{
    public RectTransform player;
    public RectTransform screen;
    private Vector2 playerPos = new Vector2(0, 20f);

    // Update is called once per frame
    void Update()
    {
        float xPos = Input.mousePosition.x;
        float x1 = screen.position.x - screen.rect.width/2 + player.sizeDelta.x / 2;
        float x2 = screen.position.x + screen.rect.width/2 - player.sizeDelta.x / 2;
        playerPos = new Vector2(Mathf.Clamp(xPos, x1, x2), screen.position.y - screen.rect.height/2 + 50);
        player.position = playerPos;
    }
}
