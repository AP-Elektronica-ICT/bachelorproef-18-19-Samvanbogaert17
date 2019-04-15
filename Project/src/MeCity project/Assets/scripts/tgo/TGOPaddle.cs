using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOPaddle : MonoBehaviour
{
    public RectTransform player;
    public RectTransform screen;
    private Vector3 playerPos = new Vector3(0, 20f, 0);

    // Update is called once per frame
    void Update()
    {
        float xPos = Input.mousePosition.x - screen.sizeDelta.x + player.sizeDelta.x / 2;
        playerPos = new Vector3(Mathf.Clamp(xPos, -screen.sizeDelta.x/2 + player.sizeDelta.x/2, screen.sizeDelta.x/2 - player.sizeDelta.x / 2), -175f, 0);
        transform.localPosition = playerPos;
    }
}
