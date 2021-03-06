﻿using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;

public class ConsumerDilemmaController : MonoBehaviour
{
    public Canvas dilemmaCanvas;
    public Button[] answerBtns;
    public Text[] answerTxts;
    public Text questionTxt;

    public Slider consumptionSlider;
    public Slider moneySlider;
    public Slider energySlider;

    private XmlDocument dilemmaDoc = new XmlDocument();
    private XmlNodeList elemList;

    private int number = 0;
    private int lastNum = 0;
    private string sceneName;

    private float prevConsumption;
    private float prevMoney;
    private float prevEnergy;
    private float consumption;
    private float money;
    private float energy;
    // script used for the event popups
    void Start()
    {
        //Set values of consumption, money and energy
        consumption = prevConsumption = consumptionSlider.value;
        money = prevMoney = moneySlider.value;
        energy = prevEnergy = energySlider.value;

        //Gets active scene name - used for getting the right XML file
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        //constantly updates consumption, money or energy
        changeConsumption(consumption, prevConsumption);
        changeMoney(money, prevMoney);
        changeEnergy(energy, prevEnergy);
    }

    public void Dilemma()
    {
        // hide all buttons so they are not visible when there are for example 3 answers (5 buttons in total)
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].onClick.RemoveAllListeners();
            answerBtns[i].interactable = true;
        }
        
        
        // load the xml script
        TextAsset xmlData = new TextAsset();

        //Make sure the file name is the name of the scene + 'ScriptsXML'
        //e.g. scene name is 'FirstLevel' then filename should be FirstLevelScriptsXML
        sceneName = SceneManager.GetActiveScene().name;
        string filename = sceneName + "ScriptsXML";
        xmlData = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        dilemmaDoc.LoadXml(xmlData.text);
        int range = dilemmaDoc.GetElementsByTagName("dtext").Count;
        
        // generate a random number to show up a random popup
        int randomgetal = RandomNumber(range);
        number = randomgetal;
        // Read the random popup in the xml file
        ReadXML(randomgetal);
        
        for (int i = 0; i < answerBtns.Length; i++)
        {
            int temp = i;
            answerBtns[temp].onClick.AddListener(() =>
            {
                BtnAnswer(temp, number);
                foreach (Button btn in answerBtns)
                {
                    btn.interactable = false;
                }
                FindObjectOfType<ConsumerLevelController>().questionAnswered = true;
            });
        }
        
    }
    // Generating a random number
    private int RandomNumber(int maxRange)
    {
        System.Random r = new System.Random();
        int x = r.Next(maxRange);
        while(x == lastNum)
        {
            x = r.Next(maxRange);
        }
        lastNum = x;
        return x;
    }

    // read the xml file
    private void ReadXML(int number)
    {
        // Search for text tags in the xml file
        elemList = dilemmaDoc.GetElementsByTagName("dilemma");
        int ansCount = elemList[number].ChildNodes[1].ChildNodes.Count;
        XmlNodeList tekstList = dilemmaDoc.GetElementsByTagName("dtext");
        questionTxt.text = tekstList[number].InnerText;

        // for each popup, the answers to the questions are given in the xml file, we search the number of answers
        //and generate this number of buttons, the answer texts are placed next to the buttons
        for (int i = 0; i < ansCount; i++)
        {
            answerBtns[i].gameObject.SetActive(true);
            answerBtns[i].GetComponentInChildren<Text>().text = elemList[number].ChildNodes[1].ChildNodes[i].InnerText;
        }
    }

    private void BtnAnswer(int btn, int number)
    {
        CameraControl.showingPopUp = false;
        CameraControl.inQuiz = false;
        //read the xml file and the attribute values of the answers
        XmlNodeList list = elemList[number].ChildNodes[1].ChildNodes;
        //add all values of the attributes to the sliders
        consumption += float.Parse(list[btn].Attributes["consumption"].Value)*100;
        money += float.Parse(list[btn].Attributes["money"].Value)*100;
        energy += float.Parse(list[btn].Attributes["energy"].Value)*100;
    }

    private void changeConsumption(float curr, float prev)
    {
        //checks if consumption changed
        if (consumption != prevConsumption)
        {
            //increases consumption slider value if consumption is higher than previous consumption
            if (consumption > prevConsumption)
            {
                consumptionSlider.value++;
                prevConsumption++;
            }
            //decreases consumption slider value if consumption is lower than previous consumption
            else
            {
                consumptionSlider.value--;
                prevConsumption--;
            }
        }
    }

    private void changeMoney(float curr, float prev)
    {
        //checks if money changed
        if (money != prevMoney)
        {
            //increases money slider value if money is higher than previous money
            if (money > prevMoney)
            {
                moneySlider.value++;
                prevMoney++;
            }
            //decreases money slider value if money is lower than previous money
            else
            {
                moneySlider.value--;
                prevMoney--;
            }
        }
    }

    private void changeEnergy(float curr, float prev)
    {
        if (energy != prevEnergy)
        {
            //increases energy slider value if energy is higher than previous energy
            if (energy > prevEnergy)
            {
                energySlider.value++;
                prevEnergy++;
            }
            //decreases money slider value if money is lower than previous money
            else
            {
                energySlider.value--;
                prevEnergy--;
            }
        }
    }

    //start dilemma function - only called at the beginning of the level
    public void StartDilemma(int mode)
    {
        // hide all buttons so they are not visible when there are for example 3 answers (5 buttons in total)
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].onClick.RemoveAllListeners();
            answerBtns[i].interactable = true;
        }
        // load the xml script
        TextAsset xmlData = new TextAsset();
        sceneName = SceneManager.GetActiveScene().name;
        string filename = sceneName + "ScriptsXML";
        xmlData = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        dilemmaDoc.LoadXml(xmlData.text);

        // read XML
        // Search for text tags in the xml file
        elemList = dilemmaDoc.GetElementsByTagName("startdilemma");
        int ansCount = elemList[mode].ChildNodes[1].ChildNodes.Count;
        XmlNodeList tekstList = dilemmaDoc.GetElementsByTagName("sdtext");
        questionTxt.text = tekstList[mode].InnerText;
        //
        for (int i = 0; i < ansCount; i++)
        {
            answerBtns[i].gameObject.SetActive(true);
            answerBtns[i].GetComponentInChildren<Text>().text = elemList[mode].ChildNodes[1].ChildNodes[i].InnerText;
        }
        //
        for (int i = 0; i < answerBtns.Length; i++)
        {
            int temp = i;
            answerBtns[temp].onClick.RemoveAllListeners();
            answerBtns[temp].onClick.AddListener(() =>
            {
                BtnAnswer(temp, mode);
                foreach (Button btn in answerBtns)
                {
                    btn.interactable = false;
                }
            });
        }
    }
}