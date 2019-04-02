using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOMastermindGridFiller : MonoBehaviour
{
    public GameObject prevAnswerPrefab;
    public List<GameObject> prevAnswers = new List<GameObject>();

    public void AddPrevAnswer(Image[] answerArray)
    {
        GameObject prevAnswer = Instantiate(prevAnswerPrefab, transform);
        for(int i = 0; i < answerArray.Length; i++)
        {
            //prevAnswer.Get
        }
        prevAnswers.Add(Instantiate(prevAnswerPrefab, transform));
    }
    
    public void ClearPrevAnswers()
    {
        for(int i = 0; i < prevAnswers.Count; i++)
        {
            Destroy(prevAnswers[i]);
        }

        prevAnswers.Clear();
    }
}
