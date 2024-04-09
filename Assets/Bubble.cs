using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D (Collider2D collider2D)
    {
        if (collider2D.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Va cham vao HP cong mau");
            //  Viết hàm cộng máu ở đây nhé
        }
    }
}
