using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoss1 : MonoBehaviour
{
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            boss.SetActive(true);
        }
    }
}
