using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecomsLevelController : MonoBehaviour
{
    public Canvas pickCanvas;
    public Canvas quizCanvas;
    public Canvas correctOrderCanvas;
    public Canvas oddOneOutCanvas;
    // Start is called before the first frame update

    //These functions all should speak for themselves
    public void EnablePickCanvas()
    {
        pickCanvas.enabled = true;
    }

    public void DisablePickCanvas()
    {
        pickCanvas.enabled = false;
    }

    public void OpenMultipleChoice()
    {
        quizCanvas.enabled = true;
    }

    public void OpenCorrectOrder()
    {
        correctOrderCanvas.enabled = true;
    }

    public void OpenOddOneOut()
    {
        oddOneOutCanvas.enabled = true;
    }

    //opens a random type of question
    public void OpenRandom()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(3);
        switch (num)
        {
            case 0:
                OpenMultipleChoice();
                break;
            case 1:
                OpenCorrectOrder();
                break;
            case 2:
                OpenOddOneOut();
                break;
        }

    }
}
