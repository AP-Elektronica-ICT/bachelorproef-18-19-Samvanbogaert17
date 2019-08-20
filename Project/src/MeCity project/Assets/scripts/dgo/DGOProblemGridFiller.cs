using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Problem = DGOProblemController.Problem;
using System;

public class DGOProblemGridFiller : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject DeployWorkerPrefab;
    public GameObject CancelPrefab;
    public GameObject ProblemTitleGrid;
    public GameObject ProblemGrid;

    private Transform problemTitleTransform;
    private Transform problemTransform;
    //Lists of GameObjects needed for the ProblemCanvas
    [HideInInspector] public Dictionary<int, GameObject> problemPrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> durationPrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> deployedWorkerPrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> cancelPrefab = new Dictionary<int, GameObject>();

    [HideInInspector] public List<Problem> problemList = new List<Problem>();
    [HideInInspector] public Dictionary<int, Problem> ongoingProblemList = new Dictionary<int, Problem>();

    // Use this for initialization
    void Start()
    {
        //get the grid transforms
        problemTitleTransform = ProblemTitleGrid.transform;
        problemTransform = ProblemGrid.transform;

        //get lists from DGOProblemController
        problemList = FindObjectOfType<DGOProblemController>().problemList;
        ongoingProblemList = FindObjectOfType<DGOProblemController>().ongoingProblemList;
    }

    public void AddProblem(int index)
    {
        //Add a problem to the problems grid
        Problem p = ongoingProblemList[index];

        problemPrefab.Add(index, Instantiate(TextPrefab, problemTitleTransform));
        durationPrefab.Add(index, Instantiate(TextPrefab, problemTransform));
        deployedWorkerPrefab.Add(index, Instantiate(DeployWorkerPrefab, problemTransform));
        cancelPrefab.Add(index, Instantiate(CancelPrefab, problemTransform));

        problemPrefab[index].GetComponent<Text>().text = p.title;
        problemPrefab[index].GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
        var timeSpan = TimeSpan.FromSeconds(p.durationInSeconds);

        //Add duration to duration prefab
        durationPrefab[index].GetComponent<Text>().text = timeSpan.Minutes + ":" + timeSpan.Seconds.AddLeadingZero();

        deployedWorkerPrefab[index].GetComponentInChildren<Text>().text = p.deployedWorkers.ToString();
        deployedWorkerPrefab[index].GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
        {
            FindObjectOfType<DGOProblemController>().AddWorker(index);
        });
        deployedWorkerPrefab[index].GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
        {
            FindObjectOfType<DGOProblemController>().RemoveWorker(index);
        });

        cancelPrefab[index].GetComponent<Button>().GetComponentInChildren<Text>().text = "Cancel";
        cancelPrefab[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            FindObjectOfType<DGOProblemController>().RemoveHappiness(index);
            FindObjectOfType<DGOProblemController>().RemoveProblem(index);
        });
    }

    public void RemoveProblem(int index)
    {
        //remove a problem from the grid and remove it from the list
        Destroy(problemPrefab[index]);
        Destroy(durationPrefab[index]);
        Destroy(deployedWorkerPrefab[index]);
        Destroy(cancelPrefab[index]);

        problemPrefab.Remove(index);
        durationPrefab.Remove(index);
        deployedWorkerPrefab.Remove(index);
        cancelPrefab.Remove(index);
    }
}
