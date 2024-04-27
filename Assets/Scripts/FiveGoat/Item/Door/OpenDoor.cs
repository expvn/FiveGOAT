using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Transform pointTele;
    [SerializeField] GameObject text;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        { 
            GameObject player = collision.gameObject;
            if (player.GetComponent<PlayerManager>().GetKeyStatus()==1)
            {
                animator.Play("Open");
                player.SetActive(false);
                StartCoroutine(Tele(player));
            }
            else
            {
                StartCoroutine(Reset());
            }

        }
    }
    IEnumerator Reset()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(time);
        text.SetActive(false);
    }
    IEnumerator Tele(GameObject player)
    {
        yield return new WaitForSeconds(time);
        player.SetActive(true);
        player.transform.position = pointTele.position;
    }
}
