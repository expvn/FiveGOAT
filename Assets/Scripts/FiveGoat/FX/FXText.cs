using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FXText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float maxText;
    [SerializeField] private float minText;
    private float textSize;
    private bool change;
    void Start()
    {
        change = true;
        textSize = minText;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (change)
        {
            textSize += 0.1f * Time.deltaTime;
            if (textSize> maxText)
            {
                change= !change;
            }
        }
        else
        {
            textSize -= 0.1f * Time.deltaTime;
            if (textSize < minText)
            {
                change = !change;
            }
        }
        text.fontSize = textSize;
    }
}
