using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static Gun Instance { get; private set; }

    [SerializeField] private GameObject bullet;

    private float TongSoDan = 0;
    public Transform checkgun;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        TongSoDan = PlayerManager.instan.GetDan();
    }

    public void SinhBullet()
    {
        if (TongSoDan > 0)
        {
            GameObject bulletTmp = Instantiate(bullet, checkgun.position, Quaternion.identity);
            bulletScript bulletScript = bulletTmp.GetComponent<bulletScript>();

            Vector2 moveDirection = transform.parent.localScale;
            bulletScript.SetMoveDirection(moveDirection);
            TongSoDan--;
            PlayerManager.instan.SetDan(TongSoDan);
        }

    }
}