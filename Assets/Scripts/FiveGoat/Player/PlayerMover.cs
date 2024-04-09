using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float toDo;
    [SerializeField] float jumpMax;
    [SerializeField] float jumpHigh;
    private float jumpCount;
    private float ngang;
    // Start is called before the first frame update
    void Start()
    {
        jumpCount = jumpMax;
    }

    // Update is called once per frame
    void Update()
    {
        ngang = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (ngang*toDo, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space)&& jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpHigh);
            jumpCount--;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            jumpCount = jumpMax;
            // Debug.Log("Nhay de hoi");
        }
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = jumpMax;
            // Debug.Log("Nhay de hoi");
        }
    }
}
