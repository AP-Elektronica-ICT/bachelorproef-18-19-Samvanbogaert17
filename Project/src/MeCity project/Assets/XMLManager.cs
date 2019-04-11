using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Linq;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    private string localFilePath; //saves file to a local folder in the game files
    private string sharedFilePath = @"I:\svboga\MeCity Highscores/highscores.xml"; //saves file to a global folder in the Ferranti Environment. 

    private void Awake()
    {
        localFilePath = Application.dataPath + "/highscores.xml";
        instance = this;
    }

    public HighscoreDatabase db = new HighscoreDatabase();

    //save
    public void SaveHighscores()
    {
        //open a new xml file
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreDatabase));
        FileStream stream = new FileStream(sharedFilePath, FileMode.Create, FileAccess.ReadWrite);
        serializer.Serialize(stream, db);
        stream.Close();
    }

    //load
    public void LoadHighscores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(HighscoreDatabase));
        if (File.Exists(sharedFilePath))
        {
            FileStream stream = new FileStream(sharedFilePath, FileMode.Open, FileAccess.ReadWrite);
            db = serializer.Deserialize(stream) as HighscoreDatabase;
            stream.Close();
        }
        else
        {
            SaveHighscores();
        }

    }

    // Reorder db highest to lowest score
    public void ReorderHighscores()
    {
        db.list = db.list.OrderByDescending(item => int.Parse(item.highscore)).ToList();
    }

    public void AddHighscore(string username, string highscore)
    {
        if (!username.IsOneOf("", ":"))
        {
            HighscoreEntry entry = new HighscoreEntry();
            entry.username = username;
            entry.highscore = highscore;
            db.list.Add(entry);
        }
    }

    public void ModifyHighscore(string username, string highscore)
    {
        if (!username.IsOneOf("", ":"))
        {
            int index = db.list.FindIndex(item => item.username == username);
            if (int.Parse(highscore) > int.Parse(db.list[index].highscore))
            {
                db.list[index].highscore = highscore;
            }
        }
    }
}

[Serializable]
public class HighscoreEntry
{
    public string username;
    public string highscore;
}

[Serializable]
public class HighscoreDatabase
{
    public List<HighscoreEntry> list = new List<HighscoreEntry>();
}
