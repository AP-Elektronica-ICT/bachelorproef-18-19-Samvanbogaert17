using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumerLevelController : MonoBehaviour
{
    public Canvas dilemmaCanvas;
    public Text question;
    public Button[] choiceBtns;
    public Button choice1Btn;
    public Button choice2Btn;
    public Text choice1Txt;
    public Text choice2Txt;
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
        Init(0);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //choices before start of the level
    private void Init(int num)
    {
        switch (num)
        {
            case 0:
                FindObjectOfType<ConsumerDilemmaController>().StartDilemma(num);
                for (int i = 0; i < choiceBtns.Length; i++)
                {
                    int temp = i;
                    choiceBtns[i].onClick.RemoveAllListeners();
                    choiceBtns[i].onClick.AddListener(() =>
                    {
                        Debug.Log(invSlots.Length);
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
                    choiceBtns[i].onClick.RemoveAllListeners();
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
                    choiceBtns[i].onClick.RemoveAllListeners();
                    choiceBtns[i].onClick.AddListener(() =>
                    {
                        invSlots[num].texture = suppliers[temp];
                        Init(3);
                    });
                }
                break;
            case 3:
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
