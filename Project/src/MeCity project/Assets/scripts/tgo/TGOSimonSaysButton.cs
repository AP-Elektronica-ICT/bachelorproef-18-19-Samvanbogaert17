using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOSimonSaysButton : MonoBehaviour
{
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    private void OnMouseDown()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    }

    private void OnMouseUp()
    {
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f);

    }
}
