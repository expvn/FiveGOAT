using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    private void OnBecameVisible()
    {
        GetComponent<Move>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

    }
    private void OnBecameInvisible()
    {
        
    }
  
}
