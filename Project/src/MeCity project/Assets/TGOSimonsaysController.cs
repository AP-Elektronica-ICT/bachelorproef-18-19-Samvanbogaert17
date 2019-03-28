using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOSimonsaysController : MonoBehaviour
{
    public Button[] btnArray;
    public RawImage[] imgArray;

    private Color[] defaultColors = new Color[4];
    private Color[] highlightedColors = new Color[4];

    private System.Random rnd = new System.Random();

    private List<int> simonList = new List<int>();
    private List<int> playerList = new List<int>();
    private List<int> correctList = new List<int>();

    private int frameCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (simonList.Count > 0)
        {
            CompareLists();
        }
        frameCounter++;
        if (frameCounter > 120)
        {
            frameCounter = 0;
        }
    }

    void Init()
    {
        for (int i = 0; i < btnArray.Length; i++)
        {
            int temp = i;
            btnArray[i].onClick.AddListener(() =>
            {
                playerList.Add(temp);
            });

            defaultColors[i] = imgArray[i].color;
        }


        highlightedColors[0] = new Color(1f, 0.2f, 0.2f);
        highlightedColors[1] = new Color(1f, 1f, 0.2f);
        highlightedColors[2] = new Color(0.2f, 1f, 0.2f);
        highlightedColors[3] = new Color(0.2f, 0.2f, 1f);

        AddToList();
    }

    void AddToList()
    {
        simonList.Add(rnd.Next(0, 4));
    }

    void Reset()
    {
        playerList.Clear();
        simonList.Clear();
        for (int i = 0; i < imgArray.Length; i++)
        {
            imgArray[i].color = defaultColors[i];
        }
        AddToList();
    }

    void CompareLists()
    {
        if (simonList.Count == playerList.Count)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (simonList[i] != playerList[i])
                {
                    Reset();
                }
            }
            AddToList();
        }
        else
        {
            if(frameCounter == 60)
            {

            }
            else if(frameCounter == 120)
            {
                frameCounter = 0;
            }
            for (int i = 0; i < simonList.Count; i++)
            {

                if (frameCounter == 60)
                {
                    
                    imgArray[simonList[i]].color = highlightedColors[simonList[i]];
                    if(i > 1)
                    {
                        imgArray[simonList[i - 1]].color = defaultColors[simonList[i - 1]];
                    }

                }
                if (frameCounter == 120)
                {
                    imgArray[simonList[i]].color = defaultColors[simonList[i]];
                    frameCounter = 0;
                }


            }
        }
    }
}
