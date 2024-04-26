using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform zoneStop;
    [SerializeField] private LayerMask tuong;
    [SerializeField] private float doDaiTia;
    [SerializeField] private float valueTocDo;
    private Rigidbody2D rb;
    private Animator animator;
    private RaycastHit2D hit;
    private float huong;
    private float tocDo;
    public static Move istan;
    private Vector3 tempPositon;
    bool active;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        huong = transform.localScale.x;
        tocDo = valueTocDo;
        istan = this;
        active = false;
    }

    private void FixedUpdate()
    {
        if (!active) return;
        if(tempPositon == transform.position&&tocDo!=0)
        {
            huong*=-1;
        }
       tempPositon = transform.position;
        rb.velocity = new Vector2(tocDo * huong, rb.velocity.y);
        transform.localScale = new Vector3(huong, transform.localScale.y);
    }
    private void OnBecameVisible()
    {
        gameObject.GetComponent<HeartManager>().enabled = true;
        gameObject.GetComponent<Attack>().enabled = true;
        active = true;
    }
    private void Update()
    {
       
    }
    private void DinhHuongAuto()
    {
        hit = Physics2D.Raycast(zoneStop.position, new Vector2(1 * transform.localScale.x, 0f), doDaiTia, tuong);
        if (hit)
        {
            huong *= -1;
            Debug.DrawRay(zoneStop.position, new Vector2(1 * hit.distance * transform.localScale.x, 0f), Color.red);
        }
        else
        {
            Debug.DrawRay(zoneStop.position, new Vector2(doDaiTia * transform.localScale.x, 0f), Color.green);
        }
    }
   public void SetTocDo(float nTocDo)
    {
        tocDo = nTocDo;
    }
    public float GetValueTocDo()
    {
        return valueTocDo;
    }
   
}
