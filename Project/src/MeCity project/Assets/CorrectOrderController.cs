using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CorrectOrderController : MonoBehaviour
{
    public Button closeBtn;
    public Button confirmBtn;
    public Text questionTxt;
    public Button[] answerBtns;
    public Text[] answerTxts;
    private Text[] correctAnsTxts;

    private int randomNumber;
    private int lastNumber;
    private XmlDocument doc = new XmlDocument();
    private string sceneName;
    private System.Random rnd = new System.Random();
    private List<Vector3> defaultPos = new List<Vector3>();
    private List<Vector3> shuffledPos = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < answerBtns.Length; i++)
        {
            defaultPos.Add(answerBtns[i].transform.localPosition);
        }
        sceneName = SceneManager.GetActiveScene().name;
        closeBtn.onClick.AddListener(Task);

    }

    public void Task()
    {
        shuffledPos.Clear();

        // hide all buttons so they are not visible when there are for example 3 answers (5 buttons in total)
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].gameObject.SetActive(false);
            answerBtns[i].onClick.RemoveAllListeners();
            answerBtns[i].transform.localPosition = defaultPos[i];
            answerBtns[i].GetComponent<Image>().color = Color.white;
            answerBtns[i].interactable = true;
        }
        // load the xml script
        TextAsset xmlData = new TextAsset();
        //Make sure the file name is the name of the scene + 'ScriptsXML'
        //e.g. scene name is 'FirstLevel' then filename should be FirstLevelScriptsXML
        string filename = sceneName + "ScriptsXML";
        xmlData = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        int range = doc.GetElementsByTagName("text").Count;

        // generate a random number to show up a random popup
        int _randomNumber = RandomNumber(range);
        randomNumber = _randomNumber;
        // Read the random popup in the xml file
        ReadXML(randomNumber);

        for (int i = 0; i < answerBtns.Length; i++)
        {

            int temp = i;
            answerBtns[temp].onClick.AddListener(() =>
            {
                BtnAnswer(temp, randomNumber);
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
        while (x == lastNumber)
        {
            x = r.Next(maxRange);
        }
        lastNumber = x;
        return x;
    }

    // read the xml file
    private void ReadXML(int number)
    {
        // Search for text tags in the xml file
        XmlNodeList elemList = doc.GetElementsByTagName("popup");
        int ansCount = elemList[number].ChildNodes[1].ChildNodes.Count;
        XmlNodeList tekstList = doc.GetElementsByTagName("text");
        tekstvak.text = tekstList[number].InnerText;

        // for each popup, the answers to the questions are given in the xml file, we search the number of answers
        //and generate this number of buttons, the answer texts are placed next to the buttons
        for (int i = 0; i < ansCount; i++)
        {
            answerBtns[i].gameObject.SetActive(true);
            answerBtns[i].GetComponentsInChildren<Text>()[1].text = elemList[number].ChildNodes[1].ChildNodes[i].InnerText;
            //add positions to shuffle array
            shuffledPos.Add(answerBtns[i].transform.localPosition);
            //
        }
        //shuffle the positions inside the array
        Shuffle(shuffledPos);
        for (int i = 0; i < ansCount; i++)
        {
            answerBtns[i].transform.localPosition = shuffledPos[i];
        }
    }

    // every button contains an influence this is used to move the happiness slider right or left. This number is given in the xml file
    private void BtnAnswer(int btn, int number)
    {
        //answered = true;
        CameraControl.showingPopUp = false;
        CameraControl.inQuiz = false;
        XmlNodeList elemlist = doc.GetElementsByTagName("popup");
        XmlNodeList list = elemlist[number].ChildNodes[1].ChildNodes;
        int influence = int.Parse(list[btn].Attributes["influence"].Value);
        XmlNodeList tekstList = doc.GetElementsByTagName("text");

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
        if (influence < 0)
        {
            if (EndOfGame.NumberOfCorrectAnswers > 0)
            {
                EndOfGame.NumberOfCorrectAnswers = 0;
            }
            EndOfGame.NumberOfCorrectAnswers--;
        }
        if (influence > 0)
        {
            if (EndOfGame.NumberOfCorrectAnswers < 0)
            {
                EndOfGame.NumberOfCorrectAnswers = 0;
            }
            EndOfGame.NumberOfCorrectAnswers++;
        }
        DataScript.AddScore(influence * 100);

        for (int i = 0; i < list.Count; i++)
        {
            if (int.Parse(list[i].Attributes["influence"].Value) > 0)
            {
                answerBtns[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                answerBtns[i].GetComponent<Image>().color = Color.red;
            }
        }
    }

    private void Shuffle(List<Vector3> shufflelist)
    {
        shuffledPos = shufflelist.OrderBy(x => rnd.Next()).ToList();
    }

    void Confirm()
    {

    }
}
