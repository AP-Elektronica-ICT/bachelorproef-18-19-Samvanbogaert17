using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contract = ProducerContractController.Contract;

public class DGOEventSystem : MonoBehaviour
{
    //These are the prefabs the pop ups are based on.
    public GameObject EventPopUpPrefab;
    public GameObject ContractPopUpPrefab;
    public GameObject WeatherPopUp;

    public Canvas contractsCanvas;
    public Canvas UICanvas;
    public Canvas eventCanvas;

    public Text producingTxt;
    public Text totalProducingTxt;

    private int producing;
    private int totalProducing;

    [HideInInspector] public static System.Random random;
    [HideInInspector] public static float satisfaction = 500; // min: 0; max: 1000

    private List<WeatherEvent> weatherEventList = new List<WeatherEvent>();
    private List<Contract> contractList = new List<Contract>();
    private Dictionary<int, Contract> ongoingContractsList = new Dictionary<int, Contract>();

    private int eventTimer, contractTimer, weatherTimer;
    private PopUp[] popUps = new PopUp[3];
    private int eventFrameCounter = 1;
    private int contractFrameCounter = 1;
    private int weatherFrameCounter = 1;
    private int rndIndex;
    private int rndWeatherIndex;

    public void Start()
    {
        //Fill the weather events list with weather events
        FillWeatherEventList();
        //Fill the contract list in ProducerContract Controller. This happens here because otherwise you get NullReferenceExceptions
        FindObjectOfType<ProducerContractController>().FillContractsList();
        contractList = FindObjectOfType<ProducerContractController>().contractList;
        ongoingContractsList = FindObjectOfType<ProducerContractController>().ongoingContractsList;
        random = new System.Random();
        rndWeatherIndex = random.Next(0, weatherEventList.Count);
        rndIndex = random.Next(0, contractList.Count);
        eventTimer = random.Next(1500, 2100);
        contractTimer = random.Next(1500, 2100);
        weatherTimer = random.Next(1800, 3600);
        GameObject.Find("HapSlider").GetComponent<Slider>().value = satisfaction;

        producing = int.Parse(producingTxt.text);
        totalProducing = int.Parse(totalProducingTxt.text);
    }
    public void Update()
    {
        //Only update if game is running
        if (Time.timeScale == 1)
        {
            if(int.Parse(producingTxt.text) < 0)
            {
                satisfaction -= 0.25f;
                GameObject.Find("HapSlider").GetComponent<Slider>().value = satisfaction;
            }
            //Check for all existing popups if they have been visible for 10 seconds. If so, they are deleted.
            for (int i = 0; i < popUps.Length; i++)
            {
                if (popUps[i] != null)
                {
                    if (Time.frameCount - popUps[i].CreatedTimeInFrames >= 600)
                    {
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        rndIndex = random.Next(0, contractList.Count);
                    }
                }
            }
            //Every 5 seconds, the city happiness goes down. (This is done to enforce the player to keep on answerring the events).
            if (Time.frameCount % 300 == 0)
            {
                FindObjectOfType<ProducerCheckEndOfGame>().enabled = true;
                satisfaction -= 0.5f;
                GameObject.Find("HapSlider").GetComponent<Slider>().value -= 0.5f;


            }
            //At a random time between 15 and 25 seconds an invoice will be shown for the player to receive money based on the consumed energy since last invoice.
            if (contractFrameCounter % contractTimer == 0 || Input.GetKeyDown(KeyCode.O))
            {
                showContract();
                contractTimer = random.Next(900, 1500);
                contractFrameCounter = 0;
            }
            //At a random time between 15 and 25 seconds an event will be shown for the player to answer.
            if (eventFrameCounter % eventTimer == 0 || Input.GetKeyDown(KeyCode.P))
            {
                showPopUp();
                eventTimer = random.Next(900, 1500);
                eventFrameCounter = 0;
            }
            //At a random time between 30 and 60 seconds a weather event will be triggered to change behaviour in producing buildings.
            if (weatherFrameCounter % weatherTimer == 0 || Input.GetKeyDown(KeyCode.I))
            {
                showWeatherEvent(rndWeatherIndex);
                weatherTimer = random.Next(1800, 3600);
                weatherFrameCounter = 0;
            }
            eventFrameCounter++;
            contractFrameCounter++;
            weatherFrameCounter++;
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
                        FindObjectOfType<ProducerQuizController>().Task();
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

    //A contract will be shown. The player can choose to accept or decline a contract
    public void showContract()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUps[i] == null)
            {
                popUps[i] = new PopUp(Time.frameCount, Instantiate(ContractPopUpPrefab, UICanvas.transform));
                popUps[i].Prefab.transform.position = new Vector2(popUps[i].Prefab.transform.position.x, popUps[i].Prefab.transform.position.y - (i * 250));
                //Update the contract if it is already ongoing
                if (ongoingContractsList.ContainsKey(rndIndex))
                {
                    //Styling of Title
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].text = ongoingContractsList[rndIndex].name;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontStyle = FontStyle.Bold;
                    //Styling of Text
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].text = String.Format("Amount Sold: {0:n}\nYour Profit: {1:n}", contractList[rndIndex].amountSold, contractList[rndIndex].profit);
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].GetComponentInChildren<Text>().text = "Update";
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "Decline";
                    //Remove onClick Listeners
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
                    //Accept Contract
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
                    {
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        FindObjectOfType<ProducerContractController>().UpdateContract(rndIndex);

                        FindObjectOfType<ProducerContractController>().RefreshContractsList();
                        rndIndex = random.Next(0, contractList.Count);
                    });
                    //Decline Contract
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
                    {
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                    });
                }
                //Accept the new contract if the contract isn't already ongoing
                else
                {
                    //Styling of Title
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].text = contractList[rndIndex].name;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontStyle = FontStyle.Bold;
                    //Styling of Text
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].text = String.Format("Amount Sold: {0:n}\nYour Profit: {1:n}", contractList[rndIndex].amountSold, contractList[rndIndex].profit);
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].GetComponentInChildren<Text>().text = "Accept";
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "Decline";
                    //Remove onClick Listeners
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
                    //Accept Contract
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
                    {
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        FindObjectOfType<ProducerContractController>().AcceptContract(rndIndex);
                        FindObjectOfType<ProducerContractController>().RefreshContractsList();
                        rndIndex = random.Next(0, contractList.Count);

                    });
                    //Decline Contract
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
                    {
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                    });
                }

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
