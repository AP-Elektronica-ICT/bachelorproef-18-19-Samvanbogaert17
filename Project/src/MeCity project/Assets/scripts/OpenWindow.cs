using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenWindow : MonoBehaviour
{
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
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
        else
        {
            Debug.Log("opened");
            canvas.enabled = true;
        }
    }

    public void HardOpen()
    {
        canvas.gameObject.SetActive(true);
    }
}