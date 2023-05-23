using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private Transform player;
    private Vector3 target;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public float speed = 2;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Torso").transform;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        target = player.transform.position;
        AimAtPlayer();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.enabled = false;
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sprite.enabled = false;
        Destroy(gameObject, 0.1f);
    }

    private void AimAtPlayer()
    {
        Vector3 direction = target - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

    }


}
