using System;
using UnityEngine;
using UnityEngine.UI;

public class Satisfaction : MonoBehaviour
{
    //These are the prefabs the pop ups are based on.
    public GameObject EventPopUpPrefab;
    public GameObject InvoicePopUpPrefab;

    public Text moneyText;
    public Canvas UICanvas;
    public Canvas eventCanvas;
    public Text EnergyText;
    [HideInInspector] public static float satisfaction = 500;
    [HideInInspector] public static int numberOfCustomers = 0;
    private int invoiceTimer, eventTimer;
    private System.Random random;
    private double totalSpentEnergy = 0;
    private PopUp[] popUps = new PopUp[3];
    private int popUpFrameCounter = 1;
    private int invoiceFrameCounter = 1;

    public void Start()
    {
        random = new System.Random();
        invoiceTimer = random.Next(1500, 2100);
        eventTimer = random.Next(1500, 2100);
        GameObject.Find("HapSlider").GetComponent<Slider>().value = satisfaction;
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
            //Every 5 seconds, the customer satisfaction goes down. (This is done to enforce the player to keep on answerring the events).
            //The consumed energy also gets dedacted from the energy supply.
            if (Time.frameCount % 300 == 0)
            {
                satisfaction -= 0.5f;
                GameObject.Find("HapSlider").GetComponent<Slider>().value -= 0.5f;
                EnergyText.text = string.Format("{0:n}",((int)(float.Parse(EnergyText.text) - 750)));
                double removedEnergy = numberOfCustomers * random.Next(2900,6000);
                totalSpentEnergy += removedEnergy;
                EnergyText.text = string.Format("{0:n}",((int)(float.Parse(EnergyText.text) - removedEnergy)));
            }
            //At a random time between 15 and 25 seconds an invoice will be shown for the player to receive money based on the consumed energy since last invoice.
            if (invoiceFrameCounter % invoiceTimer == 0)
            {
                showInvoice();
                invoiceTimer = random.Next(900, 1500);
                invoiceFrameCounter = 0;
            }
            //At a randome time between 15 and 25 seconds an event will be shown for the player to answer.
            if (popUpFrameCounter % eventTimer == 0)
            {
                showPopUp();
                eventTimer = random.Next(900, 1500);
                popUpFrameCounter = 0;
            }
            popUpFrameCounter++;
            invoiceFrameCounter++;
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
                        CameraControl.showingPopUp = true;
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
                        FindObjectOfType<QuizController>().Task();
                        eventCanvas.enabled = true;
                    }
                });
                //If an event gets denied, customer satisfaction plumits. This is to punish the player for not answerring the questions.
                popUps[i].Prefab.GetComponentsInChildren<Button>()[1].onClick.AddListener(() =>
                {
                    Destroy(popUps[i].Prefab);
                    popUps[i] = null;
                    satisfaction -= 30;
                    UICanvas.gameObject.transform.Find("HappinessPanel").GetComponentInChildren<Slider>().value -= 30;
                });
                break;
            }
        }
    }

    //An invoice will be shown. The total price is based on the set tarifs and the amount of energy consumed since the last sent invoice.
    public void showInvoice()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUps[i] == null)
            {
                double calculatedPrice = ((0.65 * totalSpentEnergy) * FindObjectOfType<TarifScript>().HighTarif) + ((0.35 * totalSpentEnergy) * FindObjectOfType<TarifScript>().LowTarif);
                popUps[i] = new PopUp(Time.frameCount, Instantiate(InvoicePopUpPrefab, UICanvas.transform));
                popUps[i].Prefab.transform.position = new Vector2(popUps[i].Prefab.transform.position.x, popUps[i].Prefab.transform.position.y - (i * 250));
                popUps[i].Prefab.GetComponentInChildren<Text>().text = String.Format("Customer consumption: {0:n}\nTotal payment: {1:n}", totalSpentEnergy, calculatedPrice); ;
                popUps[i].Prefab.GetComponentInChildren<Text>().fontSize = 12;
                popUps[i].Prefab.GetComponentsInChildren<Button>()[0].GetComponentInChildren<Text>().text = "Send now";
                popUps[i].Prefab.GetComponentsInChildren<Button>()[1].GetComponentInChildren<Text>().text = "Send later";
                popUps[i].Prefab.GetComponentsInChildren<Button>()[0].onClick.AddListener(() =>
                {
                    if (!CameraControl.showingPopUp)
                    {
                        moneyText.text = (int.Parse(moneyText.text) + int.Parse(Math.Round(calculatedPrice).ToString())).ToString();
                        totalSpentEnergy = 0;
                        Destroy(popUps[i].Prefab);
                        popUps[i] = null;
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