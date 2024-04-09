using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private float oxyMax;
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;
    private float oxy;
    private bool isWater;

    void Start()
    {
        oxy = oxyMax;
        currentHealth = maxHealth;
    }

    void Update()
    {
        m_Text.SetText(oxy.ToString("0"));

        if (isWater)
        {
            oxy -= Time.deltaTime;
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
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(1); // Trừ 1 máu khi chạm vào Enemy
            Destroy(gameObject);
        }
        if (collision.CompareTag("Bubble"))
        { 
            IncreaseHealth(1);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        // Xử lý khi người chơi chết
        // Hiển thị màn hình Game Over, thực hiện các hành động khác,...
    }
}
