using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private bool haskey;
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == AllTag.KEY_TAG_PLAYER)
        {
            haskey = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public int GetKeyStatus()
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
