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

    public GameObject marketTitleGrid;

    public Button marketButton;
    public Button installedButton;
    public Button defaultBtn;

    private List<Building> buildingList = new List<Building>();
    private List<Building> installedBuildingList = new List<Building>();

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
        InitColorBlocks();

        InitializeBuildingList();
        GetRemoteMarketLists();

        GameObject.Find("PollutionSlider").GetComponent<Slider>().value = pollution;

        //Parsing all texts to ints
        money = int.Parse(moneyTxt.text);
        producing = int.Parse(producingTxt.text);

        marketButton.onClick.AddListener(() =>
        {
            SwitchCanvas(0);    // 1 indicates the panel will change to the market panel
        });
        installedButton.onClick.AddListener(() =>
        {
            SwitchCanvas(1);    // 0 indicates the panel will change to the installed panel
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitColorBlocks()
    {
        normalCb = disabledCb = defaultBtn.colors;

        disabledCb.normalColor = normalCb.disabledColor;
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
                    //all changes in the price_behaviour prefab
                    price_behaviourPrefab[i].GetComponentInChildren<Text>().text = "$ " + buildingList[i].price.ToString();
                    price_behaviourPrefab[i].GetComponentInChildren<RawImage>().enabled = false;
                    //all changes in the buy_sell prefab
                    buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "buy";
                    //Adds a new onclick listener to buy a building
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.AddListener(() => BuyBuilding(b.id));
                }
                break;
            case 1:
                titleText.text = "Installed";
                price_behaviour.text = "Behaviour";
                buy_sell.text = "Sell";

                marketButton.colors = disabledCb;
                installedButton.colors = normalCb;

                for (int i = 0; i < buildingList.Count; i++)
                {
                    Building b = buildingList[i];
                    //all changes in the price_behaviour prefab
                    price_behaviourPrefab[i].GetComponentInChildren<Text>().text = installedBuildingList[i].behaviour.ToString() + " %";
                    if (installedBuildingList[i].behaviour > 0)
                    {
                        price_behaviourPrefab[i].GetComponentInChildren<RawImage>().texture = greenArrow;
                    }
                    else if (installedBuildingList[i].behaviour < 0)
                    {
                        price_behaviourPrefab[i].GetComponentInChildren<RawImage>().texture = redArrow;
                    }
                    else
                    {
                        price_behaviourPrefab[i].GetComponentInChildren<RawImage>().enabled = false;
                    }
                    price_behaviourPrefab[i].GetComponentInChildren<RawImage>().enabled = true;
                    //all changes in the buy_sell prefab
                    buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "sell";
                    //Adds a new onlick listener to sell a building
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                    buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.AddListener(() => SellBuilding(b.id));
                }
                break;
        }
        SetRemoteMarketLists();
    }

    public void BuyBuilding(int index)
    {
        money -= buildingList[index].price;
        moneyTxt.text = money.ToString();

        totalProducing += buildingList[index].production;
        totalProducingTxt.text = totalProducing.ToString();

        pollution -= buildingList[index].pollution;
        GameObject.Find("PollutionSlider").GetComponent<Slider>().value -= buildingList[index].pollution;


        AddBuilding(index);

        SetRemoteMarketLists();
    }

    public void SellBuilding(int index)
    {
        money += buildingList[index].price / 2;
        moneyTxt.text = money.ToString();

        totalProducing -= buildingList[index].production;
        totalProducingTxt.text = totalProducing.ToString();

        pollution += buildingList[index].pollution;
        GameObject.Find("PollutionSlider").GetComponent<Slider>().value += buildingList[index].pollution;

        if (installedBuildingList[index].amount > 0)
        {
            RemoveBuilding(index);
        }
        buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();

        SetRemoteMarketLists();
    }

    private void ChangeBehaviour(int index, bool answerIsCorrect)
    {
        if (answerIsCorrect)
        {
            installedBuildingList[index].behaviour = UnityEngine.Random.Range(1, 11);
        }
        else if (!answerIsCorrect)
        {
            installedBuildingList[index].behaviour = UnityEngine.Random.Range(-1, -11);
        }
        installedBuildingList[index].production += (installedBuildingList[index].production * (installedBuildingList[index].behaviour) * 10);

        SetRemoteMarketLists();
    }

    private void AddBuilding(int index)
    {
        installedBuildingList[index].amount++;
        installedBuildingList[index].pollution *= installedBuildingList[index].amount;
        installedBuildingList[index].production *= installedBuildingList[index].amount;

        buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();
        SetRemoteMarketLists();
    }
    private void RemoveBuilding(int index)
    {
        installedBuildingList[index].amount--;
        installedBuildingList[index].pollution *= installedBuildingList[index].amount;
        installedBuildingList[index].production *= installedBuildingList[index].amount;

        buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();
        
        SetRemoteMarketLists();
    }

    private void InitializeBuildingList()
    {
        buildingList = FindObjectOfType<ProducerMarketGridFiller>().buildingList;
        installedBuildingList = FindObjectOfType<ProducerMarketGridFiller>().buildingList;
        for (int i = 0; i < buildingList.Count; i++)
        {
            installedBuildingList[i].production *= installedBuildingList[i].amount;
            installedBuildingList[i].pollution *= installedBuildingList[i].amount;
        }
    }
    private void SetTexts(int index)
    {
        buy_sellPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].amount.ToString();
        pollutionPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].pollution.ToString();
        productionPrefab[index].GetComponentInChildren<Text>().text = installedBuildingList[index].production.ToString();
    }
    private void GetRemoteMarketLists()
    {
        buildingTypePrefab = FindObjectOfType<ProducerMarketGridFiller>().buildingTypePrefab;
        energyTypePrefab = FindObjectOfType<ProducerMarketGridFiller>().energyTypePrefab;
        productionPrefab = FindObjectOfType<ProducerMarketGridFiller>().productionPrefab;
        pollutionPrefab = FindObjectOfType<ProducerMarketGridFiller>().pollutionPrefab;
        price_behaviourPrefab = FindObjectOfType<ProducerMarketGridFiller>().price_behaviourPrefab;
        buy_sellPrefab = FindObjectOfType<ProducerMarketGridFiller>().buy_sellPrefab;
    }
    private void SetRemoteMarketLists()
    {
        FindObjectOfType<ProducerMarketGridFiller>().buildingTypePrefab = buildingTypePrefab;
        FindObjectOfType<ProducerMarketGridFiller>().energyTypePrefab = energyTypePrefab;
        FindObjectOfType<ProducerMarketGridFiller>().productionPrefab = productionPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().pollutionPrefab = pollutionPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().price_behaviourPrefab = price_behaviourPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().buy_sellPrefab = buy_sellPrefab;
    }
}
