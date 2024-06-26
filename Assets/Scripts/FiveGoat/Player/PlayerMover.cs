using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    public static PlayerMover Intance {get; private set;}
    public Rigidbody2D rb;
    [SerializeField] float toDo;
    [SerializeField] float jumpMax;
    [SerializeField] float jumpHigh;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject sword;
    public GameObject gun;
    
    
    private float jumpCount;
    private float ngang;
    private PlayerManager playerMananger;

    private void Awake() {
        Intance = this;
    }
    void Start()
    {
        jumpCount = jumpMax;
        playerMananger = transform.parent.GetComponent<PlayerManager>();
    }
    void Update()
    {
        playerMananger = transform.parent.GetComponent<PlayerManager>();
        if (playerMananger.GetIsDead())
        {
            StartCoroutine(Dead());
            return;
        }
        if (playerMananger.GetIsWater())
        {
            MoveOnWater();
        }
        else
        {
            MoveOnGround();
        }

        RotaGun();
        
    }

    private void MoveOnGround()
    {
        rb.velocity = new Vector2(ngang * toDo, rb.velocity.y);
        if (ngang != 0)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
        Flip();
        animator.SetFloat("Jump", rb.velocity.y);
        if (playerMananger.GetIsWater() || playerMananger.GetIsGround())
        {
            jumpCount = jumpMax;

            animator.SetBool("isGround", true);
        }
        if (!playerMananger.GetIsGround())
        {
            animator.SetBool("isGround", false);
        }
    }
    private void MoveOnWater()
    {
        rb.velocity = new Vector2(ngang * toDo, rb.velocity.y);
        if (ngang > 0)
        {
            animator.SetFloat("Move", ngang);
        }
        else
        {
            animator.SetFloat("Move", ngang * -1);
        }
        Flip();
        if (!playerMananger.GetIsWater())
        {
            animator.SetFloat("Jump", rb.velocity.y);
        }
        if (playerMananger.GetIsWater() || playerMananger.GetIsGround())
        {
            jumpCount = jumpMax;
        }
    }
    private void Flip()
    {
        if (ngang == 0) return;
        transform.localScale = new Vector3(ngang, transform.localScale.y);
    }
    IEnumerator Dead()
    {
        yield return PlayAnimationDead();
        yield return new WaitForSeconds(2f);
        yield return StopTime();
    }
    IEnumerator PlayAnimationDead()
    {
        animator.Play("Player_die");
        yield return null;
    }
    IEnumerator StopTime()
    {
        playerMananger.EndGame();
        yield return null;
    }
    public void OnMove(InputAction.CallbackContext callback)
    {
        ngang = callback.ReadValue<Vector2>().x;
        Debug.Log("ngang = " + ngang);
    }
    public void OnJump(InputAction.CallbackContext callback)
    {
        if (callback.performed && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHigh);
            jumpCount--;


        }
    }
    public void OnAttack(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            CombatMananger.instance.OnAttack();
            Debug.Log("Dang tan cong " + CombatMananger.instance.canReceiveInput);
        }
    }

    public void ShowSword()
    {
        sword.SetActive(true);
    }
    public void HideSword()
    {
        sword.SetActive(false);
    }

    void RotaGun()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (gun != null && gun.activeSelf)
            {
                animator.SetTrigger("gun");
            }else
            {
                Debug.Log("chưa có súng");
            }
            
            
        }

    }

    public void BulletSih()
    {
        Gun.Instance.SinhBullet();
    }

}
