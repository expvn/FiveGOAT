using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;
    public float tocdodilen;
    public GameObject coinObject;
    public GameObject HeathObject;
    private bool hasSpawnedHeath = false;

    private static bool hasSpawnedHeathOnce = false; // Biến để kiểm tra đã sinh ra key ít nhất một lần hay chưa

    private bool hasTriggered = false; 
    private Collider2D bubbleCollider; // Lưu trữ Collider2D của Bubble

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bubbleCollider = GetComponent<Collider2D>(); // Lấy Collider2D của Bubble
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, tocdodilen);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            SinhVatPham();
            anim.Play("bubble_no");

            //Lực Đẩy Nhân Vật
            Transform playertrform = other.transform;
            if (playertrform != null)
            {
                float docao = 1f;
                Vector2 niewpositon = playertrform.position + new Vector3(-1f,docao,0f);

                playertrform.position = niewpositon;
            }

            // Tắt Collider của Bubble
            bubbleCollider.enabled = false;
        }
    }

    public void XoaBubble()
    {
        Destroy(gameObject);
    }

    public void SinhVatPham()
    {
        int getNumber = Random.Range(2, 4);

        if (getNumber == 3 && (hasSpawnedHeath || hasSpawnedHeathOnce))
        {
            return;
        }

        switch (getNumber)
        {
            case 1:
            case 2:
                Instantiate(coinObject, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(HeathObject, transform.position, Quaternion.identity);
                hasSpawnedHeath = true;
                hasSpawnedHeathOnce = true;
                break;
        }
    }
}