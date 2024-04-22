using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CombatMananger : MonoBehaviour
{
    // Start is called before the first frame update
    public static CombatMananger instance;
    public bool canReceiveInput;
    public bool inputReceive;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnAttack()
    {
            if (canReceiveInput)
            {
                inputReceive = true;
                canReceiveInput = false;
                Debug.Log("ðanh danh");
            }
            else
            {
            return;
            }
    }
    public void InputMananger()
    {
        if (!canReceiveInput)
        {
            canReceiveInput=true;
        }
        else
        {
            canReceiveInput = false;
        }
    }
}
