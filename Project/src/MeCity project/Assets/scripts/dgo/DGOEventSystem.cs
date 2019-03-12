using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Problem = DGOProblemController.Problem;

public class DGOEventSystem : MonoBehaviour
{
    //Additional code formatting information
    //you will often see a number followed by '* 60'. for example 1 * 60 or 5 * 60.
    //the 1 and 5 in the above case represent an amount in seconds
    //60 in the above case represents 60 frames or 1 second.
    //this is to make the code more readable and easier to calculate with

    //These are the prefabs the pop ups are based on.
    public GameObject EventPopUpPrefab;
    public GameObject ProblemPopUpPrefab;

    public Canvas problemCanvas;
    public Canvas UICanvas;
    public Canvas eventCanvas;

    [HideInInspector] public static System.Random random;

    private List<Problem> problemList = new List<Problem>();

    private int eventTimer, problemTimer;
    private PopUp[] popUps = new PopUp[3];
    private int eventFrameCounter = 1;
    private int problemFrameCounter = 1;
    private int rndIndex;

    [HideInInspector] public int problemTimerMinVal = 10 * 60;
    [HideInInspector] public int problemTimerMaxVal = 20 * 60;
    [HideInInspector] public int index = 0;

    public void Start()
    {
        //Fill the problem list in DGO Problem Controller. This happens here because otherwise you get NullReferenceExceptions
        FindObjectOfType<DGOProblemController>().FillProblemList();
        //share memory with other problem lists. this will make code more readable.
        problemList = FindObjectOfType<DGOProblemController>().problemList;
        random = new System.Random();
        rndIndex = random.Next(0, problemList.Count);

        eventTimer = random.Next(15 * 60, 25 * 60);
        problemTimer = random.Next(15 * 60, 25 * 60);
    }
    public void Update()
    {
        //Only update if game is running
        if (Time.timeScale == 1)
        {
            //Check for all existing popups if they have been visible for 10 seconds. If so, they are deleted.
            for (int i = 0; i < popUps.Length; i++)
            {
                if (popUps[i] != null)
                {
                    if (Time.frameCount - popUps[i].CreatedTimeInFrames >= 10 * 60)
                    {
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                    }
                }
            }

            //At a random time between 5 and 15 seconds a problem will occur for the player to solve.
            if (!CameraControl.inQuiz && !CameraControl.paused)
            {
                if (problemFrameCounter % problemTimer == 0 || Input.GetKeyDown(KeyCode.O))
                {
                    showProblem();
                    FindObjectOfType<DGOProblemController>().AddProblem(rndIndex, index);
                    rndIndex = random.Next(0, problemList.Count);
                    index++;
                    problemTimer = random.Next(problemTimerMinVal, problemTimerMaxVal);
                    problemFrameCounter = 0;
                }
            }
            //At a random time between 15 and 25 seconds an event will be shown for the player to answer.
            if (eventFrameCounter % eventTimer == 0 || Input.GetKeyDown(KeyCode.P))
            {
                showPopUp();
                eventTimer = random.Next(15 * 60, 25 * 60);
                eventFrameCounter = 0;
            }
            eventFrameCounter++;
            problemFrameCounter++;
        }
    }

    //A pop up will be generated based on the XML file that contains these questions. They will be read, and the correct answers will be filled in.
    //Every button gets an answer and an influence. There will only be as many buttons as there are answers in the XML. All others are disabled.
    private void showPopUp()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUps[i] == null)
            {
                popUps[i] = new PopUp(Time.frameCount, Instantiate(EventPopUpPrefab, UICanvas.transform));
                popUps[i].Prefab.transform.position = new Vector2(popUps[i].Prefab.transform.position.x, popUps[i].Prefab.transform.position.y - (i * 250));

                popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
                {
                    //If another screen is already showing, we cannot show the event. The player will have to choose what he spends his time on.
                    if (!CameraControl.showingPopUp)
                    {
                        CameraControl.inQuiz = true;
                        CameraControl.showingPopUp = true;
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        FindObjectOfType<QuizController>().Task();
                        eventCanvas.enabled = true;
                    }
                });
                popUps[i].Prefab.GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
                {
                    Destroy(popUps[i].Prefab);
                    popUps[i] = null;
                });
                break;
            }
        }
    }

    //A problem will be shown.
    public void showProblem()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUps[i] == null)
            {
                popUps[i] = new PopUp(Time.frameCount, Instantiate(ProblemPopUpPrefab, UICanvas.transform));
                popUps[i].Prefab.transform.position = new Vector2(popUps[i].Prefab.transform.position.x, popUps[i].Prefab.transform.position.y - (i * 250));

                //Styling of Title/Text
                popUps[i].Prefab.GetComponentsInChildren<Text>()[1].text = problemList[rndIndex].title;
                popUps[i].Prefab.GetComponentsInChildren<Text>()[1].fontSize = 12;
                popUps[i].Prefab.GetComponentsInChildren<Text>()[1].fontStyle = FontStyle.Bold;

                break;
            }
        }
    }

    //An inner popup class that stores information about currently visible popups.
    private class PopUp
    {
        public int CreatedTimeInFrames { get; set; }
        public GameObject Prefab { get; set; }

        public PopUp(int CreatedTimeInFrames, GameObject Prefab)
        {
            this.CreatedTimeInFrames = CreatedTimeInFrames;
            this.Prefab = Prefab;
        }
    }
}
