using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DGOUpgradeGridFiller : MonoBehaviour
{
    public GameObject textPrefab;
    public GameObject buttonPrefab;

    private List<Upgrade> upgradeList = new List<Upgrade>();
    private List<GameObject> titlePrefab = new List<GameObject>();
    private List<GameObject> effectPrefab = new List<GameObject>();
    private List<GameObject> buyPrefab = new List<GameObject>();

    private Transform upgradeTitleTransform;
    private Transform upgradeTransform;
    // Start is called before the first frame update
    void Start()
    {
        FillUpgradeList();
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
            effectPrefab.Add(Instantiate(textPrefab, upgradeTransform));
            buyPrefab.Add(Instantiate(buttonPrefab, upgradeTransform));

            titlePrefab[i].GetComponent<Text>().text = u.name;
            effectPrefab[i].GetComponent<Text>().text = u.strEffect;
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

    }

    private void FillUpgradeList()
    {

    }

    public class Upgrade
    {
        public string name { get; set; }
        public string strEffect { get; set; }
        public int effect { get; set; }
        public Upgrade(string name, string strEffect, int effect)
        {
            this.name = name;
            this.strEffect = strEffect+ " +" + effect.ToString();
            this.effect = effect;
        }
    }
}
