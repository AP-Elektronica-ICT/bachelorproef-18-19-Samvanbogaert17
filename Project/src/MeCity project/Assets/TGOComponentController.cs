using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOComponentController : MonoBehaviour
{
    bool isEnabled = false;
    bool isDisabled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TGOMinigamesController.GameStarted && GetComponent<Canvas>().enabled && !isEnabled)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i).gameObject;
                if (child != null)
                {
                    child.SetActive(true);
                }
            }
            Debug.Log("isEnabled = true");
            isEnabled = true;
            isDisabled = false;
        }
        else if(!isDisabled && GetComponent<Canvas>().enabled == false)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                var child = gameObject.transform.GetChild(i).gameObject;
                if (child != null)
                {
                    child.SetActive(false);
                }
            }
            Debug.Log("isEnabled = false");
            isEnabled = false;
            isDisabled = true;
        }
    }
}
