using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGOBricks : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TGOBreakoutController.instance.bricks--;
        Destroy(gameObject);
        DataScript.AddScore(250);
    }
}
