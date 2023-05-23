using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoss1 : MonoBehaviour
{
    public float health = 20;

    private void Update()
    {
        if (health < 1)
        {
            gameObject.transform.SetParent(null);
            gameObject.tag = "Untagged";
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("LaunchObject"))
        {
            health -= 5;
        };
    }
}
