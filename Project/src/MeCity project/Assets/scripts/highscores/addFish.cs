using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class addFish : MonoBehaviour
{
    public List<GameObject> fishes = new List<GameObject>();
    public string[] lines;
    public int[] scores;
    public Canvas canvas;

    // is addressed in other scripts from the same scene:
    public Canvas meganCanvas;
    public Text txtPlayer;
    public Text txtScore;

    // script used to copy the online highscores file offline to the persistent datapath and to initiate the fishes in the fish aquarium
    void Start()
    {
        // disable the highscores screen and the fish name and score screen
        FindObjectOfType<btnScoresOnClick>().highscores.enabled = false;
        canvas.enabled = false;

        // the offline highscores.txt file path
        string offlineHighscoresPath = Application.persistentDataPath + "/highscores.txt";


        // delete the offline highscores.txt file when it exists
        if (File.Exists(offlineHighscoresPath))
        {
            File.Delete(offlineHighscoresPath);
        }

        // copy the online highscores.txt to the local persistentdatapath, decrypt this file so the file can be read
        // there will not be writed to the offline highscores file so encryption is redundant
        File.Copy(Encryption.path, offlineHighscoresPath);
        Encryption.DecryptFile(offlineHighscoresPath, Encryption.key);

        // read all the lines in the offline highscores.txt
        lines = File.ReadAllLines(offlineHighscoresPath);
        
        // make a new list with all the players and scores and order this list
        List<string> orderredList = new List<string>();
        orderredList.Add(lines[0]);
        for (int i = 1; i < lines.Length; i++)
        {
            int insertAtIndex = orderredList.Count;
            for (int j = 0; j < orderredList.Count; j++)
            {
                if (float.Parse(lines[i].Split(':')[1]) > float.Parse(orderredList[j].Split(':')[1]))
                {
                    insertAtIndex = j;
                    break;
                }
            }
            orderredList.Insert(insertAtIndex, lines[i]);
        }
        lines = new string[orderredList.Count];
        lines = orderredList.ToArray();
        // initiate the fishes
        for (int x = 0; x < lines.Length; x++)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load("cruscarp"));
            fishes.Add(go);
            go.name = lines[x].Substring(0, lines[x].IndexOf(':'));
        }
        // get an average for the sizes of the fishes and set the size
        float avg = 0;
        for (int i = 0; i < fishes.Count; i++)
        {
            string test = lines[i];
            string[] laatste = test.Split(':');

            avg += float.Parse(laatste[1]);
        }

        avg = avg / fishes.Count;

        System.Random random = new System.Random();
        for (int i = 0; i < fishes.Count; i++)
        {
            string test = lines[i];
            string[] laatste = test.Split(':');

            fishes[i].transform.localScale = new Vector3(20f * ((float)Camera.main.orthographicSize / 100f) * (float.Parse(laatste[1]) / avg), 20f * ((float)Camera.main.orthographicSize / 100f) * (float.Parse(laatste[1]) / avg), 20f * ((float)Camera.main.orthographicSize / 100f) * (float.Parse(laatste[1]) / avg));
            fishes[i].transform.position = new Vector3(10 + (i * 5), 10 + (i * 5), 0);
        }
        Transform[] tempTransform = new Transform[fishes.Count];
        for (int i = 0; i < fishes.Count; i++)
        {
            tempTransform[i] = fishes[i].transform;
        }
        FindObjectOfType<CameraScript>().m_Targets = tempTransform;

        // add for each fish the moverandomly script
        foreach (GameObject fish in fishes)
        {
            fish.AddComponent<MoveRandomly>();
        }
    }
}