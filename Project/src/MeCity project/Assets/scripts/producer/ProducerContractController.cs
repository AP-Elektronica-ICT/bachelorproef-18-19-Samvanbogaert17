using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Contract = ProducerMarketGridFiller.Contract;

public class ProducerContractController : MonoBehaviour
{
    [HideInInspector]public List<Contract> contractList = new List<Contract>();
    [HideInInspector]public Dictionary<int, Contract> ongoingContractsList = new Dictionary<int, Contract>();

    //Lists of GameObjects needed for the ContractCanvas
    private Dictionary<int, GameObject> contractNamePrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> amountSoldPrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> profitPrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> cancelContractPrefab = new Dictionary<int, GameObject>();

    private System.Random random = new System.Random();

    // Use this for initialization
    void Start()
    {
        //FillContractsList(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CancelContract(int index)
    {
        ongoingContractsList.Remove(index);
    }

    public void AcceptContract(int index)
    {
        ongoingContractsList.Add(index, contractList[index]);
        FindObjectOfType<ProducerMarketGridFiller>().AddContract(index);
    }

    public void UpdateContract(int index)
    {
        ongoingContractsList[index] = contractList[index];
        FindObjectOfType<ProducerMarketGridFiller>().UpdateContract(index);
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

    public void RefreshContractsList()
    {
        for(int i = 0; i < contractList.Count; i++)
        {
            contractList[i].amountSold = random.Next(1, 100) * 1000;
            contractList[i].profit = random.Next(1, 10) * 1000;
        }
    }

    private void GetContractPrefabs()
    {
        contractNamePrefab = FindObjectOfType<ProducerMarketGridFiller>().contractNamePrefab;
        amountSoldPrefab = FindObjectOfType<ProducerMarketGridFiller>().amountSoldPrefab;
        profitPrefab = FindObjectOfType<ProducerMarketGridFiller>().profitPrefab;
        cancelContractPrefab = FindObjectOfType<ProducerMarketGridFiller>().cancelContractPrefab;
    }

    private void SetContractPrefabs()
    {
        FindObjectOfType<ProducerMarketGridFiller>().contractNamePrefab = contractNamePrefab;
        FindObjectOfType<ProducerMarketGridFiller>().amountSoldPrefab = amountSoldPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().profitPrefab = profitPrefab;
        FindObjectOfType<ProducerMarketGridFiller>().cancelContractPrefab = cancelContractPrefab;
    }

}
