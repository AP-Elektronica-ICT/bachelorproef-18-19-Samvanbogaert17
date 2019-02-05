using UnityEngine;
using UnityEngine.UI;

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
        if (!CameraControl.showingPopUp)
        {
            CameraControl.showingPopUp = true;
            if (canvas.name == "MarketCanvas")
            {
                FindObjectOfType<SupplierChangeDGO>().RefreshItems();
            }
            canvas.enabled = true;
        }
    }
}