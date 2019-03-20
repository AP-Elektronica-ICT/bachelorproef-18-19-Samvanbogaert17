using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOBrickGrid : MonoBehaviour
{
    public GameObject brickPrefab;
    public Texture[] textures;

    private List<GameObject> bricks = new List<GameObject>();
    private int bricksCount = 20;
    private int textureCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //bricksCount = TGOBreakoutController.instance.bricks;

        Init();
    }

    void Init()
    {
        for (int i = 0; i < bricksCount; i++)
        {
            if (i % 5 == 0) //5 = (bricksCount / textures.Length)
            {
                brickPrefab.GetComponentInChildren<RawImage>().texture = textures[textureCount];
                textureCount++;
            }
            bricks.Add(Instantiate(brickPrefab, transform));
        }
    }
}
