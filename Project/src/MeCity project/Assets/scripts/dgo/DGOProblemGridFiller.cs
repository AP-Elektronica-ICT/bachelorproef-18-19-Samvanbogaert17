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

    //Lists of GameObjects needed for the ProblemCanvas
    [HideInInspector] public List<GameObject> problemPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> durationPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> deployedWorkerPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> cancelPrefab = new List<GameObject>();

    [HideInInspector] public List<Problem> problemList = new List<Problem>();
    [HideInInspector] public List<Problem> ongoingProblemList = new List<Problem>();


    // Use this for initialization
    void Start()
    {
        //InitializeProblemCanvas();
        problemList = FindObjectOfType<DGOProblemController>().problemList;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddProblem(int index)
    {
        Problem p = ongoingProblemList[index];

        problemPrefab.Add(Instantiate(TextPrefab, transform));
        durationPrefab.Add(Instantiate(TextPrefab, transform));
        deployedWorkerPrefab.Add(Instantiate(DeployWorkerPrefab, transform));
        cancelPrefab.Add(Instantiate(CancelPrefab, transform));

        problemPrefab[index].GetComponent<Text>().text = p.title;
        var timeSpan = TimeSpan.FromSeconds(p.durationInSeconds);
        durationPrefab[index].GetComponent<Text>().text = timeSpan.Minutes + ":" + timeSpan.Seconds;

        deployedWorkerPrefab[index].GetComponentInChildren<Text>().text = p.deployedWorkers.ToString();
        deployedWorkerPrefab[index].GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
        {
            FindObjectOfType<DGOProblemController>().AddWorker(p.id);
        });
        deployedWorkerPrefab[index].GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
        {
            FindObjectOfType<DGOProblemController>().RemoveProblem(p.id);
        });

        cancelPrefab[index].GetComponent<Button>().GetComponentInChildren<Text>().text = "Cancel";
        cancelPrefab[index].GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<DGOProblemController>().RemoveProblem(index));
    }

    public void RemoveProblem(int index)
    {
        Destroy(problemPrefab[index]);
        Destroy(durationPrefab[index]);
        Destroy(deployedWorkerPrefab[index]);
        Destroy(cancelPrefab[index]);

        problemPrefab.RemoveAt(index);
        durationPrefab.RemoveAt(index);
        deployedWorkerPrefab.RemoveAt(index);
        cancelPrefab.RemoveAt(index);
    }

   /* public void InitializeProblemCanvas()
    {
        problemList = FindObjectOfType<DGOProblemController>().problemList;
        for (int i = 0; i < problemList.Count; i++)
        {
            Problem p = problemList[i];

            problemPrefab.Add(i, Instantiate(TextPrefab, transform));
            durationPrefab.Add(i, Instantiate(TextPrefab, transform));
            cancelPrefab.Add(i, Instantiate(TextPrefab, transform));

            problemPrefab[i].GetComponent<Text>().text = p.title;
            var timeSpan = TimeSpan.FromSeconds(p.durationInSeconds);
            durationPrefab[i].GetComponent<Text>().text = timeSpan.Minutes + ":" + timeSpan.Seconds;

            deployedWorkerPrefab[i].GetComponentInChildren<Text>().text = p.deployedWorkers.ToString();
            deployedWorkerPrefab[i].GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
            {
                FindObjectOfType<DGOProblemController>().AddWorker(p.id);
            });
            deployedWorkerPrefab[i].GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
            {
                FindObjectOfType<DGOProblemController>().RemoveProblem(p.id);
            });

            cancelPrefab[i].GetComponent<Button>().GetComponentInChildren<Text>().text = "Cancel";
            cancelPrefab[i].GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<DGOProblemController>().RemoveProblem(p.id));
        }
    }*/
}
