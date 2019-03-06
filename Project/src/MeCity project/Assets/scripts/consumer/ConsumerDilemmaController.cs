using UnityEngine;
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
    private int number;
    private bool answered = false;
    private int lastNum = 0;
    private string sceneName;

    private float prevConsumption;
    private float prevMoney;
    private float prevEnergy;
    private float consumption;
    private float money;
    private float energy;


    // script used for the event popups
    public void Start()
    {
        consumption = prevConsumption = consumptionSlider.value;
        money = prevMoney = moneySlider.value;
        energy = prevEnergy = energySlider.value;

        sceneName = SceneManager.GetActiveScene().name;
    }

    public void Dilemma()
    {
        answered = false;
        // hide all buttons so they are not visible when there are for example 3 answers (5 buttons in total)
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].onClick.RemoveAllListeners();
            answerBtns[i].GetComponent<Image>().color = Color.white;
            answerBtns[i].interactable = true;
        }
        // load the xml script
        TextAsset xmlData = new TextAsset();
        //Make sure the file name is the name of the scene + 'ScriptsXML'
        //e.g. scene name is 'FirstLevel' then filename should be FirstLevelScriptsXML
        string filename = sceneName + "ScriptsXML";
        xmlData = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        dilemmaDoc.LoadXml(xmlData.text);
        int range = dilemmaDoc.GetElementsByTagName("text").Count;

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
        XmlNodeList elemList = dilemmaDoc.GetElementsByTagName("popup");
        int ansCount = elemList[number].ChildNodes[1].ChildNodes.Count;
        XmlNodeList tekstList = dilemmaDoc.GetElementsByTagName("text");
        questionTxt.text = tekstList[number].InnerText;

        // for each popup, the answers to the questions are given in the xml file, we search the number of answers
        //and generate this number of buttons, the answer texts are placed next to the buttons
        for (int i = 0; i < ansCount; i++)
        {
            answerBtns[i].gameObject.SetActive(true);
            answerBtns[i].GetComponentsInChildren<Text>()[1].text = elemList[number].ChildNodes[1].ChildNodes[i].InnerText;
        }
    }

    // every button contains an influence this is used to move the happiness slider right or left. This number is given in the xml file
    private void BtnAnswer(int btn, int number)
    {
        answered = true;
        CameraControl.showingPopUp = false;
        CameraControl.inQuiz = false;
        XmlNodeList elemlist = dilemmaDoc.GetElementsByTagName("popup");
        XmlNodeList list = elemlist[number].ChildNodes[1].ChildNodes;
        consumption += float.Parse(list[btn].Attributes["consumption"].Value);
        money += float.Parse(list[btn].Attributes["money"].Value);
        energy += float.Parse(list[btn].Attributes["energy"].Value);
        XmlNodeList tekstList = dilemmaDoc.GetElementsByTagName("text");

        if(consumption > prevConsumption || money > prevMoney || energy > prevEnergy)
        {
            if(consumption != prevConsumption)
            {
                while(consumptionSlider.value < consumption)
                {
                    consumptionSlider.value++;
                }
            }
            if(money != prevMoney)
            {
                while (moneySlider.value < money)
                {
                    moneySlider.value++;
                }
            }
            if(energy != prevEnergy)
            {
                while (energySlider.value < energy)
                {
                    energySlider.value++;
                }
            }
        }
        else
        {
            if (consumption != prevConsumption)
            {
                while (consumptionSlider.value > consumption)
                {
                    consumptionSlider.value--;
                }
            }
            if (money != prevMoney)
            {
                while (moneySlider.value > money)
                {
                    moneySlider.value--;
                }
            }
            if (energy != prevEnergy)
            {
                while (energySlider.value > energy)
                {
                    energySlider.value--;
                }
            }
        }

        prevConsumption = consumption;
        prevMoney = money;
        prevEnergy = energy;
    }
}