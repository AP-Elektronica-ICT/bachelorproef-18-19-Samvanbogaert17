using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DGOWorkerController : MonoBehaviour
{
    public Text availableWorkersCountTxt;
    public Text totalWorkersCountTxt;
    public Text moneyTxt;
    public Text priceTxt;

    private int availableWorkers;
    private int totalWorkers;
    private int money;
    private int price;
    // Start is called before the first frame update
    void Start()
    {
        availableWorkers = int.Parse(availableWorkersCountTxt.text);
        totalWorkers = int.Parse(totalWorkersCountTxt.text);
        money = int.Parse(moneyTxt.text);
        price = int.Parse(priceTxt.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuyWorker()
    {
        if(money > price)
        {
            money -= price;
            price += (price * (13/10));
            availableWorkers++;
            totalWorkers++;

            moneyTxt.text = money.ToString();
            priceTxt.text = price.ToString();

            availableWorkersCountTxt.text = availableWorkers.ToString();
            totalWorkersCountTxt.text = totalWorkers.ToString();
        }
    }
}
