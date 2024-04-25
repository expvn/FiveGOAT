using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float attackBoss = 2f;
    public GameObject chemPhai;
    public GameObject chemTrai;
    public float timeMax;
    public float timeMaxTanHinh;
    public float distanceBetween;

    private Animator animator;
    private GameObject player;
    private int heart = 2;
    private bool tanCong;
    private float huong;
    private bool tanHinh;
    float timer;
    float timeTanHinh;


    void Awake ()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    { 
        timer = timeMax;
        tanHinh = true;
        animator.SetBool("tanHinh", tanHinh);
    }

    void Update()
    {
        // Tìm đối tượng Player mỗi frame
        FindPlayer();
        if (tanCong)
        {
            timer += 1 * Time.deltaTime;

            if (timer < timeMax) return;
            if (huong > 0)
            {
                animator.Play("Left");
                Debug.Log("Trai");
            }
            else
            {
                animator.Play("Right");
                Debug.Log("Phai");
            }
            timer = 0;
            tanCong = false;
        }
        TangHinh();
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            timer = timeMax;
        }
    }

    private void TangHinh()
    {
        if (tanHinh == false)
        {
            timeTanHinh += 1 * Time.deltaTime;
            if (timeTanHinh > timeMaxTanHinh)
            {
                tanHinh = true;
                animator.SetBool("tanHinh", tanHinh);
            }
        }
    }

    public void OnChemPhai()
    {
        chemPhai.SetActive(true);
    }

    public void OffChemPhai()
    {
        chemPhai.SetActive(false);
    }

    public void OnChemTrai()
    {
        chemTrai.SetActive(true);
    }

    public void OffChemTrai()
    {
        chemTrai.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {

        if (collider2D.CompareTag(AllTag.KEY_TAG_SWORD))
        {
            animator.Play("Hit");
        }
    }

    private void TakeDamage()
    {
        heart--;
        animator.Play("Hit");
        if (heart < 0)
        {
            gameObject.SetActive(false);
        }
    }

    void FindPlayer()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distanceBetween);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                tanHinh = false;
                animator.SetBool("tanHinh", tanHinh);
                huong = transform.position.x - collider.transform.position.x;
                tanCong = true;
                return;
            }
        }
    }
}
