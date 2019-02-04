using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class EnableNextLevel : MonoBehaviour
{
    public GameObject btn;
    public GameObject btnAgain2;
    public Button btnAgain;
    public XmlDocument doc = new XmlDocument();
    public Canvas startCanvas;
    public Canvas meganCanvas;
    public Canvas supplierCanvas;
    public Text textvak;

    // script used to enable the next level and again button after the overview animation
    void Update()
    {
        btn.SetActive(true);
        btnAgain2.SetActive(true);
        btnAgain.onClick.AddListener(Task);
    }
    public void Task()
    {
        // enable the megancanvas
        meganCanvas.enabled = true;

        // load the xml script
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("IntroScriptsXML", typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        XmlNodeList list = doc.GetElementsByTagName("text");
        textvak.text = list[0].InnerText;

        if (supplierCanvas.isActiveAndEnabled)
        {
            meganCanvas.enabled = false;
        }
    }
}