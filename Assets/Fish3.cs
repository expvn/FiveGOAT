using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fish3 : MonoBehaviour
{
  
    //Di chuy?n ??n ?i?m A
    //Ngh? 2s 
    //Quay ??u và ?i ??n ?i?m B
    //??ng ngh? 2s
    //T?c ?? di chuy?n là 5f
    //th?y ng??i ch?i (m?t nó nhìn v? phía ng??i ch?i ): t?ng t?c lên 8f và di chuy?n v? phía ng??i ch?i 
    //nâng cao: nó h?c tr??t, nó ??ng ?? 2s -> quay l?i -> 

    public SpriteRenderer sR;
   
    public Transform boar;
    public Transform posA;
    public Transform posB;

    public Transform boarcheck;
    private Vector3 dirition;
    public LayerMask playerLayer;
    private RaycastHit2D hitPlayer;

    public bool atk;

    float speedMove = 3f;
    bool isFaceRight;
   

    //OverLapCheckGround
    public Transform checkGroundPos;
    public LayerMask groundLayer;
    public bool isGround;
    //


    public float chaseSpeed;
    public float detectionRange;
    private bool isFacingRight = true;
    private bool isChasingPlayer = false;
    public Transform target;




    //public float knockbackForce = 3f;
    //private bool isPlayerHit = false;
    //private Rigidbody2D playerRigidbody;




    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (isChasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            MoveBetweenPoints();
        }
       
    }

    private void MoveBetweenPoints()
    {
        if (isFacingRight)
        {
            if (Vector2.Distance(boar.position, posB.position) > 0.1f)
            {
                boar.position = Vector2.MoveTowards(boar.position, posB.position, speedMove * Time.fixedDeltaTime);
            }
            else
            {
                isFacingRight = false;
                sR.flipX = false;
            }
        }
        else
        {
            if (Vector2.Distance(boar.position, posA.position) > 0.1f)
            {
                boar.position = Vector2.MoveTowards(boar.position, posA.position, speedMove * Time.fixedDeltaTime);
            }
            else
            {
                isFacingRight = true;
                sR.flipX = true;
            }
        }
    }

    private void ChasePlayer()
    {
        if (target != null)
        {
            if (Vector2.Distance(boar.position, target.position) > 0.1f)
            {
                boar.position = Vector2.MoveTowards(boar.position, target.position, chaseSpeed * Time.fixedDeltaTime);
            }
        }
    }


    private void FixedUpdate()
    {
        OverlapDetect();
    }

    private void OverlapDetect()
    {
        if (isChasingPlayer)
        {
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(boarcheck.position, detectionRange, playerLayer);

        if (colliders.Length > 0)
        {
            target = colliders[0].transform;
            isChasingPlayer = true;
            speedMove = chaseSpeed;
        }
        else
        {
            target = null;
            isChasingPlayer = false;
            speedMove = 3f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject, 6f);
        }
    }

   
}



