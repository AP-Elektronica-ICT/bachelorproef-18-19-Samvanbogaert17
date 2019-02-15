using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class ProducerIntroduction : MonoBehaviour
{
    public Canvas introCanvas;
    public Canvas uiCanvas;
    public Button btnNext;
    public RawImage image;
    public Text txtField;
    public Texture imgIntro;
    public Texture imgUI;
    public Texture imgMarketPopup;
    public Texture imgContractsPopup;
    public Texture imgEventPopup;
    private XmlDocument doc = new XmlDocument();

    // script used for the level introduction
    void Start()
    {
        uiCanvas.enabled = false;
        introCanvas.enabled = true;
        Time.timeScale = 0;

        LoadIntro(0);
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

    public void LoadIntro(int number)
    {
        switch (number)
        {
            case 0:
                image.texture = imgIntro;
                break;
            case 1:
                image.texture = imgUI;
                break;
            case 2:
                image.texture = imgMarketPopup;
                break;
            case 3:
                image.texture = imgContractsPopup;
                break;
            case 4:
                image.texture = imgEventPopup;
                break;
        }
        LoadText(number);
        btnNext.onClick.RemoveAllListeners();
        if(number + 1 != 5)
        {
            btnNext.onClick.AddListener(() => LoadIntro(number + 1));
        }
        else
        {
            btnNext.onClick.AddListener(Quit);
        }
    }

    private void LoadText(int number)
    {
        TextAsset xmlData = new TextAsset();
        xmlData = (TextAsset)Resources.Load("ProducerIntroXML", typeof(TextAsset));
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