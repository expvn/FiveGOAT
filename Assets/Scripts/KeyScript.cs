using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
     
    private bool haskey;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "Player")
        {
            haskey = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private int GetKeyStatus()
    {
        if (haskey == true)
        {
            return 1;
            
        }
        else
        {
            return 0;
        }
    }


}
