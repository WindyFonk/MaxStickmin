using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lock"))
        {
            collision.gameObject.GetComponent<Lockbox>().unlocked= true;
        }
    }
}
