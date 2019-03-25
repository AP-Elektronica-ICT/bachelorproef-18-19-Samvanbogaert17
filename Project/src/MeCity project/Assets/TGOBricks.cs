using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOBricks : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        DataScript.AddScore(250);
        FindObjectOfType<TGOBreakoutController>().brickCounter--;
    }
}
