using UnityEngine;
using UnityEngine.UI;

public class ProducerCheckEndOfGame : MonoBehaviour
{
    [HideInInspector] public static int NumberOfCorrectAnswers = 0;
    public Text resulttxt;
    public Canvas endOfGameCanvas;
    public Text moneytxt;
    private bool triggered = false;

    // script used when player wins or loses the game
    void Update()
    {
        if (!triggered)
        {
            //WIN
            if (float.Parse(moneytxt.text) > 10000000)
            {
                gameWon();
            }

            if (NumberOfCorrectAnswers == 20)
            {
                gameWon();
            }

            //LOSE
            if (float.Parse(moneytxt.text) < 0)
            {
                gameOver();
            }

            if (ProducerMarketController.pollution <= 0)
            {
                gameOver();
            }
        }
    }
    // Gamewon screen
    private void gameWon()
    {
        triggered = true;
        FindObjectOfType<ProducerContractsController>().enabled = false;
        string resultText = "CONGRATULATIONS!!!";
        calculateScore(false);
        resultText += string.Format("\n\nYou got a score of: {0:0.00}!!\nYou would be an excellent producer!!!", DataScript.GetScore());
        resulttxt.text = resultText;
        Time.timeScale = 0;
        endOfGameCanvas.enabled = true;
    }
    // Gameover screen
    private void gameOver()
    {
        triggered = true;
        FindObjectOfType<ProducerContractsController>().enabled = false;
        string resultText = "GAME OVER!!!";
        calculateScore(true);
        resultText += string.Format("\n\nYou got a score of: {0:0.00}!!\nYou should hone your skills a bit more, but you will become a great producer one day!!!", DataScript.GetScore());
        resulttxt.text = resultText;
        Time.timeScale = 0;
        endOfGameCanvas.enabled = true;
    }
    // calculate player score
    private void calculateScore(bool gameOver)
    {
        if (float.Parse(moneytxt.text) > 0)
        {
            DataScript.AddScore(float.Parse(moneytxt.text));
            DataScript.AddScore(float.Parse(ProducerMarketController.pollution.ToString()));
        }
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