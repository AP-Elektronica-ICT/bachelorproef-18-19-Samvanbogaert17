using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerCheckEndOfGame : MonoBehaviour
{
    public Slider consumptionSlider;
    public Slider moneySlider;
    public Slider energySlider;
    [HideInInspector] public int correctAnsCount = 0;
    // Update is called once per frame
    void Update()
    {
        if (!EndOfGame.triggered)
        {
            //WIN
            if(correctAnsCount > 50)
            {
                FindObjectOfType<EndOfGame>().gameWon();
            }
            //LOSE
            if (FindObjectOfType<ConsumerLevelController>().started == true)
            {
                if (consumptionSlider.value <= 0)
                {
                    FindObjectOfType<EndOfGame>().gameOver();
                }
                if (moneySlider.value <= 0)
                {
                    FindObjectOfType<EndOfGame>().gameOver();
                }
                if (energySlider.value <= 0)
                {
                    FindObjectOfType<EndOfGame>().gameOver();
                }
            }            
        }
    }
}
