using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TGOMastermindDropHandler : MonoBehaviour, IDropHandler
{
    public GameObject answer
    {
        get
        {
            if(transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!answer)
        {
            TGOMastermindDragHandler.itemBeingDragged.transform.SetParent(transform);
        }
    }
}
