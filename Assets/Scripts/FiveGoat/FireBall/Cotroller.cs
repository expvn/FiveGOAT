using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cotroller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerManager player = collision.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
    }
}
