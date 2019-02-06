using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerMarketController : MonoBehaviour
{
    public Canvas marketPanel;
    public Canvas installedPanel;
    public Button marketButton;
    public Button installedButton;
    public Button[] buyButtons;
    public Button[] sellButtons;
    public Text[] priceTxt;
    public Text moneyTxt;
    public Text totalProducingTxt;

    private List<Building> buildingList;

    private int[] price;
    private int money;
    private int producing;

    // Start is called before the first frame update
    void Start()
    {
        //Fill the BuildingsList with Buildings
        FillBuildingsList();

        //Parsing all texts to ints
        for(int i = 0; i < priceTxt.Length; i++)
        {
            price[i] = int.Parse(priceTxt[i].text);
        }
        money = int.Parse(moneyTxt.text);
        producing = int.Parse(totalProducingTxt.text);

        //Add onClickListeners to every buy button
        for(int i = 0; i < buyButtons.Length; i++)
        {
            buyButtons[i].onClick.AddListener(() => BuyBuilding(i));
        }

        marketPanel.enabled = true;
        installedPanel.enabled = false;

        marketButton.onClick.AddListener(SwitchCanvas);
        installedButton.onClick.AddListener(SwitchCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method used to switch from market panel to installed panel or vice versa
    private void SwitchCanvas()
    {
        marketButton.onClick.RemoveAllListeners();
        installedButton.onClick.RemoveAllListeners();

        marketPanel.enabled = !marketPanel.enabled;
        installedPanel.enabled = !installedPanel.enabled;

        marketButton.onClick.AddListener(SwitchCanvas);
        installedButton.onClick.AddListener(SwitchCanvas);
    }

    private void BuyBuilding(int number)
    {
        if (Input.GetMouseButtonDown(0))
        {
            money -= price[number];
            producing += buildingList[number].production;
        }
    }

    private void SellBuilding(int number)
    {
        //returns true once if button
        if (Input.GetMouseButtonDown(0))
        {
            money += price[number] / 2;
            producing -= buildingList[number].production;
        }
    }

    private class Building
    {
        public int production { get; set; }
        private string name;
        private string type;
        private int pollution;
        private int price;

        public int amount;


        public Building(string name, string type, int production, int pollution, int price)
        {
            this.name = name;
            this.type = type;
            this.production = production;
            this.pollution = pollution;
            this.production = price;
        }

    }

    private void FillBuildingsList()
    {
        //Add all Green type buildings
        buildingList.Add(new Building("Solarpanel farm", "Green", 30000, 0, 10000));
        buildingList.Add(new Building("Hydroelectric plant", "Green", 275000, 0, 70000));
        buildingList.Add(new Building("Wind turbine farm", "Green", 80000, 0, 25000));
        buildingList.Add(new Building("Geothermal station", "Green", 150000, 0, 40000));

        //Add all Gray type buildings
        buildingList.Add(new Building("Nuclear power plant", "Gray", 1000000, 10, 250000));
        buildingList.Add(new Building("Fossil fuel power station", "Green", 100000, 6, 25000));
    }


}
