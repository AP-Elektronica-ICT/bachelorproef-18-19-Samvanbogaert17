using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerMarketController : MonoBehaviour
{
    [HideInInspector] public static float pollution = 0;  // min: 0; max: 100

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

    [HideInInspector] public int mode = 0;

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
            mode = 0; // 0 indicates the panel will change to the market panel
            SwitchCanvas(mode);
        });
        installedButton.onClick.AddListener(() =>
        {
            mode = 1; // 1 indicates the panel will change to the installed panel
            SwitchCanvas(mode);
        });
    }

    //method used to switch from market panel to installed panel or vice versa
    private void SwitchCanvas(int mode)
    {
        producing = int.Parse(producingTxt.text);
        totalProducing = int.Parse(totalProducingTxt.text);
        switch (mode)
        {
            case 0:
                titleText.text = "Market";
                price_behaviour.text = "Price";
                buy_sell.text = "Buy";

                marketButton.colors = normalCb;
                installedButton.colors = disabledCb;

                UpdateBuilding(mode);
                
                SetMarketPrefabs();
                break;
            case 1:
                titleText.text = "Installed";
                price_behaviour.text = "Behaviour";
                buy_sell.text = "Sell";

                marketButton.colors = disabledCb;
                installedButton.colors = normalCb;

                UpdateBuilding(mode);

                Debug.Log("Set Remote Lists");
                SetMarketPrefabs();
                break;
        }
    }

    //add building to the installed building tab
    public void BuyBuilding(int index)
    {
        //Parsing all texts to ints
        money = int.Parse(moneyTxt.text);
        producing = int.Parse(producingTxt.text);

        money -= buildingList[index].price;
        moneyTxt.text = money.ToString();

        producing += buildingList[index].production;
        producingTxt.text = producing.ToString();

        totalProducing += buildingList[index].production;
        totalProducingTxt.text = totalProducing.ToString();

        pollution += buildingList[index].pollution;
        GameObject.Find("PollutionSlider").GetComponent<Slider>().value += buildingList[index].pollution;
        GameObject.Find("HapSlider").GetComponent<Slider>().value -=

        installedBuildingList[index].amount++;
        installedBuildingList[index].pollution += buildingList[index].pollution;
        installedBuildingList[index].production += buildingList[index].production;

        buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();
        SetMarketPrefabs();
    }

    //remove building from the installed buildings tab
    public void SellBuilding(int index)
    {
        if (installedBuildingList[index].amount > 0)
        {
            //Parsing all texts to ints
            money = int.Parse(moneyTxt.text);
            producing = int.Parse(producingTxt.text);

            money += buildingList[index].price / 2;
            moneyTxt.text = money.ToString();

            producing -= buildingList[index].production;
            producingTxt.text = producing.ToString();

            totalProducing -= buildingList[index].production;
            totalProducingTxt.text = totalProducing.ToString();

            pollution -= buildingList[index].pollution;
            GameObject.Find("PollutionSlider").GetComponent<Slider>().value -= buildingList[index].pollution;

            installedBuildingList[index].amount--;
            installedBuildingList[index].pollution -= buildingList[index].pollution;
            installedBuildingList[index].production -= buildingList[index].production;

            if(installedBuildingList[index].production < 0)
            {
                installedBuildingList[index].production = 0;
            }

            productionPrefab[index].GetComponent<Text>().text = installedBuildingList[index].production.ToString() + " kWh";
            pollutionPrefab[index].GetComponent<Text>().text = installedBuildingList[index].pollution.ToString() + " %";
            buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();
            SetMarketPrefabs();
        }
    }

    //updates buildings if a change has occured through weather
    public void UpdateBuilding(int index)
    {
        switch (index)
        {
            case 0: //updates all purchasable buildings
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
                break;
            case 1: //updates all installed buildings
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
                break;
        }

        //updated market prefabs
        SetMarketPrefabs();

    }

    private void FillBuildingList()
    {
        //Add all Green type buildings
        buildingList.Add(new Building(0, "Solar farm", "Green", 30000, 0, 10000));
        buildingList.Add(new Building(1, "Hydro-electric plant", "Green", 275000, 0, 70000));
        buildingList.Add(new Building(2, "Wind farm", "Green", 80000, 0, 25000));
        buildingList.Add(new Building(3, "Geothermal station", "Green", 150000, 0, 40000));

        //Add all Gray type buildings
        buildingList.Add(new Building(4, "Nuclear plant", "Gray", 1000000, 30, 250000));
        buildingList.Add(new Building(5, "Industrial power plant", "Gray", 30000, 10, 27500));
        buildingList.Add(new Building(6, "Coal plant", "Gray", 600000, 20, 600000));

        //DO the same for installed buildinglist

        for(int i = 0; i < buildingList.Count; i++)
        {
            installedBuildingList.Add(new Building(buildingList[i].id, buildingList[i].name, buildingList[i].type, 0, 0, buildingList[i].price));
        }
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

    public class Building
    {
        public int id { get; set; }
        public int production { get; set; }
        public int pollution { get; set; }
        public int behaviour { get; set; }

        public string name { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public int amount { get; set; }

        public Building(int id, string name, string type, int production, int pollution, int price)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.production = production;
            this.pollution = pollution;
            this.price = price;
            behaviour = 0;
            amount = 0;
        }
    }
}
