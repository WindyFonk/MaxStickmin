using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameObject : MonoBehaviour
{
    public GameObject _object;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _object.SetActive(true);
        }
    }
}
