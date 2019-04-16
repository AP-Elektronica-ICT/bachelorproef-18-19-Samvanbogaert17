using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOBrickGrid : MonoBehaviour
{
    public GameObject brickPrefab;
    public Texture[] textures;

    public List<GameObject> bricksList = new List<GameObject>();
    private int bricksCount;
    private int textureCount = 0;
    private int columns;

    // Start is called before the first frame update
    void Start()
    {
        columns = GetComponentInChildren<GridLayoutGroup>().constraintCount;
        bricksCount = FindObjectOfType<TGOBreakoutController>().bricks;
        Init();
    }

    private void Init()
    {
        Debug.Log(bricksCount / textures.Length);
        for (int i = 0; i < bricksCount; i++)
        {
            if (i % columns == 0) 
            {
                if(textureCount == textures.Length)
                {
                    textureCount = 0;
                    brickPrefab.GetComponentInChildren<RawImage>().texture = textures[textureCount];
                }
                else
                {
                    brickPrefab.GetComponentInChildren<RawImage>().texture = textures[textureCount];
                }
                textureCount++;
            }
            bricksList.Add(Instantiate(brickPrefab, transform));
        }
        textureCount = 0;
    }

    public void Reset()
    {
        for(int i = 0; i < bricksCount; i++)
        {
            Destroy(bricksList[i]);
        }

        bricksList.Clear();

        Init();
    }
}
