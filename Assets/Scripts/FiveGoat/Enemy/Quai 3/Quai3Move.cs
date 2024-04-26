using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;

public class Quai3Move : MonoBehaviour
{
    
    [SerializeField] private float tamNhin;
    [SerializeField] private float distanceBetween;
    [SerializeField] private float maxTimeAttack;
    [SerializeField] private GameObject weapon;
    [SerializeField] private float heartMax;
    [SerializeField] private float force;
    [SerializeField] private float timeFore;
    private Transform player;
    private Rigidbody2D rb;
    Animator animator;
    Collider2D collider2D;
    NavMeshAgent agent;
    Vector3 direction;
    bool isAttack;
    bool isDead;
    string aniAttack = "Attack";
    string aniHit = "Hit";
    string aniDead = "Dead";
    float timeAttack;
    float directionKnock;
    float heart;
    bool isHit;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isAttack = false;
        timeAttack = maxTimeAttack;
        heart = heartMax;
    }

    // Update is called once per frame
    void Update()
    {   if (isDead) { return; }
        if (player == null) return;
        if(isHit) { KnockBack(); return; }
        OnAction();

    }

    private void OnAction()
    {
        direction = (player.position - transform.position).normalized;
        if (!isAttack)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        Flip();
        Attacked();
        FindPlayer();
       
    }

   
    private void Attacked()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * GetHuong(), transform.position.y), new Vector3(GetHuong(), 0), tamNhin);
        if (hit.collider == null||isDead) return;
        if (hit.collider.gameObject.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            timeAttack += 1 * Time.deltaTime;
            if (timeAttack < maxTimeAttack) return;
            animator.Play(aniAttack);
            timeAttack = 0;
        }
      
    }

    private void Flip()
    {
        int i = GetHuong();
        transform.localScale = new Vector3(i, transform.localScale.y);
    }

    private int GetHuong()
    {
        int i = 1;
        if (direction.x < 0)
        {
            i = -1;
        }
        return i;
    }
    public void StartAttack()
    {
        isAttack = true;
        collider2D.enabled = false;

    }
    public void MiddleAttack()
    {
        isAttack = false;
        collider2D.enabled = true;
       
    }
    public void EndAttack()
    {
        OffWeapon();
    }
    public void OnWeaPon()
    {
        weapon.SetActive(true);
    }
    public void OffWeapon()
    {
        weapon.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) { return; }
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            player = collision.transform;
        }
        if (collision.CompareTag(AllTag.KEY_TAG_SWORD))
        {
            TakeDamage();
            animator.Play(aniHit);
            directionKnock = collision.transform.lossyScale.x;
        }
        if (collision.CompareTag(AllTag.KEY_TAG_BULLET))
        {
            Dead();
        }
    }

    private void TakeDamage()
    {
        heart--;
        animator.Play(aniHit);
        if (heart<0)
        {
            Dead();
        }
    }
    public void KnockBack()
    {
        isHit = true;
        transform.position = new Vector2((transform.position.x+ force * Time.deltaTime ) * directionKnock, ( transform.position.y + force)*Time.deltaTime);
        StartCoroutine(Force());
    }
    IEnumerator Force()
    {
        yield return new WaitForSeconds(timeFore);
        isHit = false;
    }
    void FindPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanceBetween);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player = collider.transform;
                return;
            }
        }
    }
    public void Dead()
    {
        isDead = true;
        agent.SetDestination(transform.position);
        agent = null;
        animator.SetTrigger(aniDead);
    }
    public void ApplineGravity()
    {
        gameObject.SetActive(false);
    }
}
