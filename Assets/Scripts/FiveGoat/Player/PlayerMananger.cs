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
    [SerializeField] private TMP_Text textBullet;
    [SerializeField] private TMP_Text textKhien;
    [SerializeField] private TMP_Text textKey;
    [SerializeField] private GameObject playerGround;
    [SerializeField] private GameObject playerWater;
    [SerializeField] private float oxyMax;
    [SerializeField] private float force;
    [SerializeField] private float timeFore;
    [SerializeField] private float timeMax;
    [SerializeField] private int maxHealth ;
    [SerializeField] private int setHealth ;
    [SerializeField] private LayerMask ground;
    [SerializeField] private List<Image> healths;

    public GameObject khien;
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
    private float dan;
    private float dieuKien = 0f;
    float timer;

    private void Awake()
    {
        health = setHealth;
        instan = this;
        force = 3f;
        timeFore = 0.25f;
        isHit = false;
        rb = GetComponent<Rigidbody2D>();
        key = 0;
        showMessage = false;
        Time.timeScale = 1f;
        isDead = false;
        timer = 0;
        
    }
    void Start()
    {
        oxy = oxyMax;
        time = timeMax;
        coin = 0;
        textCoin.SetText(coin.ToString());
        textKhien.SetText(0.ToString());
        textBullet.SetText(dan.ToString());
        textKey.SetText(key.ToString());

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
        timer +=Time.deltaTime;
        HeathManager();
        if (isDead)
        {
            return;
        }
        GroundOrWater();
        ModunOxy();
        ModunTime();
       
        
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
        health += amount;
        if (currentHealth > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag(AllTag.KEY_TAG_BUBBLE))
        {
            IncreaseHealth(1);
        }
        if (collision.CompareTag("GunItem"))
        {
            PlayerMover.Intance.gun.SetActive(true);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("BulletItem"))
        {
            dan++;
            textBullet.SetText(dan.ToString());
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("KhienItem"))
        {
            KhienScript khienScript = khien.GetComponent<KhienScript>();
            if (khien.activeInHierarchy)
            {
                
                khienScript.SetTextKhien(5);
            }
            else
            {
                khien.SetActive(true);
                khienScript.SetTextKhien(5);
            }
            
            Destroy(collision.gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(AllTag.KEY_TAG_KEY))
        {
            int i = collision.transform.GetComponent<KeyScript>().GetKeyStatus();
            if (i != 0)
            {
                key = i;
                textKey.SetText(key.ToString());
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

    public void TakeDamage(int damage)
    {
        health -= damage;
        Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Died!");
        isDead = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        // Xử lý khi người chơi chết
        // Hiển thị màn hình Game Over, thực hiện các hành động khác,...
    }
    public void AddHealth()
    {
        health++;
        if(health >=maxHealth) {
        health = maxHealth;
        }
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
        KnockBack();
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
    public bool GetIsDead()
    {
        return isDead;
    }

    public float GetDan()
    {
        return dan;
    }
    public void SetDan(float ndan)
    {
        dan = ndan;
        textBullet.SetText(dan.ToString());
    }
    public TMP_Text GetTextKhien()
    {
        return textKhien;
    }
  public float GetDieuKien()
    {
        return dieuKien;
    }
    public void addDieuKien()
    {
        dieuKien++;
    }
}

