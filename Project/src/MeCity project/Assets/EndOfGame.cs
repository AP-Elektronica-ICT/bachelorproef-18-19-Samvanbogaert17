using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGame : MonoBehaviour
{
    public Canvas endOfGameCanvas;
    public Text resultTxt;
    [HideInInspector] public static bool triggered = false;

    //General End Of Game Script

    void Start()
    {
        triggered = false;
    }
    // Gamewon screen
    public void gameWon()
    {
        triggered = true;
        string resultText = "CONGRATULATIONS!!!";
        calculateScore(false);
        resultText += string.Format("\n\nYou got a score of: {0:0.00}!!", DataScript.GetScore());
        resultText += "\nYou would be an excellent "+SceneManager.GetActiveScene().name+"!!!";
        resultTxt.text = resultText;
        Time.timeScale = 0;
        endOfGameCanvas.enabled = true;
    }
    // Gameover screen
    public void gameOver()
    {
        triggered = true;
        string resultText = "GAME OVER!!!";
        calculateScore(true);
        resultText += string.Format("\n\nYou got a score of: {0:0.00}!!", DataScript.GetScore());
        resultText += "\nYou should hone your skills a bit more, but you will become a great " + SceneManager.GetActiveScene().name + " one day!!!";
        resultTxt.text = resultText;
        Time.timeScale = 0;
        endOfGameCanvas.enabled = true;
    }
    // calculate player score
    private void calculateScore(bool gameOver)
    {
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
