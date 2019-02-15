using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contract = ProducerMarketGridFiller.Contract;

public class ProducerPopupController : MonoBehaviour
{
    //These are the prefabs the pop ups are based on.
    public GameObject EventPopUpPrefab;
    public GameObject ContractPopUpPrefab;

    public Canvas contractsCanvas;
    public Canvas UICanvas;
    public Canvas eventCanvas;

    [HideInInspector] public static System.Random random;

    private List<Contract> contractList = new List<Contract>();
    private Dictionary<int, Contract> ongoingContractList = new Dictionary<int, Contract>();

    private int eventTimer, contractTimer;
    private PopUp[] popUps = new PopUp[3];
    private int eventFrameCounter = 1;
    private int contractFrameCounter = 1;
    private int rndindex;

    public void Start()
    {
        FindObjectOfType<ProducerContractController>().FillContractsList();
        contractList = FindObjectOfType<ProducerContractController>().contractList;

        random = new System.Random();
        rndindex = random.Next(0, contractList.Count);
        contractTimer = random.Next(1500, 2100);
        eventTimer = random.Next(1500, 2100);
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
                        rndindex = random.Next(0, contractList.Count);
                    }
                }
            }
            //At a random time between 15 and 25 seconds an invoice will be shown for the player to receive money based on the consumed energy since last invoice.
            if (contractFrameCounter % contractTimer == 0 || Input.GetKeyDown(KeyCode.O))
            {
                showInvoice();
                contractTimer = random.Next(900, 1500);
                contractFrameCounter = 0;
            }
            //At a randome time between 15 and 25 seconds an event will be shown for the player to answer.
            if (eventFrameCounter % eventTimer == 0 || Input.GetKeyDown(KeyCode.P))
            {
                showPopUp();
                eventTimer = random.Next(900, 1500);
                eventFrameCounter = 0;
            }
            eventFrameCounter++;
            contractFrameCounter++;
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
                        FindObjectOfType<ProducerEventSystem>().Task();
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
    public void showInvoice()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUps[i] == null)
            {
                popUps[i] = new PopUp(Time.frameCount, Instantiate(ContractPopUpPrefab, UICanvas.transform));
                popUps[i].Prefab.transform.position = new Vector2(popUps[i].Prefab.transform.position.x, popUps[i].Prefab.transform.position.y - (i * 250));
                //Update the contract if it is already ongoing
                if (ongoingContractList.ContainsValue(contractList[rndindex]))
                {
                    //Styling of Title
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].text = ongoingContractList[rndindex].name;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontStyle = FontStyle.Bold;
                    //Styling of Text
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].text = String.Format("Amount Sold: {0:n}\nYour Profit: {1:n}", contractList[rndindex].amountSold, contractList[rndindex].profit);
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].GetComponentInChildren<Text>().text = "Update";
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "Decline";
                    //Accept Contract
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
                    {
                   
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        FindObjectOfType<ProducerContractController>().UpdateContract(rndindex);
                        FindObjectOfType<ProducerContractController>().RefreshContractsList();
                        rndindex = random.Next(0, contractList.Count);

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
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].text = contractList[rndindex].name;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[0].fontStyle = FontStyle.Bold;
                    //Styling of Text
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].text = String.Format("Amount Sold: {0:n}\nYour Profit: {1:n}", contractList[rndindex].amountSold, contractList[rndindex].profit);
                    popUps[i].Prefab.GetComponentsInChildren<Text>()[1].fontSize = 12;
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].GetComponentInChildren<Text>().text = "Accept";
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "Decline";
                    //Accept Contract
                    popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
                    {

                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        FindObjectOfType<ProducerContractController>().AcceptContract(rndindex);
                        FindObjectOfType<ProducerContractController>().RefreshContractsList();
                        rndindex = random.Next(0, contractList.Count);

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
