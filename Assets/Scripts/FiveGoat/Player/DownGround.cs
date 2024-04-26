using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownGround : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private float time;
    private GameObject ground;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key) )
        {
            DowmGround();
        }

        
    }

    private void DowmGround()
    {
        if (ground != null)
        {
            PlatformEffector2D effector2D = ground.GetComponent<PlatformEffector2D>();
            if (effector2D!=null)
            {
                effector2D.rotationalOffset = 180;
                StartCoroutine(Reset(effector2D));
                

            }
        }
    }
    IEnumerator Reset(PlatformEffector2D effector2)
    {
        yield return new WaitForSeconds(time);
        effector2.rotationalOffset = 0;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(AllTag.KEY_TAG_GROUND) )
        {
            ground = collision.gameObject;
        }
    }
}
