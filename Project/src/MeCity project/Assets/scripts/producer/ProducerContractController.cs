using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Contract = ProducerMarketGridFiller.Contract;

public class ProducerContractController : MonoBehaviour
{
    [HideInInspector]public List<Contract> contractsList = new List<Contract>();
    [HideInInspector]public List<Contract> ongoingContractsList = new List<Contract>();

    // Use this for initialization
    void Start()
    {
        
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
}
