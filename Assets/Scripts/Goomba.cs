using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* if (collision.gameObject.CompareTag("Player"))
         {
             Player player = collision.gameObject.GetComponent<Player>();

             if (player.starpower)
             {
                 Hit();
             }
             else if (collision.transform.DotTest(transform, Vector2.down))
             {
                 Flatten();
             }
             else
             {
                 player.Hit();
             }
         }*/
        if (collision.gameObject.CompareTag("Player"))
        {
            float direction = transform.GetComponent<EntityMovement>().direction.x;
            collision.transform.GetComponent<PlayerManager>().OnHit(direction);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell")||other.CompareTag("Sword"))
        {
            Hit();
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }

    public void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }

}
