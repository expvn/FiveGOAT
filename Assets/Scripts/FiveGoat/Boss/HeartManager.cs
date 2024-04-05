using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private float heartMax;
    [SerializeField] private float heartMin;
    [SerializeField] private Slider heartSlider;
    [SerializeField] private float heartSpeed;
    private float heart;
    public static HeartManager instance;
    void Start()
    {
        instance = this;
        heartSlider.maxValue = heartMax;
        heartSlider.minValue = heartMin;
        heart = 10f;
       
    }

    
    void Update()
    {
        if (heartSlider==null)
        {
            return;
        }
        heartSlider.value = heart;
    }
    public float GetHeart()
    {
        return heart;
    }
    public void SetHeart(float heartBonus)
    {
        heartSpeed += heartBonus;
        heart +=heartBonus;
        
    }
    public float getMaxHeart()
    {
        return heartMax;
    }
}
