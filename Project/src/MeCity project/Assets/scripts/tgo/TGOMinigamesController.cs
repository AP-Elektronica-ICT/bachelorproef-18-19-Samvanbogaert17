using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOMinigamesController : MonoBehaviour
{
    public Button minigamesBtn;
    public Button[] minigameBtnsArray;
    bool GameStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Button btn in minigameBtnsArray)
        {
            btn.onClick.AddListener(OnClick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        GameStarted = !GameStarted;

        if (GameStarted)
        {
            minigamesBtn.interactable = false;
        }
        else
        {
            minigamesBtn.interactable = true;
        }
    }
}
