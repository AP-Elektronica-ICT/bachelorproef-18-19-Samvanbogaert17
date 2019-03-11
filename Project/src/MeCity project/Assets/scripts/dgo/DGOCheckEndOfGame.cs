using UnityEngine;
using UnityEngine.UI;

public class DGOCheckEndOfGame : MonoBehaviour
{
    [HideInInspector] public static int NumberOfCorrectAnswers = 0;
    public Text moneyTxt;
    public Text problemsSolvedCountTxt;
    public Text problemsUnsolvedCountTxt;

    // script used when player wins or loses the game
    void Update()
    {
        if (!EndOfGame.triggered)
        {
            //WIN
            if (int.Parse(problemsSolvedCountTxt.text) > 30)
            {
                FindObjectOfType<EndOfGame>().gameWon();
            }

            if (NumberOfCorrectAnswers == 20)
            {
                FindObjectOfType<EndOfGame>().gameWon();
            }

            //LOSE
            if (int.Parse(problemsUnsolvedCountTxt.text) > 10)
            {
                FindObjectOfType<EndOfGame>().gameOver();
            }

            if (DGOProblemController.satisfaction <= 0)
            {
                FindObjectOfType<EndOfGame>().gameOver();
            }

            if (int.Parse(moneyTxt.text) < 0)
            {
                FindObjectOfType<EndOfGame>().gameOver();
            }
        }
    }
}