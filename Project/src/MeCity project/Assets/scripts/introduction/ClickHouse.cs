using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class ClickHouse : MonoBehaviour
{
    public Canvas meganCanvas;
    public Text textvak;
    public Canvas infoCanvas;
    public Canvas supplierCanvas;
    private XmlDocument doc = new XmlDocument();

    // script used to start the introduction when clicking on the house
    void OnMouseDown()
    {
        // hide the infocanvas and the megancanvas
        infoCanvas.enabled = false;
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