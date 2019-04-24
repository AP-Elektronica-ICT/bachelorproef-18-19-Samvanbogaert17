using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gridGO;
    public GameObject highscorePrefab;

    private Transform gridTransform;
    void Start()
    {
        gridTransform = gridGO.transform;
        XMLManager.instance.LoadHighscores();
        XMLManager.instance.ReorderHighscores();
        XMLManager.instance.SaveHighscores();
        DisplayHighscores();
    }

    void DisplayHighscores()
    {
        foreach (Transform child in gridTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (HighscoreEntry entry in XMLManager.instance.highscoreDB.list)
        {
            GameObject highscore = Instantiate(highscorePrefab, gridTransform);
            highscore.GetComponentInChildren<Image>().color = XMLManager.instance.highscoreDB.list.FindIndex(item => item == entry) % 2 == 0 ?
                new Color(0.8f,0.8f,0.8f, 0.4f): 
                new Color(0.8f,0.8f,0.8f,0f);
            highscore.GetComponentInChildren<Image>().GetComponentsInChildren<Text>()[0].text = entry.username;
            highscore.GetComponentInChildren<Image>().GetComponentsInChildren<Text>()[1].text = entry.highscore;
        }
    }
}
