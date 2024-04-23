using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private int damamge;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerManager>().TakeDamage(damamge);
        }
    }
}
