using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private int world;
    [SerializeField] private int stage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            GameObject player = collision.gameObject;
            player.SetActive(false);
            Invoke("LoadScene",3);
        }
    }
    private void LoadScene()
    {
        SceneManager.LoadScene($"{world}-{stage}");
    }
}
