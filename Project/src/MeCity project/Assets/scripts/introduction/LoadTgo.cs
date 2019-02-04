using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LoadTgo : MonoBehaviour {

    public Button btn;
    public Canvas tgoCanvas;
    public Canvas dgoCanvas;
    public Text textvak;
    private XmlDocument doc = new XmlDocument();

    private void Start()
    {
        btn.onClick.AddListener(Task);
    }
    public void Task()
    {
        // play the tgo animation, hide the dgocanvas and open the tgocanvas
        Camera.main.GetComponent<Animation>().Play("TGOAnimation");
        dgoCanvas.enabled = false;
        tgoCanvas.enabled = true;

        // load the xml script
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("IntroScriptsXML", typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        XmlNodeList list = doc.GetElementsByTagName("text");
        textvak.text = list[3].InnerText;
    }
}