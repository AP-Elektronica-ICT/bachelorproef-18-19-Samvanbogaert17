﻿using UnityEngine;
using UnityEngine.UI;

public class DGOCheckEndOfGame : MonoBehaviour
{
    [HideInInspector] public static int NumberOfCorrectAnswers = 0;
    public Text resulttxt;
    public Text problemsSolvedCountTxt;
    public Text problemsUnsolvedCountTxt;
    public Canvas endOfGameCanvas;
    private bool triggered = false;

    // script used when player wins or loses the game
    void Update()
    {
        if (!triggered)
        {
            //WIN
            if (int.Parse(problemsSolvedCountTxt.text) > 50)
            {
                gameWon();
            }

            if (NumberOfCorrectAnswers == 20)
            {
                gameWon();
            }

            //LOSE
            if (int.Parse(problemsUnsolvedCountTxt.text) > 25)
            {
                gameOver();
            }

            if (DGOEventSystem.satisfaction <= 0)
            {
                gameOver();
            }
        }
    }
    // Gamewon screen
    private void gameWon()
    {
        triggered = true;
        string resultText = "CONGRATULATIONS!!!";
        calculateScore(false);
        resultText += string.Format("\n\nYou got a score of: {0:0.00}!!\nYou would be an excellent DGO!!!", DataScript.GetScore());
        resulttxt.text = resultText;
        Time.timeScale = 0;
        endOfGameCanvas.enabled = true;
    }
    // Gameover screen
    private void gameOver()
    {
        triggered = true;
        string resultText = "GAME OVER!!!";
        calculateScore(true);
        resultText += string.Format("\n\nYou got a score of: {0:0.00}!!\nYou should hone your skills a bit more, but you will become a great DGO one day!!!", DataScript.GetScore());
        resulttxt.text = resultText;
        Time.timeScale = 0;
        endOfGameCanvas.enabled = true;
    }
    // calculate player score
    private void calculateScore(bool gameOver)
    {
        DataScript.AddScore(float.Parse(DGOEventSystem.satisfaction.ToString()));
        if (gameOver)
        {
            DataScript.AddScore(Time.timeSinceLevelLoad * 1.5f);
        }
        else
        {
            DataScript.AddScore((60 * 900 * 40) / Time.timeSinceLevelLoad);
        }
    }
}