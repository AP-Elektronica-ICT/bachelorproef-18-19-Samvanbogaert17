using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProducerMarketGridFiller : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject Buy_SellPrefab;
    public GameObject Price_BehaviourPrefab;
    public GameObject CancelContractPrefab;

    //Lists of GameObjects needed for the MarketCanvas
    private List<GameObject> buildingTypePrefab = new List<GameObject>();
    private List<GameObject> energyTypePrefab = new List<GameObject>();
    private List<GameObject> productionPrefab = new List<GameObject>();
    private List<GameObject> pollutionPrefab = new List<GameObject>();
    private List<GameObject> price_behaviourPrefab = new List<GameObject>();
    private List<GameObject> buy_sellPrefab = new List<GameObject>();

    //Lists of GameObejcts needed for the ContractsCanvas
    private List<GameObject> contractNamePrefab = new List<GameObject>();
    private List<GameObject> amountSoldPrefab = new List<GameObject>();
    private List<GameObject> profitPrefab = new List<GameObject>();
    private List<GameObject> cancelContractPrefab = new List<GameObject>();

    [HideInInspector] public List<Building> buildingList = new List<Building>();
    [HideInInspector] public List<Contract> contractList = new List<Contract>();
    [HideInInspector] public List<Contract> ongoingContractList = new List<Contract>();

    // Use this for initialization
    void Start()
    {
        Debug.Log(gameObject.name);
        if(gameObject.name == "MarketTitleGrid")
        {
            FillBuildingsList();
            InitializeMarketCanvas();
        }
        else if(gameObject.name == "ContractTitleGrid")
        {
            FillContractsList();

            //Give the player his first contract
            AddContract(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FillBuildingsList()
    {
        //Add all Green type buildings
        buildingList.Add(new Building("Solarpanel farm", "Green", 30000, 0, 10000));
        buildingList.Add(new Building("Hydroelectric plant", "Green", 275000, 0, 70000));
        buildingList.Add(new Building("Wind turbine farm", "Green", 80000, 0, 25000));
        buildingList.Add(new Building("Geothermal station", "Green", 150000, 0, 40000));

        //Add all Gray type buildings
        buildingList.Add(new Building("Nuclear power plant", "Gray", 1000000, 10, 250000));
        buildingList.Add(new Building("Fossil fuel power station", "Gray", 100000, 6, 5000));
    }

    public void InitializeMarketCanvas()
    {
        for (int i = 0; i < buildingList.Count; i++)
        {
            Building b = buildingList[i];

            buildingTypePrefab.Add(Instantiate(TextPrefab, transform));
            energyTypePrefab.Add(Instantiate(TextPrefab, transform));
            productionPrefab.Add(Instantiate(TextPrefab, transform));
            pollutionPrefab.Add(Instantiate(TextPrefab, transform));
            price_behaviourPrefab.Add(Instantiate(Price_BehaviourPrefab, transform));
            buy_sellPrefab.Add(Instantiate(Buy_SellPrefab, transform));

            buildingTypePrefab[i].GetComponent<Text>().text = b.name;
            energyTypePrefab[i].GetComponent<Text>().text = b.type;
            productionPrefab[i].GetComponent<Text>().text = b.production.ToString() + " kWh";
            pollutionPrefab[i].GetComponent<Text>().text = b.pollution.ToString() + " %";

            price_behaviourPrefab[i].GetComponentInChildren<Text>().text = "$ " + b.price.ToString();
            buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "buy";
        }
    }
    public void SwitchMarketCanvas(int mode)
    {
        for (int i = 0; i < buildingList.Count; i++)
        {
            Building b = buildingList[i];

            switch (mode)
            {
                case 0: //Market Canvas
                    //all changes in the price_behaviour prefab
                    price_behaviourPrefab[i].GetComponentInChildren<Text>().text = "$ " + b.price.ToString();
                    //all changes in the buy_sell prefab
                    buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "buy";
                    break;
                case 1: //Installed Canvas
                    //all changes in the price_behaviour prefab
                    price_behaviourPrefab[i].GetComponentInChildren<Text>().text = b.behaviour.ToString() + " %";

                    //all changes in the buy_sell prefab
                    buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "sell";
                    break;
            }
        }
        Destroy(buildingTypePrefab[1]);
        buildingTypePrefab.RemoveAt(1);
    }

    public void FillContractsList()
    {
        contractList.Add(new Contract("Eneco", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("ENGIE Electrabel", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Enovos", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Essent", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Lampiris", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Luminus", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Mega", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Poweo", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Total", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Watz", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Aspiravi Energy", Random.Range(1, 000) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("EBEM", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Ecopower", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Elegant", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Energie2030", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("OCTA+", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Sociaal Tarief", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Trevion", Random.Range(1, 1000) * 1000, Random.Range(1, 10) * 1000));
        contractList.Add(new Contract("Wase Wind", Random.Range(1, 100) * 1000, Random.Range(1, 10) * 1000));
    }

    /*public void InitializeContractCanvas()
    {
        //Give the player a contract
        ongoingContractList[0] = contractList[0];

        //Set
        for (int i = 0; i < ongoingContractList.Count; i++)
        {
            Contract c = ongoingContractList[i];

            contractNamePrefab.Add(Instantiate(TextPrefab, transform));
            amountSoldPrefab.Add(Instantiate(TextPrefab, transform));
            profitPrefab.Add(Instantiate(TextPrefab, transform));
            cancelContractPrefab.Add(Instantiate(CancelContractPrefab, transform));

            contractNamePrefab[i].GetComponent<Text>().text = c.name;
            amountSoldPrefab[i].GetComponent<Text>().text = c.amountSold.ToString();
            profitPrefab[i].GetComponent<Text>().text = c.profit.ToString();
            cancelContractPrefab[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                CancelContract(i);
            });
        }
    }*/

    public void AddContract(int index)
    {
        contractNamePrefab.Insert(index, Instantiate(TextPrefab, transform));
        amountSoldPrefab.Insert(index, Instantiate(TextPrefab, transform));
        profitPrefab.Insert(index, Instantiate(TextPrefab, transform));
        cancelContractPrefab.Insert(index, Instantiate(CancelContractPrefab, transform));

        ongoingContractList.Insert(index, contractList[index]);

        contractNamePrefab[index].GetComponent<Text>().text = ongoingContractList[index].name;
        amountSoldPrefab[index].GetComponent<Text>().text = ongoingContractList[index].amountSold.ToString();
        profitPrefab[index].GetComponent<Text>().text = ongoingContractList[index].profit.ToString();
        cancelContractPrefab[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            CancelContract(index);
        });
    }

    public void CancelContract(int index)
    {
        Destroy(contractNamePrefab[index]);
        Destroy(amountSoldPrefab[index]);
        Destroy(profitPrefab[index]);
        Destroy(cancelContractPrefab[index]);
        FindObjectOfType<ProducerContractController>().CancelContract(index);
    }

    public class Building
    {
        public int production { get; set; }
        public int pollution { get; set; }
        public int behaviour { get; set; }

        public string name { get; set; }
        public string type { get; set; }
        public int price { get; set; }
        public int amount { get; set; }


        public Building(string name, string type, int production, int pollution, int price)
        {
            this.name = name;
            this.type = type;
            this.production = production;
            this.pollution = pollution;
            this.price = price;
            behaviour = 0;
            amount = 0;
        }
    }

    public class Contract
    {
        public string name { get; set; }
        public int amountSold { get; set; }
        public int profit { get; set; }

        public Contract(string name, int amountSold, int profit)
        {
            this.name = name;
            this.amountSold = amountSold;
            this.profit = profit;
        }
    }

}
