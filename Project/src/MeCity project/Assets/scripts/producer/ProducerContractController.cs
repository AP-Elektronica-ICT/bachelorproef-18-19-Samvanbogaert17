using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Contract = ProducerMarketGridFiller.Contract;

public class ProducerContractController : MonoBehaviour
{
    public GameObject contractTitleGrid;

    private List<Contract> contractsList = new List<Contract>();
    private List<Contract> ongoingContractsList = new List<Contract>();

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

    public void AcceptContract(int index)
    {
        ongoingContractsList.Add(contractsList[index]);
    }
}
