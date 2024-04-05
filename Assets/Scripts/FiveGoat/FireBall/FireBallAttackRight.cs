using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttackRight : MonoBehaviour
{
    [SerializeField] private float tocDo;
    private float huong;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        huong = 1f;
        Destroy(gameObject, 3f);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (tocDo*huong, 0);
    }
}
