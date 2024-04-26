using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float timeMax;
    private bool fall;
    private PlayerManager playerManager;
    private bool attack;
    private void Start()
    {
        attack = true;
    }
    private void Update()
    {
        
        if (fall&&playerManager!=null&&attack)
        {
            StartCoroutine(CountDown());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            GameObject player = collision.gameObject;
            GameObject khien = player.transform.Find("khien").gameObject;
            if (khien == null) return;
            if (khien.activeInHierarchy)
            {
                khien.GetComponent<KhienScript>().TakeDamage(damage);
                return;
            }
            playerManager = collision.GetComponent<PlayerManager>();
            fall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
           playerManager = null;
        }
    }
    IEnumerator CountDown()
    {
        fall = false;
        attack = false;
        playerManager.TakeDamage(damage);
        yield return new WaitForSeconds(timeMax);
        fall = true;
        attack = true;
        
    }
}
