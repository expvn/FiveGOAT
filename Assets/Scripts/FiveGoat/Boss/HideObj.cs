using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObj : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(AllTag.KEY_TAG_PLAYER))
        {
            gameObject.SetActive(false);
            camera.m_Lens.OrthographicSize = 15f;

        }
    }
}
