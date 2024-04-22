using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubble : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float firtTime;
    [SerializeField] private GameObject bubble;
    private float time;
    private bool onAction;
    void Start()
    {
        time = firtTime;
        onAction = false;
    }
    private void OnBecameVisible()
    {
            onAction = true;
            Debug.Log("dang bat");
    }
    private void OnBecameInvisible()
    {
        onAction = false;
        Debug.Log("dang tat");
    }
    // Update is called once per frame
    void Update()
    {
        if (onAction)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                Instantiate(bubble, transform.position, Quaternion.identity);
                time = firtTime;
            }
        }
        
    }


}
