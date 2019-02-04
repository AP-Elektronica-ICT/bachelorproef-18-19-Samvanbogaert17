using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LoadDgo : MonoBehaviour {

    public Button btn;
    public Canvas dgoCanvas;
    public Canvas supplierCanvas;
    public Text textvak;
    private XmlDocument doc = new XmlDocument();

    // script used to load the dgo canvas
    private void Start()
    {
            btn.onClick.AddListener(Task);
    }
    public void Task()
    {
        // play the dgo animation, hide the suppliercanvas and open the dgocanvas
        Camera.main.GetComponent<Animation>().Play("DGOAnimation");
        supplierCanvas.enabled = false;
        dgoCanvas.enabled = true;

        // load the xml script
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("IntroScriptsXML", typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        XmlNodeList list = doc.GetElementsByTagName("text");
        textvak.text = list[2].InnerText;
    }
}