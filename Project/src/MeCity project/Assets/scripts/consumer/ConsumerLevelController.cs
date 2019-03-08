using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerLevelController : MonoBehaviour
{
    public Canvas dilemmaCanvas;
    public Canvas quizCanvas;
    public Text question;
    public Button[] CloseBtns;
    public Button[] choiceBtns;
    public RawImage[] invSlots;
    public Slider consumptionSlider;
    public Slider moneySlider;
    public Slider energySlider;

    public Texture[] solarpanels;
    public Texture[] tariffs;
    public Texture[] suppliers;

    private System.Random random = new System.Random();
    private int rnd;

    private float consumption;
    private float money;
    private float energy;
    // Start is called before the first frame update
    void Start()
    {
        rnd = random.Next(0, 2);

        consumption = consumptionSlider.value;
        money = moneySlider.value;
        energy = energySlider.value;

        dilemmaCanvas.enabled = true;
        Init(0);

        foreach (Button btn in CloseBtns)
        {
            btn.enabled = false;
            btn.GetComponent<Image>().color = Color.clear;
            btn.GetComponentInChildren<Text>().color = Color.clear;
            btn.onClick.AddListener(() =>
            {
                AskQuestion(rnd);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //choices before start of the level
    private void Init(int num)
    {
        for(int i = 0; i < choiceBtns.Length; i++)
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
                        invSlots[num].texture = solarpanels[temp];
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
                        invSlots[num].texture = suppliers[temp];
                        Init(3);
                    });
                }
                break;
            case 3:
                foreach(Button btn in CloseBtns)
                {
                    btn.enabled = true;
                    btn.GetComponent<Image>().color = Color.white;
                    btn.GetComponentInChildren<Text>().color = Color.black;
                }
                dilemmaCanvas.enabled = false;
                AskQuestion(rnd);
                break;
        }
    }

    public void AskQuestion(int mode)
    {
        rnd = random.Next(0, 2);
        quizCanvas.enabled = false;
        dilemmaCanvas.enabled = false;
        switch (mode)
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
    }
}
