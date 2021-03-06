﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DataScript {

    private static string name="";
    private static float score=0;

    // script used to save the player name and score (static)
    public static string GetName()
    {
        return name;
    }

    public static void SetName(string value)
    {
        name = value;
    }

    public static float GetScore()
    {
        return score;
    }

    public static void SetScore(float value)
    {
        score = value;
    }

    public static void AddScore(float value)
    {
        score += value;
        if (XMLManager.instance.highscoreDB.list.Any(item => item.username == GetName()))
        {
            XMLManager.instance.ModifyHighscore(GetName(), GetScore().ToString());
        }
        else
        {
            XMLManager.instance.AddHighscore(GetName(), GetScore().ToString());
        }
    }
}
