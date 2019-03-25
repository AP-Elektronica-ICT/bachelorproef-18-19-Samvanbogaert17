using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOPaddle : MonoBehaviour
{
    public float paddleSpeed = 1f;
    public RectTransform player;
    public RectTransform screen;
    private Vector3 playerPos = new Vector3(0, 20f, 0);
    public GameObject testPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Input.mousePosition.x - screen.sizeDelta.x + player.sizeDelta.x / 2;
        playerPos = new Vector3(Mathf.Clamp(xPos, -screen.sizeDelta.x/2 + player.sizeDelta.x/2, screen.sizeDelta.x/2 - player.sizeDelta.x / 2), -175f, 0);
        transform.localPosition = playerPos;
    }


    //Theoretical unfinishedfunction for aiming the ball with the paddle

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.GetContact(0).rigidbody.transform.localPosition);
        Debug.Log(collision.GetContact(0).otherRigidbody.transform.localPosition);

        Debug.Log(collision.contacts[0].point);
        //Instantiate(testPrefab, new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 2), Quaternion.identity);
        Instantiate(testPrefab, collision.gameObject.transform.localPosition, Quaternion.identity);
    }*/
}
