using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchObject : MonoBehaviour
{
    public float speed;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            transform.position = Vector2.MoveTowards(transform.position,
            Camera.main.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime * speed);
        }
    }



}
