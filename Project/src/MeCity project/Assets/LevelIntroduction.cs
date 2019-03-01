using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;

public class LevelIntroduction : MonoBehaviour
{
    public Canvas introCanvas;
    public Canvas uiCanvas;
    public Button btnNext;
    public RawImage image;
    public Text txtField;
    [Tooltip("Make sure the images correspond to the text in the IntroXML file")]
    public Texture[] imgArray;
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
        //Make sure the images correspond to the text in the IntroXML file
        image.texture = imgArray[number];

        LoadText(number);
        btnNext.onClick.RemoveAllListeners();
        if (number + 1 != imgArray.Length)
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

        string filename = SceneManager.GetActiveScene().name + "IntroXML";
        xmlData = (TextAsset)Resources.Load(filename, typeof(TextAsset));
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
