using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish3 : MonoBehaviour
{

    //Di chuy?n ??n ?i?m A
    //sau ?ó ngh? 2s
    //Quay ??u và ?i ??n ?i?m B
    //??ng ngh? 2s
    //T?c ?? di chuy?n c?a nó là 5f 

    //Th?y ng??i ch?i(m?t nó nhìn v? ng??i ch?i) ; t?ng t?c lên 8f và di chuy?n v? phía ng??i ch?i
    //nâng cao : nó húc tr??t . nó ??ng ?? 2s -> quay l?i 
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Transform boar;
    public Transform posA;
    public Transform posB;

    public Transform boarcheck;
    private Vector2 dirition;
    public LayerMask playerLayer;
    private RaycastHit2D hitPlayer;

    public bool atk;

    float speedMove = 5f;
    bool isFacingRight;
    float timer;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Destroy(gameObject);
        }

        if (isFacingRight) //Ph?i di chuy?n qu B
        {

            dirition = Vector2.left;
            if (Vector2.Distance(boar.position, posB.position) > 0.1f)
            {
                boar.position = Vector2.MoveTowards(boar.position, posB.position, speedMove * Time.deltaTime);
                animator.Play("fish_3");
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= 2f)
                {
                    spriteRenderer.flipX = false;
                    isFacingRight = false;
                    timer = 0;
                }
                else if (timer <= 2f)
                {
                    animator.Play("fish_3");
                }

            }
        }
        else //m?t quay v? trái di chuy?n qua A
        {
            dirition = Vector2.right;
            if (Vector2.Distance(boar.position, posA.position) > 0.1f)
            {
                boar.position = Vector2.MoveTowards(boar.position, posA.position, speedMove * Time.deltaTime);
                animator.Play("fish_3");
            }
            else
            {

                timer += Time.deltaTime;
                if (timer > 2f)
                {
                    spriteRenderer.flipX = true;
                    isFacingRight = true;
                    timer = 0;
                }
                else
                {
                    animator.Play("fish_3");
                }

            }
        }

        
    }

    private void FixedUpdate()
    {
      //  RayDetect();
        Atk();
    }

    public void RayDetect()
    {
        hitPlayer = Physics2D.Raycast(boarcheck.position, dirition, 10f, playerLayer);

        if (hitPlayer)
        {
            Debug.DrawRay(boarcheck.position, dirition * hitPlayer.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(boarcheck.position, dirition * 10f, Color.green);
        }
    }
    public void Atk()
    {
        RaycastHit2D hit = Physics2D.Raycast(boarcheck.position, dirition, 10f, playerLayer);

        if (hit)
        {
            Debug.DrawRay(boarcheck.position, dirition * hit.distance, Color.red);
            speedMove = 8f;
            
        }
        else
        {
            Debug.DrawRay(boarcheck.position, dirition * 10f, Color.green);
            speedMove = 5f;
        }
    }

}