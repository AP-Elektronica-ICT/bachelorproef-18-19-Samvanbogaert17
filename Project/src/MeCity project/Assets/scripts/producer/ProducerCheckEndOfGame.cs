using UnityEngine;
using UnityEngine.UI;

public class ProducerCheckEndOfGame : MonoBehaviour
{
    [HideInInspector] public static int NumberOfCorrectAnswers = 0;
    public Text moneytxt;

    void Update()
    {
        if (!EndOfGame.triggered)
        {
            //WIN
            if (float.Parse(moneytxt.text) > 10000000)
            {
                FindObjectOfType<EndOfGame>().gameWon();
            }

            if (NumberOfCorrectAnswers == 20)
            {
                FindObjectOfType<EndOfGame>().gameWon();
            }

            //LOSE
            if (float.Parse(moneytxt.text) < 0)
            {
                FindObjectOfType<EndOfGame>().gameOver();
            }

            if (ProducerMarketController.pollution >= 100)
            {
                FindObjectOfType<EndOfGame>().gameOver();
            }

            if (ProducerEventSystem.satisfaction <= 0)
            {
                FindObjectOfType<EndOfGame>().gameOver();
            }
        }
    }
}