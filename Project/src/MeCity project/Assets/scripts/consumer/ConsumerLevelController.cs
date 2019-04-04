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
        consumption = consumptionSlider.value;
        money = moneySlider.value;
        energy = energySlider.value;

        dilemmaCanvas.enabled = true;
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
        for (int i = 0; i < choiceBtns.Length; i++)
        {
            choiceBtns[i].onClick.RemoveAllListeners();
        }
        switch (num)
        {
            case 0:
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
                break;
            case 1:
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
                break;
            case 2:
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
                break;
            case 3:
                dilemmaCanvas.enabled = false;
                started = true;
                questionAnswered = true;
                break;
        }
    }

    public void AskQuestion()
    {
        System.Random random = new System.Random();
        int rnd = random.Next(0, 2);
        if (questionAnswered && timePassed)
        {
            quizCanvas.enabled = false;
            dilemmaCanvas.enabled = false;
            switch (rnd)
            {
                case 0:
                    FindObjectOfType<ConsumerDilemmaController>().Dilemma();
                    dilemmaCanvas.enabled = true;
                    break;
                case 1:
                    FindObjectOfType<ConsumerQuizController>().MultipleChoice();
                    quizCanvas.enabled = true;
                    break;
            }
            questionAnswered = timePassed = false;
        }

    }
}
