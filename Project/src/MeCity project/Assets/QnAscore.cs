using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QnAscore : MonoBehaviour
{
    public GameObject QnAContentPanel;

    [HideInInspector] public List<string> questionList = new List<string>();
    [HideInInspector] public List<string> correctAnsList = new List<string>();
    [HideInInspector] public List<string> playerAnsList = new List<string>();

    private List<GameObject> QnAGrid = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addPanel()
    {
        QnAGrid.Add(Instantiate(QnAContentPanel, transform));
        int ind = QnAGrid.Count - 1;
        QnAGrid[ind].GetComponentsInChildren<Text>()[1].text = questionList[ind];
        QnAGrid[ind].GetComponentInChildren<RectTransform>().GetComponentsInChildren<Text>()[0].text = correctAnsList[ind];
        QnAGrid[ind].GetComponentInChildren<RectTransform>().GetComponentsInChildren<Text>()[1].text = playerAnsList[ind];

        if (correctAnsList[ind] == playerAnsList[ind])
        {
            QnAGrid[ind].GetComponentInChildren<RectTransform>().GetComponentsInChildren<Image>()[1].color = Color.green;
        }
        else
        {
            QnAGrid[ind].GetComponentInChildren<RectTransform>().GetComponentsInChildren<Image>()[1].color = Color.red;
        }
    }
}
