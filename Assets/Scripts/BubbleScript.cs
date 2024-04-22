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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update() {
        
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, tocdodilen);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            SinhVatPham();
            anim.Play("bubble_no");

            Transform playertrform = other.transform;
            if (playertrform != null)
            {
                float docao = 1f;
                Vector2 niewpositon = playertrform.position + new Vector3(-1f,docao,0f);
                playertrform.GetComponent<PlayerManager>().AddOxy();
                playertrform.position = niewpositon;
            }
            
        }
    }

    public void XoaBubble()
    {
        Destroy(gameObject);
    }
    public void SinhVatPham()
    {
        int getNumber = Random.Range(1, 4);

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