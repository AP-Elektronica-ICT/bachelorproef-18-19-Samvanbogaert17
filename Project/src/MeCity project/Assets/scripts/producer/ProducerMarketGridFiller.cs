using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Building = ProducerMarketController.Building;


public class ProducerMarketGridFiller : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject Buy_SellPrefab;
    public GameObject Price_BehaviourPrefab;

    //Lists of GameObjects needed for the MarketCanvas
    [HideInInspector] public List<GameObject> buildingTypePrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> energyTypePrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> productionPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> pollutionPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> price_behaviourPrefab = new List<GameObject>();
    [HideInInspector] public List<GameObject> buy_sellPrefab = new List<GameObject>();

    [HideInInspector] public List<Building> buildingList = new List<Building>();


    // Use this for initialization
    void Start()
    {
            InitializeMarketCanvas();
    }

    //Add all buildings from the buildings list to the market grid
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
}
