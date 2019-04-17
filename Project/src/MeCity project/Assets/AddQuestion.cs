using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddQuestion : MonoBehaviour
{
    public GameObject pickPanel;
    public GameObject questionPanel;

    public Button confirmBtn;
    public Button[] pickBtns;

    public InputField questionInput;
    public InputField[] answerInputs;
    // Start is called before the first frame update
    void Start()
    {
        //Add onclick listeners to the pick buttons
        for(int i = 0; i < pickBtns.Length; i++)
        {
            pickBtns[i].onClick.AddListener(() =>
            {
                PickType(i);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method for picking what the question is about
    void PickType(int type)
    {
        pickPanel.gameObject.SetActive(false);
        questionPanel.gameObject.SetActive(true);

        switch (type)
        {
            case 0: //Producer
                break;
            case 1: //TGO
                break;
            case 2: //DGO
                break;
            case 3: //Supplier
                break;
            case 4: //Consumer
                break;
            case 5: //Mecoms
                break;
        }
    }
}
