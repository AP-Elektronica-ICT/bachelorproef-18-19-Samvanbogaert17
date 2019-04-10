using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CorrectOrderController : MonoBehaviour
{
    public string childquestiontag = "cotext";
    public string parenttag = "CorrectOrder";
    public string childattribute = "orderpos";
    public Button closeBtn;
    public Button confirmBtn;
    public Text questionTxt;
    public GameObject[] answerBtnsWrappers;
    public Button[] answerBtns;
    public Text[] answerTxts;

    private int randomNumber;
    private int lastNumber;
    private int ansCount;
    private XmlDocument doc = new XmlDocument();
    private string sceneName;
    private System.Random rnd = new System.Random();
    private List<Vector3> defaultLocalPos = new List<Vector3>();
    private List<Vector3> shuffledLocalPos = new List<Vector3>();
    private List<Vector3> defaultPos = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < answerBtns.Length; i++)
        {
            defaultLocalPos.Add(answerBtnsWrappers[i].transform.localPosition);
            defaultPos.Add(answerBtnsWrappers[i].transform.position);
        }
        sceneName = SceneManager.GetActiveScene().name;
        closeBtn.onClick.AddListener(Task);
        confirmBtn.onClick.AddListener(Confirm);
        Task();
    }

    public void Task()
    {
        shuffledLocalPos.Clear();

        // hide all buttons so they are not visible when there are for example 3 answers (5 buttons in total)
        for (int i = 0; i < answerBtns.Length; i++)
        {
            answerBtns[i].gameObject.SetActive(false);
            answerBtns[i].onClick.RemoveAllListeners();
            answerBtns[i].GetComponent<Image>().color = Color.white;
            answerBtns[i].interactable = true;

            answerBtnsWrappers[i].transform.localPosition = defaultLocalPos[i];
        }
        // load the xml script
        TextAsset xmlData = new TextAsset();
        //Make sure the file name is the name of the scene + 'ScriptsXML'
        //e.g. scene name is 'FirstLevel' then filename should be FirstLevelScriptsXML
        string filename = sceneName + "ScriptsXML";
        xmlData = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        int range = doc.GetElementsByTagName(childquestiontag).Count;

        // generate a random number to show up a random popup
        int _randomNumber = RandomNumber(range);
        randomNumber = _randomNumber;
        // Read the random popup in the xml file and randomize button positions
        ReadXML(randomNumber);


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
        XmlNodeList elemList = doc.GetElementsByTagName(parenttag);
        ansCount = elemList[number].ChildNodes[1].ChildNodes.Count;
        XmlNodeList tekstList = doc.GetElementsByTagName(childquestiontag);
        questionTxt.text = tekstList[number].InnerText;

        // for each popup, the answers to the questions are given in the xml file, we search the number of answers
        //and generate this number of buttons, the answer texts are placed next to the buttons
        for (int i = 0; i < ansCount; i++)
        {
            answerBtns[i].gameObject.SetActive(true);
            answerBtns[i].GetComponentsInChildren<Text>()[1].text = elemList[number].ChildNodes[1].ChildNodes[i].InnerText;
            //add positions to shuffle array
            shuffledLocalPos.Add(answerBtnsWrappers[i].transform.localPosition);
            //
        }
        //shuffle the positions inside the array
        Shuffle(shuffledLocalPos);
        for (int i = 0; i < ansCount; i++)
        {
            //replace position of button with a random position
            answerBtnsWrappers[i].transform.localPosition = shuffledLocalPos[i];
        }
    }

    private void Shuffle(List<Vector3> shufflelist)
    {
        shuffledLocalPos = shufflelist.OrderBy(x => rnd.Next()).ToList();
    }

    void Confirm()
    {
        for (int i = 0; i < ansCount; i++)
        {
            Debug.Log(defaultPos[i].y + " | " + answerBtns[i].transform.position.y);
            if (defaultPos[i].y == answerBtns[i].transform.position.y)
            {
                answerBtns[i].GetComponent<Image>().color = new Color(0, 1, 0);
            }
            else
            {
                answerBtns[i].GetComponent<Image>().color = new Color(1, 0, 0);
            }
        }
    }
}
