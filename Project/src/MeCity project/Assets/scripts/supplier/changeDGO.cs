using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeDGO : MonoBehaviour
{
    public Text NormalGridText;
    public Text GreenGridText;
    public Dropdown NormalEnergyDropDown;
    public Dropdown GreenEnergyDropDown;
    public Text NormalPriceText;
    public Text GreenPriceText;
    private List<EnergyItem> normalEnergyItems = new List<EnergyItem>();
    private List<EnergyItem> greenEnergyItems = new List<EnergyItem>();
    private System.Random random;

    public Button greenBtn;
    public Button regBtn;
    public Text money;
    public Text energy;

    // script used for the market canvas
    void Start()
    {
        random = new System.Random();

        // randomly load prices, mwh,..
        RefreshItems();

        greenBtn.onClick.AddListener(() => BuyGreen());
        regBtn.onClick.AddListener(() => BuyRegular());
    }

    public void RefreshItems()
    {
        Setoptions(NormalEnergyDropDown, NormalPriceText, normalEnergyItems, NormalGridText);
        Setoptions(GreenEnergyDropDown, GreenPriceText, greenEnergyItems, GreenGridText);
    }

    private void Setoptions(Dropdown dropdown, Text textfield, List<EnergyItem> energyItems, Text txtGrid)
    {
        GenerateEnergyItems(txtGrid, energyItems);
        dropdown.onValueChanged.AddListener(delegate { changedValue(dropdown, textfield, energyItems); });

        List<string> items = new List<string>();
        for (int i = 0; i < energyItems.Count; i++)
        {
            items.Add(energyItems[i].getSeller());
        }
        //dropdown.ClearOptions();
        //dropdown.AddOptions(items);
        textfield.text = string.Format("{0:n0}",energyItems[0].getPrice());
    }

    private void GenerateEnergyItems(Text GridText, List<EnergyItem> energyItems)
    {
        for (int i = 0; i < 4; i++)
        {
            int amount = random.Next(100, 5000);
            int price = random.Next(30, 120);
            energyItems.Add(new EnergyItem(amount, amount * price, price, i.ToString()));
        }
        string gridText = string.Format("{0,-15}\t{1,-7}\t{2,-10}\t{3,-7}\n", "Seller", "MWh", "Total Price", "$/MWh");

        for (int i = 0; i < 4; i++)
        {
            gridText += string.Format("{0,-15}\t{1,-9:n0}\t{2,-12:n0}\t{3,-7:n0}\n", energyItems[i].getSeller(), energyItems[i].getAmount(), energyItems[i].getPrice(), energyItems[i].getPricePerUnit());
        }
        GridText.text = gridText;
    }

    private void changedValue(Dropdown usedDropDown, Text priceTextField, List<EnergyItem> energyItems)
    {
        for (int i = 0; i < 4; i++)
        {
            if (int.Parse(energyItems[i].getSeller()) == usedDropDown.value)
            {
                priceTextField.text = string.Format("{0:n0}", energyItems[i].getPrice());
            }
        }
    }
    private void BuyGreen()
    {
        int test = GreenEnergyDropDown.value;
        money.text = int.Parse((float.Parse(money.text) - float.Parse(greenEnergyItems[test].getPrice().ToString())).ToString()).ToString();
        float en = (float.Parse(energy.text) + (float)(float.Parse(greenEnergyItems[test].getAmount().ToString())*1000f));
        energy.text = string.Format("{0:n0}", en);
        int amount = random.Next(100, 5000);
        int price = random.Next(30, 120);
        greenEnergyItems[test] = new EnergyItem(amount, amount * price, price, test.ToString());
        string gridText = string.Format("{0,-15}\t{1,-7}\t{2,-10}\t{3,-7}\n", "Seller", "MWh", "Total Price", "$/MWh");

        for (int i = 0; i < 4; i++)
        {
            gridText += string.Format("{0,-15}\t{1,-9:n0}\t{2,-12:n0}\t{3,-7:n0}\n", greenEnergyItems[i].getSeller(), greenEnergyItems[i].getAmount(), greenEnergyItems[i].getPrice(), greenEnergyItems[i].getPricePerUnit());
        }
        GreenGridText.text = gridText;
        GreenPriceText.text = string.Format("{0:n0}",greenEnergyItems[test].getPrice());
    }
    private void BuyRegular()
    {
        int test = NormalEnergyDropDown.value;
        money.text = int.Parse((float.Parse(money.text) - float.Parse(normalEnergyItems[test].getPrice().ToString())).ToString()).ToString();
        float en = (float.Parse(energy.text) + (float)(float.Parse(normalEnergyItems[test].getAmount().ToString())*1000f));
        energy.text = string.Format("{0:n0}", en);
        int amount = random.Next(100, 5000);
        int price = random.Next(30, 120);
        normalEnergyItems[test] = new EnergyItem(amount, amount * price, price, test.ToString());
        string gridText = string.Format("{0,-15}\t{1,-7}\t{2,-10}\t{3,-7}\n", "Seller", "MWh", "Total Price", "$/MWh");

        for (int i = 0; i < 4; i++)
        {
            gridText += string.Format("{0,-15}\t{1,-9:n0}\t{2,-12:n0}\t{3,-7:n0}\n", normalEnergyItems[i].getSeller(), normalEnergyItems[i].getAmount(), normalEnergyItems[i].getPrice(), normalEnergyItems[i].getPricePerUnit());
        }
        NormalGridText.text = gridText;
        NormalPriceText.text = string.Format("{0:n0}",normalEnergyItems[test].getPrice());
    }
    private class EnergyItem
    {
        private int amount;
        private int price;
        private int pricePerUnit;
        private string seller;

        public EnergyItem(int amount, int price, int pricePerUnit, string seller)
        {
            this.amount = amount;
            this.price = price;
            this.pricePerUnit = pricePerUnit;
            this.seller = seller;
        }

        public int getAmount()
        {
            return amount;
        }

        public int getPrice()
        {
            return price;
        }

        public int getPricePerUnit()
        {
            return pricePerUnit;
        }

        public string getSeller()
        {
            return seller;
        }
    }
}