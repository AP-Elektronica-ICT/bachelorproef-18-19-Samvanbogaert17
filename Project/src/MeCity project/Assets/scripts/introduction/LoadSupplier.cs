using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LoadSupplier : MonoBehaviour {
    public Button btn;
    public Canvas supplierCanvas;
    public Canvas meganCanvas;
    public Text textvak;
    private XmlDocument doc = new XmlDocument();

    // script used to load the supplier canvas
    private void Start()
    {
        Update();
    }
    private void Update()
    {
         if (!Camera.main.GetComponent<Animation>().isPlaying)
        {
            btn.onClick.AddListener(Task);
          
        }
    }
    public void Task()
    {
        // play the supplier animation, hide the megancanvas and open the supplier canvas
        Camera.main.GetComponent<Animation>().Play("SupplierAnimation");
        meganCanvas.enabled = false;
        supplierCanvas.enabled = true;

        // load the xml file
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("IntroScriptsXML", typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        XmlNodeList list = doc.GetElementsByTagName("text");
        textvak.text = list[1].InnerText;
    }
}