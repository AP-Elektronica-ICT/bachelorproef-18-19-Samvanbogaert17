using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerLevelController : MonoBehaviour
{
    public Canvas dilemmaCanvas;
    public Text question;
    public Button choice1Btn;
    public Button choice2Btn;
    public Text choice1Txt;
    public Text choice2;
    public RawImage[] invSlots;
    public Slider consumptionSlider;
    public Slider moneySlider;
    public Slider energySlider;

    public Texture[] solarpanels;
    public Texture[] tariffs;
    public Texture[] suppliers;

    private System.Random rnd = new System.Random();

    private float consumption;
    private float money;
    private float energy;
    // Start is called before the first frame update
    void Start()
    {
        consumption = consumptionSlider.value;
        money = moneySlider.value;
        energy = energySlider.value;

        dilemmaCanvas.enabled = true;
        choice1Btn.onClick.AddListener(() => Init(0, 0));
        choice1Btn.onClick.AddListener(() => Init(0, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //choices before start of the level
    private void Init(int num, int choice)
    {
        choice1Btn.onClick.RemoveAllListeners();
        choice2Btn.onClick.RemoveAllListeners();
        switch (num)
        {
            case 0:
                invSlots[num].texture = solarpanels[choice];
                choice1Btn.onClick.AddListener(() => Init(1, 0));
                choice2Btn.onClick.AddListener(() => Init(1, 1));
                break;
            case 1:
                invSlots[num].texture = tariffs[choice];
                choice1Btn.onClick.AddListener(() => Init(2, 0));
                choice2Btn.onClick.AddListener(() => Init(2, 1));
                break;
            case 2:
                invSlots[num].texture = tariffs[choice];
                dilemmaCanvas.enabled = false;
                AskQuestion(rnd.Next(0, 2));
                break;
        }
    }

    private void AskQuestion(int mode)
    {
        switch (mode)
        {
            case 0:
                FindObjectOfType<ConsumerDilemmaController>().Dilemma();
                break;
            case 1:
                FindObjectOfType<ConsumerQuizController>().MultipleChoice();
                break;
        }
    }

    private abstract class Question
    {
        private Question(int id, string Question, string note = "")
        {

        }
    }
}
