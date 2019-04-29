using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Admin : MonoBehaviour
{
    public Button adminBtn;
    public InputField passwordInput;

    public Toggle[] toggles;

    public GameObject itemPrefab;
    public GameObject contentGrid;

    private Transform contentTransform;
    private Dictionary<int, GameObject> itemList = new Dictionary<int, GameObject>();

    private int prevToggle = 0;

    private static string GetSHA512(string String)
    {
        UnicodeEncoding ue = new UnicodeEncoding();
        byte[] hashValue;
        byte[] message = ue.GetBytes(String);
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";

        hashValue = hashString.ComputeHash(message);

        foreach(byte x in hashValue)
        {
            hex += string.Format("{0:x2}", x);
        }
        return hex;
    }

    // Start is called before the first frame update
    void Start()
    {
        contentTransform = contentGrid.transform;
        //adminBtn.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (passwordInput.text != "" && Input.GetKeyDown(KeyCode.Return))
        {
            if(passwordInput.text == )
        }*/

        CheckToggle();
    }

    void Confirm()
    {
        //kept in two different loops in order to correctly remove multiple game objects at once
        //
        //loop destroying GameObjects
        for(int i = 0; i < itemList.Count; i++)
        {
            int temp = i;
            if (itemList[temp].GetComponentInChildren<Toggle>().isOn)
            {
                Destroy(itemList[i]);
            }
        }
        //loop for removing GameObjects from the itemList
        for (int i = 0; i < itemList.Count; i++)
        {
            int temp = i;
            if (itemList[temp].GetComponentInChildren<Toggle>().isOn)
            {
                itemList.Remove(temp);
            }
        }
    }

    void CheckToggle()
    {
        for(int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn && prevToggle != i)
            {
                Debug.Log("filling grid ..." + i);
                FillGrid(i);
                prevToggle = i;
            }
        }
    }

    void FillGrid(int type)
    {
        ClearGrid();
        switch (type)
        {
            case 0:     //reports
                for(int i = 0; i < XMLManager.instance.reportDB.list.Count; i++)
                {
                    int temp = i;
                    itemList.Add(temp, Instantiate(itemPrefab, contentTransform));
                    itemList[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = XMLManager.instance.reportDB.list[i].title;
                    itemList[i].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = XMLManager.instance.reportDB.list[i].description;
                    itemList[i].transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => Remove(temp));
                }
                break;
            case 1:     //suggestions
                for (int i = 0; i < XMLManager.instance.suggestionDB.list.Count; i++)
                {
                    int temp = i;
                    itemList.Add(temp, Instantiate(itemPrefab, contentTransform));
                    itemList[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = XMLManager.instance.suggestionDB.list[i].title;
                    itemList[i].transform.GetChild(1).GetChild(1).GetComponent<Text>().text = XMLManager.instance.suggestionDB.list[i].description;
                    itemList[i].transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(() => Remove(temp));
                }
                break;
            case 2:     //unconfirmed questions
                break;
            default:
                break;
        }
    }

    void ClearGrid()
    {
        itemList.Clear();
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }
    }

    void Remove(int index)
    {
        Destroy(itemList[index]);
        itemList.Remove(index);
    }
}
