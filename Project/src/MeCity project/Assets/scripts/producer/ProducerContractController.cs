using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProducerContractController : MonoBehaviour
{
    public Text moneyTxt;
    public Text producingTxt;

    private int money;
    private int producing;

    [HideInInspector] public List<Contract> contractList = new List<Contract>();
    [HideInInspector] public Dictionary<int, Contract> ongoingContractsList = new Dictionary<int, Contract>();

    //Lists of GameObjects needed for the ContractCanvas
    private Dictionary<int, GameObject> contractNamePrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> amountSoldPrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> profitPrefab = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> cancelContractPrefab = new Dictionary<int, GameObject>();

    private System.Random random = new System.Random();

    // Use this for initialization
    void Start()
    {
        money = int.Parse(moneyTxt.text);
        producing = int.Parse(producingTxt.text);

        GameObject.FindWithTag("ContractCount").GetComponent<Text>().text = ongoingContractsList.Count + " / " + contractList.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CancelContract(int index)
    {
        money = int.Parse(moneyTxt.text);
        producing = int.Parse(producingTxt.text);

        money -= ongoingContractsList[index].profit;
        producing += ongoingContractsList[index].amountSold;

        moneyTxt.text = money.ToString();
        producingTxt.text = producing.ToString();

        ongoingContractsList.Remove(index);
        FindObjectOfType<ProducerContractGridFiller>().RemoveContract(index);
        GameObject.FindWithTag("ContractCount").GetComponent<Text>().text = ongoingContractsList.Count + " / " + contractList.Count;
    }

    public void AcceptContract(int index)
    {
        ongoingContractsList.Add(index, new Contract(contractList[index].id, contractList[index].name, contractList[index].amountSold, contractList[index].profit));

        money = int.Parse(moneyTxt.text);
        producing = int.Parse(producingTxt.text);

        money += ongoingContractsList[index].profit;
        producing -= ongoingContractsList[index].amountSold;

        moneyTxt.text = money.ToString();
        producingTxt.text = producing.ToString();

        FindObjectOfType<ProducerContractGridFiller>().AddContract(index);
        GameObject.FindWithTag("ContractCount").GetComponent<Text>().text = ongoingContractsList.Count + " / " + contractList.Count;
    }

    public void UpdateContract(int index)
    {
        ongoingContractsList[index] = contractList[index];
        FindObjectOfType<ProducerContractGridFiller>().UpdateContract(index);
    }

    public void FillContractsList()
    {
        contractList.Add(new Contract(0, "Eneco", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(1, "ENGIE Electrabel", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(2, "Enovos", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(3, "Essent", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(4, "Lampiris", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(5, "Luminus", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(6, "Mega", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(7, "Poweo", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(8, "Total", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(9, "Watz", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(10, "Aspiravi Energy", Random.Range(1, 000) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(11, "EBEM", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(12, "Ecopower", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(13, "Elegant", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(14, "Energie2030", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(15, "OCTA+", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(16, "Sociaal Tarief", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(17, "Trevion", Random.Range(1, 1000) * 1000, Random.Range(1, 100) * 1000));
        contractList.Add(new Contract(18, "Wase Wind", Random.Range(1, 100) * 1000, Random.Range(1, 100) * 1000));
    }

    public void RefreshContractsList()
    {
        for (int i = 0; i < contractList.Count; i++)
        {
            contractList[i].amountSold = random.Next(1, 100) * 1000;
            contractList[i].profit = random.Next(1, 10) * 1000;
        }
    }

    private void GetContractPrefabs()
    {
        contractNamePrefab = FindObjectOfType<ProducerContractGridFiller>().contractNamePrefab;
        amountSoldPrefab = FindObjectOfType<ProducerContractGridFiller>().amountSoldPrefab;
        profitPrefab = FindObjectOfType<ProducerContractGridFiller>().profitPrefab;
        cancelContractPrefab = FindObjectOfType<ProducerContractGridFiller>().cancelContractPrefab;
    }

    private void SetContractPrefabs()
    {
        FindObjectOfType<ProducerContractGridFiller>().contractNamePrefab = contractNamePrefab;
        FindObjectOfType<ProducerContractGridFiller>().amountSoldPrefab = amountSoldPrefab;
        FindObjectOfType<ProducerContractGridFiller>().profitPrefab = profitPrefab;
        FindObjectOfType<ProducerContractGridFiller>().cancelContractPrefab = cancelContractPrefab;
    }

    public class Contract
    {
        public int id { get; set; }
        public string name { get; set; }
        public int amountSold { get; set; }
        public int profit { get; set; }

        public Contract(int id, string name, int amountSold, int profit)
        {
            this.id = id;
            this.name = name;
            this.amountSold = amountSold;
            this.profit = profit;
        }
    }

}
