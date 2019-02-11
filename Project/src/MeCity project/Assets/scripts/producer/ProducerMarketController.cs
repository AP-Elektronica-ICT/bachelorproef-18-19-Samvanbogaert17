using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Building = ProducerMarketGridFiller.Building;

public class ProducerMarketController : MonoBehaviour
{
    [HideInInspector] public static float pollution = 500;  // min: 0; max: 1000

    public Text titleText;
    public Text price_behaviour;
    public Text buy_sell;

    public GameObject marketTitleGrid;

    public Button marketButton;
    public Button installedButton;
    public Button defaultBtn;

    public Text moneyTxt;
    //public Text totalProducingTxt;
    private List<Building> buildingList;

    private int money;
    private int producing;

    private ColorBlock normalCb;
    private ColorBlock disabledCb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Producer Start");
        InitColorBlocks();

        buildingList = FindObjectOfType<ProducerMarketGridFiller>().buildingList;
   
        GameObject.Find("PollutionSlider").GetComponent<Slider>().value = pollution;

        //Parsing all texts to ints
        money = int.Parse(moneyTxt.text);
        //producing = int.Parse(totalProducingTxt.text);

        marketButton.onClick.AddListener(() =>
        {
            SwitchCanvas(1);    // 1 indicates the panel will change to the market panel
        });
        installedButton.onClick.AddListener(() => 
        {
            SwitchCanvas(0);    // 0 indicates the panel will change to the installed panel
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitColorBlocks()
    {
        normalCb = defaultBtn.colors;
        disabledCb = normalCb;

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

                marketButton.colors = disabledCb;
                installedButton.colors = normalCb;

                FindObjectOfType<ProducerMarketGridFiller>().SwitchMarketCanvas(0);
                break;
            case 1:
                titleText.text = "Installed";
                price_behaviour.text = "Behaviour";
                buy_sell.text = "Sell";

                marketButton.colors = normalCb;
                installedButton.colors = disabledCb;

                FindObjectOfType<ProducerMarketGridFiller>().SwitchMarketCanvas(1);
                break;
        }
    }

    private void BuyBuilding(int number)
    {
        if (Input.GetMouseButtonDown(0))
        {
            money -= buildingList[number].price;
            producing += buildingList[number].production;

            buildingList[number].amount++;

            pollution -= buildingList[number].pollution;
            GameObject.Find("PollutionSlider").GetComponent<Slider>().value -= buildingList[number].pollution;
        }
    }

    private void SellBuilding(int number)
    {
        //returns true once if button
        if (Input.GetMouseButtonDown(0))
        {
            money += buildingList[number].price / 2;
            producing -= buildingList[number].production;

            buildingList[number].amount--;

            pollution += buildingList[number].pollution;
            GameObject.Find("PollutionSlider").GetComponent<Slider>().value += buildingList[number].pollution;
        }
    }
}
