using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGame : MonoBehaviour
{
    [HideInInspector] public static int NumberOfCorrectAnswers = 0;
    private bool triggered = false;

    public Slider consumptionSlider;
    public Slider moneySlider;
    public Slider energySlider;

    public Text moneyTxt;

    public Text problemsSolvedCountTxt;
    public Text problemsUnsolvedCountTxt;

    public Canvas endOfGameCanvas;
    public Text resultTxt;

    private string sceneName;

    //General End Of Game Script
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        triggered = false;
    }

    //
    void Update()
    {
        if (!triggered)
        {
            switch (sceneName)
            {
                case "Producer":
                    //WIN
                    if (float.Parse(moneyTxt.text) > 10000000)
                    {
                        GameWon();
                    }

                    if (NumberOfCorrectAnswers == 20)
                    {
                        GameWon();
                    }

                    //LOSE
                    if (float.Parse(moneyTxt.text) < 0)
                    {
                        GameOver();
                    }

                    if (ProducerMarketController.pollution >= 100)
                    {
                        GameOver();
                    }

                    if (ProducerEventSystem.satisfaction <= 0)
                    {
                        GameOver();
                    }
                    break;
                case "TGO":
                    break;
                case "DGO":
                    //WIN
                    if (int.Parse(problemsSolvedCountTxt.text) > 30)
                    {
                        GameWon();
                    }

                    if (NumberOfCorrectAnswers == 20)
                    {
                        GameWon();
                    }

                    //LOSE
                    if (int.Parse(problemsUnsolvedCountTxt.text) > 10)
                    {
                        GameOver();
                    }

                    if (DGOProblemController.satisfaction <= 0)
                    {
                        GameOver();
                    }

                    if (int.Parse(moneyTxt.text) < 0)
                    {
                        GameOver();
                    }
                    break;
                case "Supplier":
                    //WIN
                    if (float.Parse(moneyTxt.text) > 10000000)
                    {
                        GameWon();
                    }

                    if (NumberOfCorrectAnswers == 20)
                    {
                        GameWon();
                    }
                    if ((Satisfaction.numberOfCustomers / 136f) > 0.8)
                    {
                        GameWon();
                    }

                    //LOSE
                    if (float.Parse(moneyTxt.text) < 0)
                    {
                        GameOver();
                    }

                    if (NumberOfCorrectAnswers == -10)
                    {
                        GameOver();
                    }
                    break;
                case "Consumer":
                    //WIN
                    if (NumberOfCorrectAnswers > 25)
                    {
                        GameWon();
                    }
                    if (FindObjectOfType<ConsumerLevelController>().started == true)
                    {
                        if (consumptionSlider.value >= 1000)
                        {
                            GameWon();
                        }
                        if (moneySlider.value >= 1000)
                        {
                            GameWon();
                        }
                        if (energySlider.value >= 1000)
                        {
                            GameWon();
                        }
                    }
                    //LOSE
                    if (FindObjectOfType<ConsumerLevelController>().started == true)
                    {
                        if (consumptionSlider.value <= 0)
                        {
                            GameOver();
                        }
                        if (moneySlider.value <= 0)
                        {
                            GameOver();
                        }
                        if (energySlider.value <= 0)
                        {
                            GameOver();
                        }
                    }
                    break;
                case "Mecoms":
                    //WIN
                    if(NumberOfCorrectAnswers >= 100)
                    {
                        GameWon();
                    }
                    break;
            }
        }
    }
    // Gamewon screen
    public void GameWon()
    {
        triggered = true;
        string resultText = "CONGRATULATIONS!!!";
        //calculateScore(false);
        resultText += string.Format("\n\nYou got a score of: {0:0}!!", DataScript.GetScore());
        resultText += "\nYou would be an excellent " + sceneName + "!!!";
        resultTxt.text = resultText;
        Time.timeScale = 0;
        endOfGameCanvas.enabled = true;
    }
    // Gameover screen
    public void GameOver()
    {
        triggered = true;
        string resultText = "GAME OVER!!!";
        //calculateScore(true);
        resultText += string.Format("\n\nYou got a score of: {0:0}!!", DataScript.GetScore());
        resultText += "\nYou should hone your skills a bit more, but you will become a great " + sceneName + " one day!!!";
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



