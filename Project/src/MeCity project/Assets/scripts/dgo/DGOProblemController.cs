using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class DGOProblemController : MonoBehaviour
{
    public Text availableWorkersCountxt;
    public Text unsolvedProblemsCountTxt;
    public Text solvedProblemsCountTxt;
    public Text moneyTxt;
    public Text happinessTxt;

    private int availableWorkers;
    private int unsolvedProblems;
    private int solvedProblems;
    private int money;
    private Slider hapSlider;
    [HideInInspector] public static float satisfaction;

    [HideInInspector] public List<Problem> problemList = new List<Problem>();
    [HideInInspector] public Dictionary<int, Problem> ongoingProblemList = new Dictionary<int, Problem>();

    //Lists of GameObjects needed for the ProblemCanvas
    private Dictionary<int, GameObject> problemPrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> durationPrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> deployedWorkerPrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> cancelPrefab = new Dictionary<int, GameObject>();

    private int frameCounter;
    // Use this for initialization
    void Start()
    {
        hapSlider = GameObject.Find("HapSlider").GetComponent<Slider>();
        satisfaction = hapSlider.maxValue;
        hapSlider.value = satisfaction;
        GetProblemPrefabs();
    }

    // Update is called once per frame
    void Update()
    {
        //update happiness (in percentage) every frame
        happinessTxt.text = Math.Round((hapSlider.value / hapSlider.maxValue * 100)).ToString() + '%';

        //checks if the game isn't paused or the player is not in a quiz
        if (!CameraControl.paused && !CameraControl.inQuiz)
        {
            //checks if there is atleast 1 ongoing problem
            if (ongoingProblemList.Count > 0)
            {
                for (int i = 0; i <= FindObjectOfType<DGOEventSystem>().index + 1; i++)
                {
                    //checks if the ongoingproblems list contains a value
                    if (ongoingProblemList.ContainsKey(i))
                    {
                        //checks if the problem is finished
                        if (ongoingProblemList[i].durationInSeconds <= 0)
                        {
                            //add money based on the severity of the problem
                            money = int.Parse(moneyTxt.text);
                            money += 1000 * ongoingProblemList[i].severity;
                            moneyTxt.text = money.ToString();

                            //Add happiness based on the severity of the problem
                            satisfaction += 1500 * ongoingProblemList[i].severity;
                            hapSlider.value = satisfaction;

                            //remove a problem
                            //increase and update the solved problems counter
                            RemoveProblem(i);
                            solvedProblems++;

                            solvedProblemsCountTxt.text = solvedProblems.ToString();
                            unsolvedProblemsCountTxt.text = unsolvedProblems.ToString();
                        }
                        //if the problem has more than 1 worker deployed
                        else if (ongoingProblemList[i].deployedWorkers > 0)
                        {
                            //decreases duration of the problem faster if there are more workers deployed
                            if (Time.frameCount % (60 / ongoingProblemList[i].deployedWorkers) == 0)
                            {
                                ongoingProblemList[i].durationInSeconds--;
                                var timeSpan = TimeSpan.FromSeconds(ongoingProblemList[i].durationInSeconds);
                                durationPrefab[i].GetComponent<Text>().text = timeSpan.Minutes + ":" + timeSpan.Seconds.AddLeadingZero();
                            }
                        }
                        //if the problem has no workers deployed to it, then happiness will decrease
                        else if (ongoingProblemList[i].deployedWorkers == 0)
                        {
                            satisfaction -= (float)ongoingProblemList[i].happinessDecrease;
                            hapSlider.value = satisfaction;
                        }
                    }
                }
            }
        }
    }

    public void AddProblem(int rndindex, int index)
    {
        //Add the problem to the ongoing problems list
        ongoingProblemList.Add(index, new Problem(problemList[rndindex].id, problemList[rndindex].title, problemList[rndindex].severity, problemList[rndindex].durationInSeconds));

        //Add the problem to the problems grid
        FindObjectOfType<DGOProblemGridFiller>().AddProblem(index);

        //increase unsolved problems counter
        unsolvedProblems++;
        unsolvedProblemsCountTxt.text = unsolvedProblems.ToString();
    }

    public void RemoveProblem(int index)
    {
        //removes problem in the grid
        FindObjectOfType<DGOProblemGridFiller>().RemoveProblem(index);

        //return all deployed workers to available workers
        availableWorkers = int.Parse(availableWorkersCountxt.text);
        availableWorkers += ongoingProblemList[index].deployedWorkers;
        availableWorkersCountxt.text = availableWorkers.ToString();
        
        //remove the problem from the ongoing problems list
        ongoingProblemList.Remove(index);
        
        //decrease unsolved problems counter
        unsolvedProblems--;
        unsolvedProblemsCountTxt.text = unsolvedProblems.ToString();
    }

    public void RemoveHappiness(int index)
    {
        //removes happiness based on the severity of the problem that has been removed
        satisfaction -= ongoingProblemList[index].severity * 5000;
        hapSlider.value = satisfaction;
    }

    public void AddWorker(int index)
    {
        //checks how many workers the player has available
        availableWorkers = int.Parse(availableWorkersCountxt.text);
        //checks if the player has atleast 1 worker available
        //then add worker to the current problem
        //and remove worker from available workers
        if (availableWorkers > 0)
        {
            ongoingProblemList[index].deployedWorkers++;
            deployedWorkerPrefab[index].GetComponentInChildren<Text>().text = ongoingProblemList[index].deployedWorkers.ToString();

            availableWorkers--;
            availableWorkersCountxt.text = availableWorkers.ToString();
        }
    }
    public void RemoveWorker(int index)
    {
        //checks how many workers the player has available
        availableWorkers = int.Parse(availableWorkersCountxt.text);
        //checks if a problem has atleast 1 worker deployed to it
        //then removes the worker from the problem
        //and add the worker to the available workers
        if (ongoingProblemList[index].deployedWorkers > 0)
        {
            ongoingProblemList[index].deployedWorkers--;
            deployedWorkerPrefab[index].GetComponentInChildren<Text>().text = ongoingProblemList[index].deployedWorkers.ToString();

            availableWorkers++;
            availableWorkersCountxt.text = availableWorkers.ToString();
        }
    }

    public void FillProblemList()
    {
        //fill problems list with different problems that can occur
        problemList.Add(new Problem(0, "A customer needs his meterreadings taken.", 1));
        problemList.Add(new Problem(1, "A customer needs his meter replaced", 1));
        problemList.Add(new Problem(2, "A customer needs his meter removed", 1));
        problemList.Add(new Problem(3, "A customer needs his new meter installed", 1));
        problemList.Add(new Problem(4, "The grid connections needs maintaining", 2));
        problemList.Add(new Problem(5, "A high voltage transformer broke down", 3));
        problemList.Add(new Problem(6, "A customer reported a misreading and needs his readings retaken", 1));
        problemList.Add(new Problem(7, "Your employees are on strike! extra workers need to be deployed!", 2));
        problemList.Add(new Problem(8, "The GPRS-network is disrupted! online meterreadings are not possible!", 2));
        problemList.Add(new Problem(9, "A customer has a malfunctioning electricity connection", 1));
        problemList.Add(new Problem(10, "A neighbourhood substation needs maintaining", 2));
        problemList.Add(new Problem(11, "A neighbourhood substation has broken down and needs fixing", 2));
        problemList.Add(new Problem(12, "A city-wide blackout has occured!", 4));
    }

    private void GetProblemPrefabs()
    {
        //get all prefab lists from DGOProblemGridFiller to make code more readable
        problemPrefab = FindObjectOfType<DGOProblemGridFiller>().problemPrefab;
        durationPrefab = FindObjectOfType<DGOProblemGridFiller>().durationPrefab;
        deployedWorkerPrefab = FindObjectOfType<DGOProblemGridFiller>().deployedWorkerPrefab;
        cancelPrefab = FindObjectOfType<DGOProblemGridFiller>().cancelPrefab;
    }

    private void SetProblemPrefabs()
    {
        //set all prefab lists from DGOProblemGridFiller to make code more readable
        FindObjectOfType<DGOProblemGridFiller>().problemPrefab = problemPrefab;
        FindObjectOfType<DGOProblemGridFiller>().durationPrefab = durationPrefab;
        FindObjectOfType<DGOProblemGridFiller>().deployedWorkerPrefab = deployedWorkerPrefab;
        FindObjectOfType<DGOProblemGridFiller>().cancelPrefab = cancelPrefab;
    }

    public class Problem
    {
        public int id { get; set; }
        public int severity { get; set; }
        public double happinessDecrease;
        public int durationInSeconds { get; set; }
        public int deployedWorkers { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public Problem(int _id, string _title, int _severity, int _durationInSeconds = 60, int _deployedWorkers = 0, string _desc = "")
        {
            System.Random rnd = new System.Random();

            id = _id;
            severity = _severity;
            happinessDecrease = _severity * 10;
            //if the duration in seconds is one minute (or default value)
            if(_durationInSeconds == 60)
            {
                //give the problem a random duration
                durationInSeconds = rnd.Next(10, _durationInSeconds * _severity);
            }
            else
            {
                //give the problem the player defined duration
                durationInSeconds = _durationInSeconds;
            }
            deployedWorkers = _deployedWorkers;
            title = _title;
            desc = _desc;
        }
    }
}
