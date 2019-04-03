using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QnAscore : MonoBehaviour
{
    public Button ViewScoreBtn;
    public Button NextBtn;

    public Text ansCorrectCountTxt;
    public Text ansWrongCountTxt;

    public Image playerAnsImg;
    public Text questionTxt;
    public Text correctAnsTxt;
    public Text playerAnsTxt;

    [HideInInspector] public List<string> questionList = new List<string>();
    [HideInInspector] public List<string> correctAnsList = new List<string>();
    [HideInInspector] public List<string> playerAnsList = new List<string>();

    private int ansCorrectCount;
    private int ansWrongCount;
    private bool initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        ViewScoreBtn.onClick.AddListener(() =>
        {
            FindObjectOfType<CameraControl>().DisableAllCanvases();
            Init();
            Next(0);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Next(int index)
    {
        if(index < questionList.Count)
        {
            questionTxt.text = questionList[index];
            correctAnsTxt.text = correctAnsList[index];
            playerAnsTxt.text = playerAnsList[index];
            if (correctAnsList[index] == playerAnsList[index])
            {
                playerAnsImg.color = Color.green;
            }

            else
            {
                playerAnsImg.color = Color.red;
            }

            NextBtn.onClick.RemoveAllListeners();
            NextBtn.onClick.AddListener(() =>
            {
                Next(index + 1);
            });
        }
    }
    public void Init()
    {
        if (!initialized)
        {
            for (int i = 0; i < questionList.Count; i++)
            {
                if (correctAnsList[i] == playerAnsList[i])
                {
                    ansCorrectCount++;
                }
                else
                {
                    ansWrongCount++;
                }
            }

            ansCorrectCountTxt.text = ansCorrectCount.ToString();
            ansWrongCountTxt.text = ansWrongCount.ToString();
            initialized = true;
        }
    }
}
