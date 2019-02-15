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
    [HideInInspector] public List<GameObject> buildingTypePrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> energyTypePrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> productionPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> pollutionPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> price_behaviourPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> buy_sellPrefab = new List<GameObject>();

    //Lists of GameObejcts needed for the ContractsCanvas
    [HideInInspector] public Dictionary<int, GameObject> contractNamePrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> amountSoldPrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> profitPrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> cancelContractPrefab = new Dictionary<int, GameObject>();

    [HideInInspector] public List<Building> buildingList = new List<Building>();
    [HideInInspector] public List<Contract> contractList = new List<Contract>();
    [HideInInspector] public Dictionary<int, Contract> ongoingContractList = new Dictionary<int, Contract>();

    // Use this for initialization
    void Start()
    {
        contractList = FindObjectOfType<ProducerContractController>().contractList;
        if (gameObject.name == "MarketTitleGrid")
        {
            InitializeMarketCanvas();
        }
        else if(gameObject.name == "ContractTitleGrid")
        {
            AddContract(0);
            AddContract(1);
            Debug.Log(ongoingContractList[0].amountSold);
            UpdateContract(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeMarketCanvas()
    {
        buildingList = FindObjectOfType<ProducerMarketController>().buildingList;
        for (int i = 0; i < buildingList.Count; i++)
        {
            Building b = buildingList[i];

            buildingTypePrefab.Add(Instantiate(TextPrefab, transform));
            energyTypePrefab.Add(Instantiate(TextPrefab, transform));
            productionPrefab.Add(Instantiate(TextPrefab, transform));
            pollutionPrefab.Add(Instantiate(TextPrefab, transform));
            price_behaviourPrefab.Add(Instantiate(Price_BehaviourPrefab, transform));
            buy_sellPrefab.Add(Instantiate(Buy_SellPrefab, transform));

            //Scaling price_behaviour scale back to 1. not doing this will leave scale at 0, making the object invisible
            price_behaviourPrefab[i].transform.localScale = new Vector3(1f, 1f, 1f);
            buy_sellPrefab[i].transform.localScale = new Vector3(1f, 1f, 1f);

            buildingTypePrefab[i].GetComponent<Text>().text = b.name;
            energyTypePrefab[i].GetComponent<Text>().text = b.type;
            productionPrefab[i].GetComponent<Text>().text = b.production.ToString() + " kWh";
            pollutionPrefab[i].GetComponent<Text>().text = b.pollution.ToString() + " %";

            price_behaviourPrefab[i].GetComponentInChildren<Text>().text = "$ " + b.price.ToString();
            price_behaviourPrefab[i].GetComponentInChildren<RawImage>().enabled = false;
            buy_sellPrefab[i].GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "buy";
            buy_sellPrefab[i].GetComponentInChildren<Button>().onClick.AddListener(() => FindObjectOfType<ProducerMarketController>().BuyBuilding(b.id));
        }
    }

    public void AddContract(int index)
    {
        contractNamePrefab.Add(index, Instantiate(TextPrefab, transform));
        amountSoldPrefab.Add(index, Instantiate(TextPrefab, transform));
        profitPrefab.Add(index, Instantiate(TextPrefab, transform));
        cancelContractPrefab.Add(index, Instantiate(CancelContractPrefab, transform));


        ongoingContractList.Add(index, FindObjectOfType<ProducerContractController>().contractList[index]);

        contractNamePrefab[index].GetComponent<Text>().text = ongoingContractList[index].name;
        amountSoldPrefab[index].GetComponent<Text>().text = ongoingContractList[index].amountSold.ToString();
        profitPrefab[index].GetComponent<Text>().text = ongoingContractList[index].profit.ToString();
        cancelContractPrefab[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            RemoveContract(index);
        });

        Debug.Log(contractNamePrefab[index].GetComponent<Text>().text);
    }
    public void UpdateContract(int index)
    {
        contractNamePrefab[index].GetComponent<Text>().text = ongoingContractList[index].name;
        amountSoldPrefab[index].GetComponent<Text>().text = ongoingContractList[index].amountSold.ToString();
        profitPrefab[index].GetComponent<Text>().text = ongoingContractList[index].profit.ToString();
        cancelContractPrefab[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            RemoveContract(index);
        });
    }
    public void RemoveContract(int index)
    {
        ongoingContractList.Remove(index);

        Destroy(contractNamePrefab[index]);
        Destroy(amountSoldPrefab[index]);
        Destroy(profitPrefab[index]);
        Destroy(cancelContractPrefab[index]);
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
