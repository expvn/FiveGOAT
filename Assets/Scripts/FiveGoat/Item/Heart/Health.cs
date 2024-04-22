using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            GameObject player = collision.gameObject;
            if (player != null)
            {
                player.GetComponent<PlayerManager>().AddHealth();
                Destroy(gameObject);
            }
        }
    }
}
