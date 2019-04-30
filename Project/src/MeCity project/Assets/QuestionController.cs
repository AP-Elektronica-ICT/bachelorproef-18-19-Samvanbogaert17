using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    private string parenttag = "CorrectOrder";
    private string childquestiontag = "cotext";
    private string childattribute = "influence";

    private XmlDocument doc = new XmlDocument();
    private int number;
    private int lastNum = 0;
    private string sceneName;

    private System.Random rng = new System.Random();


    // script used for the quiz canvas
    public void Start()
    {
        sceneName = "Mecoms";
    }

    public void Task()
    {
        // load the xml script
        TextAsset xmlData = new TextAsset();
        //Make sure the file name is the name of the scene + 'ScriptsXML'
        //e.g. scene name is 'FirstLevel' then filename should be FirstLevelScriptsXML
        string filename = sceneName + "ScriptsXML";
        xmlData = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        int range = doc.GetElementsByTagName(childquestiontag).Count;

        // generate a random number to show up a random popup

        // Read the random popup in the xml file
        ReadCorrectOrderXML(range);
    }

    // read the xml file
    private void ReadQuestionXML(int number)
    {
        for(int i = 0; i < number; i++)
        {
            // Search for text tags in the xml file
            XmlNodeList elemList = doc.GetElementsByTagName(parenttag);
            int ansCount = elemList[i].ChildNodes[1].ChildNodes.Count;
            XmlNodeList tekstList = doc.GetElementsByTagName(childquestiontag);
            List<Answer> ansList = new List<Answer>();
            for (int j = 0; j < ansCount; j++)
            {
                int _modifier;
                if(int.Parse(elemList[i].ChildNodes[1].ChildNodes[j].Attributes[childattribute].Value) < 0){
                    _modifier = -1;
                }
                else
                {
                    _modifier = 1;
                }
                ansList.Add(new Answer
                {
                    answer = elemList[i].ChildNodes[1].ChildNodes[j].InnerText,
                    modifier = _modifier
                });
                //
            }
            XMLManager.instance.AddQuestion(sceneName, tekstList[i].InnerText, ansList);
        }
        XMLManager.instance.SaveQuestions();
    }

    private void ReadDilemmaXML(int number)
    {
        for (int i = 0; i < number; i++)
        {
            // Search for text tags in the xml file
            XmlNodeList elemList = doc.GetElementsByTagName(parenttag);
            int ansCount = elemList[i].ChildNodes[1].ChildNodes.Count;
            XmlNodeList tekstList = doc.GetElementsByTagName(childquestiontag);
            List<Dilemma> ansList = new List<Dilemma>();
            for (int j = 0; j < ansCount; j++)
            {
                ansList.Add(new Dilemma
                {
                    choice = elemList[i].ChildNodes[1].ChildNodes[j].InnerText,
                    consumption = int.Parse(elemList[i].ChildNodes[1].ChildNodes[j].Attributes["consumption"].Value),
                    money = int.Parse(elemList[i].ChildNodes[1].ChildNodes[j].Attributes["money"].Value),
                    energy = int.Parse(elemList[i].ChildNodes[1].ChildNodes[j].Attributes["energy"].Value)
                });
                //
            }
            XMLManager.instance.AddDilemma(sceneName, tekstList[i].InnerText, ansList);
        }
        XMLManager.instance.SaveQuestions();
    }

    private void ReadCorrectOrderXML(int number)
    {
        for (int i = 0; i < number; i++)
        {
            // Search for text tags in the xml file
            XmlNodeList elemList = doc.GetElementsByTagName(parenttag);
            int ansCount = elemList[i].ChildNodes[1].ChildNodes.Count;
            XmlNodeList tekstList = doc.GetElementsByTagName(childquestiontag);
            List<Position> ansList = new List<Position>();
            for (int j = 0; j < ansCount; j++)
            {
                ansList.Add(new Position
                {
                   position = elemList[i].ChildNodes[1].ChildNodes[j].InnerText
                });
                //
            }
            XMLManager.instance.AddCorrectOrder(sceneName, tekstList[i].InnerText, ansList);
        }
        XMLManager.instance.SaveQuestions();
    }
}
