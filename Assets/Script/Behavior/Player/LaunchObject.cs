using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchObject : MonoBehaviour
{
    private void OnMouseEnter()
    {
        UnityEngine.Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.3f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }
    private void OnMouseOver()
    {
        
    }

    private void OnMouseExit()
    {
        UnityEngine.Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
        {
            Invoke("ResetTag", 1f);
        }
        if (collision.gameObject.CompareTag("LaunchObject"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    private void ResetTag()
    {
        gameObject.tag = "Ground";
    }


}
