using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private GameObject key;
    private Animator animator;
    private string nameAnimation = "OpenChest";
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            animator.Play(nameAnimation);
        }
    }
    public void DropKey()
    {
        Instantiate(key, transform.position, Quaternion.identity);
    }
}
