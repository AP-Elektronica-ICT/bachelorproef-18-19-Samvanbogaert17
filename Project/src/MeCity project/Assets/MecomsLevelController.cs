using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecomsLevelController : MonoBehaviour
{
    public Canvas pickCanvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnablePickCanvas()
    {
        pickCanvas.enabled = true;
    }

    void DisablePickCanvas()
    {
        pickCanvas.enabled = false;
    }

    void AskMultipleChoice()
    {
        FindObjectOfType<QuizController>().Task();
    }

    void AskCorrectOrder()
    {

    }

    void AskOddOneOut()
    {

    }
}
