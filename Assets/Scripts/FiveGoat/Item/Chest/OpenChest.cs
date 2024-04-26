using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private GameObject key;
    [SerializeField] private int dieuKien;
    [SerializeField] private float time;
    [SerializeField] private TMP_Text message;
    [SerializeField] private string messageText;
    [SerializeField] private GameObject text;
    private Animator animator;
    private string nameAnimation = "OpenChest";
    void Start()
    {
        animator = GetComponent<Animator>();
        message.SetText(messageText);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            if(PlayerManager.instan.GetDieuKien()>=dieuKien)
            {
                animator.Play(nameAnimation);
            }
            else
            {
                text.SetActive(true);
                StartCoroutine(Reset());
            }
            
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(time);
        text.SetActive(false);
    }
    public void DropKey()
    {
        Instantiate(key, transform.position, Quaternion.identity);
    }
}
