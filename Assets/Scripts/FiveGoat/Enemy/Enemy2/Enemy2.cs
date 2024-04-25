using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float attackBoss = 2f;

    private Animator animator;
    private GameObject player;
    private int heart = 2;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Tìm đối tượng Player mỗi frame
        FindPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            // Trừ máu của Player
            PlayerManager.instan.TakeDamage(3);

            float playerPosition = collider2D.transform.position.x;
            float enemyPosition = transform.position.x;

            if (playerPosition < enemyPosition)
            {
                animator.Play("Left");
                Debug.Log("Trai");
            }
            else
            {
                animator.Play("Right");
                Debug.Log("Phai");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            animator.Play("idle");
            Debug.Log("Dung im");
        }
    }
    private void TakeDamage()
    {
        heart--;
        animator.Play("Hit");
        if (heart < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void FindPlayer()
    {
        // Tìm đối tượng Player với tag là "Player"
        player = GameObject.FindWithTag("Player");
    }
}
