using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DGOProblemController : MonoBehaviour
{
    public Text availableWorkersCountxt;
    public Text unsolvedProblemsCountTxt;
    public Text solvedProblemsCountTxt;

    private int availableWorkers;
    private int unsolvedProblems;
    private int solvedProblems;

    [HideInInspector] public List <Problem> problemList = new List <Problem>();
    [HideInInspector] public List <Problem> ongoingProblemList = new List<Problem>();

    //Lists of GameObjects needed for the ProblemCanvas
    private List<GameObject> problemPrefab = new List<GameObject>();
    private List<GameObject> durationPrefab = new List<GameObject>();
    private List<GameObject> deployedWorkerPrefab = new List<GameObject>();
    private List<GameObject> cancelPrefab = new List<GameObject>();

    private int frameCounter;
    // Use this for initialization
    void Start()
    {
        GetProblemPrefabs();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < ongoingProblemList.Count; i++)
        {
            if(ongoingProblemList[i].durationInSeconds <= 0)
            {
                RemoveProblem(i);
                unsolvedProblems--;
                solvedProblems++;

                solvedProblemsCountTxt.text = solvedProblems.ToString();
                unsolvedProblemsCountTxt.text = unsolvedProblems.ToString();

            }
            if (ongoingProblemList[i].deployedWorkers > 0)
            {
                if(Time.frameCount % (60 / ongoingProblemList[i].deployedWorkers) == 0)
                {
                    ongoingProblemList[i].durationInSeconds--;
                    var timeSpan = TimeSpan.FromSeconds(ongoingProblemList[i].durationInSeconds);
                    durationPrefab[i].GetComponent<Text>().text = timeSpan.Minutes + ":" + timeSpan.Seconds;
                }
            }
        }
    }

    public void AddProblem(int rndindex)
    {
        ongoingProblemList.Add(new Problem(problemList[rndindex].id, problemList[rndindex].title, problemList[rndindex].severity , problemList[rndindex].durationInSeconds));
        FindObjectOfType<DGOProblemGridFiller>().AddProblem(ongoingProblemList.Count - 1);
        unsolvedProblems++;
        unsolvedProblemsCountTxt.text = unsolvedProblems.ToString();
    }

    public void RemoveProblem(int index)
    {
        ongoingProblemList.RemoveAt(index);
        FindObjectOfType<DGOProblemGridFiller>().RemoveProblem(index);
        unsolvedProblems--;
        unsolvedProblemsCountTxt.text = unsolvedProblems.ToString();
    }

    public void AddWorker(int index)
    {
        ongoingProblemList[index].deployedWorkers++;
        deployedWorkerPrefab[index].GetComponentInChildren<Text>().text = ongoingProblemList[index].deployedWorkers.ToString();
    }
    public void RemoveWorker(int index)
    {
        if(ongoingProblemList[index].deployedWorkers > 0)
        {
            ongoingProblemList[index].deployedWorkers--;
            deployedWorkerPrefab[index].GetComponentInChildren<Text>().text = ongoingProblemList[index].deployedWorkers.ToString();
        }
    }

    public void FillProblemList()
    {
        problemList.Add(new Problem(0, "A customer needs his meterreadings taken.", 1, 10));
        problemList.Add(new Problem(1, "A customer needs his meter replaced", 1, 10));
        problemList.Add(new Problem(2, "A customer needs his meter removed", 1, 10));
        problemList.Add(new Problem(3, "A customer needs his new meter installed", 1, 10));
        problemList.Add(new Problem(4, "The grid needs maintaining", 2, 15));
        problemList.Add(new Problem(5, "A high voltage transformer broke down", 3, 100));
    }

    private void GetProblemPrefabs()
    {
        problemPrefab = FindObjectOfType<DGOProblemGridFiller>().problemPrefab;
        durationPrefab = FindObjectOfType<DGOProblemGridFiller>().durationPrefab;
        deployedWorkerPrefab = FindObjectOfType<DGOProblemGridFiller>().deployedWorkerPrefab;
        cancelPrefab = FindObjectOfType<DGOProblemGridFiller>().cancelPrefab;
    }

    private void SetProblemPrefabs()
    {
        FindObjectOfType<DGOProblemGridFiller>().problemPrefab = problemPrefab;
        FindObjectOfType<DGOProblemGridFiller>().durationPrefab = durationPrefab;
        FindObjectOfType<DGOProblemGridFiller>().deployedWorkerPrefab = deployedWorkerPrefab;
        FindObjectOfType<DGOProblemGridFiller>().cancelPrefab = cancelPrefab;
    }

    public class Problem
    {
        public int id { get; set; }
        public int severity { get; set; }
        public int durationInSeconds { get; set; }
        public int deployedWorkers { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public Problem(int id, string title, int severity, int durationInSeconds, int deployedWorkers = 0, string desc = "")
        {
            this.id = id;
            this.severity = severity;
            this.durationInSeconds = durationInSeconds;
            this.deployedWorkers = deployedWorkers;
            this.title = title;
            this.desc = desc;
        }
    }
}
