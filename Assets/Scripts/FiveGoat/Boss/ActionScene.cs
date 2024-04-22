using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionScene : MonoBehaviour
{
    [SerializeField] private GameObject finalBoss;
    [SerializeField] private GameObject fireBalls;
    [SerializeField] private GameObject Player;
    bool _active = false;
    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_active&&speed!=0f)
        {
            finalBoss.GetComponent<Rigidbody2D>().velocity = new Vector2(speed*-1, finalBoss.GetComponent<Rigidbody2D>().velocity.y);
            speed += Time.deltaTime;
            Player.transform.GetChild(0).GetComponent<PlayerMover>().enabled = false;
            Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            if (speed>=5f)
            {
                finalBoss.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                finalBoss.transform.GetComponent<Animator>().Play("Atack_phase1");
                fireBalls.SetActive(true);
                Player.transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                speed = 0f;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _active = true;
            Debug.Log("Bat dau cut scene");
        }
    }
}
