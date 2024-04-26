using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KhienScript : MonoBehaviour
{
    public float MauKhien = 5;
    public TMP_Text text;
    void Start()
    {
        text = PlayerManager.instan.GetTextKhien();
        text.SetText(MauKhien.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        MauKhien-=damage;
        text.SetText(MauKhien.ToString());
        if (MauKhien<1)
        {
            gameObject.SetActive(false);
        }
    }

}
