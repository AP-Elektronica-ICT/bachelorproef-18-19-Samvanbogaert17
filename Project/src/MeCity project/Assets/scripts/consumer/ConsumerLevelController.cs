using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerLevelController : MonoBehaviour
{
    public Canvas dilemmaCanvas;
    public Canvas quizCanvas;
    public Text question;
    public Button[] choiceBtns;
    public RawImage[] invSlots;
    public Slider consumptionSlider;
    public Slider moneySlider;
    public Slider energySlider;

    public Texture[] solarpanels;
    public Texture[] tariffs;
    public Texture[] suppliers;

    private float consumption;
    private float money;
    private float energy;

    private int frameCounter = 0;
    private bool timePassed = false;
    [HideInInspector] public bool questionAnswered = false;
    [HideInInspector] public bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        //disables all startimages
        foreach(RawImage img in invSlots)
        {
            img.enabled = false;
        }

        //sets the values of the variables to that of the sliders
        consumption = consumptionSlider.value;
        money = moneySlider.value;
        energy = energySlider.value;

        //enables dilemma canvas
        dilemmaCanvas.enabled = true;
        //starts 1st startdilemma
        Init(0);
    }

    // Update is called once per frame
    void Update()
    {
        //A question is answered
        if (questionAnswered == true)
        {
            frameCounter++;
            //5 seconds have passed - Ask a random question or dilemma
            if (frameCounter % (5 * 60) == 0)
            {
                timePassed = true;
                frameCounter = 0;
                AskQuestion();
            }
        }
    }

    //choices before start of the level
    private void Init(int num)
    {
        //remove all onclick listeners
        for (int i = 0; i < choiceBtns.Length; i++)
        {
            choiceBtns[i].onClick.RemoveAllListeners();
        }
        switch (num)
        {
            case 0:
                //starts 1st start dilemma
                FindObjectOfType<ConsumerDilemmaController>().StartDilemma(num);
                for (int i = 0; i < choiceBtns.Length; i++)
                {
                    int temp = i;
                    choiceBtns[i].onClick.AddListener(() =>
                    {
                        invSlots[num].texture = suppliers[temp];
                        Init(1);
                    });
                }
                invSlots[num].enabled = true;
                break;
            case 1:
                //starts 2nd start dilemma
                FindObjectOfType<ConsumerDilemmaController>().StartDilemma(num);
                for (int i = 0; i < choiceBtns.Length; i++)
                {
                    int temp = i;
                    choiceBtns[i].onClick.AddListener(() =>
                    {
                        invSlots[num].texture = tariffs[temp];
                        Init(2);
                    });
                }
                invSlots[num].enabled = true;
                break;
            case 2:
                //starts 3rd start dilemma
                FindObjectOfType<ConsumerDilemmaController>().StartDilemma(num);
                for (int i = 0; i < choiceBtns.Length; i++)
                {
                    int temp = i;
                    choiceBtns[i].onClick.AddListener(() =>
                    {
                        invSlots[num].texture = solarpanels[temp];
                        Init(3);
                    });
                }
                invSlots[num].enabled = true;
                break;
            case 3:
                //starts with normal level functionality
                //(ask a question or dilemma every 5 seconds)
                dilemmaCanvas.enabled = false;
                started = true;
                questionAnswered = true;
                break;
        }
    }

    public void AskQuestion()
    {
        System.Random random = new System.Random();
        //random variable that will determine the type of question that will be asked
        int rnd = random.Next(0, 2);
        //checks if a question has been answered and if 5 seconds have passed
        if (questionAnswered && timePassed)
        {
            quizCanvas.enabled = false;
            dilemmaCanvas.enabled = false;
            switch (rnd)
            {
                case 0:
                    //ask a dilemma
                    FindObjectOfType<ConsumerDilemmaController>().Dilemma();
                    dilemmaCanvas.enabled = true;
                    break;
                case 1:
                    //ask a multiple choice question
                    FindObjectOfType<ConsumerQuizController>().MultipleChoice();
                    quizCanvas.enabled = true;
                    break;
            }
            //new question: question isn't answered and 5 seconds haven't passed yet
            questionAnswered = timePassed = false;
        }

    }
}
