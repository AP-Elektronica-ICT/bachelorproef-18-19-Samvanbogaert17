using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOBreakoutController : MonoBehaviour
{
    public GameObject breakoutPanel;
    public GameObject player;
    public GameObject Ball;
    public GameObject[] platformPrefabs;

    private List<GameObject> platforms = new List<GameObject>();

    private int rows = 4;
    private int columns = 16;

    private Vector2 playerpos;
    private Vector2 speed = new Vector2(4, -4);

    bool gameStarted = false;

    private Transform breakoutTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerpos = player.transform.position;
        breakoutTransform = breakoutPanel.transform;

        Instantiate(player, breakoutTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !gameStarted)
        {
            gameStarted = true;
            playerControl();
        }
    }

    private void Init()
    {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                platforms.Add(Instantiate(platformPrefabs[i], breakoutTransform));
            }
        }
    }

    private void playerControl()
    {
        playerpos.y = 50;
        playerpos.x = Input.GetAxis("Mouse X");

        player.transform.position = playerpos;
    }
}
