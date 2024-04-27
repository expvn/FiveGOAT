using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;
using UnityEngine.InputSystem.Utilities;

public class Quai4Move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speedMax;
    [SerializeField] private float speedMin;
    [SerializeField] private float maxTimeAttack;
    [SerializeField] private float tamNhin;
    [SerializeField] private float force;
    [SerializeField] private float timeFore;
    [SerializeField] private float maxHeart;
    [SerializeField] private float distanceBetween;
    [SerializeField] private float timeWait;
    [SerializeField] private Transform postA;
    [SerializeField] private Transform postB;
    [SerializeField] private Transform rayPosition;
    [SerializeField] private GameObject weapon;
    Rigidbody2D rb;
    Animator animator;
    float huong;
    float time;
    float speed;
    float timeAttack;
    float heart;
    float directionKnock;
    bool isDead;
    bool isHit;
    bool isATK;
    string aniAttack = "Attack";
    string aniHit = "Hit";
    string aniDead = "Dead";
    string aniWalk = "isWalk";
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        huong = 1;
        speed = speedMin;
        heart = maxHeart;
        timeAttack = maxTimeAttack;
    }


    private void FixedUpdate()
    {
        if(isDead) return;
        if(isATK) { return; }
        Mover();
        Attacked();
        Hit();
    }

    private void Hit()
    {
        if (isHit)
        {
            rb.velocity = new Vector2(force * directionKnock, rb.velocity.y);
            speed = 0;
        }
    }

    private void Mover()
    {
        DinhHuong();
        rb.velocity = new Vector2(huong * speed, rb.velocity.y);
        Flip();
        FindPlayer();
        if (rb.velocity.x != 0)
        {
            animator.SetBool(aniWalk, true);
        }
        else
        {
            animator.SetBool(aniWalk, false);
        }
    }

    private void DinhHuong()
    {
        if (huong == 1f)
        {
            Vector2 huongVT = transform.position - postB.position;
            if (huongVT.x > 0)
            {
                huong = 0;
            }
        }
        else if (huong == -1f)
        {
            Vector2 huongVT = transform.position - postA.position;
            if (huongVT.x < 0)
            {
                huong = 0;
            }
        }
        else if(huong == 0)
        {
            time += 1 * Time.fixedDeltaTime;
            if (time >= timeWait)
            {
                time = 0;
                huong += -1f*transform.localScale.x;
                speed = speedMin;
            }

        }
    }

    private void Flip()
    {
        if (huong != 0)
        {
            transform.localScale = new Vector3(huong, transform.localScale.y);
        }

    }
    void FindPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanceBetween);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                GameObject player = collider.gameObject;
                speed = speedMax;
                if (player.transform.localScale.x!=huong&&huong!=0)
                {
                    huong = player.transform.localScale.x * -1;
                }
                return;
            }
        }
    }
    private void Attacked()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPosition.position, new Vector3(huong, 0), tamNhin);
        if (hit.collider == null || isDead) return;
        if (hit.collider.gameObject.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            timeAttack += 1 * Time.deltaTime;
            speed = 0f;
            if (timeAttack < maxTimeAttack) return;
            animator.Play(aniAttack);
            isATK = true;
            timeAttack = 0;
        }
        Debug.Log(hit.collider.name);
        Debug.DrawRay(rayPosition.position,new Vector3(tamNhin * huong,0f),Color.green);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) { return; }
       
        if (collision.CompareTag(AllTag.KEY_TAG_SWORD))
        {
            TakeDamage();
            directionKnock = collision.transform.lossyScale.x;
            KnockBack();
        }
        if (collision.CompareTag(AllTag.KEY_TAG_BULLET))
        {
            Dead();
        }
    }
    public void Dead()
    {
        isDead = true;
        animator.SetTrigger(aniDead);
    }
    private void TakeDamage()
    {
        heart--;
        animator.Play(aniHit);
        if (heart < 0)
        {
            Dead();
        }
    }
    public void KnockBack()
    {
        isHit = true;
        StartCoroutine(Force());
    }
    IEnumerator Force()
    {
        yield return new WaitForSeconds(timeFore);
        isHit = false;
    }
    public void OnWeapon()
    {
        weapon.SetActive(true);
        speed = 0f;
    }
    public void OffWeapon()
    {
        weapon.SetActive(false);
        speed = speedMin;
        isATK = false;
        rb.bodyType = RigidbodyType2D.Dynamic;

    }
    public void HideQuai()
    {
       gameObject.SetActive(false);
    }
}
