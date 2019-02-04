using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Introduction : MonoBehaviour
{
    public Canvas introCanvas;
    public Canvas uiCanvas;
    public Button btnNext;
    public RawImage image;
    public Text txtField;
    public Texture imgIntro;
    public Texture imgRegulated;
    public Texture imgHousePopup;
    public Texture imgMarketPopup;
    public Texture imgTarrifPopup;
    public Texture imgEventPopup;
    private XmlDocument doc = new XmlDocument();

    // script used for the level introduction
    void Start()
    {
        uiCanvas.enabled = false;
        introCanvas.enabled = true;
        Time.timeScale = 0;
        TextAsset xmlData = new TextAsset();

        xmlData = (TextAsset)Resources.Load("SupplierIntroXML", typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        XmlNodeList list = doc.GetElementsByTagName("text");
        image.texture = imgIntro;
        txtField.text = list[0].InnerText;
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(Regulated);
    }
    private void OnMouseDown()
    {
        if (!CameraControl.showingPopUp)
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                CameraControl.showingPopUp = true;
                Start();
            }
        }
    }
    public void Regulated()
    {
        image.texture = imgRegulated;
        LoadText(1);
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(Task);
    }
    public void Task()
    {
        image.texture = imgHousePopup;
        LoadText(2);
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(Task2);
    }
    public void Task2()
    {
        image.texture = imgMarketPopup;
        LoadText(3);
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(Task3);
    }
    public void Task3()
    {
        image.texture = imgTarrifPopup;
        LoadText(4);
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(Task4);
    }
    public void Task4()
    {
        image.texture = imgEventPopup;
        LoadText(5);
        btnNext.onClick.RemoveAllListeners();
        btnNext.onClick.AddListener(Quit);
    }
    private void LoadText(int number)
    {
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("SupplierIntroXML", typeof(TextAsset));
        doc.LoadXml(xmlData.text);
        XmlNodeList list = doc.GetElementsByTagName("text");
        txtField.text = list[number].InnerText;
    }
    public void Quit()
    {
        Time.timeScale = 1;
        introCanvas.enabled = false;
        uiCanvas.enabled = true;
        CameraControl.showingPopUp = false;
    }
}