using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenWindow : MonoBehaviour
{
    public Button btn;
    public Canvas canvas;
    public void Start()
    {
        btn.onClick.AddListener(Open);
    }

    // script for opening the market canvas
    public void Open()
    {
        if(SceneManager.GetActiveScene().name == "Supplier")
        {
            if (!CameraControl.showingPopUp)
            {
                CameraControl.showingPopUp = true;
                if (canvas.name == "MarketCanvas")
                {
                    FindObjectOfType<changeDGO>().RefreshItems();
                }
                canvas.enabled = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Producer" || SceneManager.GetActiveScene().name == "DGO" || SceneManager.GetActiveScene().name == "TGO")
        {
                CameraControl.showingPopUp = !CameraControl.showingPopUp;
                canvas.enabled = !canvas.enabled;
        }
    }
}