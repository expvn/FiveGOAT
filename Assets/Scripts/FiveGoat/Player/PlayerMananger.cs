using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instan;
    [SerializeField] private TMP_Text textOxy;
    [SerializeField] private TMP_Text textCoin;
    [SerializeField] private TMP_Text textTime;
    [SerializeField] private GameObject playerGround;
    [SerializeField] private GameObject playerWater;
    [SerializeField] private float oxyMax;
    [SerializeField] private float force;
    [SerializeField] private float timeFore;
    [SerializeField] private float timeMax;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private LayerMask ground;
    [SerializeField] private List<Image> healths;
    private Rigidbody2D rb;
    private int currentHealth;
    private float oxy;
    private bool isWater;
    private float directionKnock;
    private bool isGround;
    private int health;
    private int coin;
    private RaycastHit2D hit;
    private bool isHit;
    private int key;
    private bool showMessage;
    private float time;
    private bool isDead;


    private void Awake()
    {
        health = 1;
        instan = this;
        force = 3f;
        timeFore = 0.25f;
        isHit = false;
        rb = GetComponent<Rigidbody2D>();
        key = 0;
        showMessage = false;
        Time.timeScale = 1f;
        isDead = false;
    }
    void Start()
    {
        oxy = oxyMax;
        time = timeMax;
        coin = 0;
        textCoin.SetText(coin.ToString());
    }
    private void FixedUpdate()
    {
        if (isHit)
        {
            KnockBack();
        }
        CheckIsGround();
    }
    void Update()
    {
        if(isDead)
        {
            return;
        }
        currentHealth = maxHealth;
        GroundOrWater();
        ModunOxy();
        ModunTime();
        HeathManager();
    }

    private void ModunTime()
    {
        if (textTime == null)
        {
            return;
        }
        time -= 1*Time.deltaTime;
        if (textTime != null)
        {
            textTime.SetText(time.ToString("0"));
        }
        if (time <= 0)
        {
            EndGame();
        }
    }

    private void HeathManager()
    {
        for(int i = 0; i < healths.Count; i++)
        {
            healths[i].color = Color.gray;
        }
        for (int i = 0; i < health; i++)
        {
            healths[i].color = Color.white;
        }
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
        if (textOxy == null)
        {
            return;
        }
        if (textOxy != null)
        {
            textOxy.SetText(oxy.ToString("0"));
        }
        if (GetIsWater())
        {
            oxy -= Time.deltaTime;
            if (oxy < 0)
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
        return health < 1 ? true : false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isWater = false;
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
        isDead = true;
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag(AllTag.KEY_TAG_BUBBLE))
        {
            IncreaseHealth(1);
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(AllTag.KEY_TAG_ENEMY))
        {
            if (rb.Raycast(Vector2.up))
            {
                Debug.Log("cham quai");
                collision.transform.GetComponent<Goomba>().Hit();
            }
            else
            {   
                Debug.Log("0o");
                OnHit(collision.transform.GetComponent<EntityMovement>().direction.x);
            }
        }
        if (collision.transform.CompareTag(AllTag.KEY_TAG_KEY))
        {
            int i = collision.transform.GetComponent<KeyScript>().GetKeyStatus();
            if (i != 0)
            {
                key = i;
            }
        }
        if(collision.gameObject.CompareTag(AllTag.KEY_TAG_PIPE))
        {
            showMessage = true;
          
        }
        if (collision.gameObject.CompareTag(AllTag.KEY_TAG_COIN))
        {
            Destroy(collision.gameObject);
            AddCoin();
            textCoin.SetText(coin.ToString());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(AllTag.KEY_TAG_PIPE))
        {
            showMessage = false;

        }
    }
    public void OnHit(float direction)
    {
        TakeDamage(1); // Trừ 1 máu khi chạm vào Enemy
        isHit = true;
        directionKnock = direction;
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Died!");
        Hit();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // Xử lý khi người chơi chết
        // Hiển thị màn hình Game Over, thực hiện các hành động khác,...
    }
    public void AddHealth()
    {
        health++;
    }
    public void AddCoin()
    {
        coin += 200;
    }
    public int GetCoin()
    {
        return coin;
    }
    public void Hit()
    {
        EndGame();
    }

    public void CheckIsGround()
    {
        hit = Physics2D.Raycast(transform.position + new Vector3(0, -1), Vector2.down, 1f, ground);

        if (hit)
        {
            isGround = true;
            Debug.DrawLine(transform.position + new Vector3(0, -1), transform.position + new Vector3(0,hit.distance),Color.red);
        }
        else
        {
            isGround = false;
            Debug.DrawLine(transform.position + new Vector3(0, -1), transform.position, Color.green);
        }
    }
    public void KnockBack()
    {
        rb.velocity = new Vector2 (force*directionKnock,force);
        StartCoroutine(Force());
    }
    IEnumerator Force()
    {
        yield return new WaitForSeconds(timeFore);
        isHit = false;
    }
    public int GetKeyStatus()
    {
        return key;
    }
    public void SetMessager(bool status)
    {
        showMessage = status;
    }
    public bool GetShowMessage()
    {
        return showMessage;
    }
    public void AddOxy()
    {
        if (oxy < 60)
        {
            oxy += 5;
            if(oxy > 60)
            {
                oxy = 60;
            }
        }
    }
}
