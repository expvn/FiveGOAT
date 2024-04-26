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
            GameObject khien = player.transform.Find("khien").gameObject;
            if (khien == null) return;
            if (khien.activeInHierarchy)
            {
                khien.GetComponent<KhienScript>().TakeDamage(damamge);
                return;
            }
            player.GetComponent<PlayerManager>().TakeDamage(damamge);
        }
    }
}
