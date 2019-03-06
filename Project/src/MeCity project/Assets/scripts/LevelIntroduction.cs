using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;

public class LevelIntroduction : MonoBehaviour
{
    public Canvas introCanvas;
    public Canvas uiCanvas;
    public Button nextBtn;
    public Button prevBtn;
    public Button skipBtn;
    public RawImage image;
    public Text txtField;
    [Tooltip("Make sure the images correspond to the text in the IntroXML file")]
    public Texture[] imgArray;
    private XmlDocument doc = new XmlDocument();

    // script used for the level introduction
    void Start()
    {
        skipBtn.onClick.AddListener(Quit);

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
        //make sure number does not go below 0
        if(number < 0)
        {
            number = 0;
        }
        //Make sure the images correspond to the text in the IntroXML file
        image.texture = imgArray[number];

        LoadText(number);

        prevBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.RemoveAllListeners();
        if (number + 1 != imgArray.Length)
        {
            prevBtn.onClick.AddListener(() => LoadIntro(number - 1));
            nextBtn.onClick.AddListener(() => LoadIntro(number + 1));
        }
        else
        {
            nextBtn.onClick.AddListener(Quit);
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
