using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    [SerializeField] private GameObject message;   
    private Collider2D collider2D;
    private GameObject player;
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            collider2D.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            GameObject gameObject = collision.gameObject;
            if (gameObject!=null)
            {
              int key =   gameObject.GetComponent<PlayerManager>().GetKeyStatus();
                if (key == 1)
                {
                    player = gameObject;
                }
                
            }
            message.SetActive(true);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            message.SetActive(false);

        }
    }
}
