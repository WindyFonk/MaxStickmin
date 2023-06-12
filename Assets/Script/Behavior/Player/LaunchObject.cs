using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchObject : MonoBehaviour
{
    private string startTag;
    private void Start()
    {
        startTag = gameObject.tag;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            UnityEngine.Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.3f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            UnityEngine.Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            gameObject.GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
        {
            Invoke("ResetTag", 2);
        }

        if (collision.gameObject.CompareTag("LaunchObject"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("LaunchObject"))
        {
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ef.doExplosion(transform.position);
        }
    }

    private void ResetTag()
    {
        gameObject.tag = startTag;
    }


}
