using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOSimonSays : MonoBehaviour
{
    public Button startBtn;

    public Image[] colors;
    public Button[] btns;
    public AudioSource[] sounds;

    private System.Random rnd = new System.Random();

    public float stayLit;
    private float stayLitCounter;

    public float pauseLight;
    private float pauseCounter;

    private bool isLit;
    private bool isUnlit;
    private bool sequenceActive;
    private bool gameActive;

    private List<int> activeSequence = new List<int>();
    private int sequencePos;
    private int sequenceInput;

    public AudioSource correct;
    public AudioSource incorrect;
    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(() =>
        {
            StartGame();
        });

        for (int i = 0; i < btns.Length; i++)
        {
            int temp = i;
            btns[i].onClick.AddListener(() =>
            {
                ColorPressed(temp);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isLit)
        {
            stayLitCounter -= Time.deltaTime;

            if (stayLitCounter < 0)
            {
                colors[activeSequence[sequencePos]].color = new Color(colors[activeSequence[sequencePos]].color.r, colors[activeSequence[sequencePos]].color.g, colors[activeSequence[sequencePos]].color.b, 0.5f);
                sounds[activeSequence[sequencePos]].Stop();

                pauseCounter = pauseLight;
                sequencePos++;

                isLit = false;
                isUnlit = true;
            }
        }

        if (isUnlit)
        {
            pauseCounter -= Time.deltaTime;

            if (sequencePos >= activeSequence.Count)
            {
                isUnlit = false;
                sequenceActive = true;
            }
            else if (pauseCounter < 0)
            {
                colors[activeSequence[sequencePos]].color = new Color(colors[activeSequence[sequencePos]].color.r, colors[activeSequence[sequencePos]].color.g, colors[activeSequence[sequencePos]].color.b, 1f);
                sounds[activeSequence[sequencePos]].Play();

                stayLitCounter = stayLit;

                isLit = true;
                isUnlit = false;
            }
        }

        if (gameActive)
        {
            startBtn.interactable = false;
        }
        else
        {
            startBtn.interactable = true;
        }
    }

    public void StartGame()
    {
        sequencePos = 0;
        sequenceInput = 0;

        activeSequence.Clear();

        activeSequence.Add(rnd.Next(0, colors.Length));

        colors[activeSequence[sequencePos]].color = new Color(colors[activeSequence[sequencePos]].color.r, colors[activeSequence[sequencePos]].color.g, colors[activeSequence[sequencePos]].color.b, 1f);
        sounds[activeSequence[sequencePos]].Play();

        stayLitCounter = stayLit;

        isLit = true;
        gameActive = true;
    }

    public void ColorPressed(int btnIndex)
    {
        if (sequenceActive)
        {
            if (btnIndex == activeSequence[sequenceInput])
            {
                sequenceInput++;

                if (sequenceInput >= activeSequence.Count)
                {
                    DataScript.AddScore(1000*activeSequence.Count);

                    correct.Play();

                    sequencePos = 0;
                    sequenceInput = 0;

                    activeSequence.Add(rnd.Next(0, colors.Length));

                    colors[activeSequence[sequencePos]].color = new Color(colors[activeSequence[sequencePos]].color.r, colors[activeSequence[sequencePos]].color.g, colors[activeSequence[sequencePos]].color.b, 1f);
                    sounds[activeSequence[sequencePos]].Play();

                    stayLitCounter = stayLit;

                    isLit = true;

                    sequenceActive = false;
                }
            }
            else
            {
                incorrect.Play();
                sequenceActive = false;
                gameActive = false;
            }
        }
    }
}
