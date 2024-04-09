using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMananger : MonoBehaviour
{
    public static PlayerMananger instan;
    [SerializeField] private TMP_Text textOxy;
    [SerializeField] private GameObject playerGround;
    [SerializeField] private GameObject playerWater;
    [SerializeField] private float oxyMax;
    private float oxy;
    private bool isWater;

    public Animator animatorCa1;
    public Animator animatorNo;

    private bool isGround;
    private int health;
    private int coin;



    private void Awake()
    {
        health = 1;
        instan = this;
    }
    void Start()
    {
        oxy = oxyMax;

        animatorCa1 = GetComponent<Animator>();
        animatorNo = GameObject.Find("no").GetComponent<Animator>(); // Đặt tên đúng cho animator "no"
       
    }

    // Update is called once per frame
    void Update()
    {
        GroundOrWater();
        ModunOxy();
        
    }

    private void GroundOrWater()
    {
        if (isWater)
        {
            playerWater.SetActive(true);
            playerGround.SetActive(false);
        }
        else
        {
            playerWater.SetActive(false);
            playerGround.SetActive(true);
        }
    }

    private void ModunOxy()
    {
        if (textOxy==null)
        {
            return;
        }
        textOxy.SetText(oxy.ToString("0"));
        if (GetIsWater())
        {
            oxy -= Time.deltaTime;
            if (oxy<0)
            {
                health--; ;
            }
        }
        else
        {
            if (oxy <= oxyMax)
            {
                oxy += 5f * Time.deltaTime;
                if (oxy > oxyMax)
                {
                    oxy = oxyMax;
                }
            }
        }
    }

    public bool checkeSuvive()
    {
       return health<1? true: false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isWater = true;
            Debug.Log("Dang duoi nuoc");
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isWater = false;
            Debug.Log("Roi khoi nuoc");
        }
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
    public bool GetIsWater()
    {
        return isWater;
    }
    public bool GetIsGround()
    {
        return isGround;
    }
    public void EndGame()
    {
        // sau nay hien man hinh chet
        Time.timeScale = 0f;
    }

    public void AddHealth()
    {
        health++;
    }
    public void AddCoin()
    {
        coin+=200;
    }
    public int GetCoin()
    {
        return coin;
    }
    public void Hit()
    {
       EndGame();
    }
}
