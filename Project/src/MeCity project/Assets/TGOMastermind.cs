using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TGOMastermind : MonoBehaviour
{
    public GameObject prevAnswerPanel;
    public GameObject prevAnswerPrefab;

    public RectTransform[] answerSlots;
    public RawImage[] possibleAnswers;
    private int[] playerColorCounter = new int[8];
    private int[] gameColorCounter = new int[8];
    private Color[] gameAnswers = new Color[4];
    private Color[] playerAnswers = new Color[4];

    private Color[] gameHints = new Color[4];
    private bool[] isAnswered = new bool[4];

    private Transform prevAnswerPanelTransform;
    private List<GameObject> prevAnswers = new List<GameObject>();

    public Button confirmBtn;
    public Button startBtn;

    private System.Random rnd = new System.Random();

    private int hintCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        prevAnswerPanelTransform = prevAnswerPanel.transform;
        GetCombination();

        confirmBtn.onClick.AddListener(() =>
        {
            Confirm();
        });
    }

    void Confirm()
    {
        //Initial check to see if player has filled in every color
        for(int i = 0; i < isAnswered.Length; i++)
        {
            if(answerSlots[i].childCount > 0)
            {
                isAnswered[i] = true;
            }
            else
            {
                isAnswered[i] = false;
            }
        }

        if (isAnswered.All(x => x == true))
        {
            //set player answers
            for(int i = 0; i < playerAnswers.Length; i++)
            {
                playerAnswers[i] = answerSlots[i].GetChild(0).GetComponent<RawImage>().color;
            }
            //
            for (int i = 0; i < possibleAnswers.Length; i++){
                for(int j = 0; j < gameAnswers.Length; j++)
                {
                    if(gameAnswers[j] == possibleAnswers[i].color)
                    {
                        playerColorCounter[i]++;
                    }
                }
            }
            //Resets hint colors to white
            ResetHints();
            //checks if colors are present in the solution
            CheckPosition();
            //checks if the colors on the positions match
            CheckColor();
            //Adds answer to the previous answer panel
            AddPrevAnswer(playerAnswers, gameHints);

            //if the player has guessed the answer
            if (playerAnswers == gameAnswers)
            {

            }

        }
    }

    void GetCombination()
    {
        for (int i = 0; i < gameAnswers.Length; i++)
        {
            gameAnswers[i] = possibleAnswers[rnd.Next(0, possibleAnswers.Length)].color;
            Debug.Log(gameAnswers[i]);
            for(int j = 0; j < gameColorCounter.Length; j++)
            {
                if(possibleAnswers[j].color == gameAnswers[i])
                {
                    gameColorCounter[j]++;
                }
            }
        }
    }

    void AddPrevAnswer(Color[] answerArray, Color[] hintArray)
    {
        GameObject prevAnswer = Instantiate(prevAnswerPrefab, prevAnswerPanelTransform);

        for (int i = 0; i < answerArray.Length; i++)
        {
            prevAnswer.GetComponentsInChildren<Image>()[0].GetComponentsInChildren<RawImage>()[i].color = answerArray[i];
            prevAnswer.GetComponentsInChildren<Image>()[1].GetComponentsInChildren<RawImage>()[i].color = hintArray[i];
        }

        prevAnswers.Add(prevAnswer);
    }

    public void ClearPrevAnswers()
    {
        for (int i = 0; i < prevAnswers.Count; i++)
        {
            Destroy(prevAnswers[i]);
        }

        prevAnswers.Clear();
    }

    void CheckPosition()
    {
        hintCount = 0;
        for (int i = 0; i < gameColorCounter.Length; i++)
        {
            if(playerColorCounter[i] >= gameColorCounter[i])
            {
                gameHints[hintCount] = new Color(1f, 1f, 0f);
                hintCount++;
            }
        }
    }

    void CheckColor()
    {
        hintCount = 0;
        for(int i = 0; i < gameAnswers.Length; i++)
        {
            if(playerAnswers[i] == gameAnswers[i])
            {
                gameHints[hintCount] = new Color(0f, 1f, 0f);
                hintCount++;
            }
        }
    }

    void ResetHints()
    {
        for(int i = 0;  i < gameHints.Length; i++)
        {
            gameHints[i] = new Color(1f, 1f, 1f);
        }
    }
}
