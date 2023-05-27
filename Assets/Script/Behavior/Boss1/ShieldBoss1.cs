using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoss1 : MonoBehaviour
{
    public EnemyHealth bosshealth;
    public float health = 20;

    private void Update()
    {
        if (health < 1)
        {
            Destroy(gameObject);
        }

        if (bosshealth.health < 1)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
            Destroy(collision.gameObject);
        };
    }
}
