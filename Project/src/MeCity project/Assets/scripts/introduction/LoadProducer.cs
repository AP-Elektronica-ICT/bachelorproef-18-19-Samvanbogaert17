using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LoadProducer : MonoBehaviour {

    public Button btn;
    public Canvas producer;
    public Canvas tgoCanvas;
    public Text textvak;
    private XmlDocument doc = new XmlDocument();

    // script used to load the producer canvas
    private void Start()
    {
        btn.onClick.AddListener(Task);
    }
    public void Task()
    {
        // play the producer animation, hide the tgocanvas and open the producer canvas
        Camera.main.GetComponent<Animation>().Play("ProducerAnimation");
        tgoCanvas.enabled = false;
        producer.enabled = true;

        // load the xml script
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("IntroScriptsXML", typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        XmlNodeList list = doc.GetElementsByTagName("text");

        textvak.text = list[4].InnerText;
    }
}