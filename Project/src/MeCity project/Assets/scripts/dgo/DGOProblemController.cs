using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DGOProblemController : MonoBehaviour
{
    [HideInInspector] public Dictionary<int, Problem> problemList = new Dictionary<int, Problem>();
    [HideInInspector] public Dictionary<int, Problem> ongoingProblemList = new Dictionary<int, Problem>();

    //Lists of GameObjects needed for the ProblemCanvas
    private List<GameObject> problemTitlePrefab = new List<GameObject>();
    private List<GameObject> timeRemainingPrefab = new List<GameObject>();
    private List<GameObject> cancelPrefab = new List<GameObject>();

    private int frameCounter;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ongoingProblemList.Count; i++)
        {
            if (ongoingProblemList[i].workersDeployed > 0)
            {
                if(Time.frameCount % (60 / ongoingProblemList[i].workersDeployed) == 0)
                {
                    ongoingProblemList[i].timeRemainingInSeconds--;
                    var timeSpan = TimeSpan.FromSeconds(ongoingProblemList[i].timeRemainingInSeconds);
                    timeRemainingPrefab[i].GetComponent<Text>().text = timeSpan.Minutes + ":" + timeSpan.Seconds;
                }
            }
        }
    }

    public void AddProblem(int index)
    {
        ongoingProblemList.Add(problemList[index].id, new Problem(problemList[index].id, problemList[index].title, problemList[index].severity , problemList[index].timeRemainingInSeconds));
    }

    public void RemoveProblem(int index)
    {
        ongoingProblemList.Remove(index);
    }

    public void FillProblemList()
    {
        problemList.Add(0, new Problem(0, "A customer needs his meterreadings taken.", 1, 10));
        problemList.Add(1, new Problem(1, "A customer needs his meter replaced", 1, 10));
        problemList.Add(2, new Problem(2, "A customer needs his meter removed", 1, 10));
        problemList.Add(3, new Problem(3, "A customer needs his new meter installed", 1, 10));
        problemList.Add(4, new Problem(4, "The grid needs maintaining", 2, 15));
        problemList.Add(5, new Problem(5, "A high voltage transformer broke down", 3, 100));
    }

    public class Problem
    {
        public int id { get; set; }
        public int severity { get; set; }
        public int timeRemainingInSeconds { get; set; }
        public int workersDeployed { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public Problem(int id, string title, int severity, int timeRemainingInSeconds, int workersDeployed = 0, string desc = "")
        {

        }
    }
}
