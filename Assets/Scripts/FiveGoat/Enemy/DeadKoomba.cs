using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadKoomba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            player.TakeDamage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_SWORD))
        {
            Koopa koopa = transform.GetComponent<Koopa>();
            koopa.Hit();
        }
    }
}
