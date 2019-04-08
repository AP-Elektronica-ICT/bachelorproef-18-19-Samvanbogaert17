using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MecomsDropHandler : MonoBehaviour, IDropHandler
{
    public GameObject answer
    {
        get
        {
            if (transform.childCount > 0)
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
            MecomsDragHandler.itemBeingDragged.transform.SetParent(transform);
        }
    }
}
