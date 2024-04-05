using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffKhien : MonoBehaviour
{
    [SerializeField] private float timerValue;
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerValue)
        {   
            timer= 0;
            gameObject.SetActive(false); 
        }
    }
}
