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
        if (XMLManager.instance.db.list.Any(item => item.username == DataScript.GetName()))
        {
            XMLManager.instance.ModifyHighscore(DataScript.GetName(), DataScript.GetScore().ToString());
        }
        else
        {
            XMLManager.instance.AddHighscore(DataScript.GetName(), DataScript.GetScore().ToString());
        }
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

        foreach (HighscoreEntry entry in XMLManager.instance.db.list)
        {
            GameObject highscore = Instantiate(highscorePrefab, gridTransform);
            highscore.GetComponentsInChildren<Text>()[0].text = entry.username;
            highscore.GetComponentsInChildren<Text>()[1].text = entry.highscore;
        }
    }
}
