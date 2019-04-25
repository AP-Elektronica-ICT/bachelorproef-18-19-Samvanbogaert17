using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Contract = ProducerContractController.Contract;

public class ProducerContractGridFiller : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject CancelContractPrefab;

    //Lists of GameObejcts needed for the ContractsCanvas
    [HideInInspector] public Dictionary<int, GameObject> contractNamePrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> amountSoldPrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> profitPrefab = new Dictionary<int, GameObject>();
    [HideInInspector] public Dictionary<int, GameObject> cancelContractPrefab = new Dictionary<int, GameObject>();

    [HideInInspector] public List<Contract> contractList = new List<Contract>();
    [HideInInspector] public Dictionary<int, Contract> ongoingContractList = new Dictionary<int, Contract>();

    // Use this for initialization
    void Start()
    {
        contractList = FindObjectOfType<ProducerContractController>().contractList;
        ongoingContractList = FindObjectOfType<ProducerContractController>().ongoingContractsList;
    }

    //Add contract to the contract grid
    public void AddContract(int index)
    {
        Contract c = contractList[index];

        contractNamePrefab.Add(index, Instantiate(TextPrefab, transform));
        amountSoldPrefab.Add(index, Instantiate(TextPrefab, transform));
        profitPrefab.Add(index, Instantiate(TextPrefab, transform));
        cancelContractPrefab.Add(index, Instantiate(CancelContractPrefab, transform));

        contractNamePrefab[index].GetComponent<Text>().text = c.name;
        amountSoldPrefab[index].GetComponent<Text>().text = c.amountSold.ToString();
        profitPrefab[index].GetComponent<Text>().text = c.profit.ToString();
        cancelContractPrefab[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            FindObjectOfType<ProducerContractController>().CancelContract(c.id);
        });
    }
    //Update contract on the contract grid
    public void UpdateContract(int index)
    {
        Contract c = ongoingContractList[index];

        contractNamePrefab[index].GetComponent<Text>().text = c.name;
        amountSoldPrefab[index].GetComponent<Text>().text = c.amountSold.ToString();
        profitPrefab[index].GetComponent<Text>().text = c.profit.ToString();
        cancelContractPrefab[index].GetComponent<Button>().onClick.AddListener(() =>
        {
            FindObjectOfType<ProducerContractController>().CancelContract(c.id);
        });
    }
    //Remove contract from the contract grid
    public void RemoveContract(int index)
    {
        Destroy(contractNamePrefab[index]);
        Destroy(amountSoldPrefab[index]);
        Destroy(profitPrefab[index]);
        Destroy(cancelContractPrefab[index]);

        contractNamePrefab.Remove(index);
        amountSoldPrefab.Remove(index);
        profitPrefab.Remove(index);
        cancelContractPrefab.Remove(index);
    }
}
