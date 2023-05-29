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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground") || 
            collision.gameObject.CompareTag("LaunchObject") || collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }

    private void AimAtPlayer()
    {
        Vector3 direction = target - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;


        float rotat = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotat - 180);

    }


}
