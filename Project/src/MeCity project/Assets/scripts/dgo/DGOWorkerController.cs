﻿using System;
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
    public Button buyWorkerBtn;

    private int availableWorkers;
    private int totalWorkers;
    private double money;
    private double price;
    // Start is called before the first frame update
    void Start()
    {
        buyWorkerBtn.onClick.AddListener(BuyWorker);
    }

    private void BuyWorker()
    {
        //adds worker to the available workers
        availableWorkers = int.Parse(availableWorkersCountTxt.text);
        totalWorkers = int.Parse(totalWorkersCountTxt.text);
        money = double.Parse(moneyTxt.text);
        price = double.Parse(priceTxt.text);

        //decreases money based on the price
        //increases price everytime a worker is bought
        money -= price;
        price *= 1.3;
        availableWorkers++;
        totalWorkers++;

        moneyTxt.text = Math.Floor(money).ToString();
        priceTxt.text = Math.Floor(price).ToString();

        availableWorkersCountTxt.text = availableWorkers.ToString();
        totalWorkersCountTxt.text = totalWorkers.ToString();
    }
}
