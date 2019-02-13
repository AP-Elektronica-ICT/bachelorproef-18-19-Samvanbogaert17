using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Contract = ProducerMarketGridFiller.Contract;

public class ProducerContractController : MonoBehaviour
{
    [HideInInspector]public List<Contract> contractList = new List<Contract>();
    [HideInInspector]public List<Contract> ongoingContractsList = new List<Contract>();

    //Lists of GameObjects needed for the ContractCanvas
    private List<GameObject> contractNamePrefab = new List<GameObject>();
    private List<GameObject> amountSoldPrefab = new List<GameObject>();
    private List<GameObject> profitPrefab = new List<GameObject>();
    private List<GameObject> cancelContractPrefab = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        FillContractsList(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CancelContract(int index)
    {
        ongoingContractsList.RemoveAt(index);
    }

    public void AcceptContract(int index, Contract contract)
    {
        ongoingContractsList.Insert(index, contract);
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
