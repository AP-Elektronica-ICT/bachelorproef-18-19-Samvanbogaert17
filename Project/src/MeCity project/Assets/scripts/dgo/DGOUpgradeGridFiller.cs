using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Problem = DGOProblemController.Problem;

public class DGOUpgradeGridFiller : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject buttonPrefab;

    public GameObject upgradeTitleGrid;
    public GameObject upgradeEffectGrid;
    public GameObject upgradeGrid;

    public Text moneyTxt;
    private int money;

    private List<Upgrade> upgradeList = new List<Upgrade>();

    private List<GameObject> titlePrefab = new List<GameObject>();
    private List<GameObject> effectPrefab = new List<GameObject>();
    private List<GameObject> pricePrefab = new List<GameObject>();
    private List<GameObject> buyPrefab = new List<GameObject>();

    private Transform upgradeTitleTransform;
    private Transform upgradeEffectTransform;
    private Transform upgradeTransform;
    // Start is called before the first frame update
    void Start()
    {
        FillUpgradeList();

        upgradeTitleTransform = upgradeTitleGrid.transform;
        upgradeEffectTransform = upgradeEffectGrid.transform;
        upgradeTransform = upgradeGrid.transform;

        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Initialize()
    {
        for (int i = 0; i < upgradeList.Count; i++)
        {
            Upgrade u = upgradeList[i];
            int index = i;
            titlePrefab.Add(Instantiate(textPrefab, upgradeTitleTransform));
            effectPrefab.Add(Instantiate(textPrefab, upgradeEffectTransform));
            pricePrefab.Add(Instantiate(textPrefab, upgradeTransform));
            buyPrefab.Add(Instantiate(buttonPrefab, upgradeTransform));

            titlePrefab[i].GetComponent<Text>().text = u.name;
            titlePrefab[i].GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
            pricePrefab[i].GetComponent<Text>().text = "$ " + u.price;
            effectPrefab[i].GetComponent<Text>().text = u.strEffect;
            buyPrefab[i].GetComponentInChildren<Text>().text = "Buy";
            buyPrefab[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                InstallUpgrade(index);
                buyPrefab[index].GetComponent<Button>().interactable = false;
                buyPrefab[index].GetComponentInChildren<Text>().text = "Bought";
            });
        }
    }

    private void InstallUpgrade(int index)
    {
        Upgrade u = upgradeList[index];

        switch (u.type)
        {
            case 1: //decreases frequency of a problem
                FindObjectOfType<DGOEventSystem>().problemTimerMaxVal += u.effect;
                FindObjectOfType<DGOEventSystem>().problemTimerMinVal += u.effect;
                break;
            case 2: //decreases duration of a problem
                foreach (Problem p in FindObjectOfType<DGOProblemController>().problemList)
                {
                    if(p.durationInSeconds - u.effect * 20 > 10)
                    {
                        p.durationInSeconds -= u.effect * 10;
                    }
                    else
                    {
                        p.durationInSeconds = 10;
                    }
                }
                break;
            case 3: //decreases happiness decline of a problem
                foreach (Problem p in FindObjectOfType<DGOProblemController>().problemList)
                {
                    if(p.happinessDecrease - u.effect > 1)
                    {
                        p.happinessDecrease -= u.effect;
                    }
                    Debug.Log(p.severity);
                }
                break;
        }

        money = int.Parse(moneyTxt.text);
        money -= u.price;
        moneyTxt.text = money.ToString();

    }

    private void FillUpgradeList()
    {
        upgradeList.Add(new Upgrade(3, "Install a smart grid", 5, 10000));
        upgradeList.Add(new Upgrade(1, "Invest in better grid connections", 2, 4750));
        upgradeList.Add(new Upgrade(1, "Install advanced substations", 3, 8500));
        upgradeList.Add(new Upgrade(2, "train your workers to increase efficiency", 2, 2500));
        upgradeList.Add(new Upgrade(2, "invest in an andvanced toolset for your workers", 1, 1250));
        upgradeList.Add(new Upgrade(3, "Install a failsafe over the grid", 3, 6850));
    }

    public class Upgrade
    {
        public string name { get; set; }
        public string strEffect { get; set; }
        public int effect { get; set; }
        public int type { get; set; }
        public int price { get; set; }
        //type 1: decreases frequency of a problem
        //type 2: decreases duration of a problem
        //type 3: decreases happiness decline
        public Upgrade(int type, string name, int effect, int price)
        {
            this.type = type;
            this.name = name;
            this.effect = effect;
            this.price = price;
            switch (type)
            {
                case 1:
                    strEffect = "frequency decrease: -";
                    break;
                case 2:
                    strEffect = "duration decrease: -";
                    break;
                case 3:
                    strEffect = "happiness decrease: -";
                    break;
            }
            strEffect = strEffect + effect.ToString();
        }
    }
}
