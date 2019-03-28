using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOSimonsaysController : MonoBehaviour
{
    public Button[] btnArray;
    public RawImage[] imgArray;
    public AudioClip[] AudioArray;

    private AudioSource source = new AudioSource();

    private Color[] defaultColors = new Color[4];
    private Color[] highlightedColors = new Color[4];

    private System.Random rnd = new System.Random();

    private List<int> simonList = new List<int>();
    private List<int> playerList = new List<int>();
    private List<int> correctList = new List<int>();

    private int frameCounter = 0;
    private int sequenceCounter = 0;

    private bool gameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.playOnAwake = false;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
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
    }

    void Init()
    {
        for (int i = 0; i < btnArray.Length; i++)
        {
            int temp = i;
            btnArray[i].onClick.AddListener(() =>
            {
                playerList.Add(temp);
                PlaySound(temp);
            });

            defaultColors[i] = imgArray[i].color;
        }

        for(int i = 0; i < AudioArray.Length; i++)
        {
            Debug.Log(AudioArray[i].length);
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

    public void StartGame()
    {
        gameStarted = true;
    }

    public void EndGame()
    {
        gameStarted = false;
    }

    void PlaySound(int index)
    {
        source.PlayOneShot(AudioArray[index]);
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
                imgArray[simonList[sequenceCounter]].color = highlightedColors[simonList[sequenceCounter]];
                //PlaySound(simonList[sequenceCounter]);
                /*if (sequenceCounter > 1)
                {
                    imgArray[simonList[sequenceCounter - 1]].color = defaultColors[simonList[sequenceCounter - 1]];
                    AudioArray[sequenceCounter -1] = 
                }*/
            }
            else if(frameCounter == 120)
            {
                imgArray[simonList[sequenceCounter]].color = defaultColors[simonList[sequenceCounter]];
                sequenceCounter++;
                if(sequenceCounter == simonList.Count)
                {
                    sequenceCounter = 0;
                }
                frameCounter = 0;
            }
        }
    }
}
