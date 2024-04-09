using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMananger : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text;
    [SerializeField] private float oxyMax;
    private float oxy;
    private bool isWater;
    public Animator animatorCa1;
    public Animator animatorNo;

    // Start is called before the first frame update
    void Start()
    {
        oxy = oxyMax;
        animatorCa1 = GetComponent<Animator>();
        animatorNo = GameObject.Find("no").GetComponent<Animator>(); // Đặt tên đúng cho animator "no"
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.SetText(oxy.ToString("0"));
        if (isWater)
        {
            oxy -= Time.deltaTime;
        }
        else
        {
            if (oxy<=oxyMax)
            {
                oxy += 5f*Time.deltaTime;
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

}
