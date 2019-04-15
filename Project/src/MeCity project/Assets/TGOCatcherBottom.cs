using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGOCatcherBottom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TGOCatcher.lives -= 3;
    }
}
