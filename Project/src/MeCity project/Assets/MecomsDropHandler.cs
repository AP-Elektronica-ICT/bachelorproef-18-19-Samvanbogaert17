using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MecomsDropHandler : MonoBehaviour, IDropHandler
{
    public static GameObject child;
    public GameObject answer
    {
        get
        {
            if (transform.childCount > 0)
            {
                child = transform.GetChild(0).gameObject;
                Destroy(transform.GetChild(0).gameObject);
            }
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!answer)
        {
            Debug.Log(child.GetComponentsInChildren<Text>()[1].text);
            child.transform.SetParent(MecomsDragHandler.itemBeingDragged.transform.parent);
            MecomsDragHandler.itemBeingDragged.transform.SetParent(transform);
        }
    }
}
