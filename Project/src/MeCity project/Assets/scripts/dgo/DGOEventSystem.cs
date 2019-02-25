using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Problem = DGOProblemController.Problem;

public class DGOEventSystem : MonoBehaviour
{
    //These are the prefabs the pop ups are based on.
    public GameObject EventPopUpPrefab;
    public GameObject ProblemPopUpPrefab;

    public Canvas problemCanvas;
    public Canvas UICanvas;
    public Canvas eventCanvas;

    [HideInInspector] public static System.Random random;

    private List<Problem> problemList = new List<Problem>();
    private Dictionary<int, Problem> ongoingProblemList = new Dictionary<int, Problem>();
    //private Dictionary<int, Problem> ongoingContractsList = new Dictionary<int, Contract>();

    private int eventTimer, problemTimer;
    private PopUp[] popUps = new PopUp[3];
    private int eventFrameCounter = 1;
    private int problemFrameCounter = 1;
    private int rndIndex;
    [HideInInspector] public int index = 0;

    public void Start()
    {
        //Fill the contract list in ProducerContract Controller. This happens here because otherwise you get NullReferenceExceptions
        FindObjectOfType<DGOProblemController>().FillProblemList();
        problemList = FindObjectOfType<DGOProblemController>().problemList;
        ongoingProblemList = FindObjectOfType<DGOProblemController>().ongoingProblemList;
        random = new System.Random();
        rndIndex = random.Next(0, problemList.Count);
        eventTimer = random.Next(1500, 2100);
        problemTimer = random.Next(1500, 2100);
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
                    if (Time.frameCount - popUps[i].CreatedTimeInFrames >= 600)
                    {
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                    }
                }
            }
            //Every 5 seconds, the city happiness goes down. (This is done to enforce the player to keep on answerring the events).
            if (Time.frameCount % 300 == 0)
            {
                FindObjectOfType<DGOCheckEndOfGame>().enabled = true;
            }
            //At a random time between 15 and 25 seconds an invoice will be shown for the player to receive money based on the consumed energy since last invoice.
            if (problemFrameCounter % problemTimer == 0 || Input.GetKeyDown(KeyCode.O))
            {
                showProblem();
                FindObjectOfType<DGOProblemController>().AddProblem(rndIndex, index);
                rndIndex = random.Next(0, problemList.Count);
                index++;
                problemTimer = random.Next(900, 1500);
                problemFrameCounter = 0;
            }
            //At a random time between 15 and 25 seconds an event will be shown for the player to answer.
            if (eventFrameCounter % eventTimer == 0 || Input.GetKeyDown(KeyCode.P))
            {
                showPopUp();
                eventTimer = random.Next(900, 1500);
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
                    //If another screen is already showing, we cannot show the event. The player will have to chose what he spends his time on.
                    if (!CameraControl.showingPopUp)
                    {
                        CameraControl.showingPopUp = true;
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        FindObjectOfType<DGOQuizController>().Task();
                        eventCanvas.enabled = true;
                    }
                });
                //If an event gets denied, customer satisfaction plumits. This is to punish the player for not answerring the questions.
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
