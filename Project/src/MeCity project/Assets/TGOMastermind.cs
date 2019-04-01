using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOMastermind : MonoBehaviour
{
    public Image[] possibleAnswers;
    public Image[] playerAnswers;
    private Image[] gameAnswers = new Image[4];

    public Button confirmBtn;

    private System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Confirm()
    {

    }

    void Init()
    {
        GetCombination();
    }

    void GetCombination()
    {
        for (int i = 0; i < gameAnswers.Length; i++)
        {
            gameAnswers[i] = possibleAnswers[rnd.Next(0, possibleAnswers.Length)];
        }
    }
}
