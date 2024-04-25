using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public static bulletScript instance { get; private set; }
    public Rigidbody2D rb;
    public float ThoiGianXoa = 0.5f;
    public float bulletSpeed = 20f;
    public bool isFired = false;

    private Vector2 moveDirection;

    private void Awake()
    {
        instance = this;

        rb.isKinematic = true;
        Destroy(gameObject, ThoiGianXoa);
    }

    private void Update()
    {
        rb.velocity = new Vector2(moveDirection.x * bulletSpeed,rb.velocity.y) ;
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
}