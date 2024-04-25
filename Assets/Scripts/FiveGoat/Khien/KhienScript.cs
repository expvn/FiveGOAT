using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhienScript : MonoBehaviour
{
    public float MauKhien = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        MauKhien--;
        if(MauKhien<1)
        {
            gameObject.SetActive(false);
        }
    }

}
