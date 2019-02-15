using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Building = ProducerMarketGridFiller.Building;

public class ProducerMarketController : MonoBehaviour
{
    [HideInInspector] public static float pollution = 0;  // min: 0; max: 1000

    public Texture greenArrow;
    public Texture redArrow;

    public Text titleText;
    public Text price_behaviour;
    public Text buy_sell;
    public Text moneyTxt;
    public Text producingTxt;
    public Text totalProducingTxt;

    public Button marketButton;
    public Button installedButton;
    public Button defaultBtn;

    [HideInInspector] public List<Building> buildingList = new List<Building>();
    [HideInInspector] public List<Building> installedBuildingList = new List<Building>();

    private int money;
    private int producing;
    private int totalProducing;

    private ColorBlock normalCb;
    private ColorBlock disabledCb;

    //Lists of GameObjects needed for the MarketCanvas
    private List<GameObject> buildingTypePrefab = new List<GameObject>();
    private List<GameObject> energyTypePrefab = new List<GameObject>();
    private List<GameObject> productionPrefab = new List<GameObject>();
    private List<GameObject> pollutionPrefab = new List<GameObject>();
    private List<GameObject> price_behaviourPrefab = new List<GameObject>();
    private List<GameObject> buy_sellPrefab = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Producer Start");

        normalCb = disabledCb = defaultBtn.colors;
        disabledCb.normalColor = normalCb.disabledColor;

        FillBuildingList();
        GetMarketPrefabs();

        GameObject.Find("PollutionSlider").GetComponent<Slider>().value = pollution;

        //Parsing all texts to ints
        money = int.Parse(moneyTxt.text);
        producing = int.Parse(producingTxt.text);

        marketButton.onClick.AddListener(() =>
        {
            SwitchCanvas(0);    // 0 indicates the panel will change to the market panel
        });
        installedButton.onClick.AddListener(() =>
        {
            SwitchCanvas(1);    // 1 indicates the panel will change to the installed panel
        });
        /*
        //Give player his 1st buildings
        BuyBuilding(0); //Adds a solarpanel farm
        BuyBuilding(5); //Adds a Fossil fuel power station
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    //method used to switch from market panel to installed panel or vice versa
    private void SwitchCanvas(int mode)
    {
        switch (mode)
        {
            case 0:
                titleText.text = "Market";
                price_behaviour.text = "Price";
                buy_sell.text = "Buy";

                marketButton.colors = normalCb;
                installedButton.colors = disabledCb;

                for (int i = 0; i < buildingList.Count; i++)
                {
                    Building b = buildingList[i];
                    //all changes in the production prefab
                    productionPrefab[i].GetComponent<Text>().text = b.production.ToString() + " kWh";
                    //all changes in the pollution prefab
                    pollutionPrefab[i].GetComponent<Text>().text = b.pollution.ToString() + " %";
                    //all changes in the price_behaviour prefab
                    price_behaviourPrefab[i].GetComponentInChildren<Text>().text = "$ " + b.price.ToString();
                    price_behaviourPrefab[i].GetComponentInChildren<RawImage>().enabled = false;
                    //all changes in the buy_sell prefab
                    buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "buy";
                    //Adds a new onclick listener to buy a building
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.AddListener(() => BuyBuilding(b.id));
                }
                Debug.Log("Set Remote Lists");
                SetMarketPrefabs();
                break;
            case 1:
                titleText.text = "Installed";
                price_behaviour.text = "Behaviour";
                buy_sell.text = "Sell";

                marketButton.colors = disabledCb;
                installedButton.colors = normalCb;

                for (int i = 0; i < installedBuildingList.Count; i++)
                {
                    Building b = installedBuildingList[i];
                    //all changes in the production prefab
                    productionPrefab[i].GetComponent<Text>().text = b.production.ToString() + " kWh";
                    //all changes in the pollution prefab
                    pollutionPrefab[i].GetComponent<Text>().text = b.pollution.ToString() + " %";
                    //all changes in the price_behaviour prefab
                    price_behaviourPrefab[i].GetComponentInChildren<Text>().text = b.behaviour.ToString() + " %";
                    if (b.behaviour > 0)
                    {
                        price_behaviourPrefab[i].GetComponentInChildren<RawImage>().texture = greenArrow;
                    }
                    else if (b.behaviour < 0)
                    {
                        price_behaviourPrefab[i].GetComponentInChildren<RawImage>().texture = redArrow;
                    }
                    else
                    {
                        price_behaviourPrefab[i].GetComponentInChildren<RawImage>().enabled = false;
                    }
                    //all changes in the buy_sell prefab
                    buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "sell";
                    //Adds a new onlick listener to sell a building
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.AddListener(() => SellBuilding(b.id));
                }
                Debug.Log("Set Remote Lists");
                SetMarketPrefabs();
                break;
        }
    }

    public void BuyBuilding(int index)
    {
        money -= buildingList[index].price;
        moneyTxt.text = money.ToString();

        totalProducing += buildingList[index].production;
        totalProducingTxt.text = totalProducing.ToString();

        pollution += buildingList[index].pollution;
        GameObject.Find("PollutionSlider").GetComponent<Slider>().value += buildingList[index].pollution;

        installedBuildingList[index].amount++;
        installedBuildingList[index].pollution += buildingList[index].pollution;
        installedBuildingList[index].production += buildingList[index].production;

        buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();
        SetMarketPrefabs();
    }

    public void SellBuilding(int index)
    {
        if (installedBuildingList[index].amount > 0)
        {
            money += buildingList[index].price / 2;
            moneyTxt.text = money.ToString();

            totalProducing -= buildingList[index].production;
            totalProducingTxt.text = totalProducing.ToString();

            pollution -= buildingList[index].pollution;
            GameObject.Find("PollutionSlider").GetComponent<Slider>().value -= buildingList[index].pollution;

            installedBuildingList[index].amount--;
            installedBuildingList[index].pollution -= buildingList[index].pollution;
            installedBuildingList[index].production -= buildingList[index].production;

            productionPrefab[index].GetComponent<Text>().text = installedBuildingList[index].production.ToString() + " kWh";
            pollutionPrefab[index].GetComponent<Text>().text = installedBuildingList[index].pollution.ToString() + " %";
            buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();
            SetMarketPrefabs();
        }
    }

    public void ChangeBehaviour(int index, int answerIsCorrect)
    {
        if (answerIsCorrect == 1)
        {
            installedBuildingList[index].behaviour = UnityEngine.Random.Range(1, 11);
        }
        else if (answerIsCorrect == 0)
        {
            installedBuildingList[index].behaviour = UnityEngine.Random.Range(-1, -11);
        }
        installedBuildingList[index].production += (installedBuildingList[index].production * (installedBuildingList[index].behaviour) * 10);

        SetMarketPrefabs();
    }

    private void FillBuildingList()
    {
        //Add all Green type buildings
        buildingList.Add(new Building(0, "Solarpanel farm", "Green", 30000, 0, 10000));
        buildingList.Add(new Building(1, "Hydroelectric plant", "Green", 275000, 0, 70000));
        buildingList.Add(new Building(2, "Wind turbine farm", "Green", 80000, 0, 25000));
        buildingList.Add(new Building(3, "Geothermal station", "Green", 150000, 0, 40000));

        //Add all Gray type buildings
        buildingList.Add(new Building(4, "Nuclear power plant", "Gray", 1000000, 20, 250000));
        buildingList.Add(new Building(5, "Fossil fuel power station", "Gray", 100000, 10, 50000));

        //DO the same for installed buildinglist

        //Add all Green type buildings
        installedBuildingList.Add(new Building(0, "Solarpanel farm", "Green", 0, 0, 10000));
        installedBuildingList.Add(new Building(1, "Hydroelectric plant", "Green", 0, 0, 70000));
        installedBuildingList.Add(new Building(2, "Wind turbine farm", "Green", 0, 0, 25000));
        installedBuildingList.Add(new Building(3, "Geothermal station", "Green", 0, 0, 40000));

        //Add all Gray type buildings
        installedBuildingList.Add(new Building(4, "Nuclear power plant", "Gray", 0, 0, 250000));
        installedBuildingList.Add(new Building(5, "Fossil fuel power station", "Gray", 0, 0, 5000));
    }
    private void GetMarketPrefabs()
    {
        buildingTypePrefab = FindObjectOfType<ProducerMarketGridFiller>().buildingTypePrefab;
        energyTypePrefab = FindObjectOfType<ProducerMarketGridFiller>().energyTypePrefab;
        productionPrefab = FindObjectOfType<ProducerMarketGridFiller>().productionPrefab;
        pollutionPrefab = FindObjectOfType<ProducerMarketGridFiller>().pollutionPrefab;
        price_behaviourPrefab = FindObjectOfType<ProducerMarketGridFiller>().price_behaviourPrefab;
        buy_sellPrefab = FindObjectOfType<ProducerMarketGridFiller>().buy_sellPrefab;
    }
    private void SetMarketPrefabs()
    {
        FindObjectOfType<ProducerMarketGridFiller>().buildingTypePrefab = buildingTypePrefab;
        FindObjectOfType<ProducerMarketGridFiller>().energyTypePrefab = energyTypePrefab;
        FindObjectOfType<ProducerMarketGridFiller>().productionPrefab = productionPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().pollutionPrefab = pollutionPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().price_behaviourPrefab = price_behaviourPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().buy_sellPrefab = buy_sellPrefab;
    }
}
