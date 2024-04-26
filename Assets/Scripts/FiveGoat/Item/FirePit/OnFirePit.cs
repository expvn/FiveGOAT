using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFirePit : MonoBehaviour
{
    [SerializeField] GameObject light;
    private Animator animator;
    private bool active;
    void Start()
    {
        animator = GetComponent<Animator>();
        active = false;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER)&&!active)
        {
           active = true;
             animator.Play("On");
            collision.GetComponent<PlayerManager>().addDieuKien();
            light.SetActive(true);
        }
    }
}
