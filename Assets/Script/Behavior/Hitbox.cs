using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyBehavior;

public class Hitbox : MonoBehaviour
{
    [SerializeField] float damage;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            EnemyBehaviorStatic.enemyBehavior.health -= damage;
        }
    }
}
