using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Problem = DGOProblemController.Problem;
using System;

public class DGOProblemGridFiller : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject CancelPrefab;

    //Lists of GameObjects needed for the ProblemCanvas
    [HideInInspector] public List<GameObject> problemPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> timeRemainingPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> cancelPrefab = new List<GameObject>();

    [HideInInspector] public Dictionary<int, Problem> problemList = new Dictionary<int, Problem>();


    // Use this for initialization
    void Start()
    {
        InitializeProblemCanvas();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeProblemCanvas()
    {
        problemList = FindObjectOfType<DGOProblemController>().problemList;
        for (int i = 0; i < problemList.Count; i++)
        {
            Problem p = problemList[i];

            problemPrefab.Add(Instantiate(TextPrefab, transform));
            timeRemainingPrefab.Add(Instantiate(TextPrefab, transform));
            cancelPrefab.Add(Instantiate(TextPrefab, transform));

            problemPrefab[i].GetComponent<Text>().text = p.title;
            var timeSpan = TimeSpan.FromSeconds(p.timeRemainingInSeconds);
            timeRemainingPrefab[i].GetComponent<Text>().text = timeSpan.Minutes + ":" + timeSpan.Seconds;

            cancelPrefab[i].GetComponent<Button>().GetComponentInChildren<Text>().text = "cancel";
            cancelPrefab[i].GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<DGOProblemController>().RemoveProblem(p.id));
        }
    }
}
