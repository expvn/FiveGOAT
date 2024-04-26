using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quaicho : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
     Animator animator;
    public Transform boar;
    public Transform posA;
    public Transform posB;

    public Transform boarcheck;

    public bool atk;
    public bool quai_hit;
    public float attackDuration = 2f;
    private float attackTimer = 2f; // B? ??m th?i gian cho t?n công

    public GameObject can;

    public float speedMove;
    public float tamNhin;
    bool isFacingRight;
    float timer;
    public float hp;


    public float distanceBetween;

    private int CanCount = 0; // ??m s? l?n c?n
    private int HitCount = 0;
    private float attackCooldown = 1.5f; // Th?i gian gi?a các t?n công
    private float nextAttackTime = 0f; // Th?i gian k? ti?p ?? t?n công
    private Vector2 dirition;

    private RaycastHit2D hitPlayer;

    void Start()
    {
        isFacingRight = true;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (!atk)
        {
            if (isFacingRight)
            {
                dirition = Vector2.right;
                MoveTowards(posB.position);
            }
            else
            {
                dirition = Vector2.left;
                MoveTowards(posA.position);
            }
        }
        
        
    }

    // Di chuy?n
    private void MoveTowards(Vector3 targetPosition)
    {
        transform.localScale = new Vector3(dirition.x, transform.localScale.y);
        if (Vector2.Distance(boar.position, targetPosition) > 0.1f)
        {
            boar.position = Vector2.MoveTowards(boar.position, targetPosition, speedMove * Time.deltaTime);
            animator.Play("walk");
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                isFacingRight = !isFacingRight;
                
                timer = 0;
            }
            else if (timer <= 2f)
            {
                animator.Play("quai1_idle");
            }
        }
    }


    private void FixedUpdate()
    {
            FindPlayer();
            Attacked();
        
    }


    
    private void Attacked()
    {
        if (Time.time >= nextAttackTime && !atk)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 1 * dirition.x, transform.position.y), new Vector3(dirition.x, 0), tamNhin);
            Debug.DrawRay(boarcheck.position, dirition * tamNhin, Color.red);

            if (hit.collider != null && hit.collider.CompareTag(AllTag.KEY_TAG_PLAYER))
            {
                if (CanCount < 3)
                {
                    atk = true;
                    animator.SetTrigger("atk");
                    CanCount++; // T?ng s? l?n t?n công
                    nextAttackTime = Time.time + attackCooldown; // C?p nh?t th?i gian k? ti?p ?? t?n công
                    Debug.Log("atk");
                }
               
            }
        }

    }


    private void TakeDamage()
    {
        hp--;
        quai_hit = true;
        animator.Play("quai_hit");
        if (hp <= 0)
        {
            Dead();
        }

    }
    public void Dead()
    {
        animator.Play("quai_hit");
        quai_hit = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_SWORD))
        {
            quai_hit = true;
            animator.Play("quai_hit");
            TakeDamage();
            Debug.Log("hit");
        }
        if (collision.CompareTag(AllTag.KEY_TAG_BULLET))
        {
            Dead();
        }
    }
    void FindPlayer()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanceBetween);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                speedMove = 10f;
                return;
            }
        }
    }

    public void OnCan()
    {
        can.SetActive(true);
    }
    public void OffCan()
    {
        can.SetActive(false);
        atk = false;
    }

    public void StartAttack()
    {
        atk = true;

    }


    public void StopAttack()
    {
        atk = false;

    }
}
