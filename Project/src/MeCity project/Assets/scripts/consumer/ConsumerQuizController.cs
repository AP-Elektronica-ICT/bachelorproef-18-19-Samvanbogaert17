using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;

public class ConsumerQuizController : MonoBehaviour
{
    public Canvas quizCanvas;
    public Button btn;
    public Button[] answerBtns;
    public Text[] answerTxts;
    public Text questionTxt;

    public Slider consumptionSlider;
    public Slider moneySlider;
    public Slider energySlider;

    private XmlDocument multipleChoiceDoc = new XmlDocument();
    private int money;
    private int number;
    private bool answered = false;
    private int lastNum = 0;
    private string sceneName;

    // script used for the event popups
    public void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        btn.onClick.AddListener(MultipleChoice);
    } 

    public void MultipleChoice()
    {
        answered = false;
        // hide all buttons so they are not visible when there are for example 3 answers (5 buttons in total)
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].gameObject.SetActive(false);
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
        multipleChoiceDoc.LoadXml(xmlData.text);
        int range = multipleChoiceDoc.GetElementsByTagName("text").Count;

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
        XmlNodeList elemList = multipleChoiceDoc.GetElementsByTagName("popup");
        int ansCount = elemList[number].ChildNodes[1].ChildNodes.Count;
        XmlNodeList tekstList = multipleChoiceDoc.GetElementsByTagName("text");
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
        XmlNodeList elemlist = multipleChoiceDoc.GetElementsByTagName("popup");
        XmlNodeList list = elemlist[number].ChildNodes[1].ChildNodes;
        int influence = int.Parse(list[btn].Attributes["influence"].Value);
        XmlNodeList tekstList = multipleChoiceDoc.GetElementsByTagName("text");

        //All code for ScoreCanvas
        //
        //Add questionList to QnA question list
        FindObjectOfType<QnAscore>().questionList.Add(tekstList[number].InnerText);
        //check which answer is correct answer and add to CorrectAnswerList
        for (int i = 0; i < elemlist[number].ChildNodes[1].ChildNodes.Count; i++)
        {
            if (int.Parse(elemlist[number].ChildNodes[1].ChildNodes[i].Attributes["influence"].Value) > 0)
            {
                FindObjectOfType<QnAscore>().correctAnsList.Add(elemlist[number].ChildNodes[1].ChildNodes[i].InnerText);
            }
        }
        //Add player answer to PlayerAnswerList
        FindObjectOfType<QnAscore>().playerAnsList.Add(elemlist[number].ChildNodes[1].ChildNodes[btn].InnerText);
        //
        DataScript.AddScore(influence * 100);

        for (int i = 0; i < list.Count; i++)
        {
            if (int.Parse(list[i].Attributes["influence"].Value) > 0)
            {
                EndOfGame.NumberOfCorrectAnswers++;
                answerBtns[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                answerBtns[i].GetComponent<Image>().color = Color.red;
            }
        }
    }
}