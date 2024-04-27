using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCutScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            player.Die();   
        }
    }
}
